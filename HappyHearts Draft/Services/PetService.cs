using HappyHearts_Draft.Interfaces;
using HappyHearts_Draft.Models;

namespace HappyHearts_Draft.Services
{
    public class PetService : IPetService
    {
        private readonly ISupabaseService _supabase;

        public PetService(ISupabaseService supabase)
        {
            _supabase = supabase;
        }

        public async Task<List<Pet>> GetPetsBySpeciesAsync(long speciesId)
        {
            var response = await _supabase.Client
                .From<Pet>()
                .Where(x => x.SpeciesId == speciesId)
                .Where(x => x.IsAvailable == true)
                .Get();
              
            return response.Models;
        }

        public async Task<List<Pet>> GetFeaturedPetsAsync()
        {
            var response = await _supabase.Client
                .From<Pet>()
                .Where(x => x.IsFeatured)
                .Where(x => x.IsAvailable)
                .Get();

            return response.Models;
        }

        public async Task<Pet?> GetPetAsync(long petId)
        {
            var response = await _supabase.Client
                .From<Pet>()
                .Where(x => x.PetId == petId)
                .Get();

            return response.Models.FirstOrDefault();
        }
    }


}
