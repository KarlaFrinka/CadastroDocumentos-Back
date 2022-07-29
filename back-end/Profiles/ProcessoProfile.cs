using AutoMapper;
using QualyTeamTest.Data.Dto.ProcessoDto;
using QualyTeamTest.Models;

namespace QualyTeamTest.Profiles
{
    public class ProcessoProfile : Profile
    {
        public ProcessoProfile()
        {
            CreateMap<CreateProcessoDto, Processo>();
            CreateMap<UpdateProcessoDto, Processo>();
            CreateMap<Processo, ReadProcessoDto>();
        }
    }
}
