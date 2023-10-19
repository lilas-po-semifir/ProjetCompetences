using MongoDB.Driver;
using ProjetCompetences.Domain.Models;

namespace ProjetCompetences.Infrastructure.Mongo
{
    public class EquipeRepoMongo
    {
        public IMongoCollection<Equipe> Equipes { get; }

        public EquipeRepoMongo(MongoCollectionProvider collectionProvider)
        {
            Equipes = collectionProvider.GetEquipeCollection();
        }

    }
}
