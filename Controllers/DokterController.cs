using System.Collections.Generic;
using ASPCoreGroupB.Models;
using Microsoft.AspNetCore.Mvc;
namespace ASPCoreGroupB.Controllers
{
    public class DokterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DaftarDokter()
        {
            List<Dokter> lstDokter = new List<Dokter>();
            lstDokter.Add(
                new Dokter {
                    FirstName = "Ary",
                    LastName = "Wijaya",
                    Address = "Kudus",
                    Telp = "123345678"
                }
            );
            lstDokter.Add(
                new Dokter {
                    FirstName = "Argo",
                    LastName = "Dwipangga",
                    Address = "Semarang",
                    Telp = "1234"
                }
            );
             lstDokter.Add(
                new Dokter {
                    FirstName = "Argo",
                    LastName = "Lawu",
                    Address = "Jogja",
                    Telp = "888888"
                }
            );

            return View(lstDokter);
        }

        public IActionResult Tampil(Dokter dokter)
        {
            // return Content($"Firstname: {dokter.FirstName}, LastName: {dokter.LastName}, Address: {dokter.Address}, Telp: {dokter.Telp}");
            Dokter Model = dokter;
            return View(Model);
        }
    }
}
