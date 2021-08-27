using System;
using System.Collections.Generic;
using System.Text;

namespace CardDetails.Data.Models
{
    public class Number
    {
        public int Id { get; set; }

        public int Length { get; set; }

        public bool Luhn { get; set; }
    }
}
