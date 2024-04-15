using ApplicationCore.Interfaces.UserService;
using ApplicationCore.Models.QuizAggregate;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/v1/user/quizzes")]
public class ApiQuizUserController : ControllerBase
{
    private readonly IQuizUserService _service;
    private readonly IMapper _mapper;

    public ApiQuizUserController(IQuizUserService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [Route("{id}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<QuizDto> GetQuiz(int id)
    {
        var quiz = _service.FindQuizById(id);
        return quiz == null ? NotFound() : Ok(_mapper.Map<QuizDto>(quiz));
    }

    [Route("{quizId}/items/{itemId}/answers/{userId}")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<object> SaveAnswer(
        int quizId,
        int itemId,
        int userId,
        QuizItemUserAnswerDto dto,
        LinkGenerator linkGenerator
    )
    {
        _service.SaveUserAnswerForQuiz(quizId, userId, itemId, dto.Answer ?? "");
        return Created(
            linkGenerator.GetUriByAction(HttpContext, nameof(GetQuizFeedback), null,
                new { quizId = quizId, userId = 1 }),
            new
            {
                answer = dto.Answer,
            });
    }

    [Route("{quizId}/answers/{userId}")]
    [HttpGet]
    public ActionResult<object> GetQuizFeedback(int quizId, int userId)
    {
        var feedback = _service.GetUserAnswersForQuiz(quizId, userId);
        return new
        {
            quizId = quizId,
            userId = userId,
            totalQuestions = _service.FindQuizById(quizId)?.Items.Count??0,
            answers = feedback.Select(a =>
                new
                {
                    question = a.QuizItem.Question,
                    answer = a.Answer,
                    isCorrect = a.IsCorrect()
                }
            ).AsEnumerable()
        };
    }
}