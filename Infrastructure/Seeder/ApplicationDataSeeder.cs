// // 

// using Data.Entities.AbilitiesTracker; 
// using Infrastructure.Data; 
// using Infrastructure.Context;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using Data.Entities.Identity;

// namespace Infrastructure.Seeder
// {
//     public static class ApplicationDataSeeder
//     {
//         public static async Task SeedDataAsync(ApplicationDBContext context)
//         {
//             // 🚨 خطوة أمان لـ SQLite: لو الجدول فاضي أو فيه الـ 3 أسئلة القدام بس، امسحهم وافرش الـ 45 الجداد
//             if (context.AbilityQuestions.Count() <= 3)
//             {
//                 context.AbilityQuestions.RemoveRange(context.AbilityQuestions);
//                 context.AbilityCategories.RemoveRange(context.AbilityCategories);
//                 await context.SaveChangesAsync();
//             }

//             // 1. التأكد من أن جدول التصنيفات جاهز
//             if (!context.AbilityCategories.Any())
//             {
//                 var categories = new List<AbilityCategory>
//                 {
//                     new AbilityCategory { NameEn = "Language and Communication Skills" },
//                     new AbilityCategory { NameEn = "Social Skills" }
//                 };

//                 await context.AddRangeAsync(categories);
//                 await context.SaveChangesAsync(); 
//             }

//             // 2. فرش الـ 45 سؤالاً الجدد بشكل احترافي
//             if (!context.AbilityQuestions.Any())
//             {
//                 var languageCategory = context.AbilityCategories.FirstOrDefault(c => c.NameEn == "Language and Communication Skills");
//                 var socialCategory = context.AbilityCategories.FirstOrDefault(c => c.NameEn == "Social Skills");

//                 var questions = new List<AbilityQuestion>();

//                 // 🌐 القسم الأول: Language and Communication Skills (23 سؤال)
//                 if (languageCategory != null)
//                 {
//                     questions.AddRange(new List<AbilityQuestion>
//                     {
//                         new AbilityQuestion { QuestionTextEn = "Can the child express basic needs using clear sentences?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child have difficulty understanding simple instructions?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child repeat words or phrases mechanically out of context (Echolalia)?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Can the child follow a two-step command (e.g., 'Pick up the toy and put it on the table')?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child use pronouns correctly (e.g., saying 'I want' instead of 'He wants')?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child rely mostly on gestures or pulling hands rather than words to communicate?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Can the child initiate a conversation or ask spontaneous questions?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child invert or mix up letters and words consistently when speaking?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child respond consistently when their name is called?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child seem to ignore verbal communication as if they do not hear?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Can the child name at least 5 common household objects correctly?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child struggle to vary their tone of voice (e.g., speaks in a monotonous or robotic way)?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Can the child use simple non-verbal gestures like waving goodbye or nodding yes?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child take literal meanings of words and struggle with simple jokes or sarcasm?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Is the child able to maintain a back-and-forth conversation for at least three turns?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child struggle to find the right words to explain something that happened recently?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Can the child understand and react properly to 'Who', 'What', and 'Where' questions?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child use non-functional sounds or humming instead of speech when stressed?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Can the child express emotions verbally (e.g., saying 'I am happy' or 'I am scared')?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child have difficulty adjusting their vocal volume based on the environment?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Can the child describe a picture or a scene using simple descriptive adjectives?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child frequently use jumbled speech or self-invented words that others cannot understand?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Can the child clearly understand simple conditional sentences (e.g., 'If you finish, you can play')?", IsPositiveSkill = true, CategoryId = languageCategory.Id }
//                     });
//                 }

//                 // 🤝 القسم الثاني: Social Skills (22 سؤال)
//                 if (socialCategory != null)
//                 {
//                     questions.AddRange(new List<AbilityQuestion>
//                     {
//                         new AbilityQuestion { QuestionTextEn = "Does the child make eye contact while being spoken to?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child prefer to play alone rather than join group activities with peers?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child share toys or food spontaneously with siblings or peers?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child exhibit difficulties taking turns during games or shared activities?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child wave or greet familiar people when prompted?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child struggle to understand or mirror the facial expressions of others (e.g., sadness, joy)?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child respond positively to physical comfort or affection from parents?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child look at an object when you point to it (Shared/Joint Attention)?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child show objects of interest to others just to share the excitement?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child react with extreme distress or tantrums to minor changes in their social routine?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Can the child copy or imitate simple actions of other children during play?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child seem unaware of social boundaries (e.g., standing too close or interrupting others abruptly)?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child show empathy or try to comfort someone who appears visibly sad or crying?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child struggle to understand the concept of rules in competitive play?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Can the child participate appropriately in basic pretend or imaginative role-play games?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child become overwhelmingly defensive or isolate themselves when approached by new peers?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child recognize the difference between familiar people and complete strangers?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child focus obsessively on parts of objects (like spinning wheels) instead of interacting with people?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Can the child verbally or non-verbally seek help from adults when facing a problem?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child struggle to interpret basic social cues, like someone walking away when bored?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child smile or laugh in response to someone else's positive interaction?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child exhibit inappropriate behaviors (like laughing out loud) during serious or sad social situations?", IsPositiveSkill = false, CategoryId = socialCategory.Id }
//                     });
//                 }

//                 if (questions.Any())
//                 {
//                     await context.AddRangeAsync(questions);
//                     await context.SaveChangesAsync();
//                 }
//             }
//         }
   
   
//     public static async Task SeedAsync(UserManager<User> _userManager)
//         {
//             var usersCount = await _userManager.Users.CountAsync();
//             if (usersCount <= 0)
//             {
//                 var defaultuser = new User()
//                 {
//                     UserName = "admin",
//                     Email = "admin@project.com",
//                     FullName="",
//                     Country="Egypt",
//                     PhoneNumber="123456",
//                     Address="Egypt",
//                     EmailConfirmed = true,
//                     PhoneNumberConfirmed = true
//                 };
//                 await _userManager.CreateAsync(defaultuser, "M123_m");
//                 await _userManager.AddToRoleAsync(defaultuser, "Admin");
//             }
//         }
   
   



//     }
// }




// using Data.Entities.AbilitiesTracker; 
// using Infrastructure.Data; 
// using Infrastructure.Context;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using Data.Entities.Identity;
// using Data.Entities.Community; // استدعاء مجلد كلاسات الـ Community الفعلي
// using Data.Enums;             // استدعاء الـ Enums الخاصة بك (PostStatus, ReactionType, إلخ)
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace Infrastructure.Seeder
// {
//     public static class ApplicationDataSeeder
//     {
//         public static async Task SeedDataAsync(ApplicationDBContext context)
//         {
//             // 🚨 خطوة أمان لـ SQLite: لو الجدول فاضي أو فيه الـ 3 أسئلة القدام بس، امسحهم وافرش الـ 45 الجداد
//             if (context.AbilityQuestions.Count() <= 3)
//             {
//                 context.AbilityQuestions.RemoveRange(context.AbilityQuestions);
//                 context.AbilityCategories.RemoveRange(context.AbilityCategories);
//                 await context.SaveChangesAsync();
//             }

//             // 1. التأكد من أن جدول التصنيفات جاهز
//             if (!context.AbilityCategories.Any())
//             {
//                 var categories = new List<AbilityCategory>
//                 {
//                     new AbilityCategory { NameEn = "Language and Communication Skills" },
//                     new AbilityCategory { NameEn = "Social Skills" }
//                 };

//                 await context.AddRangeAsync(categories);
//                 await context.SaveChangesAsync(); 
//             }

//             // 2. فرش الـ 45 سؤالاً الجدد بشكل احترافي
//             if (!context.AbilityQuestions.Any())
//             {
//                 var languageCategory = context.AbilityCategories.FirstOrDefault(c => c.NameEn == "Language and Communication Skills");
//                 var socialCategory = context.AbilityCategories.FirstOrDefault(c => c.NameEn == "Social Skills");

//                 var questions = new List<AbilityQuestion>();

//                 if (languageCategory != null)
//                 {
//                     questions.AddRange(new List<AbilityQuestion>
//                     {
//                         new AbilityQuestion { QuestionTextEn = "Can the child express basic needs using clear sentences?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child have difficulty understanding simple instructions?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child repeat words or phrases mechanically out of context (Echolalia)?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Can the child follow a two-step command (e.g., 'Pick up the toy and put it on the table')?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child use pronouns correctly (e.g., saying 'I want' instead of 'He wants')?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child rely mostly on gestures or pulling hands rather than words to communicate?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Can the child initiate a conversation or ask spontaneous questions?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child invert or mix up letters and words consistently when speaking?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child respond consistently when their name is called?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child seem to ignore verbal communication as if they do not hear?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Can the child name at least 5 common household objects correctly?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child struggle to vary their tone of voice (e.g., speaks in a monotonous or robotic way)?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Can the child use simple non-verbal gestures like waving goodbye or nodding yes?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child take literal meanings of words and struggle with simple jokes or sarcasm?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Is the child able to maintain a back-and-forth conversation for at least three turns?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child struggle to find the right words to explain something that happened recently?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Can the child understand and react properly to 'Who', 'What', and 'Where' questions?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child use non-functional sounds or humming instead of speech when stressed?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Can the child express emotions verbally (e.g., saying 'I am happy' or 'I am scared')?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child have difficulty adjusting their vocal volume based on the environment?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Can the child describe a picture or a scene using simple descriptive adjectives?", IsPositiveSkill = true, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child frequently use jumbled speech or self-invented words that others cannot understand?", IsPositiveSkill = false, CategoryId = languageCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Can the child clearly understand simple conditional sentences (e.g., 'If you finish, you can play')?", IsPositiveSkill = true, CategoryId = languageCategory.Id }
//                     });
//                 }

//                 if (socialCategory != null)
//                 {
//                     questions.AddRange(new List<AbilityQuestion>
//                     {
//                         new AbilityQuestion { QuestionTextEn = "Does the child make eye contact while being spoken to?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child prefer to play alone rather than join group activities with peers?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child share toys or food spontaneously with siblings or peers?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child exhibit difficulties taking turns during games or shared activities?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child wave or greet familiar people when prompted?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child struggle to understand or mirror the facial expressions of others (e.g., sadness, joy)?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child respond positively to physical comfort or affection from parents?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child look at an object when you point to it (Shared/Joint Attention)?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child show objects of interest to others just to share the excitement?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child react with extreme distress or tantrums to minor changes in their social routine?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Can the child copy or imitate simple actions of other children during play?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child seem unaware of social boundaries (e.g., standing too close or interrupting others abruptly)?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child show empathy or try to comfort someone who appears visibly sad or crying?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child struggle to understand the concept of rules in competitive play?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Can the child participate appropriately in basic pretend or imaginative role-play games?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child become overwhelmingly defensive or isolate themselves when approached by new peers?", IsPositiveSkill = false, CategoryId = socialCategory.Id },
//                         new AbilityQuestion { QuestionTextEn = "Does the child recognize the difference between familiar people and complete strangers?", IsPositiveSkill = true, CategoryId = socialCategory.Id },
//                     });
//                 }

//                 if (questions.Any())
//                 {
//                     await context.AddRangeAsync(questions);
//                     await context.SaveChangesAsync();
//                 }
//             }

//             // 🌟 استدعاء سيرفر فرش مجتمع التطبيق بالـ Enums المطابقة تماماً لكودك
//             await SeedFullCommunityDataAsync(context);
//         }

//         public static async Task SeedFullCommunityDataAsync(ApplicationDBContext context)
//         {
//             // 1️⃣ تأمين وجود مستخدم لربط المنشورات والتعليقات بقاعدة البيانات
//             var defaultAdmin = await context.Users.FirstOrDefaultAsync(u => u.UserName == "admin");
//             if (defaultAdmin == null) return;

//             var secondUser = await context.Users.Skip(1).FirstOrDefaultAsync() ?? defaultAdmin;

//             // ==========================================
//             // 2️⃣ فرش الـ CommunityPosts (باستخدام PostStatus المعرّف لديك)
//             // ==========================================
//             if (!context.CommunityPosts.Any())
//             {
//                 var seedPosts = new List<CommunityPost>
//                 {
//                     new CommunityPost
//                     {
//                         Content = "Hello everyone! My 3-year-old child is showing great progress in communication, but still struggles with naming colors. Does anyone have fun interactive game recommendations to help with this?",
//                         UserId = secondUser.Id,
//                         CreatedAt = DateTime.UtcNow.AddDays(-6),
//                         Status = PostStatus.Approved, // متوافق مع الـ Enum الخاص بك
//                         CommentsCount = 2,
//                         ReactionsCount = 2
//                     },
//                     new CommunityPost
//                     {
//                         Content = "An absolute milestone today! After weeks of consistent tracking and applying the communication recommendations, my son initiated eye contact and asked for water clearly! Consistency is key, parents.",
//                         UserId = defaultAdmin.Id,
//                         CreatedAt = DateTime.UtcNow.AddDays(-4),
//                         Status = PostStatus.Approved, // متوافق مع الـ Enum الخاص بك
//                         CommentsCount = 1,
//                         ReactionsCount = 1
//                     },
//                     new CommunityPost
//                     {
//                         Content = "Quick tip for managing screen time: High sensory videos often overstimulate kids, making social focus harder later. Try shifting to interactive or educational audiobooks for 20 minutes instead.",
//                         UserId = secondUser.Id,
//                         CreatedAt = DateTime.UtcNow.AddDays(-2),
//                         Status = PostStatus.Approved, // متوافق مع الـ Enum الخاص بك
//                         CommentsCount = 0,
//                         ReactionsCount = 0
//                     },
//                     new CommunityPost
//                     {
//                         Content = "This is an inappropriate spam post containing promotional advertising links and non-community related content.",
//                         UserId = secondUser.Id,
//                         CreatedAt = DateTime.UtcNow.AddHours(-5),
//                         Status = PostStatus.Pending, // يظهر تلقائياً في طابور الـ Moderation للآدمن
//                         CommentsCount = 0,
//                         ReactionsCount = 0
//                     }
//                 };

//                 await context.CommunityPosts.AddRangeAsync(seedPosts);
//                 await context.SaveChangesAsync(); 
//             }

//             // جلب المنشورات من الداتا بيز لعمل الـ Mapping الصحيح للعلاقات
//             var trackPosts = await context.CommunityPosts.ToListAsync();
//             if (!trackPosts.Any()) return;

//             var postOne = trackPosts[0];
//             var postTwo = trackPosts[1];
//             var postFour = trackPosts.Last(); 

//             // ==========================================
//             // 3️⃣ فرش الـ CommunityComments (باستخدام CommentStatus المعرّف لديك)
//             // ==========================================
//             if (!context.CommunityComments.Any())
//             {
//                 var seedComments = new List<CommunityComment>
//                 {
//                     new CommunityComment
//                     {
//                         Content = "For colors, sorting colored blocks into matching cups worked like magic for my daughter! Start with just two primary colors (Red and Blue) then expand.",
//                         PostId = postOne.Id,
//                         UserId = defaultAdmin.Id,
//                         CreatedAt = DateTime.UtcNow.AddDays(-5),
//                         Status = CommentStatus.Approved // متوافق مع الـ Enum الخاص بك
//                     },
//                     new CommunityComment
//                     {
//                         Content = "I highly recommend the 'I Spy' game around the living room. Say things like 'I spy something red!' It keeps them moving and learning dynamically.",
//                         PostId = postOne.Id,
//                         UserId = secondUser.Id,
//                         CreatedAt = DateTime.UtcNow.AddDays(-5),
//                         Status = CommentStatus.Approved // متوافق مع الـ Enum الخاص بك
//                     },
//                     new CommunityComment
//                     {
//                         Content = "This is incredibly heartwarming to read! Congratulations on this beautiful milestone! 🎉",
//                         PostId = postTwo.Id,
//                         UserId = secondUser.Id,
//                         CreatedAt = DateTime.UtcNow.AddDays(-3),
//                         Status = CommentStatus.Approved // متوافق مع الـ Enum الخاص بك
//                     }
//                 };

//                 await context.CommunityComments.AddRangeAsync(seedComments);
//             }

//             // ==========================================
//             // 4️⃣ فرش الـ CommunityReactions (باستخدام ReactionType و TargetType)
//             // ==========================================
//             if (!context.CommunityReactions.Any())
//             {
//                 var seedReactions = new List<CommunityReaction>
//                 {
//                     new CommunityReaction 
//                     { 
//                         PostId = postOne.Id, 
//                         UserId = defaultAdmin.Id, 
//                         ReactionType = ReactionType.ThumbsUp, // متوافق مع كود الـ Enum عندك
//                         TargetType = ReactionTargetType.Post,
//                         CreatedAt = DateTime.UtcNow.AddDays(-5) 
//                     },
//                     new CommunityReaction 
//                     { 
//                         PostId = postOne.Id, 
//                         UserId = secondUser.Id, 
//                         ReactionType = ReactionType.ThumbsUp, 
//                         TargetType = ReactionTargetType.Post,
//                         CreatedAt = DateTime.UtcNow.AddDays(-5) 
//                     },
//                     new CommunityReaction 
//                     { 
//                         PostId = postTwo.Id, 
//                         UserId = secondUser.Id, 
//                         ReactionType = ReactionType.Heart, // متوافق مع كود الـ Enum عندك
//                         TargetType = ReactionTargetType.Post,
//                         CreatedAt = DateTime.UtcNow.AddDays(-3) 
//                     }
//                 };

//                 await context.CommunityReactions.AddRangeAsync(seedReactions);
//             }

//             // ==========================================
//             // 5️⃣ فرش الـ CommunityReports (باستخدام ReportedByUserId و ReportStatus)
//             // ==========================================
//             if (!context.CommunityReports.Any())
//             {
//                 var seedReports = new List<CommunityReport>
//                 {
//                     new CommunityReport
//                     {
//                         PostId = postFour.Id, 
//                         ReportedByUserId = defaultAdmin.Id, // اسم الحقل الفعلي المطابق للكلاس الخاص بك
//                         Reason = "Spam, commercial advertising, and violations of community guidelines.",
//                         TargetType = ReportTargetType.Post, // متوافق مع الـ Enum الخاص بك
//                         Status = ReportStatus.Open,         // بلاغ مفتوح ينتظر الإشراف من الـ Admin
//                         CreatedAt = DateTime.UtcNow.AddHours(-4)
//                     }
//                 };

//                 await context.CommunityReports.AddRangeAsync(seedReports);
//             }

//             await context.SaveChangesAsync();
//         }

//         public static async Task SeedAsync(RoleManager<Role> _roleManager)
//         {
//             var rolesCount = await _roleManager.Roles.CountAsync();
//             if (rolesCount <= 0)
//             {
//                 await _roleManager.CreateAsync(new Role() { Name = "Admin" });
//                 await _roleManager.CreateAsync(new Role() { Name = "User" });
//             }
//         }

//         public static async Task SeedAsync(UserManager<User> _userManager)
//         {
//             var usersCount = await _userManager.Users.CountAsync();
//             if (usersCount <= 0)
//             {
//                 var defaultuser = new User()
//                 {
//                     UserName = "admin",
//                     Email = "admin@project.com",
//                     FullName = "Admin Account",
//                     Country = "Egypt",
//                     PhoneNumber = "123456",
//                     Address = "Egypt",
//                     EmailConfirmed = true,
//                     PhoneNumberConfirmed = true
//                 };
//                 await _userManager.CreateAsync(defaultuser, "M123_m");
//                 await _userManager.AddToRoleAsync(defaultuser, "Admin");
//             }
//         }
//     }
// }








using Data.Entities.AbilitiesTracker; 
using Infrastructure.Data; 
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Data.Entities.Identity;
using Data.Entities.Community; 
using Data.Enums;             
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            // 1️⃣ أولاً: فرش الـ Categories لو الجدول فارغ
            if (!context.AbilityCategories.Any())
            {
                var categories = new List<AbilityCategory>
                {
                    new AbilityCategory { NameEn = "Language and Communication Skills" },
                    new AbilityCategory { NameEn = "Social Skills" }
                };

                await context.AbilityCategories.AddRangeAsync(categories);
                await context.SaveChangesAsync(); // حفظ فوري في الـ SQLite لتوليد الـ Ids
            }

            // 2️⃣ ثانياً: فرش الـ 45 سؤالاً بناءً على الـ Categories المحفوظة فعلياً
            if (!context.AbilityQuestions.Any())
            {
                var languageCategory = await context.AbilityCategories.FirstOrDefaultAsync(c => c.NameEn == "Language and Communication Skills");
                var socialCategory = await context.AbilityCategories.FirstOrDefaultAsync(c => c.NameEn == "Social Skills");

                var questions = new List<AbilityQuestion>();

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
                    await context.AbilityQuestions.AddRangeAsync(questions);
                    await context.SaveChangesAsync(); // حفظ الأسئلة منفصلة
                }
            }

            // 3️⃣ ثالثاً: فرش مجتمع التطبيق (Community Data) بتسلسل آمن للـ SQLite
            await SeedFullCommunityDataAsync(context);
        }

        // public static async Task SeedFullCommunityDataAsync(ApplicationDBContext context)
        // {
        //     // التحقق من وجود حساب الآدمن لربطه بالعلاقات
        //     var defaultAdmin = await context.Users.FirstOrDefaultAsync(u => u.UserName == "admin");
        //     if (defaultAdmin == null) return;

        //     var secondUser = await context.Users.Skip(1).FirstOrDefaultAsync() ?? defaultAdmin;

        //     // أ. فرش المنشورات (CommunityPosts) أولاً وحفظها منفصلة لضمان توليد الـ IDs في الـ SQLite
        //     if (!context.CommunityPosts.Any())
        //     {
        //         var seedPosts = new List<CommunityPost>
        //         {
        //             new CommunityPost
        //             {
        //                 Content = "Hello everyone! My 3-year-old child is showing great progress in communication, but still struggles with naming colors. Does anyone have fun interactive game recommendations to help with this?",
        //                 UserId = secondUser.Id,
        //                 CreatedAt = DateTime.UtcNow.AddDays(-6),
        //                 Status = PostStatus.Approved,
        //                 CommentsCount = 2,
        //                 ReactionsCount = 2
        //             },
        //             new CommunityPost
        //             {
        //                 Content = "An absolute milestone today! After weeks of consistent tracking and applying the communication recommendations, my son initiated eye contact and asked for water clearly! Consistency is key, parents.",
        //                 UserId = defaultAdmin.Id,
        //                 CreatedAt = DateTime.UtcNow.AddDays(-4),
        //                 Status = PostStatus.Approved,
        //                 CommentsCount = 1,
        //                 ReactionsCount = 1
        //             },
        //             new CommunityPost
        //             {
        //                 Content = "Quick tip for managing screen time: High sensory videos often overstimulate kids, making social focus harder later. Try shifting to interactive or educational audiobooks for 20 minutes instead.",
        //                 UserId = secondUser.Id,
        //                 CreatedAt = DateTime.UtcNow.AddDays(-2),
        //                 Status = PostStatus.Approved,
        //                 CommentsCount = 0,
        //                 ReactionsCount = 0
        //             },
        //             new CommunityPost
        //             {
        //                 Content = "This is an inappropriate spam post containing promotional advertising links and non-community related content.",
        //                 UserId = secondUser.Id,
        //                 CreatedAt = DateTime.UtcNow.AddHours(-5),
        //                 Status = PostStatus.Pending,
        //                 CommentsCount = 0,
        //                 ReactionsCount = 0
        //             }
        //         };

        //         await context.CommunityPosts.AddRangeAsync(seedPosts);
        //         await context.SaveChangesAsync(); // خطوة إلزامية لـ SQLite عشان نقدر نربط الكومنتات واللايكات بـ IDs حقيقية
        //     }

        //     // جلب المنشورات المخزنة حالياً لبناء العلاقات الفرعية بأمان
        //     var trackPosts = await context.CommunityPosts.ToListAsync();
        //     if (!trackPosts.Any()) return;

        //     var postOne = trackPosts[0];
        //     var postTwo = trackPosts[1];
        //     var postFour = trackPosts.Last(); 

        //     // ب. فرش التعليقات (CommunityComments)
        //     if (!context.CommunityComments.Any())
        //     {
        //         var seedComments = new List<CommunityComment>
        //         {
        //             new CommunityComment
        //             {
        //                 PostId = postOne.Id,
        //                 UserId = defaultAdmin.Id,
        //                 Content = "For colors, sorting colored blocks into matching cups worked like magic for my daughter! Start with just two primary colors (Red and Blue) then expand.",
        //                 Status = CommentStatus.Approved,
        //                 CreatedAt = DateTime.UtcNow.AddDays(-5)
        //             },
        //             new CommunityComment
        //             {
        //                 PostId = postOne.Id,
        //                 UserId = secondUser.Id,
        //                 Content = "I highly recommend the 'I Spy' game around the living room. Say things like 'I spy something red!' It keeps them moving and learning dynamically.",
        //                 Status = CommentStatus.Approved,
        //                 CreatedAt = DateTime.UtcNow.AddDays(-5)
        //             },
        //             new CommunityComment
        //             {
        //                 PostId = postTwo.Id,
        //                 UserId = secondUser.Id,
        //                 Content = "This is incredibly heartwarming to read! Congratulations on this beautiful milestone! 🎉",
        //                 Status = CommentStatus.Approved,
        //                 CreatedAt = DateTime.UtcNow.AddDays(-3)
        //             }
        //         };

        //         await context.CommunityComments.AddRangeAsync(seedComments);
        //     }

        //     // ج. فرش التفاعلات (CommunityReactions)
        //     if (!context.CommunityReactions.Any())
        //     {
        //         var seedReactions = new List<CommunityReaction>
        //         {
        //             new CommunityReaction { PostId = postOne.Id, UserId = defaultAdmin.Id, ReactionType = ReactionType.ThumbsUp, TargetType = ReactionTargetType.Post, CreatedAt = DateTime.UtcNow.AddDays(-5) },
        //             new CommunityReaction { PostId = postOne.Id, UserId = secondUser.Id, ReactionType = ReactionType.ThumbsUp, TargetType = ReactionTargetType.Post, CreatedAt = DateTime.UtcNow.AddDays(-5) },
        //             new CommunityReaction { PostId = postTwo.Id, UserId = secondUser.Id, ReactionType = ReactionType.Heart, TargetType = ReactionTargetType.Post, CreatedAt = DateTime.UtcNow.AddDays(-3) }
        //         };

        //         await context.CommunityReactions.AddRangeAsync(seedReactions);
        //     }

        //     // د. فرش البلاغات (CommunityReports)
        //     if (!context.CommunityReports.Any())
        //     {
        //         var seedReports = new List<CommunityReport>
        //         {
        //             new CommunityReport
        //             {
        //                 PostId = postFour.Id, 
        //                 ReportedByUserId = defaultAdmin.Id,
        //                 Reason = "Spam, commercial advertising, and violations of community guidelines.",
        //                 TargetType = ReportTargetType.Post,
        //                 Status = ReportStatus.Open,
        //                 CreatedAt = DateTime.UtcNow.AddHours(-4)
        //             }
        //         };

        //         await context.CommunityReports.AddRangeAsync(seedReports);
        //     }

        //     // الحفظ النهائي لكافة الكومنتات واللايكات والبلاغات معاً في الـ SQLite
        //     await context.SaveChangesAsync();
        // }



public static async Task SeedFullCommunityDataAsync(ApplicationDBContext context)
{
    // 1. جلب حساب الآدمن الرئيسي
    var defaultAdmin = await context.Users.FirstOrDefaultAsync(u => u.UserName == "admin");
    if (defaultAdmin == null) return;

    // 2. محاولة جلب مستخدم آخر حقيقي، وإذا لم يوجد، لن نقوم بتكرار التفاعلات لنفس المستخدم
    var secondUser = await context.Users.Where(u => u.UserName != "admin").FirstOrDefaultAsync();
    
    // تحديد منطقي: هل لدينا مستخدم ثانٍ حقيقي أم لا؟
    bool hasSecondUser = secondUser != null;
    int postCreatorId = hasSecondUser ? secondUser.Id : defaultAdmin.Id;

    // 3. التحقق من أن المنشورات فارغة تماماً
    if (!context.CommunityPosts.Any())
    {
        // تجهيز التفاعلات للبوست الأول بدون تكرار الـ UserId
        var postOneReactions = new List<CommunityReaction>
        {
            new CommunityReaction { UserId = defaultAdmin.Id, ReactionType = ReactionType.ThumbsUp, TargetType = ReactionTargetType.Post, CreatedAt = DateTime.UtcNow.AddDays(-5) }
        };
        // لا نضيف التفاعل الثاني إلا إذا كان المستخدم الثاني حقيقي ومختلف عن الـ Admin تماماً
        if (hasSecondUser)
        {
            postOneReactions.Add(new CommunityReaction { UserId = secondUser.Id, ReactionType = ReactionType.ThumbsUp, TargetType = ReactionTargetType.Post, CreatedAt = DateTime.UtcNow.AddDays(-5) });
        }

        // تجهيز المنشور الأول وبداخله تعليقاته وتفاعلاته الآمنة
        var postOne = new CommunityPost
        {
            Content = "Hello everyone! My 3-year-old child is showing great progress in communication, but still struggles with naming colors. Does anyone have fun interactive game recommendations to help with this?",
            UserId = postCreatorId,
            CreatedAt = DateTime.UtcNow.AddDays(-6),
            Status = PostStatus.Approved,
            CommentsCount = hasSecondUser ? 2 : 1,
            ReactionsCount = postOneReactions.Count,
            Comments = new List<CommunityComment>
            {
                new CommunityComment
                {
                    UserId = defaultAdmin.Id,
                    Content = "For colors, sorting colored blocks into matching cups worked like magic for my daughter! Start with just two primary colors (Red and Blue) then expand.",
                    Status = CommentStatus.Approved,
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                }
            },
            Reactions = postOneReactions
        };

        // إضافة كومنت المستخدم الثاني فقط لو كان حسابه موجوداً ومختلفاً
        if (hasSecondUser)
        {
            postOne.Comments.Add(new CommunityComment
            {
                UserId = secondUser.Id,
                Content = "I highly recommend the 'I Spy' game around the living room. Say things like 'I spy something red!' It keeps them moving and learning dynamically.",
                Status = CommentStatus.Approved,
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            });
        }

        // تجهيز المنشور الثاني وبداخله تعليقاته وتفاعلاته
        var postTwo = new CommunityPost
        {
            Content = "An absolute milestone today! After weeks of consistent tracking and applying the communication recommendations, my son initiated eye contact and asked for water clearly! Consistency is key, parents.",
            UserId = defaultAdmin.Id,
            CreatedAt = DateTime.UtcNow.AddDays(-4),
            Status = PostStatus.Approved,
            CommentsCount = hasSecondUser ? 1 : 0,
            ReactionsCount = hasSecondUser ? 1 : 0,
            Comments = new List<CommunityComment>(),
            Reactions = new List<CommunityReaction>()
        };

        if (hasSecondUser)
        {
            postTwo.Comments.Add(new CommunityComment
            {
                UserId = secondUser.Id,
                Content = "This is incredibly heartwarming to read! Congratulations on this beautiful milestone! 🎉",
                Status = CommentStatus.Approved,
                CreatedAt = DateTime.UtcNow.AddDays(-3)
            });

            postTwo.Reactions.Add(new CommunityReaction 
            { 
                UserId = secondUser.Id, 
                ReactionType = ReactionType.Heart, 
                TargetType = ReactionTargetType.Post, 
                CreatedAt = DateTime.UtcNow.AddDays(-3) 
            });
        }

        // منشور ثالث عادي بدون تفاعلات
        var postThree = new CommunityPost
        {
            Content = "Quick tip for managing screen time: High sensory videos often overstimulate kids, making social focus harder later. Try shifting to interactive or educational audiobooks for 20 minutes instead.",
            UserId = postCreatorId,
            CreatedAt = DateTime.UtcNow.AddDays(-2),
            Status = PostStatus.Approved,
            CommentsCount = 0,
            ReactionsCount = 0
        };

        // المنشور الرابع (Spam) وبداخله البلاغ الموجه ضده مباشرة 
        var postFour = new CommunityPost
        {
            Content = "This is an inappropriate spam post containing promotional advertising links and non-community related content.",
            UserId = postCreatorId,
            CreatedAt = DateTime.UtcNow.AddHours(-5),
            Status = PostStatus.Pending,
            CommentsCount = 0,
            ReactionsCount = 0,
            Reports = new List<CommunityReport>
            {
                new CommunityReport
                {
                    ReportedByUserId = defaultAdmin.Id,
                    Reason = "Spam, commercial advertising, and violations of community guidelines.",
                    TargetType = ReportTargetType.Post,
                    Status = ReportStatus.Open,
                    CreatedAt = DateTime.UtcNow.AddHours(-4)
                }
            }
        };

        // إضافة كل البوستات المحمية بالـ Validation في خطوة واحدة
        await context.CommunityPosts.AddRangeAsync(new List<CommunityPost> { postOne, postTwo, postThree, postFour });
        
        // 💾 حفظ نهائي آمن 100% متوافق مع الـ Unique Constraints
        await context.SaveChangesAsync();
    }
}
        public static async Task SeedAsync(RoleManager<Role> _roleManager)
        {
            var rolesCount = await _roleManager.Roles.CountAsync();
            if (rolesCount <= 0)
            {
                await _roleManager.CreateAsync(new Role() { Name = "Admin" });
                await _roleManager.CreateAsync(new Role() { Name = "User" });
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
                    FullName = "Admin Account",
                    Country = "Egypt",
                    PhoneNumber = "123456",
                    Address = "Egypt",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await _userManager.CreateAsync(defaultuser, "M123_m");
                await _userManager.AddToRoleAsync(defaultuser, "Admin");
            }
        }
    }
}


