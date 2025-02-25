using EunDeParfum_Repository.DbContexts;
using EunDeParfum_Repository.Repository.Implement;
using EunDeParfum_Repository.Repository.Interface;
using EunDeParfum_Service.Service.Implement;
using EunDeParfum_Service.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace EXE201_EunDeParfum.AppStarts
{
    public static class DependencyInjectionContainers
    {
        public static void ServiceContainer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            //Add_DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //AddService
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IAnswersService, AnswersService>();
            services.AddScoped<IUserAnswersService, UserAnswersService>();
            services.AddScoped<IQuestionService, QuestionsService>();
            services.AddScoped<IResultService, ResultsService>();
            //AddRepository
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IUserAnswerRepository, UserAnswerRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IResultRepository, ResultRepository>();
        }
    }
}
