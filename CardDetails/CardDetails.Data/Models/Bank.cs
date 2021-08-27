using System.ComponentModel.DataAnnotations;

namespace CardDetails.Data.Models
{
    public class Bank
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Url { get; set; }

        public string Phone { get; set; }

        public string City { get; set; }
    }
}