using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirthdayDiaryApp.Models
{

    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(5), Index(IsUnique = true), Column(TypeName = "VARCHAR")]
        public string Username { get; set; }
        [MinLength(5), Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [EmailAddress, Index(IsUnique = true), Column(TypeName = "VARCHAR"), Required]
        public string Email { get; set; }
        [ScaffoldColumn(false)]
        public string ImageUrl { get; set; }
        [ScaffoldColumn(false)]
        public DateTime CreatedOn { get; set; }
    }
}