using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSFILM.Model
{
    public class Favori
    {
        public int CompteId { get; set; }

        public int FilmId { get; set; }

        public virtual Film FilmNavigation { get; set; }

        public virtual Compte CompteNavigation { get; set; }

        public Favori()
        {

        }
    }
}
