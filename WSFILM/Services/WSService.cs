using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WSFILM.Model;

namespace WSFILM.Services
{
    public class WSService
    {
        private HttpClient client = new HttpClient();

        public async Task<Compte> GetCompteByMail(string email)
        {
            client = new HttpClient();
            string url = $"http://localhost:5000/api/Comptes/ByEmail/{email}";
            HttpResponseMessage response = await client.GetAsync(url);

            Compte compte = null;
            if (response.IsSuccessStatusCode)
            {
                compte = await response.Content.ReadAsAsync<Compte>();
            }

            return compte;
        }

        public async Task ModifCompte(Compte compte)
        {
            client = new HttpClient();
            string url = $"http://localhost:5000/api/Comptes/{compte.Id}";
            HttpResponseMessage response = await client.PutAsJsonAsync(url,compte);           
        }

        internal async Task AddCompte(Compte addedCompte)
        {
            client = new HttpClient();
            string url = $"http://localhost:5000/api/Comptes/";
            HttpResponseMessage response = await client.PostAsJsonAsync(url, addedCompte);
        }
    }
}
