using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Contracts.Category
{
    public class EditCategory : CreateCategory
    {
        public int Id { get; set; }
    }
}
