using System.ComponentModel.DataAnnotations.Schema;

namespace UserAndRoms.Models
{
    public class UsersRooms
    {
        public int Id { set; get; }
        public int Users_Id { get; set; }
        [ForeignKey("Users_Id")]
        public User Users { get; set; }
        public int Rooms_Id { get; set; }
        [ForeignKey("Rooms_Id")]
        public Room Room { get; set; }

    }
}