using System;

namespace Project_Management_System.Models
{
    //Holds the error view model
    public class ErrorViewModel
    {
        //Holds request id
        public string RequestId { get; set; }

        //Checks whethe the request id is shown or not.
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}