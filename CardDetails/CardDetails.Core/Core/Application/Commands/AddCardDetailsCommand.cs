using AutoMapper;
using CardDetails.Data;
using CardDetails.Data.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardDetails.Core.Core.Application.Commands
{
    public class AddCardDetailsCommand : IRequest<bool>
    {
        public int Bin { get; set; }

        public Number Number { get; set; }

        public string Scheme { get; set; }

        public string Type { get; set; }

        public string Brand { get; set; }

        public bool Prepaid { get; set; }

        public Country Country { get; set; }

        public Bank Bank { get; set; }
    }

    public class CardDetailCommandHandler : IRequestHandler<AddCardDetailsCommand, bool>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CardDetailCommandHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Handle(AddCardDetailsCommand request, CancellationToken cancellationToken)
        {
            var cardDetailToAdd = _mapper.Map<CardDetail>(request);

            // confirm that card details is not in DB before adding?
            if (_context.CardDetails.Any(e => e.Bin == cardDetailToAdd.Bin))
            {
                return false;
            }

            //Find if bank and country exist so as not to create duplicates
            var BankExist = _context.Banks.FirstOrDefault(b => b.Name == cardDetailToAdd.Bank.Name);
            var CountryExist = _context.Countries.FirstOrDefault(c => c.Name == cardDetailToAdd.Country.Name);
            if (BankExist != null) cardDetailToAdd.Bank = BankExist;
            
            if (CountryExist != null) cardDetailToAdd.Country = CountryExist;
            
             _context.CardDetails.Add(cardDetailToAdd);

            return await SaveAsync();
        }

        private async Task<bool> SaveAsync()
        {
            var ValueReturned = false;
            if (await _context.SaveChangesAsync() > 0)
                ValueReturned = true;
            else
                ValueReturned = false;
            return ValueReturned;
        }
    }

}
