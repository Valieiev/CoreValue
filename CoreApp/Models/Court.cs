using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreApp.Models
{
    public class Court
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        [Required]
        [RegularExpression(@"[а-яА-Яa-zA-Z0-9]{2,50}", ErrorMessage = "Only letters or numbers allowed")]
        public string Summary { get; set; }
        [Required]
        public string Location { get; set; }
        [RegularExpression(@"[а-яА-Яa-zA-Z0-9\s]{1,255}", ErrorMessage = "No more than 255")]
        public string Description { get; set; }
    }
}
