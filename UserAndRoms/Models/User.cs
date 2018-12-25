using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserAndRoms.Models
{
    public class User
    {
        public User()
        {
            Rooms = new HashSet<Room>();
        }
        [Key]
        public int Id { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}