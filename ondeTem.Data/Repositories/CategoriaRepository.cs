using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ondeTem.Data.Context;
using ondeTem.Domain.CategoriaRoot;
using ondeTem.Domain.Interfaces;

namespace ondeTem.Data.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly OndeTemContext Context;
        public CategoriaRepository(OndeTemContext context)
        {
            this.Context = context;
        }
        
        public async Task<string> AddAsync(Categoria obj)
        {
            obj.DataHoraCadastro = DateTime.Now;
            Context.Entry(obj).State = EntityState.Added;

            await SaveChangesAsync();

            return "success";
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<List<Categoria>> GetAllAsync()
        {
            return await Context.Categorias
                    .OrderByDescending(i => i.DataHoraCadastro)
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<Categoria> GetByIdAsync(long id)
        {
            return await Context.Categorias
                    .SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task<string> RemoveAsync(long id)
        {
            var obj = await Context.Categorias
                                    .Include(i => i.Produtos)
                                    .SingleOrDefaultAsync(i => i.Id == id);

            if(obj.Produtos.Count > 0)
                return "Não é possível remover uma categoria que possua produtos cadastrados.";

            Context.Categorias.Remove(obj);
            await SaveChangesAsync();

            return "success";
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public async Task<string> UpdateAsync(Categoria obj)
        {
            Context.Entry(obj).State = EntityState.Modified;
            await SaveChangesAsync();

            return "success";
        }
    }
}