using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSFILM.Model
{
    public class Film
    {
        public int Id { get; set; }

        public string Titre { get; set; }

        public string Synopsis { get; set; }

        public string DateParution { get; set; }

        public decimal Duree { get; set; }

        public string Genre { get; set; }

        public string UrlPhoto { get; set; }

        public virtual ICollection<Favori> Favoris { get; set; }

        public Film()
        {

        }
    }
}
