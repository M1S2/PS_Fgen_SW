﻿using System;
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
    public class DdsChannelViewModel : ViewModelBase
    {
        private IDdsChannelModel _ddsModel;
        public IDdsChannelModel DdsModel
        {
            get => _ddsModel;
            set { Set(ref _ddsModel, value); }
        }

        public DdsChannelViewModel(int instanceNumber)
        {
            IDeviceModel devModel = ServiceLocator.Current.GetInstance<IDeviceModel>();
            DdsModel = instanceNumber == 1 ? devModel.DD1_Channel : devModel.DD2_Channel;
        }

    }
}
