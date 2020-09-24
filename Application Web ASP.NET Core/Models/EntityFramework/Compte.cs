using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Application_Web_ASP.NET_Core.Models.EntityFramework
{
    [Table("compte")]
    public partial class Compte
    {
        [Key]
        [Column("CPT_ID")]
        public int Id { get; set; }

        [Column("CPT_NOM")]
        [MaxLength(50)]
        [NotNull]
        public string Nom { get; set; }

        [Column("CPT_PRENOM")]
        [MaxLength(50)]
        [NotNull]
        public string Prenom { get; set; }

        [Column("CPT_TELPORTABLE", TypeName = "char(10)")]
        public string TelPortable { get; set; }

        [Column("CPT_MEL")]
        [MaxLength(100)]
        public string Mel { get; set; }

        [Column("CPT_PWD")]
        [MaxLength(64)]
        public string Pwd { get; set; }

        [Column("CPT_RUE")]
        [MaxLength(200)]
        public string Rue { get; set; }

        [Column("CPT_CP", TypeName = "char(5)")]
        public string CodePostal { get; set; }

        [Column("CPT_VILLE", TypeName = "char(50)")]
        public string Ville { get; set; }

        [Column("CPT_PAYS", TypeName = "char(50)")]
        [DefaultValue("France")]
        public string Pays { get; set; }

        [Column("CPT_LATITUDE")]
        public float? Latitude { get; set; }

        [Column("CPT_LONGITUDE")]
        public float? Longitude { get; set; }

        [Column("CPT_DATECREATION")]
        [NotNull]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateCreation { get; set; }

        [InverseProperty("CompteNavigation")]
        public virtual ICollection<Favori> Favoris { get; set; }
    }
}
