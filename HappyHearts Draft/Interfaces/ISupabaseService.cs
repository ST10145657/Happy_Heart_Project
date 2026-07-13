using Supabase;

namespace HappyHearts_Draft.Interfaces
{
    public interface ISupabaseService
    {
        Client Client { get; }
    }
}
