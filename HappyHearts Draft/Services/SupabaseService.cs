using HappyHearts_Draft.Interfaces;
using HappyHearts_Draft.Models;
using Microsoft.Extensions.Options;
using Supabase;

namespace HappyHearts_Draft.Services
{
    public class SupabaseService : ISupabaseService
    {
        public Client Client { get; }

        public SupabaseService(IOptions<SupabaseSettings> settings)
        {
           

            Console.WriteLine("========== SUPABASE ==========");
            Console.WriteLine($"URL: {settings.Value.Url}");
            Console.WriteLine($"Key Empty: {string.IsNullOrWhiteSpace(settings.Value.Key)}");
            Console.WriteLine($"Key Length: {settings.Value.Key?.Length}");
            Console.WriteLine("==============================");

            throw new Exception("Stop here");
        }
    }


}
