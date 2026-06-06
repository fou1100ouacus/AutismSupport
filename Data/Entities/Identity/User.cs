// using EntityFrameworkCore.EncryptColumn.Attribute;
// using Microsoft.AspNetCore.Identity;
// using System.ComponentModel.DataAnnotations.Schema;
// using Data.Entities.Child;
// namespace Data.Entities.Identity
// {
//     public class User : IdentityUser<int>
//     {
//         public User()
//         {
//             UserRefreshTokens=new HashSet<UserRefreshToken>();
//         }
//         public string FullName { get; set; }
//         public string? Address { get; set; }
//         public string? Country { get; set; }
//         [EncryptColumn]
//         public string? Code { get; set; }
//         [InverseProperty(nameof(UserRefreshToken.user))]
//         public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; }
//         // علاقة One-to-One مع ChildProfile
//         public virtual ChildProfile? ChildProfile { get; set; }
//     }
// }
using EntityFrameworkCore.EncryptColumn.Attribute;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Entities.Child;

namespace Data.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            UserRefreshTokens = new HashSet<UserRefreshToken>();
        }

        public string FullName { get; set; } = string.Empty;

        public string? Address { get; set; }

        public string? Country { get; set; }

        [EncryptColumn]
        public string? Code { get; set; }

        [InverseProperty(nameof(UserRefreshToken.user))]
        public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; }

        // One-to-One relationship with Child Profile
        // Each mother can have only one child profile
        public virtual ChildProfile? ChildProfile { get; set; }
    }
}