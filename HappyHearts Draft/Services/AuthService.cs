using HappyHearts_Draft.Interfaces;
using HappyHearts_Draft.Models;
using HappyHearts_Draft.Models.ViewModels;

namespace HappyHearts_Draft.Services
{
    public class AuthService : IAuthService
    {
        private readonly ISupabaseService _supabase;

        public AuthService(ISupabaseService supabase)
        {
            _supabase = supabase;
        }

        public async Task<AuthResult> LoginAsync(LoginViewModel model)
        {
            try
            {
                Console.WriteLine("========== LOGIN ==========");

                var email = model.Email.Trim().ToLower();

                Console.WriteLine($"Email Entered: {email}");
                Console.WriteLine($"Password Length: {model.Password.Length}");

                // Check if the user's profile exists first
                var existingUser = await _supabase.Client
                    .From<AppUser>()
                    .Where(x => x.Email == email)
                    .Get();

                Console.WriteLine($"Profiles Found: {existingUser.Models.Count}");

                // Attempt Supabase login
                var session = await _supabase.Client.Auth.SignIn(
                    email,
                    model.Password);

                Console.WriteLine($"Session Null: {session == null}");
                Console.WriteLine($"User Null: {session?.User == null}");

                if (session?.User == null)
                {
                    return new AuthResult
                    {
                        Success = false,
                        Message = "Invalid email or password."
                    };
                }

                Console.WriteLine($"Supabase User ID: {session.User.Id}");
                Console.WriteLine($"Supabase Email: {session.User.Email}");

                // Get the user's profile from public.Users
                var response = await _supabase.Client
                    .From<AppUser>()
                    .Where(x => x.UserId == session.User.Id)
                    .Get();

                Console.WriteLine($"Matching Profiles: {response.Models.Count}");

                var profile = response.Models.FirstOrDefault();

                if (profile == null)
                {
                    return new AuthResult
                    {
                        Success = false,
                        Message = "User profile not found in public.Users."
                    };
                }

                return new AuthResult
                {
                    Success = true,
                    Message = "Login Successful",

                    UserId = profile.UserId,
                    Email = profile.Email,
                    FullName = profile.FullName,
                    Role = profile.Role
                };
            }
            catch (Supabase.Gotrue.Exceptions.GotrueException ex)
            {
                Console.WriteLine("========== SUPABASE AUTH ERROR ==========");
                Console.WriteLine(ex.Message);

                return new AuthResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("========== GENERAL ERROR ==========");
                Console.WriteLine(ex);

                return new AuthResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<AuthResult> RegisterAsync(RegisterViewModel model)
        {
            try
            {
                var session = await _supabase.Client.Auth.SignUp(
                    model.Email,
                    model.Password);

                if (session?.User == null)
                {
                    return new AuthResult
                    {
                        Success = false,
                        Message = "Supabase Auth failed."
                    };
                }

                var newUser = new AppUser
                {
                    UserId = session.User.Id,
                    FullName = model.FullName,
                    Email = model.Email,
                    Role = "Customer",
                    Newsletter = model.Newsletter,
                    Created_at = DateTime.UtcNow
                };

                try
                {
                    // Insert into public.Users
                    await _supabase.Client
                        .From<AppUser>()
                        .Insert(newUser);

                    // Verify it was inserted
                    var verify = await _supabase.Client
                        .From<AppUser>()
                        .Where(x => x.UserId == session.User.Id)
                        .Get();

                    if (!verify.Models.Any())
                    {
                        return new AuthResult
                        {
                            Success = false,
                            Message = "User was created in auth.users but NOT inserted into public.Users."
                        };
                    }

                    return new AuthResult
                    {
                        Success = true,
                        Message = "Account created successfully."
                    };
                }
                catch (Exception ex)
                {
                    return new AuthResult
                    {
                        Success = false,
                        Message = "INSERT FAILED:\n\n" + ex.ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                return new AuthResult
                {
                    Success = false,
                    Message = ex.ToString()
                };
            }
        }
    }
}