using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using AlmicantaratXF.Data;
using AlmicantaratXF.Model;
using AlmicantaratXF.Views;

namespace AlmicantaratXF.ViewModels
{
    class ConstantCorrectionsViewModel : BaseViewModel
    {
        public static ObservableCollection<ConstantCorrection> ConstantCorrections { get; set; }

        public ConstantCorrectionsViewModel()
        {
            ConstantCorrections = new ObservableCollection<ConstantCorrection>();
            List<ConstantCorrection> ccs = App.SettingsDB.GetListCorrectionsSorted();
            foreach (ConstantCorrection cc in ccs)
            {
                ConstantCorrections.Add(cc);
            }
            //ConstantCorrections = new ObservableCollection<ConstantCorrection>();
            //Device.BeginInvokeOnMainThread(async () =>
            //{
            //    Task<List<ConstantCorrection>> ConstantCorrectionsList = App.SettingsDB.GetListCorrectionsSortedAsync();
            //    await ConstantCorrectionsList.ContinueWith(t =>
            //    {
            //        foreach (ConstantCorrection cc in t.Result)
            //        {
            //            ConstantCorrections.Add(cc);
            //        }
            //    });
            //});
        }
    }
}
