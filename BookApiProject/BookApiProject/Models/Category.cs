﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiProject.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //id is generated by db not user
        public int Id {get; set;}
        [Required]
        [MaxLength(50, ErrorMessage = "Category Name cannot be more than 50 characters")]
        public string Name { get; set; }
        public virtual ICollection<BookCategory> BookCategories{ get; set; }
    }
}
