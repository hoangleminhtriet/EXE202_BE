using EunDeParfum_Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Interface
{
    public interface IResultRepository
    {
        Task<bool> CreateResultAsync(Result result);
        Task<bool> UpdateResultAsync(Result result);
        Task<bool> DeleteResultAsync(int resultId);
        Task<Result> GetResultByIdAsync(int resultId);
        Task<List<Result>> GetAllResultAsync();
    }
}
