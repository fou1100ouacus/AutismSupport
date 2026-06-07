// using MediatR;
// using Microsoft.AspNetCore.Mvc;
// using Core.Bases;
// using System.Net;

// namespace Api.Base
// {
//     [ApiController]
//     public class AppControllerBase : ControllerBase
//     {
//         private IMediator _mediatorInstance;
//         protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

//         #region Actions
//         public ObjectResult NewResult<T>(Response<T> response)
//         {
//             switch (response.StatusCode)
//             {
//                 case HttpStatusCode.OK:
//                     return new OkObjectResult(response);
//                 case HttpStatusCode.Created:
//                     return new CreatedResult(string.Empty, response);
//                 case HttpStatusCode.Unauthorized:
//                     return new UnauthorizedObjectResult(response);
//                 case HttpStatusCode.BadRequest:
//                     return new BadRequestObjectResult(response);
//                 case HttpStatusCode.NotFound:
//                     return new NotFoundObjectResult(response);
//                 case HttpStatusCode.Accepted:
//                     return new AcceptedResult(string.Empty, response);
//                 case HttpStatusCode.UnprocessableEntity:
//                     return new UnprocessableEntityObjectResult(response);
//                 default:
//                     return new BadRequestObjectResult(response);
//             }
//         }
//         #endregion

//     }
// }
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Core.Bases;
using System.Net;
using Microsoft.Extensions.DependencyInjection; // للتأكد من عمل GetService بدون مشاكل

namespace Api.Base
{
    [ApiController]
    public class AppControllerBase : ControllerBase
    {
        private IMediator _mediatorInstance;
        protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

        #region Actions
        public ObjectResult NewResult<T>(Response<T> response)
        {
            // تأمين أولى: لو الـ Response مش موجود أصلاً
            if (response == null)
            {
                return new BadRequestObjectResult(new { Succeeded = false, Message = "Response data is null." });
            }

            // لو العملية نجحت والـ StatusCode راجع بـ 0 أو مش مضبوط، حوّله تلقائياً لـ OK (200)
            if (response.Succeeded && (response.StatusCode == 0 || response.StatusCode == HttpStatusCode.OK))
            {
                // اختياري: يمكنك تعيين القيمة داخل الأوبجكت أيضاً ليظهر في الـ JSON بـ 200 بدلاً من 0
                response.StatusCode = HttpStatusCode.OK; 
                return new OkObjectResult(response);
            }

            // هندلة باقي الحالات بناءً على الـ StatusCode المبعوت في حالات الفشل أو الحالات الأخرى
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(response);
                case HttpStatusCode.Created:
                    return new CreatedResult(string.Empty, response);
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedObjectResult(response);
                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(response);
                case HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(response);
                case HttpStatusCode.Accepted:
                    return new AcceptedResult(string.Empty, response);
                case HttpStatusCode.UnprocessableEntity:
                    return new UnprocessableEntityObjectResult(response);
                default:
                    // إذا كانت العملية فشلت والـ StatusCode غير معرف، يرجع BadRequest (400) كـ حماية
                    return new BadRequestObjectResult(response);
            }
        }


        
        #endregion
    }
}