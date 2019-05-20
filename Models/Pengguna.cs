
using System.ComponentModel.DataAnnotations;

namespace ASPCoreGroupB.Models{

    public class Pengguna {
        [Required (ErrorMessage="Username Harus Diisi!")] //data annotation cari di internet
        public string Username { get; set; }

        [Required]
        [StringLength(30,MinimumLength=8)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string Repassword { get; set; }

        [Required]
        public string Aturan { get; set; }
        public string Nama { get; set; }
        public string Telp { get; set; }
    }
}