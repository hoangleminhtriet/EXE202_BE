using EunDeParfum_Repository.DbContexts;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Implement
{
    public class UserAnswerRepository : IUserAnswerRepository
    {
        private readonly ApplicationDbContext _context;
        public UserAnswerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateUserAnswerAsync(UserAnswer userAnswer)
        {
            try
            {
                _context.UserAnswers.AddAsync(userAnswer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteUserAnswerAsync(int userAnswerId)
        {
            try
            {
                var userAnswer = await _context.UserAnswers.FindAsync(userAnswerId);
                if (userAnswer == null)
                {
                    return false;
                }
                _context.UserAnswers.Remove(userAnswer);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<UserAnswer>> GetAllUserAnswersAsync()
        {
            return await _context.UserAnswers.ToListAsync();
        }

        public Task<UserAnswer> GetUserAnswerByIdAsync(int id)
        {
            try
            {
                return _context.UserAnswers.FirstOrDefaultAsync(u => u.Id == id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateUserAnswerAsync(UserAnswer userAnswer)
        {
            try
            {
                _context.UserAnswers.Update(userAnswer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
