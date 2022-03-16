using BookStore.Application.Contracts.User;
using BookStore.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IApplicationUserRepository _userRepository;

        public UserApplication(IApplicationUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ApplicationUserViewModel GetApplicationUser(string id)
        {
            var userFromDb = _userRepository.GetFirstOrDefault(x => x.Id == id,"Company");
            ApplicationUserViewModel user = new()
            {
                Id = userFromDb.Id,
                Name = userFromDb.Name,
                City = userFromDb.City,
                
                PhoneNumber = userFromDb.PhoneNumber,
                PostalCode = userFromDb.PostalCode,
                State = userFromDb.State,
                StreetAddress = userFromDb.StreetAddress
            };
            if(userFromDb.Company != null)
            {
                user.CompanyId = (int)userFromDb.CompanyId;
                user.CompanyName = userFromDb.Company.Name;
            }
            return user;
        }
    }
}
