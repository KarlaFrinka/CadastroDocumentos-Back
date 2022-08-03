using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QualyTeamTest.Data;
using QualyTeamTest.Data.Dto.DocumentoDto;
using QualyTeamTest.Models;

namespace QualyTeamTest.Controller
{
    [EnableCors("MyAllowSpecificOrigins")]
    [ApiController]
    [Route("[controller]")]
    public class DocumentoController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public DocumentoController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }


        [HttpPost]
        [Produces("application/json")]

        public ActionResult<ReadDocumentoDto> CadastrarDocumento(CreateDocumentoDto createDocumentoDto)
        {
            if (ChecarCodigo(createDocumentoDto.Codigo))
            {
                var documentoConvertido = _mapper.Map<Documento>(createDocumentoDto);
                _context.Documento.Add(documentoConvertido);

                if (_context.SaveChanges() > 0)
                {
                    var documentoReadConvertido = _mapper.Map<ReadDocumentoDto>(documentoConvertido);
                    return Ok(documentoReadConvertido);
                }
                return BadRequest();
            }
            return BadRequest("Código já cadastrado");
        }
           
       
        [HttpGet]
       
        public ActionResult<List<ReadDocumentoDto>> ConsultarDocumentoGeral()
        {
            var documentoGeral = from documento in _context.Documento
                                 join processo in _context.Processo on documento.IdProcesso equals processo.Id 
                                 select new ReadDocumentoDto()
                                 {
                                     Id = documento.Id,
                                     Codigo = documento.Codigo,
                                     Titulo = documento.Titulo,
                                     Processo = processo,
                                     Categoria = documento.Categoria
                                     
                                 };
            
            if (documentoGeral.ToList().Any()) 
            {
                return Ok(documentoGeral.OrderBy(o => o.Titulo));
            }
            return BadRequest();
        }


        [HttpGet("{id}")]

        public ActionResult<ReadDocumentoDto> ConsultarDocumentoPorId(int id)
        {
            var documentoGeral = from documento in _context.Documento
                                 join processo in _context.Processo on documento.IdProcesso equals processo.Id
                                 where documento.Id == id
                                 select new ReadDocumentoDto()
                                 {
                                     Id = documento.Id,
                                     Codigo = documento.Codigo,
                                     Titulo = documento.Titulo,
                                     Processo = processo,
                                     Categoria = documento.Categoria

                                 };

            if (documentoGeral.ToList().Any())
            {
                return Ok(documentoGeral.OrderBy(o => o.Titulo));
            }
            return BadRequest();
        }

      
        [HttpPut]
       
        public ActionResult<ReadDocumentoDto> EditarDocumento(UpdateDocumentoDto documentoDto)
        {
            var documentoEditar = _mapper.Map<Documento>(documentoDto);
            _context.Update(documentoEditar);
            if(_context.SaveChanges() > 0) 
            {
                var documentoRetorno = _mapper.Map<ReadDocumentoDto>(documentoEditar);
                return Ok(documentoRetorno);
            }
            return BadRequest();

        }
       

        [HttpDelete("{id}")]
       
        public IActionResult DeletarDocumento(int id)
        {
            var documentoDeletar = _context.Documento.FirstOrDefault(d => d.Id == id);
            _context.Remove(documentoDeletar);
            if(_context.SaveChanges() > 0) 
            {
                return NoContent();
            }
            return BadRequest();
        }

        private bool ChecarCodigo (int c)
        {
            var consultaCod = _context.Documento.FirstOrDefault(d => d.Codigo == c);
            if (consultaCod != null) return false;
            return true;
        }

    }
}