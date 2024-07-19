using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using CryptoMarketClient.ViewModels;
using Eremex.AvaloniaUI.Controls.Bars;
using Prosoft.ECAD.UI.Base.ViewModels;

namespace CryptoMarketClient.Utils;

public class DropDownControlConverter : MarkupExtension, IValueConverter
{
    public static DropDownControlConverter Instance { get; set; } = new DropDownControlConverter();
	
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value == null)
            return null;
        if(value is PopupMenuViewModel pm)
            return new PopupMenu() { ItemsSource = pm.Items };
        if(value is PopupContainerViewModel pc)
        {
            if(pc.Control.GetLogicalParent() is PopupContainer prev)
            {
                prev.Child = null;
            }

            var pcontrol = new PopupContainer()
            {
                Child = pc.Control, ShowSizeGrip = pc.ShowSizeGrip
            };

            if(pc.Width != 0 && pc.Height != 0)
            {
                pcontrol.Width = pc.Width;
                pcontrol.Height = pc.Height;
            }

            return pcontrol;
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}