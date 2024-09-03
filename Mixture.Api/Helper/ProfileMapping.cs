using AutoMapper;
using Mixture.Core.Dto;
using Mixture.Core.Entity;

namespace Mixture.Api.Helper
{
    public class ProfileMapping:Profile
    {
        public ProfileMapping()
        {
            CreateMap<PumpReadingDto, PumpReading>().ForMember(dst => dst.Id,opt=>opt.Ignore());
            
        }
    }
}
