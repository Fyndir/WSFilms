using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Application_Web_ASP.NET_Core.Models.EntityFramework
{
    [Table("T_E_FILM_FLM")]
    public class Film
    {
        [Key]
        [Column("FLM_ID")]
        public int Id { get; set; }

        [Column("FLM_TITRE")]
        [MaxLength(100)]
        [NotNull]
        public string Titre { get; set; }

        [Column("FLM_SYNOPSIS")]
        [MaxLength(500)]
        [AllowNull]
        public string Synopsis { get; set; }

        [Column("FLM_DATEPARUTION")]
        [NotNull]
        public string DateParution { get; set; }

        [Column("FLM_DUREE", TypeName = "numeric(3,0)")]
        [NotNull]
        public decimal Duree { get; set; }

        [Column("FLM_GENRE")]
        [MaxLength(30)]
        [NotNull]
        public string Genre { get; set; }

        [Column("FLM_URLPHOTO")]
        [MaxLength(200)]
        public string UrlPhoto { get; set; }

        [InverseProperty("FilmNavigation")]
        public virtual ICollection<Favori> Favoris { get; set; }
    }
}
