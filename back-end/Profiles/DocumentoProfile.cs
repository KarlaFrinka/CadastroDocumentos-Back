using AutoMapper;
using QualyTeamTest.Data.Dto.DocumentoDto;
using QualyTeamTest.Models;

namespace QualyTeamTest.Profiles
{
    public class DocumentoProfile : Profile
    {
        public DocumentoProfile() 
        {
            CreateMap<CreateDocumentoDto, Documento>();
            CreateMap<UpdateDocumentoDto, Documento>();
            CreateMap<Documento, ReadDocumentoDto>();
        }
    }
}
