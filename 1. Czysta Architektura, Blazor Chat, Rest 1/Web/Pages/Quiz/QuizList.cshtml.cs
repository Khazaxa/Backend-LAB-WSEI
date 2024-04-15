using ApplicationCore.Commons.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Quiz
{
    public class QuizList : PageModel
    {
        private readonly IGenericRepository<ApplicationCore.Models.QuizAggregate.Quiz, int> _quizRepo;

        public QuizList(IGenericRepository<ApplicationCore.Models.QuizAggregate.Quiz, int> quizRepo)
        {
            _quizRepo = quizRepo;
        }

        public List<ApplicationCore.Models.QuizAggregate.Quiz> Quizzes { get; set; }

        public async Task OnGetAsync()
        {
            Quizzes = await _quizRepo.FindAllAsync();
        }
    }
}