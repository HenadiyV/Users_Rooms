using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UserAndRoms.Connect;
using UserAndRoms.Models;

namespace UserAndRoms.Controllers
{
    public class HomeController : Controller
    {
        IRepository<User> Userdb;
        IRepository<Room> Roomdb;
        IRepository<UsersRooms> UsersRoomsdb;
        
        public HomeController()
        {
            Userdb = new SQLServerDB_User();
            Roomdb = new SQLServerDB_Room();
            UsersRoomsdb = new SQLServerDB_UsersRooms();

        }

        public ActionResult Index()
        {
            // список не пустых комнат
            List<UserRoomView> user_room = new List<UserRoomView>();
            var ur = UsersRoomsdb.GetList();
            foreach (var dr in ur)
            {
                UserRoomView usrRoom = new UserRoomView();
                usrRoom.UserViewID = dr.Users_Id;
                usrRoom.RoomViewID = dr.Rooms_Id;
                usrRoom.UserEmail = Userdb.GetObj(dr.Users_Id).Email;
                usrRoom.UserName = Userdb.GetObj(dr.Users_Id).FirstName;
                usrRoom.RoomName = Roomdb.GetObj(dr.Rooms_Id).Name;
                user_room.Add(usrRoom);
            }
            ViewBag.UserRoom = user_room;

            return View();
        }
        //удалить из комнаты
        public JsonResult DeleteUserAndRoom(UsersRooms usersRooms)
        {
            return Json(UsersRoomsdb.Delete(UsersRoomsdb.GetObjID(usersRooms)), JsonRequestBehavior.AllowGet);
        }
        //список пользователей
        public JsonResult UserList()
        {
            //List<UsersRooms> ur = UsersRoomsdb.GetList().Where(usr=>usr.Rooms_Id!=ID).ToList();
            return Json(Userdb.GetList(), JsonRequestBehavior.AllowGet);
        }
        //список комнат
        public JsonResult RoomList()
        {
            return Json(Roomdb.GetList(), JsonRequestBehavior.AllowGet);
        }
        //список размещенных пользователей в комнатах
        public JsonResult UserRoomList()
        {
            return Json(UsersRoomsdb.GetList(), JsonRequestBehavior.AllowGet);
        }
        //отображение списка комнат
        public ActionResult HomePartialRoomList()
        {
            return PartialView();
        }
        //отображение списка пользователей
        public ActionResult HomePartialUserList()
        {
            return PartialView();
        }
        //добавление пользователя в комнату
        public JsonResult RoomAddUser(UsersRooms roomsUsers)
        {
            if (UsersRoomsdb.GetObjID(roomsUsers) == 0)
            {
                return Json(UsersRoomsdb.Create(roomsUsers), JsonRequestBehavior.AllowGet);
            }
            else { return Json(JsonRequestBehavior.AllowGet); }
        }
        // список пользователей в комнате
        public JsonResult Users_Room(int Id)
        {
            return Json(UserVRoom(Id), JsonRequestBehavior.AllowGet);
        }
        //список пользователей которых нет в этой комнате
        public JsonResult UsersNotRoom(int Id)
        {
            List<User> userVroom = UserVRoom(Id);
            List<User> user_Room = Userdb.GetList();
            user_Room = user_Room.Except(userVroom).ToList();
            return Json(user_Room.Distinct().ToList(), JsonRequestBehavior.AllowGet);

        }
        //получение списка пользователей в определеной комнате
        public List<User> UserVRoom(int Id)
        {
            List<User> user_Room = new List<User>();
            var ur = UsersRoomsdb.GetList();

            foreach (var dr in ur)
            {
                User usrRoom = new User();

                if (dr.Rooms_Id == Id)
                {
                    usrRoom = Userdb.GetObj(dr.Users_Id);
                    user_Room.Add(usrRoom);
                }

            }
            return user_Room;
        }
        //список комнат у пользователя
        public JsonResult Rooms_User(int Id)
        {
            return Json(RoomVUser(Id), JsonRequestBehavior.AllowGet);
        }
        //список комнат
        public List<Room> RoomVUser(int Id)
        {
            List<Room> rooms_User = new List<Room>();
            var rm = UsersRoomsdb.GetList();
            foreach (var dr in rm)
            {
                Room rmUser = new Room();

                if (dr.Users_Id == Id)
                {
                    rmUser = Roomdb.GetObj(dr.Rooms_Id);
                    rooms_User.Add(rmUser);
                }

            }
            return rooms_User;
        }
        //свободные комнаты 
        public JsonResult RoomNotUsers(int Id)
        {
            List<Room> roomVuser = RoomVUser(Id);
            List<Room> rooms = Roomdb.GetList();
            rooms = rooms.Except(roomVuser).ToList();

            return Json(rooms.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}