using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using converter.Model;
using System.Runtime.CompilerServices;


namespace converter.ViewModel
{
    class DataRateViewModel : BindableBase
    {
        private DataRate dataRate;
        public DataRateViewModel()
        {
            this.DataRate = new DataRate();
        }
        public DataRate DataRate
        {
            get { return this.dataRate; }
            set
            {
                SetProperty(ref this.dataRate, value);
            }
        }


        //protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        //{
        //    if (object.Equals(storage, value)) return false;

        //    storage = value;
        //    this.OnPropertyChanged(propertyName);

        //    return true;
        //}
    }
}
