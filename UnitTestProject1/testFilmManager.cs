using Application_Web_ASP.NET_Core.Controllers;
using Application_Web_ASP.NET_Core.Models.DataManager;
using Application_Web_ASP.NET_Core.Models.EntityFramework;
using Application_Web_ASP.NET_Core.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UnitTestProject1
{
    [TestClass]
    public class testFilmManager
    {
        private FilmsDBContext _context;
        private FilmsController _controller;
        private IDatarepository<Film> _dataRepository;

        [TestInitialize]
        public void TestInit()
        {
            var builder = new DbContextOptionsBuilder<FilmsDBContext>().UseNpgsql("Server=localhost;port=5432;Database=FilmsDBTP3;uid=postgres;password=postgres;");
            _context = new FilmsDBContext(builder.Options);
            _dataRepository = new FilmManager(_context);
            _controller = new FilmsController(_dataRepository);
        }

        [TestMethod]
        public void Postfilm_ModelValidated_CreationOK()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le film après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE du WS => la décommenter
            Film filmATester = new Film()
            {
                Titre = "Salsa" + chiffre,
                Synopsis = "Sais tu dancer ????",
                DateParution = "19/08/1995",
                Duree = 130,
                Genre = "Comedie"
            };
            // Act
            var result = _controller.PostFilm(filmATester).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'obtenir le résultat
            var result2 = _controller.GetFilmByTitre(filmATester.Titre);
            var actionResult = result2.Result as ActionResult<Film>;
            // Assert
            Assert.IsInstanceOfType(actionResult.Value, typeof(Film), "Pas un film");
            Film filmrecup = _context.Film.Where(c => c.Titre == filmATester.Titre).FirstOrDefault();
            // On ne connait pas l'ID du film envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 films
            filmATester.Id = filmrecup.Id;
            Assert.AreEqual(filmrecup, filmATester, "Film pas identiques");

        }

        [TestMethod]
        public void Getfilm_GetGoodTypeObject()
        {
            var films = _controller.GetFilm().Result;

            Assert.IsInstanceOfType(films.Value, typeof(IEnumerable<Film>));
        }

        [TestMethod]
        public void GetfilmById_ExistingIdPassedReturnOkObject()
        {
            var result = _controller.GetFilmById(10).Result;
            Assert.IsInstanceOfType(result.Value, typeof(Film), "Pas du bon type");
        }

        [TestMethod]
        public void GetfilmById_NoneExistingIdPassedReturn404()
        {
            var result = _controller.GetFilmById(int.MaxValue);
            Assert.IsInstanceOfType(result.Result.Result, typeof(NotFoundResult), "Pas de 404");
        }

        [TestMethod]
        public void GetfilmByTitre_ExistingIdPassedReturnOkObject()
        {

            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);

            Random rand = new Random();
            int toSkip = rand.Next(0, _context.Film.Count());
            string mail = _context.Film.Skip(toSkip).Take(1).First().Titre;
            var result = _controller.GetFilmByTitre(mail).Result;
            Assert.IsInstanceOfType(result.Value, typeof(Film), "Pas du bon type");
        }

        [TestMethod]
        public void GetfilmByTitre_NoneExistingIdPassedReturn404()
        {
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            var result = _controller.GetFilmByTitre(chiffre + "AZERTY").Result;
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Pas de 404");
        }

    }
}
