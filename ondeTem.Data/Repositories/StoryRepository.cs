using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ondeTem.Data.Context;
using ondeTem.Domain.Interfaces;
using ondeTem.Domain.StoryRoot;

namespace ondeTem.Data.Repositories
{
    public class StoryRepository : IStoryRepository
    {
        private readonly OndeTemContext Context;
        public StoryRepository(OndeTemContext context)
        {
            this.Context = context;
        }
        
        public async Task<string> AddAsync(Story obj)
        {
            obj.DataFinalPostagem = DateTime.Now.AddDays(1);
            Context.Entry(obj).State = EntityState.Added;

            await SaveChangesAsync();

            return "success";
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<List<Story>> GetAllAsync()
        {
            return await Context.Stories
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<Story> GetByIdAsync(long id)
        {
            return await Context.Stories
                    .SingleOrDefaultAsync(i => i.Id == id);
        }

        public Task<string> RemoveAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> UpdateAsync(Story obj)
        {
            throw new System.NotImplementedException();
        }
    }
}