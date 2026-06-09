// // using Data.Entities.AbilitiesTracker;

// // namespace Service.Abstracts
// // {
// //     public interface IAbilityService
// //     {  


// //         // جلب الأقسام والأسئلة لعرضها للأم
// //         Task<List<AbilityCategory>> GetAbilitiesCategoriesOnlyAsync();
// //         Task<List<AbilityTestResultDto>> GetHistoryByMotherAsync(int id);

// //         // معالجة وحفظ نتيجة التقييم
// //      //   Task<string> AddTestResultAsync(int childId, int categoryId, Dictionary<int, int> answers);
// //         Task<string> AddTestResultByMotherAsync(int motherId, int categoryId, Dictionary<int, int> answers);
   
// //         Task<List<AbilityQuestion>> GetQuestionsAsync(int categoryId);
// //     }
// // }

// using Data.Entities.AbilitiesTracker;
// using Core.Features.AbilitiesTracker; // عشان يشوف الـ DTOs (SubmitTestRequestDto و TestResultResponseDto)
// using System.Collections.Generic;
// using System.Threading.Tasks;

// namespace Service.Abstracts
// {
//     public interface IAbilityService
//     {
//         // الميثودز القديمة بتاعتك
//         Task<List<AbilityCategory>> GetAbilitiesCategoriesOnlyAsync();
//         Task<List<AbilityQuestion>> GetQuestionsAsync(int categoryId);
//         Task<List<AbilityTestResultDto>> GetHistoryByMotherAsync(int id);

//         // 🔥 الميثود الجديدة المجمعة اللي هناديها من الـ Controller
//         Task<TestResultResponseDto> SubmitTestAsync(SubmitTestRequestDto request);
//     }
// }

using Data.Entities.AbilitiesTracker;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Abstracts
{
    public interface IAbilityService
    {
        // جلب الأقسام والأسئلة من قاعدة البيانات ليستخدمها الـ Handler أو الـ Controller
        Task<List<AbilityCategory>> GetAbilitiesCategoriesOnlyAsync();
        Task<List<AbilityQuestion>> GetQuestionsAsync(int categoryId);
        
        // جلب قائمة الأسئلة كاملة (الـ 15 سؤال) لكي يستطيع الـ Handler حساب السكور بناءً عليها
        Task<IEnumerable<AbilityQuestion>> GetAllQuestionsWithCategoriesAsync();

        // حفظ أو جلب التاريخ للتقارير
        Task<bool> SaveTestResultAsync(AbilityTestResult testResult);
        Task<List<AbilityTestResult>> GetHistoryByMotherAsync(int motherId);
    }
}

