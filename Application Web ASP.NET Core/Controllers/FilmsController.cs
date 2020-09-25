using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application_Web_ASP.NET_Core.Models.EntityFramework;
using Application_Web_ASP.NET_Core.Models.Repository;

namespace Application_Web_ASP.NET_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        readonly IDatarepository<Film> _dataRepository;

        /// <summary>
        /// Constructeur pour l'injection de dependance
        /// </summary>
        /// <param name="dataRepository"></param>
        public FilmsController(IDatarepository<Film> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        /// <summary>
        /// Retourne la liste de tout les Films en bdd
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilm()
        {
            return await _dataRepository.GetAll();
        }

        /// <summary>
        /// retourne le Film correspondant à l'id en parametre
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("ById/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Film>> GetFilmById(int id)
        {
            var Film = await _dataRepository.GetById(id);

            if (Film.Value == null)
            {
                return NotFound();
            }

            return Film;
        }

        /// <summary>
        /// retorune le Film correspondant au mail en parametre
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("ByEmail/{email}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Film>> GetFilmByEmail(string titre)
        {
            var Film = await _dataRepository.GetByString(titre);

            if (Film.Value == null)
            {
                return NotFound();
            }

            return Film;
        }

        /// <summary>
        /// Met à jour le Film correspondant à l'id avec les info passé dans l'object Film en parametre
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Film"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilm(int id, Film Film)
        {
            if (id != Film.Id)
            {
                return BadRequest();
            }
            var FilmToUpdate = await _dataRepository.GetById(id);
            if (FilmToUpdate == null)
            {
                return NotFound();
            }
            await _dataRepository.Update(FilmToUpdate.Value, Film);
            return NoContent();
        }

        /// <summary>
        /// Insere l'objet Film en parametre dans la bdd
        /// </summary>
        /// <param name="Film"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Film>> PostFilm(Film Film)
        {
            await _dataRepository.Add(Film);

            return CreatedAtAction("GetFilm", new { id = Film.Id }, Film);
        }

    }
}
