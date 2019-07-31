using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Yozian.MyGrid.Example.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Display(Name = "Book Name")]
        public string Name { get; set; }

        [Display(Name = "Create Time")]
        public DateTime CreatedAt { get; set; }
    }
}