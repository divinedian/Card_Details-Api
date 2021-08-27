using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CardDetails.Data.Models
{
    public class CardDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Bin { get; set; }

        public Number Number { get; set; }

        public string Scheme { get; set; }

        public string Type { get; set; }

        public string Brand { get; set; }

        public bool Prepaid { get; set; }

        public Country Country { get; set; }

        public Bank Bank { get; set; }
    }
}
