using IdentityServer4.Validation;
using System.Diagnostics;
using System.Threading.Tasks;

namespace IdentityServer.Auth.Services
{
    public class ExtensionGrantValidator : IExtensionGrantValidator
    {
        public Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            // This is not being used at the moment
            Debug.WriteLine(context);
            return Task.FromResult(0);
        }

        public string GrantType => "delegation";
    }
}
