using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CoreApp.Models
{
    //should be singluar - Client, not plural
    public class Clients
    {
        [Key]
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        [DataType(DataType.PhoneNumber)]
        public  string PhoneNumber { get; set; }
    }
}
