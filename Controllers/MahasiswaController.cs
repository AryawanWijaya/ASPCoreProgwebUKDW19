

using System;
using System.Collections.Generic;
using ASPCoreGroupB.DAL;
using ASPCoreGroupB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreGroupB.Controllers{
    public class MahasiswaController:Controller{
        private IMahasiswa _mhs;

        public MahasiswaController (IMahasiswa mhs){
            _mhs = mhs;
        }

          private bool IsLogin(){
            if(HttpContext.Session.GetString("username")==null){
                return false;
            }else {
                return true;
            }
        }

        public IActionResult Index()
        {
            if(!IsLogin()){
                TempData["pesan"] = "<span class='alert alert-danger'>Silahkan Login terlebih dahulu untuk mengakses halaman mahasiswa.</span>";
                return RedirectToAction("Login","Pengguna");
            }

            var data = _mhs.GetAll();
            return View(data);
        }

        public IActionResult Create(){
            return View();
        }

        public IActionResult Delete(string id){
            try{
                _mhs.Delete(id);
                var data =_mhs.GetAll();
                   ViewData["pesan"]=
                    "<span class='alert alert-success'>Data Mahasiswa Berhasil Dihapus</span>";
                return View("Index",data);
            }catch (Exception ex){
                return Content($"Error: {ex.Message}");
            }
        }
        public IActionResult Details(string id){

            try{
            var data =_mhs.GetById(id);
            return View(data);
            }catch(Exception ex)
            {
                return Content($"Erorr: {ex.Message}");
            }
        }

        
        [HttpPost]
           public IActionResult Search(string keyword, string cari){
            IEnumerable<Mahasiswa> data;
            if(cari=="Nim")
            {
                data = _mhs.GetAllByNim(keyword);
            }
            else if(cari=="Nama")
            {
                data = _mhs.GetAllByName(keyword);
            }
            else
            {
                data= _mhs.GetAll();
            }
            return View("Index",data);
        }

        // [HttpPost]
        // public IActionResult SearchName(string name){
        //     var data = _mhs.GetAllByName(name);
        //     // return Content(data);
        //     return View ("Index",data);
        // }

        [HttpPost]
        public IActionResult Edit(Mahasiswa mhs){
            try{
                _mhs.Update(mhs);
                ViewData["pesan"]=
                    "<span class='alert alert-success'>Data Mahasiswa Berhasil Diedit</span>";
                return View("Details");
            }catch (Exception ex){
                ViewData["pesan"]=
                $"<span class='alert alert-danger'> Data Gagal Diedit, {ex.Message}</span>";
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreatePost(Mahasiswa mhs)
        {
            try{
                _mhs.Insert(mhs)    ;
                ViewData["pesan"]=
                    "<span class='alert alert-success'>Data Mahasiswa Berhasil Ditambah</span>";
                return View("Create");
            }
            catch(Exception ex){
                ViewData["pesan"]=
                $"<span class='alert alert-danger'> Data Gagal Ditambah, {ex.Message}</span>";
                return View("Create");
            }
        }
    }
}