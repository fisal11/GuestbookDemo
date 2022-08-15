using GB.Services.Model;
using Microsoft.AspNetCore.Identity;

namespace GB.Services.Interfaces
{
    public interface IAccount
    {
        Task<IdentityResult> SignUp(SignUpModel signUpModel);
        Task<string> SignIn(SignInModel signInModel);
    }
}
