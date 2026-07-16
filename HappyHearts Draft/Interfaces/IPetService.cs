using HappyHearts_Draft.Models;

namespace HappyHearts_Draft.Interfaces
{
    public interface IPetService
    {
        Task<List<Pet>> GetPetsBySpeciesAsync(long speciesId);

        Task<List<Pet>> GetFeaturedPetsAsync();

        Task<Pet?> GetPetAsync(long petId);
    }
}
