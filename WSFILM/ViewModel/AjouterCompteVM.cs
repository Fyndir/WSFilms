using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
           await _wsService.AddCompte(this.AddedCompte);
        }
    }
}
