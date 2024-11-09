using System;
using System.Collections.Generic;
using System.Text;

namespace DatingApp.Application.Responses
{
    public class BaseCommandResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; protected set; } 
        public void AddError(string error)
        {
            if (Errors == null)
            {
                Errors = new List<string>();
            }
            Errors.Add(error);
        }
    }
}
