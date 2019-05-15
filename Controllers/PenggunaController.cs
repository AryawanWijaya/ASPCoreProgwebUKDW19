using System;
using System.Collections.Generic;
using ASPCoreGroupB.DAL;
using ASPCoreGroupB.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreGroupB.Controllers
{
    public class PenggunaController : Controller{
        private IPengguna _pengguna;
        public PenggunaController(IPengguna pengguna){
            _pengguna = pengguna;
        }

        public IActionResult Registrasi(){
            return View();
        }

        [HttpPost]
        public IActionResult RegistrasiPost(Pengguna pengguna){
            try{
                _pengguna.Insert(pengguna);
                ViewData["pesan"]=
                    "<span class='alert alert-success'>Data Pengguna Berhasil Ditambah</span>";
                return View("Registrasi");
            }
            catch(Exception ex){
                ViewData["pesan"]=
                $"<span class='alert alert-danger'> Data Gagal Ditambah, {ex.Message}</span>";
                return View("Registrasi");
            }
        }
    }
}