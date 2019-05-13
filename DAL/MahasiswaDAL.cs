using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ASPCoreGroupB.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ASPCoreGroupB.DAL {
    public class MahasiswaDAL : IMahasiswa
    {
       private IConfiguration _config;
       public MahasiswaDAL(IConfiguration config)
        {
             _config = config;
        }
        private string GetConnStr(){
            return _config.GetConnectionString("DefaultConnection");
        }

      public IEnumerable<Mahasiswa> GetAll(){
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                //List<Mahasiswa> lstMahasiswa = new List<Mahasiswa>();
                string strSql = @"select * from Mahasiswa order by Nama";
                return conn.Query<Mahasiswa>(strSql);
                
                
                /* SqlCommand cmd = new SqlCommand(strSql,conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows){
                    while(dr.Read()){
                        var mhs = new Mahasiswa {
                            Nim = dr["Nim"].ToString(),
                            Nama = dr["Nama"].ToString(),
                            Email = dr["Email"].ToString(),
                            Telp = dr["Telp"].ToString()
                        };
                        lstMahasiswa.Add(mhs);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
                return lstMahasiswa;*/
            }
        }

        public List<Mahasiswa> getAll()
        {
            throw new System.NotImplementedException();
        }

        public void Insert(Mahasiswa mhs)
        {
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                string strSql = @"insert into Mahasiswa (Nim,Nama,Email,Telp)
                values (@Nim,@Nama,@Email,@Telp) "; 

                try{
                    var param = new {Nim=mhs.Nim,Nama=mhs.Nama,Email=mhs.Email,Telp=mhs.Telp};
                    conn.Execute(strSql,param);
                }
                catch(SqlException sqlEx){
                    throw new Exception ($"Error: {sqlEx.Message}"); 
                }
            }
        }

        public void Update(Mahasiswa mhs)
        {
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                var strSql =@"update Mahasiswa set Nama=@Nama, Email=@Email, Telp=@Telp
                            where Nim=@Nim";
                try{
                    var param=new{Nama=mhs.Nama,Email=mhs.Email,Telp=mhs.Telp,Nim=mhs.Nim};
                    conn.Execute(strSql,param);
                }catch(SqlException sqlEx)
                {
                    throw new Exception($"Error: {sqlEx.Message}");
                }
            }
        }

        public Mahasiswa GetById(string nim)
        {
            using(SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                var strSql = @"select * from Mahasiswa
                                where Nim=@Nim"; //@ untuk membuktikan bahwa itu jadi 1 string meskipun dienter,klo tdk nti klo dienter error
                var param = new {Nim=nim};
                var result = conn.QuerySingleOrDefault<Mahasiswa>(strSql, param);
                if(result==null)
                    throw new Exception("Error: data tidak ditemukan !");
                else
                    return result;
            }
        }
        public void Delete(string nim)
        {
            using (SqlConnection conn = new SqlConnection(GetConnStr())){
                 var strSql=@"delete from Mahasiswa Where Nim=@Nim";
                 try{
                    var param = new{nim=nim};
                    conn.Execute(strSql,param);
                 }catch(SqlException sqlEx){
                        throw new Exception($"Error: {sqlEx.Message}");
                 }
            }
        }

        public IEnumerable<Mahasiswa> GetAllByNim(string nim){
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                string strSql = @"select * from Mahasiswa where Nim=@Nim 
                                  order by Nama";
                var param = new {nim=nim};
                return conn.Query<Mahasiswa>(strSql,param);
            }
        }

         public IEnumerable<Mahasiswa> GetAllByName(string name){
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                string strSql = @"select * from Mahasiswa where Nama like @Nama
                                  order by Nama";
                var param = new {Nama="%"+name+"%"};
                return conn.Query<Mahasiswa>(strSql,param);
            }
        }


        
    }
   
}