using AutoMapper;
using CardDetails.Core.Core.Application.Commands;
using CardDetails.Core.Core.Application.Queries;
using CardDetails.Core.Core.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardDetails.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardDetailsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IBinListIntegration _integratingWithBin;
        private readonly CancellationTokenSource _cancellationTokenSource =
            new CancellationTokenSource();
        private readonly IMapper _mapper;

        public CardDetailsController(IBinListIntegration integratingWithBin, IMediator mediator, IMapper mapper)
        {
            _integratingWithBin = integratingWithBin;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCard([FromQuery] GetCardDetailsQuery BinObject)
         {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CardDetail = await _mediator.Send(BinObject);
            if (CardDetail == null) return BadRequest("Bin is invalid");
            return Ok(CardDetail);
        }
    }
}
