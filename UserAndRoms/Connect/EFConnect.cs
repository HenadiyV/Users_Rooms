using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using UserAndRoms.Models;


namespace UserAndRoms.Connect
{
    public class Inisilization : DropCreateDatabaseAlways<EFContext>
    {
        //DropCreateDatabaseIfModelChanges
        protected override void Seed(EFContext context)
        {
            context.Roles.Add(new Role { Id = 1, Name = "admin" });
            context.Roles.Add(new Role { Id = 2, Name = "moderator" });
            context.Roles.Add(new Role { Id = 3, Name = "user" });
            context.SaveChanges();
            User us = new User{ FirstName = "Admin", LastName = "Admin", Email = "test@test.com", Password = "admin", RoleId=1 };
                context.Users.Add(us);
               context.SaveChanges();
            try {
                
            } catch (Exception e)
            {
                string s = e.Message;
            }
           
            Room room = new Room { Name = "public_room", RoomTypeAsString="Large",    SizeRoom = 25 };
            context.Rooms.Add(room);
            context.SaveChanges();
            try {
                }
           catch(Exception e)
            {
                string s = e.Message;
            }
           
            try
            {
                UsersRooms usersRooms = new UsersRooms { Users_Id = us.Id, Rooms_Id = room.Id};
                context.UsersRoom.Add(usersRooms);
        context.SaveChanges();
            } catch(Exception e)
            {
                string s = e.Message;
            }
          
            try {
                
            }
            catch (Exception e)
            {
                string s = e.Message;
            }
            
        }
    }

    public class EFContext : DbContext
    {
        // иницилизация 
        //static EFContext()
        //{
        //    Database.SetInitializer<EFContext>(new Inisilization());
        //}

        public EFContext() : base("DefaultConection")
        { }
        public DbSet<User> Users { set; get; }
        public DbSet<Room> Rooms { set; get; }
       public DbSet<UsersRooms> UsersRoom { set; get; }
        public DbSet<Role> Roles { set; get; }
        static public bool SearchFile( bool result )
        { return result; }

    }
}

