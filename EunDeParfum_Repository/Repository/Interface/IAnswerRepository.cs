using EunDeParfum_Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Interface
{
    public interface IAnswerRepository
    {
        Task<bool> CreateAnswerAsync(Answer answer);
        Task<bool> UpdateAnswerAsync(Answer answer);
        Task<bool> DeleteAnswerAsync(int answerId);
        Task<Answer> GetAnswerByIdAsync(int id);
        Task<List<Answer>> GetAllAnswersAsync();
    }
}
