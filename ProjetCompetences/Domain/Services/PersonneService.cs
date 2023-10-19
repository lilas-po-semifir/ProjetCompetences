using ProjetCompetences.Domain.Models;
using ProjetCompetences.Domain.Repository;
using ProjetCompetences.Domain.Services.Exceptions.Personne;

namespace ProjetCompetences.Domain.Services
{
    public class PersonneService
    {
        private readonly PersonneRepo _repo;

        public PersonneService(PersonneRepo repo) 
        { 
            _repo = repo;
        }

        public async Task<Personne> AddPersonne(Personne personne)
        {
            return await _repo.AddPersonne(personne);
        }

        public async Task DeletePersonne(Guid id)
        {
            var personne = await _repo.GetPersonne(id) ?? throw new PersonneNotFoundException("La personne ayant l'ID " + id
                + " n'a pas été trouvée.");

            await _repo.DeletePersonne(id);
        }

        public async Task<List<Personne>> GetAllPersonnes()
        {
            return await _repo.GetAllPersonnes();
        }

        public async Task<Personne> GetPersonne(Guid id)
        {
            var personne = await _repo.GetPersonne(id) ?? throw new PersonneNotFoundException("La personne ayant l'ID " + id
                + " n'a pas été trouvée.");
            return personne;
        }

        public async Task<Personne> UpdatePersonne(Personne personne)
        {
            var currentPersonne = await _repo.GetPersonne(personne.Id) ??
                throw new PersonneNotFoundException("La personne ayant l'ID " + personne.Id.ToString() + " n'a pas été trouvée.");
            return await _repo.UpdatePersonne(personne);
        }
    }
}
