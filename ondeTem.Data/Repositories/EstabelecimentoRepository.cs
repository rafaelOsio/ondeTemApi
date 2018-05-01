using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ondeTem.Data.Context;
using ondeTem.Domain.Core;
using ondeTem.Domain.EstabelecimentoRoot;
using ondeTem.Domain.Interfaces;

namespace ondeTem.Data.Repositories
{
    public class EstabelecimentoRepository : IEstabelecimentoRepository
    {
        private readonly OndeTemContext Context;
        public EstabelecimentoRepository(OndeTemContext context)
        {
            this.Context = context;
        }

        public async Task<string> AddAsync(Estabelecimento obj)
        {
            var user = await Context.Estabelecimentos
                                .SingleOrDefaultAsync(i => i.Email.Equals(obj.Email));

            if(user != null)
                return "Este email já está sendo usado.";

            byte[] passwordHash;

            CreatePasswordHash(obj.Password, out passwordHash);

            obj.PasswordHash = passwordHash;
            obj.DataCadastro = DateTime.Now;
            
            Context.Entry(obj).State = EntityState.Added;
            await SaveChangesAsync();

            return "success";
        }

        public async Task<Estabelecimento> AuthenticateAsync(EstabelecimentoUser item)
        {
            if (string.IsNullOrEmpty(item.Email) || string.IsNullOrEmpty(item.Password))
                return null;

            var user = await Context.Estabelecimentos
                                .SingleOrDefaultAsync(x => x.Email == item.Email);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(item.Password, user.PasswordHash))
                return null;

            return user;
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<List<Estabelecimento>> GetAllAsync()
        { 
            return await Context.Estabelecimentos
                    .AsNoTracking()
                    .OrderByDescending(i => i.DataCadastro)
                    .ToListAsync();
        }

        public async Task<Estabelecimento> GetByIdAsync(long id)
        {
            return await Context.Estabelecimentos
                            .SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task<string> RemoveAsync(long id)
        {
            var obj = await Context.Estabelecimentos
                                    .Include(i => i.Produtos)
                                    .SingleOrDefaultAsync(i => i.Id == id);

            if(obj.Produtos.Count > 0)
                return "Não é possível remover um estabelecimento que possua produtos cadastrados.";

            Util.RemoveImage(obj.CaminhoImage);

            Context.Estabelecimentos.Remove(obj);
            await SaveChangesAsync();

            return "success";
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public async Task<string> UpdateAsync(Estabelecimento obj)
        {

            var estabelecimentos = await Context.Estabelecimentos
                                .Where(i => i.Email == obj.Email && i.Id != obj.Id)
                                .ToListAsync();

            if(estabelecimentos.Count > 0)
                return "Este email já está sendo utilizado.";

            var estabelecimentoAntigo = await Context.Estabelecimentos
                                                .SingleOrDefaultAsync(i => i.Id == obj.Id);

            // Se o obj que veio do front tiver uma 'imageHash'
            //      ou não for igual a que já existe, o usuário quis alterar a foto
            //      então isso será feito
            if(obj.ImageHash != null || obj.ImageHash != estabelecimentoAntigo.ImageHash)
            {
                // Se o estabelecimento tinha uma imagem antes, esta será removida 
                //      para que possa inserir uma nova
                if(estabelecimentoAntigo.CaminhoImage != null)
                    Util.RemoveImage(estabelecimentoAntigo.CaminhoImage);
                else // se não, será criada uma imagem pela primeira vez
                    obj.CaminhoImage = Guid.NewGuid().ToString();
                
                Util.InsertImage(obj.CaminhoImage, obj.ImageHash);
            }
            else
                obj.ImageHash = estabelecimentoAntigo.ImageHash;

            // Se o usuário editar seu cadastro e não enviar uma senha, 
            //      significa que ele quer manter a mesma senha
            if(obj.Password != null)
            {
                byte[] passwordHash;
                CreatePasswordHash(obj.Password, out passwordHash);
                obj.PasswordHash = passwordHash;
            }
            else
                obj.PasswordHash = estabelecimentoAntigo.PasswordHash;
            
            

            Context.Entry(estabelecimentoAntigo).State = EntityState.Detached;
            Context.Entry(obj).State = EntityState.Modified;
            
            await SaveChangesAsync();

            return "success";
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash)
        {
            var sha256 = SHA256.Create();            
            passwordHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));            
        }
        
        private bool VerifyPasswordHash(string password, byte[] storedHash)
        {            
            var sha256 = SHA256.Create();
            byte[] passwordHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != storedHash[i])
                    return false;
            }

            return true;
        }
    }
}