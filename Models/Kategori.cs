

using System.ComponentModel.DataAnnotations;

namespace ASPCoreGroupB.Models{
    public class Kategori {
        [Required]
        public string KategoriID{ get; set;}
        [Required]
        public string KategoriName{ get; set;}
    }
}