using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System.Runtime.CompilerServices;

namespace converter.Model
{
    class DataRate : BindableBase
    {
        private string rubtorub;
        private string rubtoeur;
        private string rubtousd;
        private string usdtousd;
        private string usdtoeur;
        private string usdtorub;
        private string eurtoeur;
        private string eurtousd;
        private string eurtorub;
        private string currentday;
        public string RUBtoRUB
        {
            get
            { return rubtorub; }
            set
            {
                SetProperty(ref rubtorub, value); ;
            }
        }
        public string RUBtoEUR
        {
            get
            { return rubtoeur; }
            set
            {
                SetProperty(ref rubtoeur, value); ;
            }
        }
        public string RUBtoUSD
        {
            get
            { return rubtousd; }
            set
            {
                SetProperty(ref rubtousd, value); ;
            }
        }
        public string USDtoUSD
        {
            get
            { return usdtousd; }
            set
            {
                SetProperty(ref usdtousd, value); ;
            }
        }
        public string USDtoEUR
        {
            get
            { return usdtoeur; }
            set
            {
                SetProperty(ref usdtoeur, value); ;
            }
        }
        public string USDtoRUB
        {
            get
            { return usdtorub; }
            set
            {
                SetProperty(ref usdtorub, value); ;
            }
        }
        public string EURtoEUR
        {
            get
            { return eurtoeur; }
            set
            {
                SetProperty(ref eurtoeur, value); ;
            }
        }
        public string EURtoUSD
        {
            get
            { return eurtousd; }
            set
            {
                SetProperty(ref eurtousd, value); ;
            }
        }
        public string EURtoRUB
        {
            get
            { return eurtorub; }
            set
            {
                SetProperty(ref eurtorub, value);
            }
        }
        public string CurrentDay
        {
            get
            { return currentday; }
            set
            {
                SetProperty(ref currentday, value);
            }
        }
    }
}
