using System.Xml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace G_CustomeIdentity.Models
{
    public class Registration
    {
        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [Column(TypeName = "nvarchar(150)")]
        public string Email { get; set;} = string.Empty;
        [Required]
        [Column(TypeName ="nvarchar(15)")]
        [MaxLength(15, ErrorMessage = "Username should not be greater than 15 characters!")]
        public string Username { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{10,}$", ErrorMessage = "Minimum length 10 and must contain 1 Uppercase, 1 lowercase, 1 digit and 1 special charater")]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string? Role {get;  set;} = string.Empty;
    }
} 