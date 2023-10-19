using MongoDB.Driver;
using ProjetCompetences.Domain.Models;
using ProjetCompetences.Domain.Repository;
using ProjetCompetences.Infrastructure.Mongo;

namespace ProjetCompetences.Infrastructure.Adapters
{
    public class PersonneRepoAdaptMongo : PersonneRepo
    {
        private readonly IMongoCollection<Personne> _personnes;

        public PersonneRepoAdaptMongo(PersonneRepoMongo personnes) 
        {
            _personnes = personnes.Personnes;
        }

        public async Task<Personne> AddPersonne(Personne personne)
        {
            await _personnes.InsertOneAsync(personne);
            return personne;
        }

        public async Task DeletePersonne(Guid id)
        {
            await _personnes.DeleteOneAsync(p => p.Id == id);
        }

        public async Task<List<Personne>> GetAllPersonnes()
        {
            return await _personnes.Find(p => true).ToListAsync();
        }

        public async Task<Personne> GetPersonne(Guid id)
        {
            return await _personnes.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Personne> UpdatePersonne(Personne personne)
        {
            await _personnes.ReplaceOneAsync(p => p.Id == personne.Id, personne);
            return personne;
        }
    }
}
