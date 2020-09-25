using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application_Web_ASP.NET_Core.Models.EntityFramework;

namespace Application_Web_ASP.NET_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComptesController : ControllerBase
    {
        private readonly FilmsDBContext _context;

        /// <summary>
        /// Constructeur pour l'injection de dependance
        /// </summary>
        /// <param name="context"></param>
        public ComptesController(FilmsDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retourne la liste de tout les comptes en bdd
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Compte>>> GetCompte()
        {
            return await _context.Compte.ToListAsync();
        }

        /// <summary>
        /// retourne le compte correspondant à l'id en parametre
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("ById/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Compte>> GetCompteById(int id)
        {            
            var compte = await _context.Compte.Include(c => c.Favoris).Where(c=> c.Id==id).FirstOrDefaultAsync();

            if (compte == null)
            {
                return NotFound();
            }

            return compte;
        }

        /// <summary>
        /// retorune le compte correspondant au mail en parametre
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("ByEmail/{email}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Compte>> GetCompteByEmail(string email)
        {
            var compte = await _context.Compte.Where(c => c.Mel.ToUpper() == email.ToUpper()).FirstOrDefaultAsync();

            if (compte == null)
            {
                return NotFound();
            }

            return compte;
        }

        /// <summary>
        /// Met à jour le compte correspondant à l'id avec les info passé dans l'object compte en parametre
        /// </summary>
        /// <param name="id"></param>
        /// <param name="compte"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompte(int id, Compte compte)
        {
            if (id != compte.Id)
            {
                return BadRequest();
            }

            _context.Entry(compte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Insere l'objet compte en parametre dans la bdd
        /// </summary>
        /// <param name="compte"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Compte>> PostCompte(Compte compte)
        {
            _context.Compte.Add(compte);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompte", new { id = compte.Id }, compte);
        }

        private bool CompteExists(int id)
        {
            return _context.Compte.Any(e => e.Id == id);
        }
    }
}
