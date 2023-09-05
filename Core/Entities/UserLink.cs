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
        public string OriginalUrl { get; set; }
        public string ShortenedUrl { get; set; }
        public string? DisplayName { get; set; }
        public string? UserId { get; set; }
        public virtual AppUser? User { get; set; }
    }
}