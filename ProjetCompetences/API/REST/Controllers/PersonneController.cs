using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using ProjetCompetences.Domain.Models;
using ProjetCompetences.Domain.Services;
using ProjetCompetences.Domain.Services.Exceptions.Personne;

namespace ProjetCompetences.API.REST.Controllers
{
    [Route("/personne")]
    [ApiController]
    public class PersonneController : ControllerBase
    {
        private readonly PersonneService _service;

        public PersonneController(PersonneService service)
        {
            _service = service;
        }

        [HttpPost("")]
        public async Task<ActionResult<Personne>> AddPersonne([FromBody] Personne personne)
        {
            if (personne == null)
            {
                return BadRequest("L'objet Personne que vous avez défini est null.");
            }

            var addedPersonne = await _service.AddPersonne(personne);
            return Ok(addedPersonne);
        }

        // GET /personnes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Personne>> GetPersonne(string id)
        {
            Personne personne;
            try
            {
                personne = await _service.GetPersonne(new Guid(id));
            }
            catch (PersonneNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(personne);
        }

        [HttpGet("")]
        public async Task<ActionResult<List<Personne>>> GetAllPersonnes()
        {
            var personnes = await _service.GetAllPersonnes();
            return Ok(personnes);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Personne>> UpdatePersonne(string id, [FromBody] Personne updatedPersonne)
        {
            Personne personne;
            try
            {
                personne = await _service.UpdatePersonne(updatedPersonne);
            }
            catch (PersonneNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            return Ok(personne);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePersonne(string id)
        {
            try
            {
                await _service.DeletePersonne(new Guid(id));
            }
            catch (PersonneNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}
