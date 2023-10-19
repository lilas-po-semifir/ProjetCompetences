using MongoDB.Driver;
using ProjetCompetences.Domain.Models;

namespace ProjetCompetences.Infrastructure.Mongo
{
    public class PersonneRepoMongo
    {
        public IMongoCollection<Personne> Personnes { get; }

        public PersonneRepoMongo(MongoCollectionProvider collectionProvider)
        {
            Personnes = collectionProvider.GetPersonneCollection();
        }

    }
}
