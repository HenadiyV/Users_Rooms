using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserAndRoms.Models
{
    public class Room
    {
        public Room()
        {
            Users = new HashSet<User>();
        }
        [Key]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        public double SizeRoom { set; get; }

        public ICollection<User> Users { get; set; }

        public string RoomTypeAsString
        {
            get
            {
                return this.RoomType.ToString();
            }
            set
            {
                RoomType = (RoomTypes)Enum.Parse(typeof(RoomTypes), value, true);
            }
        }

        public RoomTypes RoomType { get; set; }

        public enum RoomTypes
        {
            Large = 1,
            Medium = 2,
            Small = 3,
            Other = 4
        }
    }
}