
using Mixture.Core.Dto;
using Mixture.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mixture.Servise.Abstract
{
    public interface IPumpReadingService
    {
        public Task<PumpReading> AddPump(PumpReadingDto AddPumpDto);
        public Task<IReadOnlyCollection<PumpReading>>GetPump();


    }
}
