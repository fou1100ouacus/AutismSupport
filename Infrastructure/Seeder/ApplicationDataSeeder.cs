using Data.Entities.AbilitiesTracker; // تأكد من مطابقة الـ Namespaces مع مشروعك
using Infrastructure.Data; // الـ Namespace الخاص بـ ApplicationDbContext عندك
using Infrastructure.Context; // إذا كان لديك Namespace خاص بالـ Seeder
namespace Infrastructure.Seeder
{
    public static class ApplicationDataSeeder
    {
        public static async Task SeedDataAsync(ApplicationDBContext context)
        {
            // 1. التأكد من أن جدول التصنيفات فاضي تماماً قبل الفرش لمنع التكرار
            if (!context.AbilityCategories.Any())
            {
                var categories = new List<AbilityCategory>
                {
                    new AbilityCategory 
                    { 
                        NameEn = "Language and Communication Skills",
                        Description = "قياس قدرة الطفل على التعبير والفهم"
                    },
                    new AbilityCategory 
                    { 
                        NameEn = "Social Skills",
                        Description = "قياس التفاعل مع البيئة المحيطة والآخرين"
                    }
                };

                await context.AddRangeAsync(categories);
                await context.SaveChangesAsync(); // حفظ التصنيفات أولاً لإنشاء الـ IDs
            }

            // 2. التأكد من أن جدول الأسئلة فاضي تماماً قبل الفرش
            if (!context.AbilityQuestions.Any())
            {
                // جلب التصنيفات المضافة حديثاً لربط الأسئلة بها بشكل صحيح
                var languageCategory = context.AbilityCategories.FirstOrDefault(c => c.NameEn == "Language and Communication Skills");
                var socialCategory = context.AbilityCategories.FirstOrDefault(c => c.NameEn == "Social Skills");

                var questions = new List<AbilityQuestion>();

                if (languageCategory != null)
                {
                    questions.AddRange(new List<AbilityQuestion>
                    {
                        new AbilityQuestion 
                        { 
                            QuestionTextAr = "هل يستطيع الطفل التعبير عن احتياجاته الأساسية باستخدام جمل واضحة؟", 
                            QuestionTextEn = "Can the child express basic needs using clear sentences?", 
                            IsPositiveSkill = true,
                            CategoryId = languageCategory.Id 
                        },
                        new AbilityQuestion 
                        { 
                            QuestionTextAr = "هل يواجه الطفل صعوبة في فهم التوجيهات البسيطة؟", 
                            QuestionTextEn = "Does the child have difficulty understanding simple instructions?", 
                            IsPositiveSkill = false,
                            CategoryId = languageCategory.Id 
                        }
                    });
                }

                if (socialCategory != null)
                {
                    questions.AddRange(new List<AbilityQuestion>
                    {
                        new AbilityQuestion 
                        { 
                            QuestionTextAr = "هل يتواصل الطفل بصرياً معك أثناء التحدث إليه؟", 
                            QuestionTextEn = "Does the child make eye contact while being spoken to?", 
                            IsPositiveSkill = true,
                            CategoryId = socialCategory.Id 
                        }
                    });
                }

                if (questions.Any())
                {
                    await context.AddRangeAsync(questions);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}