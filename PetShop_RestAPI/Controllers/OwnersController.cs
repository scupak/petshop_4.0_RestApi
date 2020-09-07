using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Petshop.core.ApplicationServices;
using Petshop.Core.Entity;

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
        public IEnumerable<Owner> Get()
        {
            return OwnerService.GetOwners();
        }

        // GET api/<OwnersController>/5
        [HttpGet("{id}")]
        public Owner Get(int id)
        {
            return OwnerService.GetOwnerById(id);
        }

        // POST api/<OwnersController>
        [HttpPost]
        public ActionResult<Owner> Post([FromBody] Owner value)
        {
            if (string.IsNullOrEmpty(value.FirstName))
            {
                return BadRequest("FirstName is required to create an Owner");
            }
            return Ok(OwnerService.CreateOwner(value));
        }

        // PUT api/<OwnersController>/5
        [HttpPut("{id}")]
        public ActionResult<Owner> Put(int id, [FromBody] Owner value)
        {
            value.Id = id;

            if (OwnerService.EditOwner(value) == null)
            {
                return BadRequest("Could not find Owner with the specified id");

            }
            else
            {
                return Ok();
            }
        }

        // DELETE api/<OwnersController>/5
        [HttpDelete("{id}")]
        public ActionResult<Owner> Delete(int id)
        {
            if (OwnerService.DeleteOwner(id) == false)
            {
                return BadRequest("Could not delete Owner");

            }

            return Ok();
        }
    }
}
