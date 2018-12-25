using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UserAndRoms.Models;

namespace UserAndRoms.Connect
{
    public class SQLServerDB_UsersRooms : IRepository<UsersRooms>
    {
        private EFContext db;

        public SQLServerDB_UsersRooms()
        {
            try
            {
                db = new EFContext();
            }
            catch (Exception) { db.Dispose(); }
        }
        public List<UsersRooms> GetList()
        {
           try { 
            return db.UsersRoom.ToList();
            } catch (Exception) { return null; }
        }
        public IEnumerable<UsersRooms> GetEnumerable()
        {
            try {
            return db.UsersRoom;
            } catch (Exception) { return null; }
        }

        public UsersRooms GetObj(int id)
        {
            try { 
            return db.UsersRoom.Find(id);
            } catch (Exception) { return null;}
        }
        public int GetObjID(UsersRooms userRoom)
        {
            try
            {
                return db.UsersRoom.Where(UsrRm => UsrRm.Rooms_Id == userRoom.Rooms_Id && UsrRm.Users_Id == userRoom.Users_Id).FirstOrDefault().Id;
            }
            catch (NullReferenceException)
            { return 0; };

        }

        public int Create(UsersRooms userRoom)
        {

            int addResult = 0;
            try
            {
                db.UsersRoom.Add(userRoom);
                db.SaveChanges();
            }
            catch (Exception)
            {
                addResult = 1;
            }
            return addResult;
        }

        public int Update(UsersRooms userRoom)
        {
            int result = 0;
            try
            {
                db.Entry(userRoom).State = EntityState.Modified;
                Save();
            }
            catch (Exception)
            {
                result = 1;
            }
            return result;
        }

        public void Edit(UsersRooms userRoom)
        {
            db.Entry(userRoom).State = EntityState.Modified;
            Save();
        }

        public int Delete(int id)
        {
            int result = 0;
            try
            {
                var us = db.UsersRoom.Find(id);
                if (us != null)
                {
                    db.UsersRoom.Remove(us);
                    Save();
                }
            }
            catch (Exception)
            {
                result = 1;
            }
            return result;
        }

        public void Save()
        {
            db.SaveChanges();
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}