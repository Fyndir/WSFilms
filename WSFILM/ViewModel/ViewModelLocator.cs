using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSFILM.ViewModel
{
    class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<VMHomePage>();
            SimpleIoc.Default.Register<RechercheCompteVM>();
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public VMHomePage HomePage => ServiceLocator.Current.GetInstance<VMHomePage>();
        public RechercheCompteVM RechercheCompte => ServiceLocator.Current.GetInstance<RechercheCompteVM>();
    }
}