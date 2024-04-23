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
    [Consumes("application/json-patch+json")]
    public ActionResult<Quiz> AddQuizItem(int quizId, JsonPatchDocument<Quiz>? patchDoc)
    {
        var quiz = _service.FindAllQuizzes().FirstOrDefault(q => q.Id == quizId);
        if (quiz is null || patchDoc is null)
        {
            return NotFound(new
            {
                error = $"Quiz width id {quizId} not found"
            });
        }
        int previousCount = quiz.Items.Count;
        patchDoc.ApplyTo(quiz, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (previousCount < quiz.Items.Count)
        {
            QuizItem item = quiz.Items[^1];
            quiz.Items.RemoveAt(quiz.Items.Count - 1);
            _service.AddQuizItemToQuiz(quizId, item);
        }
        return Ok(_service.FindAllQuizzes().FirstOrDefault(q => q.Id == quizId));
    }
}