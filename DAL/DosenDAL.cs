using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ASPCoreGroupB.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ASPCoreGroupB.DAL
{
    public class DosenDAL : IDosen 
    {
         private IConfiguration _config;
        public DosenDAL(IConfiguration config)
        {
            _config = config;
        }
        private string GetConnStr()
        {
            return _config.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Dosen> GetAll(){
        using (SqlConnection conn = new SqlConnection(GetConnStr())){
            string strSql = @"select * from Dosen order by Nama";
            return conn.Query<Dosen>(strSql);
            }
        }


        public List<Dosen> getAll()
        {
            throw new System.NotImplementedException();
        }

        public Dosen GetById(string nik)
        {
            using(SqlConnection conn = new SqlConnection (GetConnStr())){
                var strSql =@"select * from Dosen where Nik=@Nik";
                var param = new{nik=nik};
                var result = conn.QuerySingleOrDefault<Dosen>(strSql,param);
                if(result==null)
                    throw new Exception("Error: data tidak ditemukan !");
                else
                    return result;
            }
        }

        public void Insert(Dosen dsn)
        {
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                string strSql=@"insert into  Dosen (Nik,Nama,Alamat,Jurusan)
                values (@Nik,@Nama,@Alamat,@Jurusan)";
                try{
                    var param = new {Nik=dsn.Nik,Nama=dsn.Nama,Alamat=dsn.Alamat,Jurusan=dsn.Jurusan};
                    conn.Execute(strSql,param);
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error: {sqlEx.Message}");
                }
            }
        }

        public void Update(Dosen dsn)
        {
              using(SqlConnection conn = new SqlConnection(GetConnStr())){
                var strSql =@"update Dosen set Nama=@Nama, Alamat=@Alamat, Jurusan=@Jurusan
                            where Nik=@Nik";
                try{
                    var param=new{Nama=dsn.Nama,Alamat=dsn.Alamat,Jurusan=dsn.Jurusan,Nik=dsn.Nik};
                    conn.Execute(strSql,param);
                }catch(SqlException sqlEx)
                {
                    throw new Exception($"Error: {sqlEx.Message}");
                }
            }
        }

        public void Delete(String nik)
        {
            using (SqlConnection conn = new SqlConnection(GetConnStr())){
                 var strSql=@"delete from Dosen Where Nik=@Nik";
                 try{
                    var param = new{Nik=nik};
                    conn.Execute(strSql,param);
                 }catch(SqlException sqlEx){
                        throw new Exception($"Error: {sqlEx.Message}");
                 }
            }
        }
    }
}

