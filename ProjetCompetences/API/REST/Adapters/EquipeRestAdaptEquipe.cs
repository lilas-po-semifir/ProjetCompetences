using ProjetCompetences.API.REST.DTOs.Equipe;
using ProjetCompetences.Domain.Models;
using ProjetCompetences.Domain.Services;

namespace ProjetCompetences.API.REST.Adapters
{
    public class EquipeRestAdaptEquipe
    {
        private readonly EquipeService _equipeService;
        private readonly PersonneService _personneService;

        public EquipeRestAdaptEquipe(EquipeService equipeService, PersonneService personneService)
        {
            _equipeService = equipeService;
            _personneService = personneService;
        }

        public async Task<EquipeAvecMembres?> ToEquipeAvecMembres(Equipe equipe)
        {
            if (equipe == null) return null;

            var equipeAvecMembres = new EquipeAvecMembres
            {
                Id = equipe.Id.ToString(),
                Nom = equipe.Nom,
                Description = equipe.Description,
                Membres = new List<MembreSansEquipe>(),
                Manager = null
            };

            if (equipe.Membres.Count > 0)
            {
                foreach (Guid membre in equipe.Membres)
                {
                    Personne personne = await _personneService.GetPersonne(membre);
                    if (personne != null)
                    {
                        equipeAvecMembres.Membres.Add(ToMembreSansEquipe(personne));
                    }
                }
            }

            if (equipe.Manager != null)
            {
                Personne manager = await _personneService.GetPersonne((Guid)equipe.Manager);
                if (manager != null)
                {
                    equipeAvecMembres.Manager = ToMembreSansEquipe(manager);
                }
            }

            return equipeAvecMembres;
        }

        public async Task<Equipe> ToEquipe(EquipeAvecMembres equipeAvecMembres)
        {
            return await _equipeService.GetEquipe(new Guid(equipeAvecMembres.Id));
        }

        public MembreSansEquipe? ToMembreSansEquipe(Personne personne)
        {
            if (personne == null) return null;

            var membreSansEquipe = new MembreSansEquipe
            {
                Id = personne.Id.ToString(),
                Nom = personne.Nom,
                Prenom = personne.Prenom,
            };

            return membreSansEquipe;
        }

        public async Task<Personne> ToPersonne(MembreSansEquipe membreSansEquipe)
        {
            return await _personneService.GetPersonne(new Guid(membreSansEquipe.Id));
        }

        public async Task<Equipe> ToEquipe(EquipeSansMembres equipeSansMembres, string? id) 
        {
            if(id == null)
            {
                Equipe equipe = new Equipe
                {
                    Nom = equipeSansMembres.Nom,
                    Description = equipeSansMembres.Description,
                };

                return equipe;
            }
            else
            {
                Equipe currentEquipe = await _equipeService.GetEquipe(new Guid(id));
                if (currentEquipe == null)
                {
                    Equipe equipe = new Equipe
                    {
                        Nom = equipeSansMembres.Nom,
                        Description = equipeSansMembres.Description,
                    };
                    return equipe;
                }
                else
                {
                    currentEquipe.Nom = equipeSansMembres.Nom;
                    currentEquipe.Description = equipeSansMembres.Description;
                    return currentEquipe;
                }
            }
        }

    }
}
