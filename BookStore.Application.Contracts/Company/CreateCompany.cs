using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Contracts.Company
{
    public class CreateCompany
    {
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
