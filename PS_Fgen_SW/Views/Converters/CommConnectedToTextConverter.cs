using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PS_Fgen_SW.Views.Converters
{
    /// <summary>
    /// Convert the Comm connection state to a text for the corresponding UI element
    /// </summary>
    public class CommConnectedToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool connected = (bool)value;
            return connected ? "Disconnect" : "Connect";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
