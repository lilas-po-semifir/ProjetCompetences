using Microsoft.AspNetCore.Mvc;
using ProjetCompetences.API.REST.Adapters;
using ProjetCompetences.API.REST.DTOs.Equipe;
using ProjetCompetences.Domain.Models;
using ProjetCompetences.Domain.Services;
using ProjetCompetences.Domain.Services.Exceptions.Equipe;

namespace ProjetCompetences.API.REST.Controllers
{
    [Route("/equipe")]
    [ApiController]
    public class EquipeController : ControllerBase
    {
        private readonly EquipeService _service;
        private readonly EquipeRestAdaptEquipe _equipeAdapter;

        public EquipeController(EquipeService service, EquipeRestAdaptEquipe equipeAdapter)
        {
            _service = service;
            _equipeAdapter = equipeAdapter;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<EquipeAvecMembres>>> GetAllEquipes()
        {
            List<EquipeAvecMembres> equipeAvecMembres = new List<EquipeAvecMembres>();
            var equipes = await _service.GetAllEquipes();
            foreach (var equipe in equipes)
            {
                var equipeAvecMembre = await _equipeAdapter.ToEquipeAvecMembres(equipe);
                if (equipeAvecMembre != null)
                {
                    equipeAvecMembres.Add(equipeAvecMembre);
                }
            }

            return Ok(equipeAvecMembres);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipeAvecMembres>> GetEquipe(string id)
        {
            var equipe = await _service.GetEquipe(new Guid(id));
            if (equipe == null)
            {
                return NotFound();
            }

            var equipeAvecMembres = _equipeAdapter.ToEquipeAvecMembres(equipe);         
            return Ok(equipeAvecMembres);
        }
        
        [HttpPost("")]
        public async Task<ActionResult<EquipeAvecMembres>> AddEquipe([FromBody] EquipeSansMembres equipeSansMembres)
        {
            Equipe equipe = await _equipeAdapter.ToEquipe(equipeSansMembres, null);
            Equipe equipeCree = await _service.CreateEquipe(equipe);
            if (equipeCree == null)
            {
                return StatusCode(500, "An error occurred while creating the equipe.");
            }
            EquipeAvecMembres equipeAvecMembres = await _equipeAdapter.ToEquipeAvecMembres(equipeCree);
            return Ok(equipeAvecMembres);
        }

        [HttpPost("{equipeId}/membres/{personneId}")]
        public async Task<ActionResult<EquipeAvecMembres>> AddMembreToEquipe(string equipeId, string personneId)
        {
            EquipeAvecMembres equipeAvecMembres;
            try
            {
                var equipe = await _service.AddMembreToEquipe(new Guid(equipeId), new Guid(personneId));
                equipeAvecMembres = await _equipeAdapter.ToEquipeAvecMembres(equipe);
            }
            catch (EquipeNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (PersonneNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AlreadyInEquipeException ex) 
            {
                return Conflict(ex.Message);
            }
            catch (AlreadyInOtherEquipeException ex) 
            { 
                return Conflict(ex.Message);
            }

            return Ok(equipeAvecMembres);
        }

        [HttpDelete("{equipeId}/membres/{personneId}")]
        public async Task<ActionResult<EquipeAvecMembres>> RemoveMembreFromEquipe(string equipeId, string personneId)
        {
            EquipeAvecMembres equipeAvecMembres;
            try
            {
                var equipe = await _service.RemoveMembreFromEquipe(new Guid(equipeId), new Guid(personneId));
                equipeAvecMembres = await _equipeAdapter.ToEquipeAvecMembres(equipe);
            }
            catch (EquipeNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (PersonneNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (NotInEquipeException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ManagerCantLeaveEquipe ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(equipeAvecMembres);
        }

        [HttpDelete("{equipeId}")]
        public async Task<ActionResult> DeleteEquipe(string equipeId)
        {
            try
            {
                await _service.DeleteEquipe(new Guid(equipeId));
            }
            catch(EquipeNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok();
        }

        [HttpPut("{equipeId}/manager/{managerId}")]
        public async Task<ActionResult<EquipeAvecMembres>> updateManager(string equipeId, string managerId)
        {
            EquipeAvecMembres equipeAvecMembres;
            try
            {
                var equipe = await _service.ChangeManager(new Guid(equipeId), new Guid(managerId));
                equipeAvecMembres = await _equipeAdapter.ToEquipeAvecMembres(equipe);
            }
            catch (EquipeNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (PersonneNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ManagerNotInEquipe ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(equipeAvecMembres);
        }

    }
}
