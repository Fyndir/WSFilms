using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using WSFILM.Model;
using WSFILM.Services;

namespace WSFILM.ViewModel
{
    public class RechercheCompteVM : ViewModelBase
    {

        private WSService _wsService;
        public string Mail { get; set; }

        private Compte _searchCompte { get; set; }

        public Compte SearchCompte { get { return _searchCompte; } set { _searchCompte = value; RaisePropertyChanged(); } }

        public ICommand BtnSearchCompteByMail { get; private set; }

        public ICommand BtnClearCompteCommand { get; set; }

        public RechercheCompteVM()
        {
            //alim pr test
            this.Mail = "paul.durand@gmail.com";

            // Fctn
            _wsService = new WSService();
            BtnSearchCompteByMail = new RelayCommand(SearchCompteByEmail);
            BtnClearCompteCommand = new RelayCommand(ClearCompte);
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

        private void ClearCompte()
        {
            SearchCompte = null;
        }
    }
}
