using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PS_Fgen_SW.Model;
using PS_Fgen_SW.ViewModel;

namespace PS_Fgen_SW.Views
{
    /// <summary>
    /// Interaktionslogik für DdsChannelUserControl.xaml
    /// </summary>
    public partial class DdsChannelUserControl : UserControl
    {
        ViewModelLocator locator;

        public static readonly DependencyProperty ViewModelNameProperty = DependencyProperty.Register("ViewModelName", typeof(string), typeof(DdsChannelUserControl), new PropertyMetadata("DDS1", OnViewModelNameChanged));
        
        public string ViewModelName
        {
            get { return (string)GetValue(ViewModelNameProperty); }
            set { SetValue(ViewModelNameProperty, value); }
        }

        private static void OnViewModelNameChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DdsChannelUserControl c = sender as DdsChannelUserControl;
            c.DataContext = ((string)e.NewValue == "DDS1" ? c.locator.DDS1VM : c.locator.DDS2VM);
        }

        public DdsChannelUserControl()
        {
            InitializeComponent();
            locator = Application.Current.Resources["Locator"] as ViewModelLocator;
            this.DataContext = ViewModelName == "DDS1" ? locator.DDS1VM : locator.DDS2VM;
        }
    }
}
