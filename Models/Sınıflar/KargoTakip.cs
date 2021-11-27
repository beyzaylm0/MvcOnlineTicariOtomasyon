using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Sınıflar
{
    public class KargoTakip
    {  [Key]
        public int KargoTakiId { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string TakipKodu { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(300)]
        public string Aciklama { get; set; }
        public DateTime TarihZaman { get; set; }


    }
}