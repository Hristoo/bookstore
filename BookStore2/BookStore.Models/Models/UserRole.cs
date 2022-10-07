using Microsoft.AspNetCore.Identity;

namespace BookStore.Models.Models
{
    public class UserRole : IdentityRole
    {
        public int UserId { get; set; }
    }
}
