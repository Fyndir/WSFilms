using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using WSFILM.Model;
using WSFILM.Services;

namespace WSFILM.ViewModel
{
    public class RechercheCompteVM : ViewModelBase
    {

        private WSService _wsService;
        public string Mail { get; set; }

        public Compte SearchCompte { get; set; }

        public ICommand BtnSearchCompteByMail { get; private set; }

        public RechercheCompteVM()
        {
            //alim pr test
            this.Mail = "paul.durand@gmail.com";

            // Fctn
            _wsService = new WSService();
            BtnSearchCompteByMail = new RelayCommand(SearchCompteByEmail);
        }

        private async void SearchCompteByEmail()
        {
            try
            {
                SearchCompte = await _wsService.GetCompteByMail(Mail);
            }
            catch(Exception e)
            {
                MessageDialog popup = new MessageDialog(e.Message);
                await popup.ShowAsync();
            }
        }
    }
}
