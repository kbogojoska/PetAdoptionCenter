using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.DTO
{
    public class BusinessDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public float Rating { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsOpen { get; set; }
        public bool IsAvailable { get; set; }
    }
}
