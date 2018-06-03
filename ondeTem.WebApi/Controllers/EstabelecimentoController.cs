using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bitti.tokenProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ondeTem.Domain.EstabelecimentoRoot;
using ondeTem.Domain.Interfaces;
using ondeTem.WebApi.ViewModel;

namespace ondeTem.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class EstabelecimentoController : Controller
    {
        private readonly IEstabelecimentoRepository _estabelecimentoRepository;
        private readonly IMapper _mapper;

        public EstabelecimentoController(IEstabelecimentoRepository estabelecimentoRepository, IMapper mapper)
        {
            _estabelecimentoRepository = estabelecimentoRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(int userId)
        {
            var item = await _estabelecimentoRepository.GetAllAsync();
            var itemVM = _mapper.Map<List<EstabelecimentoViewModel>>(item);

            return Ok(new {
                status = HttpContext.Response.StatusCode,
                data = itemVM,
                token = TokenGenerator.ReBuildToken(Request.Headers["Authorization"])
            });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var token = Request.Headers["Authorization"];
            var userId = TokenGenerator.GetIdProfissional(token);
            var item = await _estabelecimentoRepository.GetByIdAsync(id);
            var itemVM = _mapper.Map<EstabelecimentoViewModel>(item);

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

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]EstabelecimentoUser item)
        {
            if (item == null)
                return BadRequest(new {
                    status = 400,
                    message = "Objeto inválido."
                });

            if(ModelState.IsValid)
            {
                var response = await _estabelecimentoRepository.AddAsync(MapperEstabelecimento(item));

                if(response.Equals("success"))
                    return Ok(new {
                        status = HttpContext.Response.StatusCode,
                        message = "Cadastrado com sucesso.",
                        data = item
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
        public async Task<IActionResult> PutAsync(int id, [FromBody]Estabelecimento item)
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
                var response = await _estabelecimentoRepository.UpdateAsync(item);

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
            var item = await _estabelecimentoRepository.GetByIdAsync(id);
            var response = "";

            if(item != null)
            {
                response = await _estabelecimentoRepository.RemoveAsync(id);

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

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody]EstabelecimentoUser item)
        {
            if (item == null)
                return BadRequest(new {
                    status = 400,
                    message = "Objeto inválido."
                });

            if(ModelState.IsValid)
            {
                var user = await _estabelecimentoRepository.AuthenticateAsync(item);

                if (user == null)
                    return BadRequest(new {
                        status = 400,
                        message = "Usuário e/ou senha incorreto(s)."
                    });
                    
                return Ok(new {
                        status = HttpContext.Response.StatusCode,
                        message = "Autenticado com sucesso.",
                        data = user.Id,
                        token = TokenGenerator.BuildToken(user.Id, user.IsAdmin.Value)
                    });

            }
            else return BadRequest(new {
                status = 400,
                message = ModelState.Values.SelectMany(m => m.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList()
            });
        }

        private Estabelecimento MapperEstabelecimento(EstabelecimentoUser obj)
        {
            var estabelecimento = new Estabelecimento();

            estabelecimento.Email = obj.Email;
            estabelecimento.Password = obj.Password;

            return estabelecimento;
        }
    }
}