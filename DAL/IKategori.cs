

using System.Collections.Generic;
using ASPCoreGroupB.Models;

namespace ASPCoreGroupB.DAL{
    public interface IKategori
    {
        IEnumerable <Kategori> GetALl();
        Kategori GetById(string KategoriID);
        void Insert(Kategori ktr);
        void Update(Kategori ktr);
        void Delete(string id);
    }
}