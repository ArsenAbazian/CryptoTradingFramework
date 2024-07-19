using System;
using Avalonia;
using Avalonia.Controls.Primitives;

namespace CryptoMarketClient.Controls;

public class ProgressLineControl : TemplatedControl
{
    public static readonly DirectProperty<ProgressLineControl, double> ValueProperty =
        AvaloniaProperty.RegisterDirect<ProgressLineControl, double>(nameof(Value), o => o.Value, (o, v) => o.Value = v);

    public static readonly DirectProperty<ProgressLineControl, double> LineWidthProperty =
        AvaloniaProperty.RegisterDirect<ProgressLineControl, double>(nameof(LineWidth), o => o.LineWidth, (o, v) => o.LineWidth = v);

    private double mLineWidth;

    public double LineWidth
    {
        get => mLineWidth;
        set => SetAndRaise(LineWidthProperty, ref mLineWidth, value);
    }
    
    private double _value;
    public double Value
    {
        get => _value;
        set => SetAndRaise(ValueProperty, ref _value, value);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        var res= base.ArrangeOverride(finalSize);
        LineWidth = Math.Min(Bounds.Width * Value, Bounds.Width);
        return res;
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if(change.Property == ValueProperty)
            OnValueChanged();
    }

    private void OnValueChanged()
    {
        LineWidth = Math.Min(Bounds.Width * Value, Bounds.Width);
    }
}