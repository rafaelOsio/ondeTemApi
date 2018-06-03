using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bitti.tokenProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ondeTem.Domain.Interfaces;
using ondeTem.Domain.ProdutoRoot;
using ondeTem.WebApi.ViewModel;

namespace ondeTem.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int id)
        {
            var item = await _produtoRepository.GetAllAsync();
            var itemVM = _mapper.Map<List<ProdutoViewModel>>(item);

            return Ok(new {
                status = HttpContext.Response.StatusCode,
                data = itemVM,
            });
        }

        [HttpGet("GetAllByEstabelecimento/{id}")]
        public async Task<IActionResult> GetAllByEstabelecimentoAsync(int id)
        {
            var item = await _produtoRepository.GetAllByEstabelecimentoAsync(id);
            var itemVM = _mapper.Map<List<ProdutoViewModel>>(item);

            return Ok(new {
                status = HttpContext.Response.StatusCode,
                data = itemVM,
            });
        }

        [HttpGet("GetDestaques")]
        public async Task<IActionResult> GetDestaquesAsync()
        {
            var item = await _produtoRepository.GetDestaques();
            var itemVM = _mapper.Map<List<ProdutoViewModel>>(item);

            return Ok(new {
                status = HttpContext.Response.StatusCode,
                data = itemVM,
            });
        }

        [HttpGet("GetAdicionadosRecentemente")]
        public async Task<IActionResult> GetAdicionadosRecentemente()
        {
            var item = await _produtoRepository.GetAdicionadosRecentemente();
            var itemVM = _mapper.Map<List<ProdutoViewModel>>(item);

            return Ok(new {
                status = HttpContext.Response.StatusCode,
                data = itemVM,
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var token = Request.Headers["Authorization"];
            var userId = TokenGenerator.GetIdProfissional(token);
            var item = await _produtoRepository.GetByIdAsync(id, userId);
            var itemVM = _mapper.Map<ProdutoViewModel>(item);
            
            if(item == null)
                return BadRequest(new {
                    status = 400,
                    message = "Id inválido"
                });

            return Ok(new {
                status = HttpContext.Response.StatusCode,
                data = itemVM,
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
                var response = await _produtoRepository.UpdateAsync(item, userId);

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
            var item = await _produtoRepository.GetByIdAsync(id, userId);
            var response = "";

            if(item != null)
            {
                response = await _produtoRepository.RemoveAsync(id, userId);

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
