using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ondeTem.Data.Context;
using ondeTem.Domain.Core;
using ondeTem.Domain.Interfaces;
using ondeTem.Domain.ProdutoRoot;

namespace ondeTem.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {        
        private readonly OndeTemContext Context;

        public ProdutoRepository(OndeTemContext context)
        {
            this.Context = context;
        }

        public async Task<string> AddAsync(Produto obj)
        {
            obj.DataCadastro = DateTime.Now;

            if(obj.ImageHash != null && obj.ImageHash.Equals(""))
            {
                obj.CaminhoImage = Guid.NewGuid().ToString();
                Util.InsertImage(obj.CaminhoImage, obj.ImageHash);
            }

            Context.Entry(obj).State = EntityState.Added;

            await SaveChangesAsync();

            return "success";
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task<List<Produto>> GetAllAsync(long estabelecimentoId)
        { throw new System.NotImplementedException(); }

        public async Task<List<Produto>> GetAllAsync()
        {
            return await Context.Produtos
                            .AsNoTracking()
                            .OrderByDescending(i => i.DataCadastro)
                            .Include(i => i.Estabelecimento)
                            .ToListAsync();
        }

        public async Task<List<Produto>> GetAllByEstabelecimentoAsync(long id)
        {
            return await Context.Produtos
                            .AsNoTracking()
                            .OrderByDescending(i => i.DataCadastro)
                            .Where(i => i.EstabelecimentoId == id)
                            .ToListAsync();
        }

        public async Task<Produto> GetByIdAsync(long id, long estabelecimentoId)
        {
            return await Context.Produtos
                    .SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Produto>> GetDestaques()
        {
            return await Context.Produtos
                            .OrderByDescending(i => i.Acessos)
                            .Take(3)
                            .ToListAsync();
        }

        public async Task<string> RemoveAsync(long id, long estabelecimentoId)
        {
            var obj = await Context.Produtos
                                    .SingleOrDefaultAsync(i => i.Id == id);

            Util.RemoveImage(obj.CaminhoImage);

            Context.Produtos.Remove(obj);
            await SaveChangesAsync();

            return "success";
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public async Task<string> UpdateAsync(Produto obj, long estabelecimentoId)
        {
            var produto = await Context.Produtos
                            .SingleOrDefaultAsync(i => i.Id == obj.Id);

            if(obj.ImageHash == null)
            {
                obj.ImageHash = produto.ImageHash;
            }
            else
            {
                if(produto.CaminhoImage != null)
                    Util.RemoveImage(produto.CaminhoImage);
                else
                    obj.CaminhoImage = Guid.NewGuid().ToString();
                
                Util.InsertImage(obj.CaminhoImage, obj.ImageHash);
            }
            
            Context.Entry(produto).State = EntityState.Detached;
            Context.Entry(obj).State = EntityState.Modified;
            await SaveChangesAsync();

            return "success";
        }
    }
}