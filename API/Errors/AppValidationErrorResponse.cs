using System.Collections.Generic;
namespace API.Errors
{
    public class AppValidationErrorResponse : ApiResponse
    {
        public AppValidationErrorResponse() : base(400)
        {

        }
        public IEnumerable<string> Errors { get; set;}
    }
}