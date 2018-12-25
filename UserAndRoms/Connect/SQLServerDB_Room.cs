using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UserAndRoms.Models;

namespace UserAndRoms.Connect
{
    public class SQLServerDB_Room : IRepository<Room>
    {
        private EFContext db;
        public SQLServerDB_Room()
        {
            try
            {
                db = new EFContext();
            }
            catch (Exception) { db.Dispose(); }
        }
        public List<Room> GetList()
        {
            try { 
            return db.Rooms.ToList();
            } catch (Exception) {return null; }
        }
        public IEnumerable<Room> GetEnumerable()
        {
            try { 
            return db.Rooms;
            } catch (Exception) { return null; }
        }
        public int GetObjID(Room room)
        {
            try
            {
                return db.UsersRoom.Where(Rm => Rm.Id == room.Id).FirstOrDefault().Id;
            }
            catch (NullReferenceException)
            { return 0; };
        }
        public Room GetObj(int id)
        {
            try { 
            return db.Rooms.Find(id);
            } catch (Exception) { return null; }
        }

        public int Create(Room room)
        {
            int addResult = 0;
            foreach (var rm in db.Rooms)
            {
                if (rm.Name.Equals(room.Name))
                {
                    addResult = 1;
                }
            }
            if (addResult == 0)
            {
                db.Rooms.Add(room);
                db.SaveChanges();

            }
            return addResult;
        }

        public int Update(Room room)
        {
            int result = 0;
            try
            {
                db.Entry(room).State = EntityState.Modified;
                Save();
            }
            catch (Exception)
            {
                result = 1;
            }
            return result;

        }

        public void Edit(Room room)
        {
            db.Entry(room).State = EntityState.Modified;
            Save();
        }

        public int Delete(int id)
        {
            int result = 0;
            try
            {
                var rm = db.Rooms.Find(id);
                if (rm != null)
                {
                    db.Rooms.Remove(rm);
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