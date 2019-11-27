using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Provider : EntityBase
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
