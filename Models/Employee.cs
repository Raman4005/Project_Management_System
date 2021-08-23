using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management_System.Models
{
    //Represents an employee
    public class Employee

    {
        //Holds employee id
        public string Id { get; set; }

        //Holds username with uniqu validation 
        [Required]
        [Microsoft.AspNetCore.Mvc.Remote("UserNameAvailable", "Employees", ErrorMessage="The username is taken", AdditionalFields ="Id")]
        public string UserName { get; set; }

        //Holds employee position.
        public Position Position { get; set; }

    }

    //Employee position enum.
    public enum Position {
          Manager, Engineer 
    }
}
