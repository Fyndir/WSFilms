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
    public class AjouterCompteVM:ViewModelBase
    {
        private WSService _wsService;
        public string Mail { get; set; }

        private Compte _addedCompte { get; set; }

        public Compte AddedCompte { get { return _addedCompte; } set { _addedCompte = value; RaisePropertyChanged(); } }

        public ICommand BtnClearCompteCommand { get; set; }

        public ICommand BtnAddCompteCommand { get; set; }

        public AjouterCompteVM()
        {
            _wsService = new WSService();
            AddedCompte = new Compte();
            BtnClearCompteCommand = new RelayCommand(ClearCompte);
            BtnAddCompteCommand = new RelayCommand(AddCompte);
        }

        private void ClearCompte()
        {
            AddedCompte = new Compte();
        }

        private async void AddCompte()
        {
            try
            {
                await _wsService.AddCompte(this.AddedCompte);
                MessageDialog popup = new MessageDialog($"L'ajout du compte {AddedCompte.Nom} { AddedCompte.Prenom} a réussi");
                await popup.ShowAsync();
                this.AddedCompte = new Compte();
            }
            catch (Exception e)
            {
                MessageDialog popup = new MessageDialog(e.Message);
                await popup.ShowAsync();
            }

        }
    }
}
