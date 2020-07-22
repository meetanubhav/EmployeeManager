using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Models
{
    public class EmployeeModel
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
    }
}
