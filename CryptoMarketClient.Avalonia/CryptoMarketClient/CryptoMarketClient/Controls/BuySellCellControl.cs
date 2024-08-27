using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Crypto.Core.Common;

namespace CryptoMarketClient.Controls;

public class BuySellCellControl : TemplatedControl
{
    public static readonly DirectProperty<BuySellCellControl, OrderType> OrderTypeProperty =
        AvaloniaProperty.RegisterDirect<BuySellCellControl, OrderType>(nameof(OrderType), o => o.OrderType, (o, v) => o.OrderType = v);

    public static readonly StyledProperty<SolidColorBrush> BuyColorProperty = AvaloniaProperty.Register<BuySellCellControl, SolidColorBrush>(
        "BuyColor");

    public static readonly StyledProperty<SolidColorBrush> SellColorProperty = AvaloniaProperty.Register<BuySellCellControl, SolidColorBrush>(
        "SellColor");

    public static readonly DirectProperty<BuySellCellControl, string> TextProperty =
        AvaloniaProperty.RegisterDirect<BuySellCellControl, string>(nameof(Text), o => o.Text, (o, v) => o.Text = v);

    private string mText;

    public string Text
    {
        get => mText;
        set => SetAndRaise(TextProperty, ref mText, value);
    }   

    public SolidColorBrush SellColor
    {
        get => GetValue(SellColorProperty);
        set => SetValue(SellColorProperty, value);
    }

    public SolidColorBrush BuyColor
    {
        get => GetValue(BuyColorProperty);
        set => SetValue(BuyColorProperty, value);
    }
    
    private OrderType _mOrderType;

    public OrderType OrderType
    {
        get => _mOrderType;
        set => SetAndRaise(OrderTypeProperty, ref _mOrderType, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        Foreground = OrderType == OrderType.Buy ? BuyColor : SellColor;
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == OrderTypeProperty || change.Property == BuyColorProperty || change.Property == SellColorProperty)
        {
            Foreground = OrderType == OrderType.Buy ? BuyColor : SellColor; 
        }
    }
}
