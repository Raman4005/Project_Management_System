using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management_System.Models
{
    //Represents a project
    public class Project
    {
        //Holds the employee id
        public string Id { get; set; }

        [Required]
        // Holds project title. vlidates for uniqueness.
        [Microsoft.AspNetCore.Mvc.Remote("ProjectTitleAvailable", "Projects", ErrorMessage = "The project name is taken", AdditionalFields = "Id")]
        public string ProjectTitle { get; set; }

        [Required]
        //Holds project budget.
        public decimal Budget { get; set; }

        [Required]
        //Holds completion  date .
        [DataType(DataType.Date)]
        public DateTime CompletionDate { get; set; }

    }
}
