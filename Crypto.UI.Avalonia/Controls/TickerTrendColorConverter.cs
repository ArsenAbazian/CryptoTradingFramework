using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Crypto.Core;

namespace Crypto.UI.Avalonia.Controls;

public class TickerTrendColorConverter : MarkupExtension, IValueConverter
{
    public Color Color { get; set; }
    public Color ReductionColor { get; set; }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is Ticker t)
            return t.IsTrendUp ? Color : ReductionColor;
        return Color;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}