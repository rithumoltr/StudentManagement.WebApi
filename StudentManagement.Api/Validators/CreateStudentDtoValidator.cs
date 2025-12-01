using FluentValidation;
using StudentManagement.Api.DTO;

namespace StudentManagement.Api.Validators
{
    public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
    {
        public CreateStudentDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Enter valid email address");
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date of birth is required");
            RuleFor(x => x.Gender).NotEmpty().WithMessage("Gender is required").Must(g => g=='M' || g=='F').WithMessage("Enter M or F only");
        }
    }
}
