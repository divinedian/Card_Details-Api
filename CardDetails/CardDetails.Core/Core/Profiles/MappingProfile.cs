using AutoMapper;
using CardDetails.Core.Core.Application.Commands;
using CardDetails.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardDetails.Core.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CardDetail, AddCardDetailsCommand>().ReverseMap();
        }
    }
}
