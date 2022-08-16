﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStoreWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1,1000, ErrorMessage ="Display Order must be between 1 and 1000")]
        public int NumberOfProducts { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
