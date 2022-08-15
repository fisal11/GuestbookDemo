using System.ComponentModel.DataAnnotations;

namespace GB.DataAccess.Entities
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string Meessage { get; set; }
        public DateTime MeessageDate { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
