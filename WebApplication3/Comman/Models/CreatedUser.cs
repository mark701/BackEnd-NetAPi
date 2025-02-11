using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplication3.Comman.Models
{
    public class CreatedUser
    {
        public int? UserId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreateDateAndTime { get; set; } = DateTime.Now;

    }
}
