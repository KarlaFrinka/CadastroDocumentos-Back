using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QualyTeamTest.Data;
using QualyTeamTest.Data.Dto.ProcessoDto;
using QualyTeamTest.Models;

namespace QualyTeamTest.Controllers
{
    [EnableCors("MyAllowSpecificOrigins")]
    [ApiController]
    [Route("[controller]")]
    public class ProcessoController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ProcessoController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]

        public ActionResult<ReadProcessoDto> CadastrarProcesso(CreateProcessoDto createProcessoDto)
        {
            var processoConvertido = _mapper.Map<Processo>(createProcessoDto);
            _context.Processo.Add(processoConvertido);
            if(_context.SaveChanges()> 0);
            {
                var processoReadConvertido = _mapper.Map<ReadProcessoDto>(processoConvertido);
                return Ok(processoReadConvertido);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]

        public ActionResult<ReadProcessoDto> ConsultarProcesso(int id)
        {
            var processoConsulta = _context.Processo.FirstOrDefault(p => p.Id == id);
            if (processoConsulta != null)
            {
                var processoConsultaConvertido = _mapper.Map<ReadProcessoDto>(processoConsulta);
                return Ok(processoConsultaConvertido);
            }
            
            return BadRequest();
        }

        [HttpGet]

        public ActionResult<List<ReadProcessoDto>> ConsultarProcessoGeral()
        {
            var processoGeral = from processo in _context.Processo
                                select new ReadProcessoDto()
                                {
                                    Id = processo.Id,
                                    Descricao = processo.Descricao
                                };
            if (processoGeral.Any())
            {
                return Ok(processoGeral);
            }
            return BadRequest();
        }

        [HttpPut]

        public ActionResult<ReadProcessoDto> EditarProcesso (UpdateProcessoDto updateProcessoDto)
        {
            var processoEditar = _mapper.Map<Processo>(updateProcessoDto);
            _context.Update(processoEditar);
            if (_context.SaveChanges() > 0)
            {
                var processoRetorno = _mapper.Map<ReadProcessoDto>(processoEditar);
                return Ok(processoRetorno);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]

        public IActionResult DeletarProcesso (int id)
        {
            var processoDeletar = _context.Processo.FirstOrDefault(d => d.Id == id);
            _context.Remove(processoDeletar);

            if(_context.SaveChanges() > 0)
            {
                return NoContent();
            }
            return BadRequest();

        }
    }
}
