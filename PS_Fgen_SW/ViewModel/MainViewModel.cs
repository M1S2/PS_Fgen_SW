using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PS_Fgen_SW.Model;
using PS_Fgen_SW.Interfaces;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Globalization;
using System.Windows.Threading;
using System.Windows;
using CommonServiceLocator;

namespace PS_Fgen_SW.ViewModel
{
    /// <summary>
    /// Main View model. Holding e.g. the DeviceModel and the Comm handle.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private List<UIElement> _pageViews;
        /// <summary>
        /// List with all available views
        /// </summary>
        public List<UIElement> PageViews
        {
            get
            {
                if (_pageViews == null) { _pageViews = new List<UIElement>(); }
                return _pageViews;
            }
        }

        private UIElement _currentPageView;
        /// <summary>
        /// Currently displayed view
        /// </summary>
        public UIElement CurrentPageView
        {
            get => _currentPageView;
            set
            {
                _currentPageView = value;
                RaisePropertyChanged(nameof(CurrentPageView));
            }
        }

        /// <summary>
        /// Change the displayed view to the requested view.
        /// If the view doesn't exist in the list, it is added.
        /// </summary>
        /// <param name="view">View object to show</param>
        private void ChangeView(UIElement view)
        {
            if (!PageViews.Contains(view))
            {
                PageViews.Add(view);
            }
            CurrentPageView = PageViews.FirstOrDefault(vm => vm == view);
        }

        /// <summary>
        /// Show the communication view
        /// </summary>
        public void ShowCommView() => ChangeView(PageViews[0]);

        /// <summary>
        /// Command to show the communication view
        /// </summary>
        public ICommand ShowCommViewCommand => new RelayCommand(() => ShowCommView());

        /// <summary>
        /// Show the device view
        /// </summary>
        public void ShowDeviceView() => ChangeView(PageViews[1]);

        /// <summary>
        /// Command to show the device view
        /// </summary>
        public ICommand ShowDeviceViewCommand => new RelayCommand(() => ShowDeviceView(), () => Comm.Connected);


        private IComm _comm;
        /// <summary>
        /// Communication interface used for communication to the PS_Fgen device.
        /// </summary>
        public IComm Comm
        {
            get => _comm;
            set { Set(ref _comm, value); }
        }

        /// <summary>
        /// Command executed when the window is closing.
        /// </summary>
        public ICommand WindowClosing => new RelayCommand<CancelEventArgs>
        (
            (args) =>
            {
                Comm.Disconnect();
            }
        );

        /// <summary>
        /// Constructor for the MainViewModel.
        /// </summary>
        /// <param name="commIF">Communication interface</param>
        public MainViewModel(IComm commIF)
        {
            Comm = commIF;
            ((ObservableObject)Comm).PropertyChanged += Comm_OnConnectionStateChanged;
         
            // Add available pages and set page
            PageViews.Add(new Views.CommUserControl());
            PageViews.Add(new Views.DeviceUserControl());

            CurrentPageView = PageViews.FirstOrDefault();
        }

        /// <summary>
        /// The connection state of the communication interface changed.
        /// </summary>
        private void Comm_OnConnectionStateChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IComm.Connected))
            {
                if (Comm.Connected == true)
                {
                    ShowDeviceView();
                }
                else
                {
                    ShowCommView();
                }
            }
        }
    }
}
