


using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ASPCoreGroupB.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ASPCoreGroupB.DAL{
    public class KategoriDAL : IKategori
    {
        private IConfiguration _config;

        public KategoriDAL(IConfiguration config)
        {
            _config = config;
        }

        private string GetConnStr(){
            return _config.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Kategori> GetALl()
        {
            using (SqlConnection conn= new SqlConnection(GetConnStr())){
                string strsql= @"select * from Kategori order by KategoriID";
                return conn.Query<Kategori>(strsql);
            }
        }
          public void Insert(Kategori ktr)
        {
            using (SqlConnection conn = new SqlConnection(GetConnStr())){
                string strsql = @"insert into Kategori (KategoriID,KategoriName) values (@KategoriID,@KategoriName)";
                try {
                    var param = new {KategoriID=ktr.KategoriID,KategoriName=ktr.KategoriName};
                    conn.Execute(strsql,param);
                }catch (SqlException sqlEx){
                    throw new Exception($"Error: {sqlEx.Message}");
                }
            }
        }
        public Kategori GetById(string kategoriID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnStr())){
                var strsql = @"select * from Kategori Where KategoriID = @KategoriID";
                var param = new {KategoriID=kategoriID};
                var result = conn.QuerySingleOrDefault<Kategori>(strsql, param);
                if ( result==null)
                    throw new Exception("Error: Data tidak ditemukan !");
                else
                    return result;
            }
        }
        public void Update(Kategori ktr)
        {
            using (SqlConnection conn = new SqlConnection(GetConnStr())){
                var strsql = @"update Kategori set KategoriName=@KategoriName
                where KategoriID=@KategoriID";
                try {
                    var param= new {KategoriName=ktr.KategoriName, KategoriID=ktr.KategoriID};
                    conn.Execute(strsql,param);
                }catch(SqlException sqlEx){
                    throw new Exception($"Error: {sqlEx.Message}");
                }
            }
        }

        public void Delete(string kategoriID)
        {
             using (SqlConnection conn = new SqlConnection(GetConnStr())){
                string strsql= @"delete from Kategori Where KategoriID=@KategoriID";
                try{
                    var param = new {KategoriID=kategoriID};
                    conn.Execute(strsql,param);
                }catch (SqlException sqlEx){
                    throw new Exception($"Error: {sqlEx.Message}");
                }
            }
        }
    }
}