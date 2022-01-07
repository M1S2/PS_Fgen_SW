using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using PS_Fgen_SW.Model;
using PS_Fgen_SW.Interfaces;
using CommonServiceLocator;

namespace PS_Fgen_SW.ViewModel
{
    public class PsChannelViewModel : ViewModelBase
    {
        private IPsChannelModel _psModel;
        public IPsChannelModel PsModel
        {
            get => _psModel;
            set { Set(ref _psModel, value); }
        }

        public PsChannelViewModel(IDeviceModel devModel)
        {
            PsModel = devModel.PS_Channel;
        }

    }
}
