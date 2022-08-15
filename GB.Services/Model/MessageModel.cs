using GB.DataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace GB.Services.Model
{
    public class MessageModel
    {
        public int Id { get; set; }
        [Required]
        public string Meessage { get; set; }
        public DateTime MeessageDate { get; set; }
        public string UserId { get; set; }
        public  ApplicationUser User { get; set; }
    }
}
