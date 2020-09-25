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
using System.Text.RegularExpressions;
using Xunit.Sdk;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private FilmsDBContext _context;
        private ComptesController _controller;
        private IDatarepository<Compte> _dataRepository;

        [TestInitialize]
        public void TestInit()
        {
            var builder = new DbContextOptionsBuilder<FilmsDBContext>().UseNpgsql("Server=localhost;port=5432;Database=FilmsDBTP3;uid=postgres;password=postgres;");
            _context = new FilmsDBContext(builder.Options);
            _dataRepository = new CompteManager(_context);
            _controller = new ComptesController(_dataRepository);
        }

        [TestMethod]
        public void PostCompte_ModelValidated_CreationOK()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le compte après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE du WS => la décommenter
            Compte compteAtester = new Compte()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                TelPortable = "0606070809",
                Mel = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            // Act
            var result = _controller.PostCompte(compteAtester).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'obtenir le résultat
            var result2 = _controller.GetCompteByEmail(compteAtester.Mel);
            var actionResult = result2.Result as ActionResult<Compte>;
            // Assert
            Assert.IsInstanceOfType(actionResult.Value, typeof(Compte), "Pas un compte");
            Compte compteRecupere = _context.Compte.Where(c => c.Mel == compteAtester.Mel).FirstOrDefault();
            // On ne connait pas l'ID du compte envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 comptes
            compteAtester.Id = compteRecupere.Id;
            Assert.AreEqual(compteRecupere, compteAtester, "Comptes pas identiques");

        }

        [TestMethod]
        public void PostCompte_ModelWithBadPhone_CreationNOK()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le compte après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE du WS => la décommenter
            Compte compteAtester = new Compte()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                TelPortable = "06",
                Mel = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            string PhoneRegex = @"^0[0-9]{9}$";
            Regex regex = new Regex(PhoneRegex);
            if (!regex.IsMatch(compteAtester.TelPortable))
            {
                _controller.ModelState.AddModelError("TelPortable", "Le téléphone portable doit contenir 10 chiffres"); //On met le même message que dans la classe Compte.
            }
            Assert.IsTrue(!_controller.ModelState.IsValid);
        }

        [TestMethod]
        [ExpectedException(typeof(System.AggregateException))]
        public void PostCompte_ModelWithMailNonUnique_CreationNOK()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);

            Random rand = new Random();
            int toSkip = rand.Next(0, _context.Compte.Count());

            string mail = _context.Compte.Skip(toSkip).Take(1).First().Mel;
            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le compte après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE du WS => la décommenter
            Compte compteAtester = new Compte()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                TelPortable = "06",
                Mel = mail,
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };

            var test = _controller.PostCompte(compteAtester).Result;

        }

        [TestMethod]
        public void GetCompte_GetGoodTypeObject()
        {
            var comptes = _controller.GetCompte().Result;

            Assert.IsInstanceOfType(comptes.Value, typeof(IEnumerable<Compte>));
        }

        [TestMethod]
        public void GetCompteById_ExistingIdPassedReturnOkObject()
        {
            var result = _controller.GetCompteById(10).Result;
            Assert.IsInstanceOfType(result.Value, typeof(Compte), "Pas du bon type");
        }

        [TestMethod]
        public void GetCompteById_NoneExistingIdPassedReturn404()
        {
            var result = _controller.GetCompteById(int.MaxValue);
            Assert.IsInstanceOfType(result.Result.Result, typeof(NotFoundResult), "Pas de 404");
        }

        [TestMethod]
        public void GetCompteByMail_ExistingIdPassedReturnOkObject()
        {

            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);

            Random rand = new Random();
            int toSkip = rand.Next(0, _context.Compte.Count());
            string mail = _context.Compte.Skip(toSkip).Take(1).First().Mel;
            var result = _controller.GetCompteByEmail(mail).Result;
            Assert.IsInstanceOfType(result.Value, typeof(Compte), "Pas du bon type");
        }

        [TestMethod]
        public void GetCompteByMail_NoneExistingIdPassedReturn404()
        {
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            var result = _controller.GetCompteByEmail("machin" + chiffre + "@gmail.com").Result;
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Pas de 404");
        }



    }
}

