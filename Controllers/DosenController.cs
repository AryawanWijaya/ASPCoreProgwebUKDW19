

using System;
using ASPCoreGroupB.DAL;
using ASPCoreGroupB.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreGroupB.Controllers
{
    public class DosenController:Controller{
        private IDosen _dsn;

        public DosenController (IDosen dsn){
            _dsn = dsn;
        }
        public IActionResult index(){
            var data = _dsn.GetAll();
            return View(data);
        }
        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        public IActionResult CreatePost(Dosen dsn)
        {
            try{
                _dsn.Insert(dsn)    ;
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

        public IActionResult Details(string id){

            try{
            var data =_dsn.GetById(id);
            return View(data);
            }catch(Exception ex)
            {
                return Content($"Erorr: {ex.Message}");
            }
        }
        [HttpPost]
        public IActionResult Edit(Dosen dsn){
            try{
                _dsn.Update(dsn);
                ViewData["pesan"]=
                    "<span class='alert alert-success'>Data Mahasiswa Berhasil Diedit</span>";
                return View("Details");
            }catch (Exception ex){
                ViewData["pesan"]=
                $"<span class='alert alert-danger'> Data Gagal Diedit, {ex.Message}</span>";
                return Content(ex.Message);
            }
        }

           public IActionResult Delete(string id){
            try{
                _dsn.Delete(id);
                var data =_dsn.GetAll();
                   ViewData["pesan"]=
                    "<span class='alert alert-success'>Data Mahasiswa Berhasil Dihapus</span>";
                return View("Index",data);
            }catch (Exception ex){
                return Content($"Error: {ex.Message}");
            }
        }
    }
}