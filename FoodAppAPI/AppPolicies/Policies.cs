using Microsoft.AspNetCore.Authorization;

namespace FoodAppAPI.AppPolicies
{
    public static class Policies
    {/// <summary>
    /// registers all user roles as policies
    /// </summary>
           
            public const string Admin = "Admin";    
            public const string User = "User";    
            public const string Delivery = "Delivery";    
    
            public static AuthorizationPolicy AdminPolicy()    
            {    
                return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();    
            }    
    
            public static AuthorizationPolicy UserPolicy()    
            {    
                return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(User).Build();    
            }   
            public static AuthorizationPolicy DeliveryPolicy()    
            {    
                return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Delivery).Build();    
            }  
            
    }
}
