
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ASPCoreGroupB.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ASPCoreGroupB.DAL{
    public class ArtikelDAL : IArtikel
    {
        private IConfiguration _config;
        public ArtikelDAL(IConfiguration config)
        {
             _config = config;
        }
         private string GetConnStr(){
            return _config.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Artikel> GetAll(){
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                //List<Mahasiswa> lstMahasiswa = new List<Mahasiswa>();
                string strSql = @"select * from Artikel order by ArtikelID";
                return conn.Query<Artikel>(strSql);
            }
        }
        public void Delete(string ArtikelID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnStr())){
                string strsql= @"delete from Artikel Where ArtikelID=@ArtikelID";
                try{
                    var param = new {ArtikelID=ArtikelID};
                    conn.Execute(strsql,param);
                }catch (SqlException sqlEx){
                    throw new Exception($"Error: {sqlEx.Message}");
                }
            }
        }
 
        public Artikel GetById(string ArtikelID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnStr())){
                var strsql = @"select * from Artikel Where ArtikelID = @ArtikelID";
                var param = new {ArtikelID=ArtikelID};
                var result = conn.QuerySingleOrDefault<Artikel>(strsql, param);
                if ( result==null)
                    throw new Exception("Error: Data tidak ditemukan !");
                else
                    return result;
            }
        }
        //  public IEnumerable<Artikel> getGambar(string ArtikelID)
        // {
        //     using (SqlConnection conn = new SqlConnection(GetConnStr())){
        //         var strsql = @"select Gambar from Artikel Where ArtikelID = @ArtikelID";
        //         var param = new {ArtikelID=ArtikelID};
        //         // var result = conn.QuerySingleOrDefault<Artikel>(strsql, param);
        //         return conn.Query<Artikel>(strsql,param);
        //     }
        // }

        public void Insert(Artikel art)
        { 
            using (SqlConnection conn = new SqlConnection(GetConnStr())){
                string strsql = @"insert into Artikel (ArtikelID,KategoriID,Judul,Berita,Tanggal,Gambar,Username)
                values (@ArtikelID,@KategoriID,@Judul,@Berita,@Tanggal,@Gambar,@Username)";
                try {
                    var param = new {ArtikelID=art.ArtikelID, KategoriID=art.KategoriID,Judul=art.Judul,Berita=art.Berita,Tanggal=art.Tanggal,Gambar=art.Gambar,Username=art.Username};
                    conn.Execute(strsql,param);
                }catch (SqlException sqlEx){
                    throw new Exception($"Error: {sqlEx.Message}");
                }
            }
        }

        public void Update(Artikel art)
        {
           using (SqlConnection conn = new SqlConnection(GetConnStr())){
                var strsql = @"update Artikel set KategoriID=@KategoriID, Judul=@Judul, Berita=@Berita,
                Tanggal=@Tanggal, Gambar=@Gambar, Username=@Username where ArtikelID=@ArtikelID";
                try {
                    var param= new {KategoriID=art.KategoriID, Judul=art.Judul, Berita=art.Berita,
                    Tanggal=art.Tanggal,Gambar=art.Gambar,Username=art.Username, ArtikelID=art.ArtikelID};
                    conn.Execute(strsql,param);
                }catch(SqlException sqlEx){
                    throw new Exception($"Error: {sqlEx.Message}");
                }
            }
        }

        public IEnumerable<Artikel> GetAllByJudul(string judul)
        {
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                string strSql = @"select * from Artikel where Judul like @Judul
                                  order by Judul";
                var param = new {Judul="%"+judul+"%"};
                return conn.Query<Artikel>(strSql,param);
            }
        }

        public IEnumerable<Artikel> GetAllByIsi(string berita)
        {
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                string strSql = @"select * from Artikel where Berita like @Berita
                                  order by Judul";
                var param = new {Berita="%"+berita+"%"};
                return conn.Query<Artikel>(strSql,param);
            }
        }

        public IEnumerable<Artikel> GetAllByUsername(string username)
        {
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                string strSql = @"select * from Artikel where Username like @Username
                                  order by Judul";
                var param = new {Username="%"+username+"%"};
                return conn.Query<Artikel>(strSql,param);
            }
        }

       
    }
}