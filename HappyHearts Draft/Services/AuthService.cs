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
                var session = await _supabase.Client.Auth.SignIn(
                    model.Email,
                    model.Password);

                if (session?.User == null)
                {
                    return new AuthResult
                    {
                        Success = false,
                        Message = "Invalid email or password."
                    };
                }

                // Look up the user's profile using the Supabase UserId
                var response = await _supabase.Client
                    .From<AppUser>()
                    .Where(x => x.UserId == session.User.Id)
                    .Get();

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
            catch (Exception ex)
            {
                return new AuthResult
                {
                    Success = false,
                    Message = ex.ToString()
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
                    CreatedAt = DateTime.UtcNow
                };

                try
                {
                    await _supabase.Client
                        .From<AppUser>()
                        .Insert(newUser);

                    // Verify that the insert actually happened
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