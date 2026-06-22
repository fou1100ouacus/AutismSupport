using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Api
{
    public class EnumDocumentFilter : IDocumentFilter
    {
        private readonly Dictionary<string, XDocument> _xmlDocuments;

        public EnumDocumentFilter()
        {
            _xmlDocuments = new Dictionary<string, XDocument>();
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // Process all schemas in the document
            foreach (var schema in swaggerDoc.Components.Schemas.Values)
            {
                ProcessSchema(schema, context);
            }
        }

        private void ProcessSchema(OpenApiSchema schema, DocumentFilterContext context)
        {
            // If this schema has enum values, add descriptions
            if (schema.Enum != null && schema.Enum.Count > 0)
            {
                var enumType = FindEnumType(schema, context);
                if (enumType != null)
                {
                    AddEnumDescriptions(schema, enumType);
                }
            }

            // Process nested schemas
            if (schema.Properties != null)
            {
                foreach (var property in schema.Properties.Values)
                {
                    ProcessSchema(property, context);
                }
            }

            // Process allOf schemas
            if (schema.AllOf != null)
            {
                foreach (var allOfSchema in schema.AllOf)
                {
                    ProcessSchema(allOfSchema, context);
                }
            }
        }

        private Type? FindEnumType(OpenApiSchema schema, DocumentFilterContext context)
        {
            // Try to find the enum type from the schema reference
            if (schema.Reference != null)
            {
                var schemaId = schema.Reference.Id;
                foreach (var type in context.SchemaRepository.Schemas)
                {
                    if (type.Key == schemaId)
                    {
                        // Try to find the corresponding enum type
                        var assembly = AppDomain.CurrentDomain.GetAssemblies()
                            .FirstOrDefault(a => a.GetName().Name == "Data" || a.GetName().Name == "Core");
                        
                        if (assembly != null)
                        {
                            var enumType = assembly.GetTypes()
                                .FirstOrDefault(t => t.IsEnum && t.Name == schemaId);
                            
                            if (enumType != null)
                                return enumType;
                        }
                    }
                }
            }
            return null;
        }

        private void AddEnumDescriptions(OpenApiSchema schema, Type enumType)
        {
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

            if (_xmlDocuments.ContainsKey(assemblyName))
            {
                var xmlDoc = _xmlDocuments[assemblyName];
                var enumMembers = enumType.GetEnumValues();
                var enumDescriptions = new List<string>();

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
                            enumDescriptions.Add($"**{valueName}** ({intValue}): {description}");
                        }
                    }
                }

                if (enumDescriptions.Count > 0)
                {
                    var descriptionText = string.Join("\n", enumDescriptions);
                    
                    if (string.IsNullOrEmpty(schema.Description))
                    {
                        schema.Description = descriptionText;
                    }
                    else
                    {
                        schema.Description += "\n\n**Enum values:**\n" + descriptionText;
                    }
                }
            }
        }
    }
}
