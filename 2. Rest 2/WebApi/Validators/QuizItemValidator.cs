using ApplicationCore.Models.QuizAggregate;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace WebApi.Validators;

public class QuizItemValidator : AbstractValidator<QuizItem>
{
    public QuizItemValidator()
    {
        RuleFor(q => q.Question)
            .MaximumLength(200).WithMessage("Question cannot be longer than 200 characters!")
            .MinimumLength(6);
        RuleForEach(q => q.IncorrectAnswers)
            .MinimumLength(1)
            .MaximumLength(200);
        RuleFor(q => new { q.IncorrectAnswers, q.CorrectAnswer })
            .Must(o => !o.IncorrectAnswers.Contains(o.CorrectAnswer))
            .WithMessage("Correct answer cannot be in the list of incorrect answers!");
        RuleFor(q => q.IncorrectAnswers)
            .Must(o => o.Count > 0)
            .WithMessage("At least one incorrect answer must be provided!");

    }
}