using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Contracts.Category
{
    public class CategoryViewModel
    {
        
        public int Id { get; set; }
       
        public string Name { get; set; }
       
        public int DisplayOrder { get; set; }

        public string CreatedDateTime { get; set; }
    }
}
