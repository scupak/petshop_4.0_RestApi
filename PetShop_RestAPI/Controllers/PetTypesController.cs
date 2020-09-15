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
    public class PetTypesController : ControllerBase
    {
        private IPetTypeService _petTypeService;

        public PetTypesController(IPetTypeService petTypeService)
        {
            _petTypeService = petTypeService;
        }

        // GET: api/<PetTypesController>
        [HttpGet]
        public ActionResult<IEnumerable<PetType>> Get([FromQuery] Filter filter)
        {
            try
            {
                return Ok(_petTypeService.GetPetTypes(filter));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET api/<PetTypesController>/5
        [HttpGet("{id}")]
        public ActionResult<PetType> Get(int id)
        {
            try
            {

                PetType petType = _petTypeService.GetPetTypeById(id);
                if (petType == null)
                {
                    return StatusCode(404, "did not find petType");

                }
                return Ok(petType);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e);
            }
        }

        // POST api/<PetTypesController>
        [HttpPost]
        public ActionResult<PetType> Post([FromBody] PetType value)
        {
            try
            {
                return string.IsNullOrEmpty(value.name) ? BadRequest("Name is required to create a petType") : StatusCode(201, _petTypeService.CreatePetType(value));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e);
            }

        }

        // PUT api/<PetTypesController>/5
        [HttpPut("{id}")]
        public ActionResult<PetType> Put(int id, [FromBody] PetType value)
        {
            value.Id = id;

            if (_petTypeService.EditPetType(value) == null)
            {
                return StatusCode(404, "Could not find PetType with the specified id");

            }
            else
            {
                return StatusCode(202, "accepted");
            }
        }

        // DELETE api/<PetTypesController>/5
        [HttpDelete("{id}")]
        public ActionResult<PetType> Delete(int id)
        {
            if (_petTypeService.DeletePetType(id) == false)
            {
                return StatusCode(404, "Could not delete PetType");

            }

            return StatusCode(202, "accepted");
        }
    }
}
