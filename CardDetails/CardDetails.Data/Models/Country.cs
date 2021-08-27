using System.ComponentModel.DataAnnotations;

namespace CardDetails.Data.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Numeric { get; set; }

        public string Alpha2 { get; set; }

        public string Name { get; set; }

        public string Emoji { get; set; }

        public string Currency { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}