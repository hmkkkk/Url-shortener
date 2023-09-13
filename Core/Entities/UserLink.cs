using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Identity;

namespace Core.Entities
{
    public class UserLink
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [RegularExpression(@"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)",
            ErrorMessage = "Original URL must be a full URL link")]
        public string OriginalUrl { get; set; }
        public string ShortenedUrl { get; set; }

        [StringLength(32, ErrorMessage = "Display name must be shorter than 32 characters")]
        public string? DisplayName { get; set; }
        public string? UserId { get; set; }
        public virtual AppUser? User { get; set; }
    }
}