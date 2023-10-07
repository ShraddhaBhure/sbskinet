using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Add this using directive
using Core.Interfaces; 
using Core.Specifications;
using API.Dtos;
using API.Errors;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    public class BuggyController:BaseApiController
    {
        private readonly StoreContext _context;
        public BuggyController(StoreContext context)
        {
                 _context = context;
        }


        [HttpGet("testAuth")]
        [Authorize]
        public ActionResult<string> GetSecretText()
        {
         return "Secret Stuff";

        }
        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
             var thing =_context.Products.Find(42);

            if(thing == null)
             {
                return NotFound(new ApiResponse(404));
             }
            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {

             var thing =_context.Products.Find(42);
             var thingToReturn = thing.ToString();

              return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult BadRequest()
        {
           return BadRequest(new ApiResponse(400));
        }
      
       [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
        return Ok();
        }

    }
}