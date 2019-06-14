

using System.Collections.Generic;
using ASPCoreGroupB.Models;

namespace ASPCoreGroupB.DAL{
    public interface IArtikel
    {
        IEnumerable<Artikel> GetAll();

        Artikel GetById(string ArtikelID);
        void Insert(Artikel art);
        void Update(Artikel art);
        void Delete(string ArtikelID);
        //  IEnumerable<Artikel> getGambar(string ArtikelID);

        IEnumerable<Artikel> GetAllByJudul(string judul);
        IEnumerable<Artikel> GetAllByIsi(string isi);
        IEnumerable<Artikel> GetAllByUsername(string username);
    }
}