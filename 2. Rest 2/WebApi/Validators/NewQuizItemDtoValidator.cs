using FluentValidation;
using WebApi.Dto;

namespace WebApi.Validators;

public class NewQuizItemDtoValidator : AbstractValidator<NewQuizItemDto>
{
    public NewQuizItemDtoValidator()
    {
        RuleFor(x => x.Question)
            .NotEmpty()
            .WithMessage("Question is required.");

        RuleFor(x => x.Options)
            .NotEmpty()
            .WithMessage("At least one option is required.")
            .Must(options => options.All(option => !string.IsNullOrWhiteSpace(option)))
            .WithMessage("Option cannot be empty or whitespace.");

        RuleFor(x => x.CorrectOptionIndex)
            .InclusiveBetween(0, Int32.MaxValue)
            .WithMessage("CorrectOptionIndex must be a positive number.")
            .Must((dto, correctOptionIndex) => correctOptionIndex < dto.Options.Count)
            .WithMessage("CorrectOptionIndex must be within the range of the options array.");
    }
}