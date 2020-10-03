using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    public class PetsController : ControllerBase
    {
        private IPetService _petService;
        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        // GET: api/<PetsController>
        /// <summary>
        /// kurwa kurwa
        /// </summary>
        /// <param name="filter"></param>
        /// <returns> A FilteredList of pets </returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>  
        [HttpGet]
        public ActionResult<FilteredList<Pet>> Get([FromQuery] Filter filter)
        {
            try
            {
                

                return Ok(_petService.GetPets(filter));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e);
            }
           
        }

        // GET api/<PetsController>/5
        [HttpGet("{id}")]
        public ActionResult<Pet> Get(int id)
        {
            try
            {

                Pet pet = _petService.GetPetById(id);
                if (pet == null)
                {
                    return StatusCode(404, "did not find pet");

                }
                return Ok(pet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e);
            }

            
        }

        // POST api/<PetsController>
        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet value)
        {
            try
            {
                return string.IsNullOrEmpty(value.Name) ? BadRequest("Name is required to create a pet") : StatusCode(201, _petService.CreatePet(value));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e);
            }
            

        }
        //aka update
        // PUT api/<PetsController>/5
        [HttpPut("{id}")]
        public ActionResult<Pet> Put(int id, [FromBody] Pet value)
        {
            try
            {

            

            value.Id = id;
            Pet pet = _petService.EditPet(value);

           if ( pet == null)
           {
               return StatusCode(404,"Could not find pet with the specified id");

           }
           else
           {
               return StatusCode(202, pet);
           }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e);
            }
        }

        // DELETE api/<PetsController>/5
        [HttpDelete("{id}")]
        public ActionResult<Pet> Delete(int id)
        {
            try
            {
                Pet pet = _petService.DeletePet(id);
                if (pet == null)
                {
                    return StatusCode(404, "Could not find pet with the specified id");

                }
             

                return StatusCode(202, pet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(404, e);
            }

        }
    }
}
