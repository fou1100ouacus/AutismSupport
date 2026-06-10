// 

using Data.Entities.AbilitiesTracker; 
using Infrastructure.Data; 
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Data.Entities.Identity;

namespace Infrastructure.Seeder
{
    public static class ApplicationDataSeeder
    {
        public static async Task SeedDataAsync(ApplicationDBContext context)
        {
            // 🚨 خطوة أمان لـ SQLite: لو الجدول فاضي أو فيه الـ 3 أسئلة القدام بس، امسحهم وافرش الـ 45 الجداد
            if (context.AbilityQuestions.Count() <= 3)
            {
                context.AbilityQuestions.RemoveRange(context.AbilityQuestions);
                context.AbilityCategories.RemoveRange(context.AbilityCategories);
                await context.SaveChangesAsync();
            }

            // 1. التأكد من أن جدول التصنيفات جاهز
            if (!context.AbilityCategories.Any())
            {
                var categories = new List<AbilityCategory>
                {
                    new AbilityCategory { NameEn = "Language and Communication Skills" },
                    new AbilityCategory { NameEn = "Social Skills" }
                };

                await context.AddRangeAsync(categories);
                await context.SaveChangesAsync(); 
            }

            // 2. فرش الـ 45 سؤالاً الجدد بشكل احترافي
            if (!context.AbilityQuestions.Any())
            {
                var languageCategory = context.AbilityCategories.FirstOrDefault(c => c.NameEn == "Language and Communication Skills");
                var socialCategory = context.AbilityCategories.FirstOrDefault(c => c.NameEn == "Social Skills");

                var questions = new List<AbilityQuestion>();

                // 🌐 القسم الأول: Language and Communication Skills (23 سؤال)
                if (languageCategory != null)
                {
                    questions.AddRange(new List<AbilityQuestion>
                    {
                        new AbilityQuestion { QuestionTextEn = "Can the child express basic needs using clear sentences?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child have difficulty understanding simple instructions?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child repeat words or phrases mechanically out of context (Echolalia)?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Can the child follow a two-step command (e.g., 'Pick up the toy and put it on the table')?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child use pronouns correctly (e.g., saying 'I want' instead of 'He wants')?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child rely mostly on gestures or pulling hands rather than words to communicate?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Can the child initiate a conversation or ask spontaneous questions?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child invert or mix up letters and words consistently when speaking?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child respond consistently when their name is called?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child seem to ignore verbal communication as if they do not hear?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Can the child name at least 5 common household objects correctly?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child struggle to vary their tone of voice (e.g., speaks in a monotonous or robotic way)?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Can the child use simple non-verbal gestures like waving goodbye or nodding yes?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child take literal meanings of words and struggle with simple jokes or sarcasm?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Is the child able to maintain a back-and-forth conversation for at least three turns?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child struggle to find the right words to explain something that happened recently?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Can the child understand and react properly to 'Who', 'What', and 'Where' questions?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child use non-functional sounds or humming instead of speech when stressed?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Can the child express emotions verbally (e.g., saying 'I am happy' or 'I am scared')?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child have difficulty adjusting their vocal volume based on the environment?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Can the child describe a picture or a scene using simple descriptive adjectives?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child frequently use jumbled speech or self-invented words that others cannot understand?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Can the child clearly understand simple conditional sentences (e.g., 'If you finish, you can play')?", IsPositiveSkill = true, CategoryId = languageCategory.Id }
                    });
                }

                // 🤝 القسم الثاني: Social Skills (22 سؤال)
                if (socialCategory != null)
                {
                    questions.AddRange(new List<AbilityQuestion>
                    {
                        new AbilityQuestion { QuestionTextEn = "Does the child make eye contact while being spoken to?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child prefer to play alone rather than join group activities with peers?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child share toys or food spontaneously with siblings or peers?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child exhibit difficulties taking turns during games or shared activities?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child wave or greet familiar people when prompted?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child struggle to understand or mirror the facial expressions of others (e.g., sadness, joy)?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child respond positively to physical comfort or affection from parents?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child look at an object when you point to it (Shared/Joint Attention)?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child show objects of interest to others just to share the excitement?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child react with extreme distress or tantrums to minor changes in their social routine?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Can the child copy or imitate simple actions of other children during play?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child seem unaware of social boundaries (e.g., standing too close or interrupting others abruptly)?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child show empathy or try to comfort someone who appears visibly sad or crying?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child struggle to understand the concept of rules in competitive play?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Can the child participate appropriately in basic pretend or imaginative role-play games?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child become overwhelmingly defensive or isolate themselves when approached by new peers?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child recognize the difference between familiar people and complete strangers?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child focus obsessively on parts of objects (like spinning wheels) instead of interacting with people?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Can the child verbally or non-verbally seek help from adults when facing a problem?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child struggle to interpret basic social cues, like someone walking away when bored?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child smile or laugh in response to someone else's positive interaction?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
                        new AbilityQuestion { QuestionTextEn = "Does the child exhibit inappropriate behaviors (like laughing out loud) during serious or sad social situations?", IsPositiveSkill = false, CategoryId = socialCategory.Id }
                    });
                }

                if (questions.Any())
                {
                    await context.AddRangeAsync(questions);
                    await context.SaveChangesAsync();
                }
            }
        }
   
   
   public static async Task SeedAsync(RoleManager<Role> _roleManager)
        {
            var rolesCount = await _roleManager.Roles.CountAsync();
            if (rolesCount<=0)
            {

                await _roleManager.CreateAsync(new Role()
                {
                    Name="Admin"
                });
                await _roleManager.CreateAsync(new Role()
                {
                    Name="User"
                });
            }
        }

   
    public static async Task SeedAsync(UserManager<User> _userManager)
        {
            var usersCount = await _userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var defaultuser = new User()
                {
                    UserName = "admin",
                    Email = "admin@project.com",
                    FullName="",
                    Country="Egypt",
                    PhoneNumber="123456",
                    Address="Egypt",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await _userManager.CreateAsync(defaultuser, "M123_m");
                await _userManager.AddToRoleAsync(defaultuser, "Admin");
            }
        }
   
   
    }
}