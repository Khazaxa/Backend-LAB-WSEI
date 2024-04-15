using ApplicationCore.Commons.Repository;
using ApplicationCore.Models.QuizAggregate;

namespace Web;
public static class SeedData
{
    public static void Seed(this WebApplication app)
    {
        IGenericRepository<Quiz, int>? quizRepo;
        using (var scope = app.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            quizRepo = provider.GetService<IGenericRepository<Quiz, int>>();
            var quizItemRepo = provider.GetService<IGenericRepository<QuizItem, int>>();
        }
        
        var quiz1Items = new List<QuizItem>
        {
            new QuizItem(1, "W jakim kraju jesteś?", new List<string>{"Rosja", "Francja", "Niemcy"}, "Polska"),
            new QuizItem(2, "Ile ksiąg ma trylogia?", new List<string>{"Jedną", "Dwie", "Cztery"}, "Trzy"),
            new QuizItem(3, "Jak miał na imię Tadeusz Kościuszko?", new List<string>{"Aleksander", "Tomasz", "Marcin"}, "Tadeusz")
        };
        var quiz2Items = new List<QuizItem>
        {
            new QuizItem(4, "Ile boków ma trójkąt?", new List<string>{"Jeden", "Dwa", "Pięć"}, "Trzy"),
            new QuizItem(5, "Ile kątów ma trójkąt?", new List<string>{"Sześć", "Cztery", "Siedem"}, "Trzy"),
            new QuizItem(6, "Ile boków ma kwadrat?", new List<string>{"Jeden", "Dwa", "Trzy"}, "Cztery")
        };
        var quiz1 = new Quiz(1, quiz1Items, "Quiz o Polsce");
        var quiz2 = new Quiz(2, quiz2Items, "Quiz Matematyczny");
        
        quizRepo.Add(quiz1);
        quizRepo.Add(quiz2);
    }
}