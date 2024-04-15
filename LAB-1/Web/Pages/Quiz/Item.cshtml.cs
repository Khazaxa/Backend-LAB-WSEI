using ApplicationCore.Interfaces.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Quiz
{
    
    public class QuizModel : PageModel
    {
        private readonly IQuizUserService _userService;

        private readonly ILogger _logger;
        public QuizModel(IQuizUserService userService, ILogger<QuizModel> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [BindProperty]
        public string Question { get; set; }
        [BindProperty]
        public List<string> Answers { get; set; }
        
        [BindProperty]
        public String UserAnswer { get; set; }
        
        [BindProperty]
        public int QuizId { get; set; }
        
        [BindProperty]
        public int ItemId { get; set; }
        
        public void OnGet(int quizId, int itemId)
        {
            QuizId = quizId;
            ItemId = itemId;
            var quiz = _userService.FindQuizById(quizId);
            if (itemId > 0 && itemId <= quiz?.Items.Count)
            {
                var quizItem = quiz?.Items[itemId - 1];
                Question = quizItem?.Question;
                Answers = new List<string>();
                if (quizItem is not null)
                {
                    Answers.AddRange(quizItem?.IncorrectAnswers);
                    Answers.Add(quizItem?.CorrectAnswer);
                }
            }
        }

        public IActionResult OnPost()
        {
            var quiz = _userService.FindQuizById(QuizId);
            var quizItem = quiz?.Items[ItemId - 1];
            if (quizItem?.CorrectAnswer == UserAnswer)
            {
                var correctAnswers = HttpContext.Session.GetInt32("CorrectAnswers") ?? 0;
                correctAnswers++;
                HttpContext.Session.SetInt32("CorrectAnswers", correctAnswers);
            }

            if (ItemId == quiz?.Items.Count)
            {
                var correctAnswers = HttpContext.Session.GetInt32("CorrectAnswers") ?? 0;
                HttpContext.Session.Remove("CorrectAnswers");
                return RedirectToPage("./Summary", new { quizId = QuizId, itemId = ItemId, correctAnswers = correctAnswers, totalQuestions = quiz?.Items.Count });
            }
            else
            {
                return RedirectToPage("Item", new {quizId = QuizId, itemId = ItemId + 1});
            }
        }
    }
}
