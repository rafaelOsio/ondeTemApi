using System.Linq;
using System.Threading.Tasks;
using bitti.tokenProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ondeTem.Domain.Interfaces;
using ondeTem.Domain.ProdutoRoot;

namespace ondeTem.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [Authorize]
        [HttpGet("GetAllByEstabelecimento/{id}")]
        public async Task<IActionResult> GetAllByEstabelecimentoAsync(int id)
        {
            var token = Request.Headers["Authorization"];
            var userId = TokenGenerator.GetIdProfissional(token);
            var item = await _produtoRepository.GetAllByEstabelecimentoAsync(id);

            return Ok(new {
                status = HttpContext.Response.StatusCode,
                data = item,
                token = TokenGenerator.ReBuildToken(token)
            });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var token = Request.Headers["Authorization"];
            var userId = TokenGenerator.GetIdProfissional(token);
            var item = await _produtoRepository.GetByIdAsync(id);

            if(item == null)
                return BadRequest(new {
                    status = 400,
                    message = "Id inválido"
                });

            return Ok(new {
                status = HttpContext.Response.StatusCode,
                data = item,
                token = TokenGenerator.ReBuildToken(token)
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]Produto item)
        {
            if (item == null)
                return BadRequest(new {
                    status = 400,
                    message = "Objeto inválido."
                });

            if(ModelState.IsValid)
            {
                var response = await _produtoRepository.AddAsync(item);

                if(response.Equals("success"))
                    return Ok(new {
                        status = HttpContext.Response.StatusCode,
                        message = "Cadastrado com sucesso.",
                        data = item,
                        token = TokenGenerator.ReBuildToken(Request.Headers["Authorization"])
                    });

                return BadRequest(new {
                    status = 400,
                    message = response
                });
            }
            else return BadRequest(new {
                status = 400,
                message = ModelState.Values.SelectMany(m => m.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList()
            });
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody]Produto item)
        {
            if (item.Id != id)
                return BadRequest(new {
                    status = 400,
                    message = "Id Inválido."
                });

            if (item == null)
                return BadRequest(new {
                    status = 400,
                    message = "Objeto Inválido."
                });
            
            if (ModelState.IsValid)
            {
                var token = Request.Headers["Authorization"];
                var userId = TokenGenerator.GetIdProfissional(token);
                var response = await _produtoRepository.UpdateAsync(item);

                if(response.Equals("success"))
                    return Ok(new {
                        status = HttpContext.Response.StatusCode,
                        message = "Atualizado com sucesso.",
                        data = item,
                        token = TokenGenerator.ReBuildToken(token)
                    });

                return BadRequest(new {
                    status = 400,
                    message = response
                });
            }
            else return BadRequest(new {
                status = 400,
                message = ModelState.Values.SelectMany(m => m.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList()
            });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var token = Request.Headers["Authorization"];
            var userId = TokenGenerator.GetIdProfissional(token);
            var item = await _produtoRepository.GetByIdAsync(id);
            var response = "";

            if(item != null)
            {
                response = await _produtoRepository.RemoveAsync(id);

                if(response.Equals("success"))
                    return Ok(new {
                            status = HttpContext.Response.StatusCode,
                            message = "Deletado com sucesso",
                            token = TokenGenerator.ReBuildToken(token)
                        });

                return BadRequest(new {
                    status = 400,
                    message = response
                });
            }
            else
            {
                return BadRequest(new {
                    status = 400,
                    message = "Id inválido"
                });
            }            
        }
    }
}