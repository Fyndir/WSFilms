using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application_Web_ASP.NET_Core.Models.EntityFramework
{
    [Table("T_J_FAVORI_FAV")]
    public class Favori
    {
        [Column("CPT_ID")]        
        public int CompteId { get; set; }

        [Column("FLM_ID")]       
        public int FilmId { get; set; }

        [ForeignKey("FilmId")]
        [InverseProperty("Favoris")]        
        public virtual Film FilmNavigation { get; set; }

        [ForeignKey("CompteId")]
        [InverseProperty("Favoris")]
        [JsonIgnore]
        public virtual Compte CompteNavigation { get; set; }
    }
}
