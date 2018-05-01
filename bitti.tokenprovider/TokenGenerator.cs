using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace bitti.tokenProvider
{
    public class TokenGenerator
    {   

        private static JwtSecurityToken CreateToken(Claim[] claims, SigningCredentials creds)
        {
            var token = new JwtSecurityToken(
                "ondetem.com.br",
                "ondetem.com.br",
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds);

            return token;
        }

        public static string BuildToken(long id, bool isAdmin)
        {
            var claims = new[] {
                new Claim("id", Convert.ToString(id)),
                new Claim("isAdmin", Convert.ToString(isAdmin))
            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("f5349a80dd6937efa8ca39b61cc8c3aa"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityTokenHandler().WriteToken(CreateToken(claims, creds));
        }

        /// <summary>
        /// Rafael Osio: Recebe um token e devolve um novo
        /// </summary>
        /// <param name="token">token que foi recebido na requesição</param>
        /// <returns>String do novo token</returns>
        public static string ReBuildToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            token = token.Replace("Bearer", "");
            token = token.Trim();

            var tokenAntigo = handler.ReadToken(token) as JwtSecurityToken;

            var id = tokenAntigo.Claims.First(claim => claim.Type == "id").Value;
            var isAdmin = tokenAntigo.Claims.First(claim => claim.Type == "isAdmin").Value;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("f5349a80dd6937efa8ca39b61cc8c3aa"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim("id", Convert.ToString(id)),
                new Claim("isAdmin", Convert.ToString(isAdmin))
            };

            return new JwtSecurityTokenHandler().WriteToken(CreateToken(claims, creds));
        }

        /// <summary>
        /// Rafael Osio: transforma um token em um JwtSecurityToken. Com isso, será possível acessar o corpo do token e retirar as informações
        /// </summary>
        /// <param name="token">será recebido na requisição</param>
        /// <returns>JwtSecurityToken</returns>
        public static JwtSecurityToken DisassembleToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            token = token.Replace("Bearer", "");
            token = token.Trim();

            return handler.ReadToken(token) as JwtSecurityToken;
        }

        public static int GetIdProfissional (string token) 
        {
            var handler = new JwtSecurityTokenHandler();

            token = token.Replace("Bearer", "");
            token = token.Trim();

            var tokenAntigo = handler.ReadToken(token) as JwtSecurityToken;

            var id = tokenAntigo.Claims.First(claim => claim.Type == "id").Value;

            return Convert.ToInt32(id);
        }
    }
}