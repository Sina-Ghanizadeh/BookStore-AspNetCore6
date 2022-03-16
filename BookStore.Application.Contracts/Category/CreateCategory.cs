using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Contracts.Category
{
    public class CreateCategory
    {
        public string Name { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The Display Order Range is Invalid.")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }

    }
}
