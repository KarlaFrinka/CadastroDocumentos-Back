using AutoMapper;
using back_end.Data.Dto.UploadDto;
using back_end.Models;
using Microsoft.AspNetCore.Mvc;
using QualyTeamTest.Data;
using System.IO;

namespace back_end.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UploadController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("{id}")]

        public ActionResult<Upload> CadastrarUpload(int id,
                                                    string tipo,
                                                             [FromForm] ICollection<IFormFile> arquivo)
        {
            if (tipo == "pdf" || tipo == "doc" || tipo == "docx" || tipo == "xls" || tipo == "xlsx")
            {
                var upload = new Upload();
                foreach (var file in arquivo)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            var caminho = @$"C:\Arquivo\arquivo{id}.{tipo}";
                            System.IO.File.WriteAllBytes($"{caminho}", fileBytes);
                            upload.Arquivo = caminho;
                            upload.IdDocumento = id;

                        }
                    }
                }
                _context.Upload.Add(upload);
                if (_context.SaveChanges() > 0) ;
                {

                    return Ok(upload);
                }
            }
         
            return BadRequest();
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> UploadProcesso(int id)
        {
            var uploadConsulta = _context.Upload.FirstOrDefault(p => p.IdDocumento == id);
            if (uploadConsulta != null)
            {
                var caminho = uploadConsulta.Arquivo;
                var indexPonto = caminho.IndexOf(".");
                var extensao = caminho.Substring(indexPonto);

                var file = System.IO.File.ReadAllBytesAsync(@$"{uploadConsulta.Arquivo}");
                return File (await System.IO.File.ReadAllBytesAsync(@$"{uploadConsulta.Arquivo}"), "application/octet-stream", $"{id}{extensao}");
            }

            return BadRequest();
        }

        [HttpPut]

        public ActionResult<ReadUploadDto> EditarUpload(int id,
                                                    string tipo,
                                                             [FromForm] ICollection<IFormFile> arquivo)
        {
            
            var arquivoUpdate = _context.Upload.FirstOrDefault(a => a.IdDocumento == id);
            if (tipo == "pdf" || tipo == "doc" || tipo == "docx" || tipo == "xls" || tipo == "xlsx")
            {
                
                foreach (var file in arquivo)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            var caminho = @$"C:\Arquivo\arquivo{arquivoUpdate.IdDocumento}.{tipo}";
                            System.IO.File.WriteAllBytes($"{caminho}", fileBytes);
                            arquivoUpdate.Arquivo = caminho;
                            arquivoUpdate.IdDocumento = arquivoUpdate.IdDocumento;                      
                        }
                    }
                }
                _context.Upload.Update(arquivoUpdate);
            }
            if (_context.SaveChanges() > 0)
            {
                return Ok(arquivoUpdate);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]

        public IActionResult DeletarUpload(int id)
        {
            var uploadDeletar = _context.Upload.FirstOrDefault(d => d.IdDocumento == id);
            System.IO.File.Delete(uploadDeletar.Arquivo);
            _context.Remove(uploadDeletar);

            if (_context.SaveChanges() > 0)
            {
                return NoContent();
            }
            return BadRequest();

        }

        
    
}
}
