using EunDeParfum_Repository.DbContexts;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Implement
{
    public class ResultRepository : IResultRepository
    {
        private readonly ApplicationDbContext _context;
        public ResultRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateResultAsync(Result result)
        {
            try
            {
                _context.Results.AddAsync(result);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteResultAsync(int resultId)
        {
            try
            {
                var result = await _context.Results.FindAsync(resultId);
                if(result == null)
                {
                    return false;
                }
                _context.Results.Remove(result);
                return await _context.SaveChangesAsync()>0;
            }
            catch (Exception ex)
            {
                return false ;
            }
        }

        public async Task<List<Result>> GetAllResultAsync()
        {
            try
            {
                return await _context.Results.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Result> GetResultByIdAsync(int resultId)
        {
            try
            {
                return _context.Results.FirstOrDefaultAsync(r => r.Id == resultId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateResultAsync(Result result)
        {
            try
            {
                _context.Results.Update(result);
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
