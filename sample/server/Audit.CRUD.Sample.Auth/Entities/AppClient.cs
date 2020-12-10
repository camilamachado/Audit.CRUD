using System.ComponentModel.DataAnnotations;

namespace Audit.CRUD.Sample.Auth.Entities
{
    public class AppClient
    {
        [Key]
        [MaxLength(32)]
        public string ClientId { get; set; }

        [MaxLength(80)]
        [Required]
        public string Base64Secret { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
    }
}
