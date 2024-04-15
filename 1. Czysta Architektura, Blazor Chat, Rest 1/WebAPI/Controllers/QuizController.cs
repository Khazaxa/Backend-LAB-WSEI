using ApplicationCore.Interfaces.UserService;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/quizzes")]
public class QuizController : ControllerBase
{ 
    private readonly IQuizUserService _service;

    public QuizController(IQuizUserService service)
    {
        _service = service;
    }
    
    [HttpGet]
    [Route("{id}")]
    public ActionResult<QuizDto> FindById(int id)
    {
        var quiz = _service.FindQuizById(id);
        if (quiz == null)
        {
            return NotFound();
        }

        var quizDto = QuizDto.of(quiz);
        return Ok(quizDto);
    }
    
    [HttpGet]
    public IEnumerable<QuizDto> FindAll()
    {
        var quizzes = _service.FindAllQuizzes();
        return quizzes.Select(QuizDto.of);
    }
    
    [HttpPost]
    [Route("{quizId}/items/{itemId}")]
    public void SaveAnswer([FromBody] QuizItemAnswerDto dto, int quizId, int itemId)
    {
        _service.SaveUserAnswerForQuiz(quizId, dto.UserId, itemId, dto.Answer);
    }
    
    [HttpGet]
    [Route("{quizId}/user/{userId}/result")]
    public ActionResult<QuizResultDto> GetQuizResult(int quizId, int userId)
    {
        var correctAnswersCount = _service.CountCorrectAnswersForQuizFilledByUser(quizId, userId);
        var result = new QuizResultDto { CorrectAnswersCount = correctAnswersCount };
        return Ok(result);
    }
}
