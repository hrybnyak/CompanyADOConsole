using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Product : EntityBase
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int? ProviderId { get; set; }
        public int? CategoryId { get; set; }
    }
}
