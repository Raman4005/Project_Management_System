using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management_System.Models
{
    //Represents a project task
    public class ProjectTask {

        //Ho;ds project task id
        public string Id { get; set; }

        [Required]
        [Microsoft.AspNetCore.Mvc.Remote("TaskTitleAvailable", "ProjectTasks", ErrorMessage = "The project task  name is taken", AdditionalFields = "Id")]
        //Holds project task title . Uniquess validation is also performed
        public string TaskTitle { get; set; }

      
        //Holds estimated number of days
        public int EstimatedNumberOfDays { get; set; }

      
        //Holds actual number of days
        public int? ActualNumberOfDays { get; set; }

        //Holds link to employee
        public Employee Employee { get; set; }

        //Holds link to project
        public Project Project { get; set; }

        //Holds project id foreign key
        public string ProjectId { get; set; }

        //Holds  employee id foreign key
        public string EmployeeId { get; set; }


    }
}
