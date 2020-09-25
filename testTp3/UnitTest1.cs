using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace testTp3
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            [TestMethod]
            public void PostCompte_ModelValidated_CreationOK()
            {
                // Arrange
                Random rnd = new Random();
                int chiffre = rnd.Next(1, 1000000000);
                // Le mail doit être unique donc 2 possibilités :
                // 1. on s'arrange pour que le mail soit unique en concaténant un random ou
                un timestamp
 // 2. On supprime le compte après l'avoir créé. Dans ce cas, nous avons
besoin d'appeler la méthode DELETE du WS => la décommenter
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
                var result = _controller.PostCompte(compteAtester).Result; // .Result pour
                appeler la méthode async de manière synchrone, afin d'obtenir le résultat
 var result2 = _controller.GetCompteByEmail(compteAtester.Mel);
                var actionResult = result2.Result as ActionResult<Compte>;
                // Assert
                Assert.IsInstanceOfType(actionResult.Value, typeof(Compte), "Pas un compte");
                Compte compteRecupere = _context.Compte.Where(c => c.Mel ==
               compteAtester.Mel).FirstOrDefault();
            }
    }
}
