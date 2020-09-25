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
    public class FilmManager : IDatarepository<Film>
    {

        readonly FilmsDBContext _filmsDBContext;

        /// <summary>
        /// Contructeur pour l'injection de dépendance
        /// </summary>
        /// <param name="context"></param>
        public FilmManager(FilmsDBContext context)
        {
            _filmsDBContext = context;
        }

        /// <summary>
        /// Ajoute un film a la bdd
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Add(Film entity)
        {
            _filmsDBContext.Film.Add(entity);
            await _filmsDBContext.SaveChangesAsync();
        }

        /// <summary>
        /// Supprime le film de la bdd
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Delete(Film entity)
        {
            _filmsDBContext.Film.Remove(entity);
            await _filmsDBContext.SaveChangesAsync();
        }

        /// <summary>
        /// retorune tout les films de la bdd
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult<IEnumerable<Film>>> GetAll()
        {
            return await _filmsDBContext.Film.ToListAsync();
        }

        /// <summary>
        /// retourne le film correspondant a l'id en parametre
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult<Film>> GetById(int id)
        {
            return await _filmsDBContext.Film.FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// retourne le film correspondant au titre
        /// </summary>
        /// <param name="titreFilm"></param>
        /// <returns></returns>
        public async Task<ActionResult<Film>> GetByString(string titreFilm)
        {
            return await _filmsDBContext.Film.FirstOrDefaultAsync(e => e.Titre == titreFilm);
        }

        /// <summary>
        /// Met à jour le film passé en parametre avec les données de l'autre
        /// </summary>
        /// <param name="film"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Update(Film film, Film entity)
        {
            _filmsDBContext.Entry(film).State = EntityState.Modified;
            film.Id = entity.Id;
            film.Titre = entity.Titre;
            film.Synopsis = entity.Synopsis;
            film.DateParution = entity.DateParution;
            film.Duree = entity.Duree;
            film.Genre = entity.Genre;
            film.UrlPhoto = entity.UrlPhoto;
            film.Favoris = entity.Favoris;
            await _filmsDBContext.SaveChangesAsync();
        }
    }
}
