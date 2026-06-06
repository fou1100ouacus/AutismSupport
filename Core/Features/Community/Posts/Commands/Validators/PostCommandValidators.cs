using Core.Features.Community.Posts.Commands.Models;
using FluentValidation;

namespace Core.Features.Community.Posts.Commands.Validators
{
    public class CreatePostValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("محتوى البوست مطلوب")
                .MaximumLength(2000).WithMessage("المحتوى يجب ألا يتجاوز 2000 حرف");

            RuleFor(x => x.Photo)
                .Must(file => file == null || file.Length <= 5 * 1024 * 1024)
                .WithMessage("حجم الصورة يجب ألا يتجاوز 5 ميجا")
                .Must(file => file == null || new[] { ".jpg", ".jpeg", ".png", ".gif" }
                    .Contains(Path.GetExtension(file.FileName).ToLower()))
                .WithMessage("نوع الصورة غير مدعوم - يرجى رفع jpg أو png أو gif")
                .When(x => x.Photo != null);
        }
    }

    public class ModeratePostValidator : AbstractValidator<ModeratePostCommand>
    {
        public ModeratePostValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id غير صحيح");
            RuleFor(x => x.Note).MaximumLength(500).WithMessage("الملاحظة طويلة جداً");
        }
    }
}