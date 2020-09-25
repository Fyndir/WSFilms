using Application_Web_ASP.NET_Core.Models.EntityFramework;
using Application_Web_ASP.NET_Core.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application_Web_ASP.NET_Core.Models.DataManager
{
    public class CompteManager : IDatarepository<Compte>
    {

        readonly FilmsDBContext _filmsDBContext;

        /// <summary>
        /// Contructeur pour l'injection de dépendance
        /// </summary>
        /// <param name="context"></param>
        public CompteManager(FilmsDBContext context)
        {
            _filmsDBContext = context;
        }

        /// <summary>
        /// Ajoute le compte dans la bdd
        /// </summary>
        /// <param name="entity"></param>
        public async Task Add(Compte entity)
        {
            _filmsDBContext.Compte.Add(entity);
            await _filmsDBContext.SaveChangesAsync();
        }

        /// <summary>
        /// Supprime de la bdd le compte en parametre
        /// </summary>
        /// <param name="compte"></param>
        public async Task Delete(Compte compte)
        {
            _filmsDBContext.Compte.Remove(compte);
            await _filmsDBContext.SaveChangesAsync();
        }

        /// <summary>
        /// retourne tous les comptes present dans la bdd
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult<IEnumerable<Compte>>> GetAll()
        {
            return await _filmsDBContext.Compte.ToListAsync();
        }

        /// <summary>
        /// Retourne le compte lié a l'id en parametre
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult<Compte>> GetById(int id)
        {
            return await _filmsDBContext.Compte.FirstOrDefaultAsync(e => e.Id == id);
        }


        /// <summary>
        /// Update le premier compte en parametre avec les information du deuxieme
        /// </summary>
        /// <param name="compte"></param>
        /// <param name="entity"></param>
        public async Task Update(Compte compte, Compte entity)
        {
            _filmsDBContext.Entry(compte).State = EntityState.Modified;
            compte.Id = entity.Id;
            compte.Nom = entity.Nom;
            compte.Prenom = entity.Prenom;
            compte.Mel = entity.Mel;
            compte.Rue = entity.Rue;
            compte.CodePostal = entity.CodePostal;
            compte.Ville = entity.Ville;
            compte.Pays = entity.Pays;
            compte.Latitude = entity.Latitude;
            compte.Longitude = entity.Longitude;
            compte.Pwd = entity.Pwd;
            compte.TelPortable = entity.TelPortable;
            compte.Favoris = entity.Favoris;
            await _filmsDBContext.SaveChangesAsync();
        }

        /// <summary>
        /// Retourne le compte lié au mail en paramatre
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public async Task<ActionResult<Compte>> GetByString(string mail)
        {
            return await _filmsDBContext.Compte.FirstOrDefaultAsync(e => e.Mel.ToUpper() == mail.ToUpper());
        }
    }
}
