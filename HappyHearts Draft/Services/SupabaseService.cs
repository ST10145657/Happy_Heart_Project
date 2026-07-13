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


            var options = new SupabaseOptions
            {
                AutoConnectRealtime = false
            };

            Client = new Client(
                settings.Value.Url,
                settings.Value.Key,
                options);

            Client.InitializeAsync().Wait();
        }
    }


}
