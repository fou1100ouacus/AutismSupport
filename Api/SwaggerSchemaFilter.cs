using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Api
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        private readonly Dictionary<string, XDocument> _xmlDocuments;

        public EnumSchemaFilter()
        {
            _xmlDocuments = new Dictionary<string, XDocument>();
        }

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                var enumType = context.Type;
                var assemblyName = enumType.Assembly.GetName().Name;
                
                // Load XML document if not already loaded
                if (!_xmlDocuments.ContainsKey(assemblyName))
                {
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{assemblyName}.xml");
                    if (File.Exists(xmlPath))
                    {
                        _xmlDocuments[assemblyName] = XDocument.Load(xmlPath);
                    }
                }

                // Add enum values and descriptions
                if (_xmlDocuments.ContainsKey(assemblyName))
                {
                    var xmlDoc = _xmlDocuments[assemblyName];
                    var enumMembers = enumType.GetEnumValues();
                    var enumDescriptions = new Dictionary<int, string>();

                    // First, collect all enum descriptions
                    foreach (var enumValue in enumMembers)
                    {
                        var valueName = enumType.GetEnumName(enumValue);
                        var fieldName = $"F:{enumType.FullName}.{valueName}";
                        
                        var memberNode = xmlDoc.XPathSelectElement($"//member[@name='{fieldName}']");
                        if (memberNode != null)
                        {
                            var summaryNode = memberNode.Element("summary");
                            if (summaryNode != null)
                            {
                                var description = summaryNode.Value.Trim();
                                var intValue = Convert.ToInt32(enumValue);
                                enumDescriptions[intValue] = $"{valueName}: {description}";
                            }
                        }
                    }

                    // Set enum values as string names (for proper Swagger UI dropdown display)
                    if (schema.Enum == null)
                        schema.Enum = new List<IOpenApiAny>();

                    foreach (var enumValue in enumMembers)
                    {
                        var valueName = enumType.GetEnumName(enumValue);
                        schema.Enum.Add(new OpenApiString(valueName));
                    }

                    // Add descriptions to schema
                    if (enumDescriptions.Count > 0)
                    {
                        var descriptionList = enumDescriptions
                            .OrderBy(kvp => kvp.Key)
                            .Select(kvp => $"- **{kvp.Value}**")
                            .ToList();
                        
                        schema.Description = string.Join("\n", descriptionList);
                    }

                    // Add x-enumDescriptions extension for better Swagger UI support
                    var enumNamesArray = new OpenApiArray();
                    var enumDescriptionsArray = new OpenApiArray();
                    
                    foreach (var enumValue in enumMembers)
                    {
                        var valueName = enumType.GetEnumName(enumValue);
                        var intValue = Convert.ToInt32(enumValue);
                        
                        enumNamesArray.Add(new OpenApiString(valueName));
                        
                        if (enumDescriptions.ContainsKey(intValue))
                        {
                            enumDescriptionsArray.Add(new OpenApiString(enumDescriptions[intValue]));
                        }
                        else
                        {
                            enumDescriptionsArray.Add(new OpenApiString(valueName));
                        }
                    }
                    
                    schema.Extensions["x-enumNames"] = enumNamesArray;
                    schema.Extensions["x-enumDescriptions"] = enumDescriptionsArray;
                }
            }
        }
    }
}
