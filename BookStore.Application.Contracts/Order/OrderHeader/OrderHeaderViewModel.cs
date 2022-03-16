using BookStore.Application.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Contracts.Order.OrderHeader
{
    public class OrderHeaderViewModel
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUserViewModel ApplicationUser { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public double OrderTotal { get; set; }

        public string OrderStatus { get; set; }

        public string PaymentStatus { get; set; }

        public string TrackingNumber { get; set; }
        public string Carrier { get; set; }

        public DateTime PaymentDate { get; set; }
        public DateTime PaymentDueDate { get; set; }

        public string SessionId { get; set; }
        public string PaymentIntentId { get; set; }

        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public string UserEmail { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

    }
}
