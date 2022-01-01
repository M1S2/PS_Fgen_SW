using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using PS_Fgen_SW.Model;

namespace PS_Fgen_SW.ViewModel
{
    public class DdsChannelViewModel : ViewModelBase
    {
        private DdsChannelModel _ddsModel;
        public DdsChannelModel DdsModel
        {
            get => _ddsModel;
            set { Set(ref _ddsModel, value); }
        }

        public DdsChannelViewModel(DdsChannelModel ddsModel)
        {
            DdsModel = ddsModel;
        }

    }
}
