using ChowChoice.IdentityNPocoStores.Core;

namespace IdentityServerWithAspNetIdentity.Models
{
    // Add profile data for application role by adding properties to the ApplicationRole class
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole():base()
        { }
        public ApplicationRole(string roleName, string appName) : base(roleName)
        {
            AppName = appName;
        }
        public string AppName { get; set; }
    }
}

