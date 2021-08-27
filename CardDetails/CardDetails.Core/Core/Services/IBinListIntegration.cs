using CardDetails.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardDetails.Core.Core.Services
{
    public interface IBinListIntegration
    {
        Task<CardDetail> GetCardDetail(CancellationToken cancellationToken, int BIN);
    }
}
