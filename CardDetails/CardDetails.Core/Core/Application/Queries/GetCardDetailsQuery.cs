using AutoMapper;
using CardDetails.Core.Core.Application.Commands;
using CardDetails.Core.Core.Services;
using CardDetails.Data;
using CardDetails.Data.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardDetails.Core.Core.Application.Queries
{
    public class GetCardDetailsQuery :IRequest<CardDetail>
    {
        public int Bin { get; set; }
    }

    public class GetCardDetailsQueryValidator: AbstractValidator<GetCardDetailsQuery>
    {
        public GetCardDetailsQueryValidator()
        {
            RuleFor(x=>x.Bin).InclusiveBetween(10000000,99999999).WithMessage("Bin has to be 8 digits long");
        }
    }

    public class CardDetailsQueryHandler : IRequestHandler<GetCardDetailsQuery, CardDetail>
    {
        private readonly AppDbContext _context;
        private readonly CancellationTokenSource _cancellationTokenSource =
            new CancellationTokenSource();
        private readonly IBinListIntegration _binListIntegration;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CardDetailsQueryHandler(AppDbContext context, IBinListIntegration binListIntegration,
            IMediator mediator, IMapper mapper)
        {
            _context = context;
            _binListIntegration = binListIntegration;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<CardDetail> Handle(GetCardDetailsQuery request, CancellationToken cancellationToken)
        {

            var cardDetail = await GetCardDetail(request.Bin);
            return cardDetail;
        }

        private async Task<CardDetail> GetCardDetail(int Bin)
        {
            // Check db if Bin exists else call method for external service
            var cardDetail = await _context.CardDetails.Include(c => c.Country)
                                                .Include(c => c.Bank)
                                                .Include(c => c.Number)
                                                .Where(c => c.Bin == Bin)
                                                .SingleOrDefaultAsync();
            if (cardDetail == null)
            {
                return await GetCardDetailsFromExternalService(Bin);
            }
            
            return cardDetail;
        }

        private async Task<CardDetail> GetCardDetailsFromExternalService(int Bin)
        {
            // Call external service and save information from external service to DB

            var cardDetail = await _binListIntegration.GetCardDetail(_cancellationTokenSource.Token, Bin);
            if (cardDetail == null) return null;
            cardDetail.Bin = Bin;
            var cardToAdd = _mapper.Map<AddCardDetailsCommand>(cardDetail);
            _=_mediator.Send(cardToAdd);
            
            return cardDetail;
        }  

    }
}
