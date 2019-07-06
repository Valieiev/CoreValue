using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CoreApp.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"[а-яА-Яa-zA-Z]{2,50}", ErrorMessage = "Only letters allowed")]
        public string Surname { get; set; }
        [Required]
        [RegularExpression(@"[а-яА-Яa-zA-Z]{2,50}", ErrorMessage = "Only letters allowed")]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone no.")]
        [DataType(DataType.PhoneNumber)]
        public  string PhoneNumber { get; set; }
    }
}
