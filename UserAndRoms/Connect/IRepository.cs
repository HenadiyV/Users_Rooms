using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAndRoms.Connect
{
    interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetEnumerable();
        List<T> GetList();
        T GetObj(int id);
        int GetObjID(T obj);
        int Create(T obj);
        int Update(T obj);
        int Delete(int id);
        void Save();
    }
}
