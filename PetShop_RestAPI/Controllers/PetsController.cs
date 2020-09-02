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
    public class PetsController : ControllerBase
    {
        private IPetService _petService;
        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        // GET: api/<PetsController>
        [HttpGet]
        public IEnumerable<Pet> Get()
        {
            return _petService.GetPets();
        }

        // GET api/<PetsController>/5
        [HttpGet("{id}")]
        public Pet Get(int id)
        {
            return _petService.GetPetById(id);
        }

        // POST api/<PetsController>
        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet value)
        {
            if (string.IsNullOrEmpty(value.Name))
            {
                return BadRequest("Name is required to create a pet");
            }
            return Ok( _petService.CreatePet(value));
        }
        //aka update
        // PUT api/<PetsController>/5
        [HttpPut("{id}")]
        public ActionResult<Pet> Put(int id, [FromBody] Pet value)
        {
            value.Id = id;

           if( _petService.EditPet(value) == null)
           {
               return BadRequest("Could not find pet with the specified id");

           }
           else
           {
               return Ok();
           }
        }

        // DELETE api/<PetsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _petService.DeletePet(id);
        }
    }
}
