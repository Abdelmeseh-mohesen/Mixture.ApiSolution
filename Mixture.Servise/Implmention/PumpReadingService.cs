using AutoMapper;
using Mixture.Core.Dto;
using Mixture.Core.Entity;
using Mixture.Core.Repositery;
using Mixture.Servise.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mixture.Servise.Implmention
{
    public class PumpReadingService : IPumpReadingService
    {
        private readonly IGenericRepository<PumpReading> PumbRepository;
        private readonly IMapper mpper;

        public PumpReadingService(IGenericRepository<PumpReading> PumbRepository,IMapper mpper)
        {
            this.PumbRepository = PumbRepository;
            this.mpper = mpper;
        }
        public async Task<PumpReading> AddPump(PumpReadingDto AddPumpDto)
        {
            var PumpMapper = mpper.Map<PumpReading>(AddPumpDto);
            await PumbRepository.AddAsync(PumpMapper);
            return PumpMapper;
        }

        public async Task<IReadOnlyCollection<PumpReading>> GetPump()
        {
            return await PumbRepository.GetAllAsync();
        }
    }
}
