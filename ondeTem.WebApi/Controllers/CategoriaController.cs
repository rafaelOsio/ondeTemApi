using System.Linq;
using System.Threading.Tasks;
using bitti.tokenProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ondeTem.Domain.CategoriaRoot;
using ondeTem.Domain.Interfaces;

namespace ondeTem.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var item = await _categoriaRepository.GetAllAsync();

            return Ok(new {
                status = HttpContext.Response.StatusCode,
                data = item,
                token = TokenGenerator.ReBuildToken(Request.Headers["Authorization"])
            });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var item = await _categoriaRepository.GetByIdAsync(id);

            if(item == null)
                return BadRequest(new {
                    status = 400,
                    message = "Id inválido"
                });

            return Ok(new {
                status = HttpContext.Response.StatusCode,
                data = item,
                token = TokenGenerator.ReBuildToken(Request.Headers["Authorization"])
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]Categoria item)
        {
            if (item == null)
                return BadRequest(new {
                    status = 400,
                    message = "Objeto inválido."
                });

            if(ModelState.IsValid)
            {
                var response = await _categoriaRepository.AddAsync(item);

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
        public async Task<IActionResult> PutAsync(int id, [FromBody]Categoria item)
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
                var response = await _categoriaRepository.UpdateAsync(item);

                if(response.Equals("success"))
                    return Ok(new {
                        status = HttpContext.Response.StatusCode,
                        message = "Atualizado com sucesso.",
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var item = await _categoriaRepository.GetByIdAsync(id);
            var response = "";

            if(item != null)
            {
                response = await _categoriaRepository.RemoveAsync(id);

                if(response.Equals("success"))
                    return Ok(new {
                            status = HttpContext.Response.StatusCode,
                            message = "Deletado com sucesso",
                            token = TokenGenerator.ReBuildToken(Request.Headers["Authorization"])
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