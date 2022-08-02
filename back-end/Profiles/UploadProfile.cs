using AutoMapper;
using back_end.Data.Dto.UploadDto;
using back_end.Models;

namespace back_end.Profiles
{
    public class UploadProfile : Profile
    {
        public UploadProfile()
        {
            CreateMap<CreateUploadDto, Upload>();
            CreateMap<UpdateUploadDto, Upload>();
            CreateMap<Upload, ReadUploadDto>();
        }
    }
}
