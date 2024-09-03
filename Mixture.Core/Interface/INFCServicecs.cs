using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mixture.Core.Entity;

namespace Mixture.Core.Interface
{
    public interface INFCService
    {
        Task<double?> ProcessNFCRequestAsync(NFCRequest nfcRequest);
    }
}
