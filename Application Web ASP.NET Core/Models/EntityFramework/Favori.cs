using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Application_Web_ASP.NET_Core.Models.EntityFramework
{
    [Table("Favori")]
    public class Favori
    {
        [Column("CPT_ID")]        
        public int CompteId { get; set; }

        [Column("FLM_ID")]       
        public int FilmId { get; set; }

        [ForeignKey(nameof(Film))]
        [InverseProperty("Favoris")]
        public virtual Film FilmNavigation { get; set; }

        [ForeignKey(nameof(Compte))]
        [InverseProperty("Favoris")]
        public virtual Compte CompteNavigation { get; set; }
    }
}
