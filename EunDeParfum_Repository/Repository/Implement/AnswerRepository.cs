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
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ApplicationDbContext _context;
        public AnswerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateAnswerAsync(Answer answer)
        {
            try
            {
                _context.Answers.AddAsync(answer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteAnswerAsync(int answerId)
        {
            try
            {
                var answer = await _context.Answers.FindAsync(answerId);
                if(answer == null)
                {
                    return false;
                }
                _context.Answers.Remove(answer);
                return await _context.SaveChangesAsync()>0;
            }
            catch (Exception ex)
            {
                return false ;
            }
        }

        public async Task<List<Answer>> GetAllAnswersAsync()
        {
            try
            {
                return await _context.Answers.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Answer> GetAnswerByIdAsync(int id)
        {
            try
            {
                return _context.Answers.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateAnswerAsync(Answer answer)
        {
            try
            {
                _context.Answers.Update(answer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
