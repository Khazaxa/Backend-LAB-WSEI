using System.Runtime.InteropServices.JavaScript;
using ApplicationCore.Interfaces.AdminService;
using ApplicationCore.Models.QuizAggregate;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;
using WebApi.Validators;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/v1/admin/quizzes")]
public class ApiQuizAdminController : ControllerBase
{
    private readonly IQuizAdminService _service;
    
    public ApiQuizAdminController(IQuizAdminService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult CreateQuiz(LinkGenerator link, NewQuizDto dto)
    {
        var quiz = _service.AddQuiz(new Quiz() { Title = dto.Title });
        
        return Created(
            link.GetUriByAction(HttpContext, nameof(GetQuiz), null, new {quizId = quiz}),
            quiz
            );
    }
    
    [HttpGet]
    [Route("{quizId}")]
    public ActionResult<Quiz> GetQuiz(int quizId)
    {
        var quiz = _service.FindAllQuizzes().FirstOrDefault(q => q.Id == quizId);
        return quiz is null ? NotFound() : Ok(quiz);
    }
    
    [HttpPatch]
    [Route("{quizId}")]
    public IActionResult UpdateQuiz(IValidator<QuizItem> validator, int quizId, JsonPatchDocument<Quiz> patch)
    {
        var quiz = _service.FindAllQuizzes().FirstOrDefault(q => q.Id == quizId);

        if (patch is null)
        {
            return BadRequest(new {error = "Invalid patch document"});
        }

        var disabledOperation = patch.Operations.FirstOrDefault(op => op.OperationType == OperationType.Remove && op.path == "/Items");
        if (disabledOperation is not null)
        {
            return BadRequest(new { error = "Remove operation is not allowed" });
        }
        patch.ApplyTo(quiz);
        var patchedItem = quiz.Items[^1];
        var result = validator.Validate(patchedItem);
        if(!result.IsValid)
        {
            return BadRequest(result.Errors);
        }
        if(quizId != quiz.Id)
        {
            return BadRequest(new {error = "Id mismatch!"});
        }
        _service.UpdateQuiz(quiz);
        return Ok(quiz);
    }
}