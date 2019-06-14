

using System;
using ASPCoreGroupB.DAL;
using ASPCoreGroupB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreGroupB.Controllers{
    public class KategoriController:Controller{
        private IKategori _ktr;

        public KategoriController(IKategori ktr){
            _ktr=ktr;
        }

         private bool IsLogin(){
            if(HttpContext.Session.GetString("username")==null){
                return false;
            }else {
                return true;
            }
        }
        private bool cekPengguna(){
                if(HttpContext.Session.GetString("aturan")=="Admin"){
                return true;
                }else{
                return false;
                }
            }

         private bool CekAturan(string aturan)
        {
            if (HttpContext.Session.GetString("aturan") != null &&
            HttpContext.Session.GetString("aturan") == aturan)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public IActionResult index(){
            var data = _ktr.GetALl();
            return View(data);
        }

        public IActionResult create(){
             if(!IsLogin()){
                TempData["pesan"] = "<span class='alert alert-danger'>Silahkan Login terlebih dahulu untuk mengakses halaman mahasiswa.</span>";
                return RedirectToAction("Login","Pengguna");
            }else {
                if (!CekAturan("Admin"))
                {
                    TempData["pesan"] = "<span class='alert alert-danger'>Silahkan login sebagai admin untuk create mahasiswa</span>";
                    return RedirectToAction("Login", "Pengguna");
                }

            }
               return View();
        }

        [HttpPost]
        public IActionResult CreatePost(Kategori ktr){
            try {
                _ktr.Insert(ktr);
                ViewData["pesan"]=
                "<span class='alert alert-success'>Data Kategori Berhasil Ditambah</span>";
                return View("Create");
            }catch(Exception ex){
                ViewData["pesan"]=
                $"<span class='alert alert-danger'> Data Gagal Ditambah, {ex.Message}</span>";
                return View("Create");
            }
        }

        public IActionResult Details(string id){
             if(!IsLogin()){
                TempData["pesan"] = "<span class='alert alert-danger'>Silahkan Login terlebih dahulu untuk mengakses halaman mahasiswa.</span>";
                return RedirectToAction("Login","Pengguna");
            }else {
                if (!CekAturan("Admin"))
                {
                    TempData["pesan"] = "<span class='alert alert-danger'>Silahkan login sebagai admin untuk create mahasiswa</span>";
                    return RedirectToAction("Login", "Pengguna");
                }

            }

            try {
                var data= _ktr.GetById(id);
                return View(data);
            }catch(Exception ex){
                return Content ($"Error: {ex.Message}");
            }
        }
        [HttpPost]
        public IActionResult Edit(Kategori ktr){
            try {
                _ktr.Update(ktr);
                ViewData["pesan"]=
                    "<span class='alert alert-success'>Data Kategori Berhasil Diedit</span>";
                return View("Details");
            }catch (Exception ex){
                ViewData["pesan"]=
                $"<span class='alert alert-danger'> Data Gagal Diedit, {ex.Message}</span>";
                return View("Details");
            }
        }

        public IActionResult Delete(string id){
            if(!IsLogin()){
                TempData["pesan"] = "<span class='alert alert-danger'>Silahkan Login terlebih dahulu untuk mengakses halaman mahasiswa.</span>";
                return RedirectToAction("Login","Pengguna");
            }else {
                if (!CekAturan("Admin"))
                {
                    TempData["pesan"] = "<span class='alert alert-danger'>Silahkan login sebagai admin untuk create mahasiswa</span>";
                    return RedirectToAction("Login", "Pengguna");
                }

            }
               
            try {
                _ktr.Delete(id);
                 var data =_ktr.GetALl();
                   ViewData["pesan"]=
                    "<span class='alert alert-success'>Data Artikel Berhasil Dihapus</span>";
                return View("Index",data);
            }catch (Exception ex){
                return Content($"Error: {ex.Message}");
            }
        }
        
    }
}