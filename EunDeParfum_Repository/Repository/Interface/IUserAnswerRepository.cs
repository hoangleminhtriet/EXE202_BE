using EunDeParfum_Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Interface
{
    public interface IUserAnswerRepository
    {
        Task<bool> CreateUserAnswerAsync(UserAnswer userAnswer);
        Task<bool> UpdateUserAnswerAsync(UserAnswer userAnswer);
        Task<bool> DeleteUserAnswerAsync(int userAnswerId);
        Task<UserAnswer> GetUserAnswerByIdAsync(int id);
        Task<List<UserAnswer>> GetAllUserAnswersAsync();
    }
}
