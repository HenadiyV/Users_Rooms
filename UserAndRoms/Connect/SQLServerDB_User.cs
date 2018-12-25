using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UserAndRoms.Models;

namespace UserAndRoms.Connect
{

    public class SQLServerDB_User : IRepository<User>
    {
        private EFContext db;

        public SQLServerDB_User()
        {
            try { 
            db = new EFContext();
            } catch (Exception) { db.Dispose(); }
            
        }
        public List<User> GetList()
        {
            try { 
            return db.Users.Include(r => r.Role).ToList();
            } catch (Exception) { return null; }
        }
        public IEnumerable<User> GetEnumerable()
        {
            try { 
            return db.Users;
            } catch (Exception) { return null; }
        }

        public int GetObjID(User user)
        {
            try
            {
                return db.UsersRoom.Where(US => US.Id == user.Id).FirstOrDefault().Id;
            }
            catch (NullReferenceException)
            { return 0; };
        }
        public User GetObj(int id)
        {
            try { 
            return db.Users.Find(id);
            } catch (Exception) { return null; }
        }

        public int Create(User user)
        {

            int addResult = 0;
            try
            {
                foreach (var us in db.Users)
                {
                    if (us.Email.Equals(user.Email))
                    {
                        addResult = 1;
                    }
                }
                if (addResult == 0)
                {
                    db.Users.Add(user);
                    db.SaveChanges();

                }
            }
            catch (Exception)
            {

            }
            return addResult;
        }

        public int Update(User user)
        {
            int result = 0;
            try
            {
                db.Entry(user).State = EntityState.Modified;
                Save();
            }
            catch (Exception)
            {
                result = 1;
            }
            return result;
        }

        public void Edit(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            Save();
        }

        public int Delete(int id)
        {
            int result = 0;
            try
            {
                var us = db.Users.Find(id);
                if (us != null)
                {
                    db.Users.Remove(us);
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