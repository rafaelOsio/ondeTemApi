using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ondeTem.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class FileUploadController : Controller
    {
        private string path = Environment.CurrentDirectory + "/images/";

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFilesAsync(List<IFormFile> files)
        {
            List<string> filesName = new List<string>();

            foreach(var item in files)
            {
                Guid g = Guid.NewGuid();
                var extensao = Path.GetExtension(item.FileName);
                var fileName = g + extensao;
                
                if(!System.IO.File.Exists(this.path + fileName))
                    using (var stream = new FileStream(this.path + fileName, FileMode.Create))
                        await item.CopyToAsync(stream);
                else
                    return BadRequest(new {
                        status = 400,
                        message = "Algo de errado aconteceu. Tente novamente.",
                    });
                    

                filesName.Add(fileName);
            }

            return Ok(new {
                status = HttpContext.Response.StatusCode,
                message = "Upload concluído",
                filesName = filesName
            });
        }

        [Authorize]
        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> DownloadFilesAsync(string fileName)
        {
            if(!System.IO.File.Exists(this.path + fileName))
                return BadRequest(new {
                            status = 400,
                            message = "Este arquivo não existe.",
                        });

            var memory = new MemoryStream();
            using (var stream = new FileStream(this.path + fileName, FileMode.Open))            
                await stream.CopyToAsync(memory);
            
            memory.Position = 0;
            return File(memory, GetContentType(this.path + fileName), Path.GetFileName(this.path + fileName));
        }

        [Authorize]
        [HttpDelete("{fileName}")]
        public IActionResult PostAsync(string fileName)
        {
            if(System.IO.File.Exists(this.path + fileName))
                System.IO.File.Delete(this.path + fileName);
            else
                return BadRequest(new {
                        status = 400,
                        message = "Este arquivo não existe.",
                    });

            return Ok(new {
                status = HttpContext.Response.StatusCode,
                message = "Deletado com sucesso."
            });
        }
    }
}