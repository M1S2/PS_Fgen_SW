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
using System.Windows.Threading;

namespace PS_Fgen_SW.ViewModel
{
    public class PsChannelViewModel : ViewModelBase
    {
        DispatcherTimer _measureTimer = new DispatcherTimer();

        private IPsChannelModel _psModel;
        public IPsChannelModel PsModel
        {
            get => _psModel;
            set { Set(ref _psModel, value); }
        }

        public PsChannelViewModel(IDeviceModel devModel)
        {
            PsModel = devModel.PS_Channel;

            _measureTimer.Interval = new TimeSpan(0, 0, 1);
            _measureTimer.Tick += _measureTimer_Tick;
            _measureTimer.Start();
        }

        /// <summary>
        /// Get some parameters periodically from the devices PS_Channel.
        /// </summary>
        private void _measureTimer_Tick(object sender, EventArgs e)
        {
            PsModel?.UpdateMeasuredParameters?.Execute(null);
        }
    }
}
