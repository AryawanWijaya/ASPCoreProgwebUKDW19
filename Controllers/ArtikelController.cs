

using System;
using System.Collections.Generic;
using System.IO;
using ASPCoreGroupB.DAL;
using ASPCoreGroupB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreGroupB.Controllers{
    public class ArtikelController:Controller{
        private IArtikel _art;

        public ArtikelController (IArtikel art){
            _art = art;
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
        public IActionResult Index(){
            var data = _art.GetAll();
            return View(data);
        }

        public IActionResult Search (string keyword, string cari){
            IEnumerable<Artikel> data;
            if (cari=="Judul")
            {
                data=_art.GetAllByJudul(keyword);
            }
            else if(cari=="Isi")
            {
                data = _art.GetAllByIsi(keyword);
            }
            else if(cari=="Username")
            {
                data=_art.GetAllByUsername(keyword);
            }
            else
            {
                data = _art.GetAll();
            }
            return View("Index",data);
        }

        public IActionResult Create(){
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
        public async System.Threading.Tasks.Task<IActionResult> CreatePost(Artikel art, IFormFile file){
            try {
                if (file == null || file.Length == 0)
                    // return Content("data belom ditambahkan");
                    art.Gambar="-";
                else {
                var path = Path.Combine(
                            Directory.GetCurrentDirectory(), Path.Combine("wwwroot","fotoArtikel"),
                            file.FileName);
                // return Content(path);
                art.Gambar=file.FileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                }
                _art.Insert(art);
                ViewData["pesan"]=
                "<span class='alert alert-success'>Data Artikel Berhasil Ditambah</span>";
                return View("Create");
            }catch(Exception ex){
                ViewData["pesan"]=
                $"<span class='alert alert-danger'> Data Gagal Ditambah, {ex.Message}</span>";
                return View("Create");
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
                // string gambar = _art.getGambar(id).ToString();
                // return Content(gambar);
                // if(gambar=="-"){
                    _art.Delete(id);
                    var data =_art.GetAll();
                    ViewData["pesan"]=
                    "<span class='alert alert-success'>Data Artikel Berhasil Dihapus</span>";
                    return View("Index",data);
                // }else{
                    
                //      System.IO.File.Delete("wwwroot/fotoArtikel/"+gambar);
                //      _art.Delete(id);
                //     var data =_art.GetAll();
                //     ViewData["pesan"]=
                //     "<span class='alert alert-success'>Data Artikel Berhasil Dihapus</span>";
                //     return View("Index",data);
                // }
            }catch (Exception ex){
                return Content($"Error: {ex.Message}");
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
                var data= _art.GetById(id);
                return View(data);
            }catch(Exception ex){
                return Content ($"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> Edit(Artikel art, IFormFile file){{
            try {
                if (file == null || file.Length == 0)
                    // return Content("data belom ditambahkan");
                    art.Gambar="-";
                else {
                var path = Path.Combine(
                            Directory.GetCurrentDirectory(), Path.Combine("wwwroot","fotoArtikel"),
                            file.FileName);
                // return Content(path);
                art.Gambar=file.FileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                }
                _art.Update(art);
                ViewData["pesan"]=
                    "<span class='alert alert-success'>Data Kategori Berhasil Diedit</span>";
                return View("Details");
            }catch (Exception ex){
                ViewData["pesan"]=
                $"<span class='alert alert-danger'> Data Gagal Diedit, {ex.Message}</span>";
                return View("Details");
            }
        }
    }
    }
}