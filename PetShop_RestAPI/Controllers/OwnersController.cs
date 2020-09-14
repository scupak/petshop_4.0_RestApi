using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Petshop.core.ApplicationServices;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetShop_RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private IOwnerService OwnerService;

        public OwnersController(IOwnerService ownerService)
        {
            OwnerService = ownerService;
        }

        // GET: api/<OwnersController>
        [HttpGet]
        public ActionResult<FilteredList<Owner>> Get([FromQuery] Filter filter)
        {
            try
            {
                return Ok(OwnerService.GetOwners(filter));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        // GET api/<OwnersController>/5
        [HttpGet("{id}")]
        public ActionResult<Owner> Get(int id)
        {
            try
            {

                Owner owner = OwnerService.GetOwnerById(id);
                if (owner == null)
                {
                    return StatusCode(404, "did not find owner");

                }
                return Ok(owner);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e);
            }
        }

        // POST api/<OwnersController>
        [HttpPost]
        public ActionResult<Owner> Post([FromBody] Owner value)
        {
            try
            {
                return string.IsNullOrEmpty(value.FirstName) ? BadRequest("Name is required to create an owner") : StatusCode(201, OwnerService.CreateOwner(value));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e);
            }
        }

        // PUT api/<OwnersController>/5
        [HttpPut("{id}")]
        public ActionResult<Owner> Put(int id, [FromBody] Owner value)
        {
            value.Id = id;

            if (OwnerService.EditOwner(value) == null)
            {
                return StatusCode(404,"Could not find Owner with the specified id");

            }
            else
            {
                return StatusCode(202, "accepted");
            }
        }

        // DELETE api/<OwnersController>/5
        [HttpDelete("{id}")]
        public ActionResult<Owner> Delete(int id)
        {
            if (OwnerService.DeleteOwner(id) == false)
            {
                return StatusCode(404,"Could not delete Owner");

            }

            return StatusCode(202, "accepted"); 
        }
    }
}
