using System.Linq;
using System.Web.Mvc;
using UserAndRoms.Connect;
using UserAndRoms.Models;
namespace UserAndRoms.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        IRepository<User> Userdb;
        IRepository<Room> Roomdb;
        IRepository<UsersRooms> UsersRoomsdb;
        public AdminController()
        {
            Userdb = new SQLServerDB_User();
            Roomdb = new SQLServerDB_Room();
            UsersRoomsdb = new SQLServerDB_UsersRooms();
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        #region User 
        // отображение страницы редактирования пользователей 
        public ActionResult AdminPartialUser()
        {
            return PartialView();
        }
        // список пользователей
        public JsonResult ListUser()
        {
            return Json(Userdb.GetList(), JsonRequestBehavior.AllowGet);
        }
        //добавить пользователя
        public JsonResult AddUser(User user)
        {
            return Json(Userdb.Create(user), JsonRequestBehavior.AllowGet);
        }
        //возвращаем пользоателя по ID 
        public JsonResult GetbyIDUser(int ID)
        {
            return Json(Userdb.GetObj(ID), JsonRequestBehavior.AllowGet);
        }
        //редактирование пользователя
        public JsonResult UpdateUser(User user)
        {
            return Json(Userdb.Update(user), JsonRequestBehavior.AllowGet);
        }
        //удаление пользователя
        public JsonResult DeleteUser(int ID)
        {
            var user = UsersRoomsdb.GetList().Where(us => us.Users_Id == ID).ToList();
            if (user.Count != 0)
            {
                for (int i = 0; i < user.Count; i++)
                {
                    UsersRoomsdb.Delete(user[i].Id);
                }
            }
            return Json(Userdb.Delete(ID), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Room
        //отображение страницы редактирования комнат
        public ActionResult AdminPartialRoom()
        {
            return PartialView();
        }
        //список комнат
        public JsonResult ListRoom()
        {
            return Json(Roomdb.GetList(), JsonRequestBehavior.AllowGet);
        }
        //добавить комнату
        public JsonResult AddRoom(Room room)
        {
            return Json(Roomdb.Create(room), JsonRequestBehavior.AllowGet);
        }
        // возвращаем по ID 
        public JsonResult GetbyIDRoom(int ID)
        {
            return Json(Roomdb.GetObj(ID), JsonRequestBehavior.AllowGet);
        }
        // редактирование
        public JsonResult UpdateRoom(Room room)
        {
            return Json(Roomdb.Update(room), JsonRequestBehavior.AllowGet);
        }
        //удаление
        public JsonResult DeleteRoom(int ID)
        {
            var room = UsersRoomsdb.GetList().Where(rm => rm.Rooms_Id == ID).ToList();
            if (room.Count != 0)
            {
                for (int i = 0; i < room.Count; i++)
                {
                    UsersRoomsdb.Delete(room[i].Id);
                }
            }
            return Json(Roomdb.Delete(ID), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}