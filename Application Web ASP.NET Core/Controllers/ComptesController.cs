using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application_Web_ASP.NET_Core.Models.EntityFramework;
using Application_Web_ASP.NET_Core.Models.DataManager;
using Application_Web_ASP.NET_Core.Models.Repository;

namespace Application_Web_ASP.NET_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComptesController : ControllerBase
    {
        readonly IDatarepository<Compte> _dataRepository;

        /// <summary>
        /// Constructeur pour l'injection de dependance
        /// </summary>
        /// <param name="context"></param>
        public ComptesController(IDatarepository<Compte> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        /// <summary>
        /// Retourne la liste de tout les comptes en bdd
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Compte>>> GetCompte()
        {
            return  await _dataRepository.GetAll();
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
            var compte = await _dataRepository.GetById(id);

            if (compte.Value == null)
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
            var compte = await _dataRepository.GetByString(email);

            if (compte.Value == null)
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
            var compteToUpdate = await _dataRepository.GetById(id);
            if (compteToUpdate == null)
            {
                return NotFound();
            }
            await _dataRepository.Update(compteToUpdate.Value, compte);
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
            await _dataRepository.Add(compte);

            return CreatedAtAction("GetCompte", new { id = compte.Id }, compte);
        }
    }
}
