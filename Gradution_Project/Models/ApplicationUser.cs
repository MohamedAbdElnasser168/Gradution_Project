using Microsoft.AspNetCore.Identity;

namespace Gradution_Project.Models
{
    public class ApplicationUser: IdentityUser
    {
        // زود أي Properties إضافية هنا حسب الحاجة
        public string FullName { get; set; }
    }
}
