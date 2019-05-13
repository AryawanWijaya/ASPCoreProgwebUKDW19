
using System.Collections.Generic;
using ASPCoreGroupB.Models;

namespace ASPCoreGroupB.DAL
{
    public interface IDosen
    {
        IEnumerable<Dosen> GetAll();
        Dosen GetById(string nik);
        void Insert(Dosen dsn);
        void Update(Dosen dsn);
        void Delete(string nik);
    }
    
}