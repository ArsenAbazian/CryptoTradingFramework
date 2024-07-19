using System.Collections;
using System.Reflection;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Crypto.UI.Avalonia.Controls;

public class SparklineControl : TemplatedControl
{
    public static readonly StyledProperty<Color> ColorProperty = AvaloniaProperty.Register<SparklineControl, Color>(
        nameof(Color), Colors.DarkCyan);

    public static readonly StyledProperty<double> ThicknessProperty = AvaloniaProperty.Register<SparklineControl, double>(
        nameof(Thickness), 1.0);
    
    public static readonly StyledProperty<bool> IsEdgePointsVisibleProperty = AvaloniaProperty.Register<SparklineControl, bool>(
        nameof(IsEdgePointsVisible), true);
    
    public static readonly StyledProperty<double> EdgePointSizeProperty = AvaloniaProperty.Register<SparklineControl, double>(
        nameof(EdgePointSize), 4.0);
    
    public static readonly StyledProperty<Color> EdgePointColorProperty = AvaloniaProperty.Register<SparklineControl, Color>(
        nameof(EdgePointColor), Colors.DarkCyan);
    
    public static readonly StyledProperty<bool> IsMinMaxPointsVisibleProperty = AvaloniaProperty.Register<SparklineControl, bool>(
        nameof(IsMinMaxPointsVisible), true);
    
    public static readonly StyledProperty<double> MinMaxPointSizeProperty = AvaloniaProperty.Register<SparklineControl, double>(
        nameof(MinMaxPointSize), 4.0);
    
    public static readonly StyledProperty<Color> MinMaxPointColorProperty = AvaloniaProperty.Register<SparklineControl, Color>(
        nameof(MinMaxPointColor), Colors.RoyalBlue);
    
    public static readonly StyledProperty<Color> BaselineColorProperty = AvaloniaProperty.Register<SparklineControl, Color>(
        nameof(MinMaxPointColor), Colors.LightGray);
    
    public static readonly StyledProperty<bool> IsBaselineVisibleProperty = AvaloniaProperty.Register<SparklineControl, bool>(
        nameof(IsBaselineVisible), false);
    
    public static readonly StyledProperty<double> BaselineProperty = AvaloniaProperty.Register<SparklineControl, double>(
        nameof(Baseline), 0);
    
    public static readonly StyledProperty<double> BaselineThicknessProperty = AvaloniaProperty.Register<SparklineControl, double>(
        nameof(BaselineThickness), 1.0);

    public static readonly DirectProperty<SparklineControl, IList> DataSourceProperty =
        AvaloniaProperty.RegisterDirect<SparklineControl, IList>(nameof(DataSource), o => o.DataSource, (o, v) => o.DataSource = v);

    public static readonly DirectProperty<SparklineControl, string> ArgumentProperty =
        AvaloniaProperty.RegisterDirect<SparklineControl, string>(nameof(Argument), o => o.Argument, (o, v) => o.Argument = v);

    public static readonly DirectProperty<SparklineControl, string> ValueProperty =
        AvaloniaProperty.RegisterDirect<SparklineControl, string>(nameof(Value), o => o.Value, (o, v) => o.Value = v);

    private string mValue;

    public string Value
    {
        get => mValue;
        set => SetAndRaise(ValueProperty, ref mValue, value);
    }
    
    private string mArgument;

    public string Argument
    {
        get => mArgument;
        set => SetAndRaise(ArgumentProperty, ref mArgument, value);
    }
    
    private IList mDataSource;

    public IList DataSource
    {
        get => mDataSource;
        set => SetAndRaise(DataSourceProperty, ref mDataSource, value);
    }
    
    public bool IsEdgePointsVisible
    {
        get => GetValue(IsEdgePointsVisibleProperty);
        set => SetValue(IsEdgePointsVisibleProperty, value);
    }
    
    public double EdgePointSize
    {
        get => GetValue(EdgePointSizeProperty);
        set => SetValue(EdgePointSizeProperty, value);
    }
    
    public Color EdgePointColor
    {
        get => GetValue(EdgePointColorProperty);
        set => SetValue(EdgePointColorProperty, value);
    }
    
    public bool IsMinMaxPointsVisible
    {
        get => GetValue(IsMinMaxPointsVisibleProperty);
        set => SetValue(IsMinMaxPointsVisibleProperty, value);
    }
    
    public double MinMaxPointSize
    {
        get => GetValue(MinMaxPointSizeProperty);
        set => SetValue(MinMaxPointSizeProperty, value);
    }
    
    public Color MinMaxPointColor
    {
        get => GetValue(MinMaxPointColorProperty);
        set => SetValue(MinMaxPointColorProperty, value);
    }
    
    public bool IsBaselineVisible
    {
        get => GetValue(IsBaselineVisibleProperty);
        set => SetValue(IsBaselineVisibleProperty, value);
    }
    
    public Color BaselineColor
    {
        get => GetValue(BaselineColorProperty);
        set => SetValue(BaselineColorProperty, value);
    }
    
    public double Baseline
    {
        get => GetValue(BaselineProperty);
        set => SetValue(BaselineProperty, value);
    }
    
    public double BaselineThickness
    {
        get => GetValue(BaselineThicknessProperty);
        set => SetValue(BaselineThicknessProperty, value);
    }
    
    public double Thickness
    {
        get => GetValue(ThicknessProperty);
        set => SetValue(ThicknessProperty, value);
    }

    public Color Color
    {
        get => GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if(change.Property == DataSourceProperty || change.Property == ArgumentProperty ||
           change.Property == ValueProperty)
        {
            UpdateData();
            InvalidateVisual();
        }
    }

    private void UpdateData()
    {
        if(DataSource == null || DataSource.Count == 0)
            return;
        Type itemType = DataSource[0].GetType();
        if(itemType.IsValueType)
        {
            isValueType = true;
        }
        else
        {
            isValueType = false;
        }
        
        if(Argument != null)
        {
            argumentPropertyInfo = itemType.GetProperty(Argument, BindingFlags.Instance | BindingFlags.Public);
            if(argumentPropertyInfo == null)
                argumentFieldInfo = itemType.GetField(Argument, BindingFlags.Instance | BindingFlags.Public);
        }
        
        if(Value != null)
        {
            valuePropertyInfo = itemType.GetProperty(Value, BindingFlags.Instance | BindingFlags.Public);
            if(valuePropertyInfo == null)
                valueFieldInfo = itemType.GetField(Value, BindingFlags.Instance | BindingFlags.Public);
        }

        Values = new double[DataSource.Count];
        Arguments = new double[DataSource.Count];

        double min = double.MaxValue, max = double.MinValue;
        int minIndex = -1, maxIndex = -1;
        for(int i = 0; i < DataSource.Count; i++)
        {
            double value = GetValueCore(DataSource[i]);
            double argument = GetArgumentCore(DataSource[i], i);
            if(min >= value)
            {
                min = value;
                minIndex = i;
            }

            if(max <= value)
            {
                max = value;
                maxIndex = i;
            }
            Values[i] = value;
            Arguments[i] = argument;
        }

        MinimumIndex = minIndex;
        MaximumIndex = maxIndex;

        if(IsBaselineVisible)
        {
            min = Math.Min(Baseline, min);
            max = Math.Max(Baseline, max);
        }
        
        Minimum = minIndex == -1? new Point(0, 0): new Point(GetArgumentCore(DataSource[minIndex], minIndex), min);
        Maximum = maxIndex == -1? new Point(0, 0): new Point(GetArgumentCore(DataSource[maxIndex], maxIndex), max);
    }

    private bool isValueType;
    private PropertyInfo argumentPropertyInfo;
    private FieldInfo argumentFieldInfo;
    private PropertyInfo valuePropertyInfo;
    private FieldInfo valueFieldInfo;
    protected double[] Arguments { get; set; }
    protected double[] Values { get; set; }
    protected Point Minimum { get; set; }
    protected Point Maximum { get; set; }
    protected int MinimumIndex { get; set; }
    protected int MaximumIndex { get; set; }
    public override void Render(DrawingContext context)
    {
        base.Render(context);
        if(DataSource == null || DataSource.Count == 0)
            return;
        if(Values == null || Arguments == null)
            UpdateData();
        var items = DataSource;
        
        var pen = new Pen(new SolidColorBrush(Color), Thickness);

        var r = new Rect( Padding.Left, Padding.Top, Bounds.Width - Padding.Left - Padding.Right, Bounds.Height - Padding.Top - Padding.Bottom);
        var kh = r.Height / (Maximum.Y - Minimum.Y);
        var kw = r.Width / (Arguments[^1] - Arguments[0]);

        if(IsBaselineVisible)
        {
            var baseLinePen = new Pen(new SolidColorBrush(BaselineColor), BaselineThickness);
            var bp1 = new Point(r.X, CalcY(Baseline, r, kh));
            var bp2 = new Point(r.Right, CalcY(Baseline, r, kh));
            context.DrawLine(baseLinePen, bp1, bp2);
        }
        
        var pt1 = CalcPoint(0, r, kw, kh);
        for(int i = 1; i < items.Count; i++)
        {
            var pt2 = CalcPoint(i, r, kw, kh);
            context.DrawLine(pen, pt1,pt2);
            pt1 = pt2;
        }

        if(IsEdgePointsVisible)
        {
            var brush = new SolidColorBrush(EdgePointColor);
            context.DrawEllipse(brush, null, CalcPoint(0, r, kw, kh), EdgePointSize / 2, EdgePointSize / 2);
            context.DrawEllipse(brush, null, CalcPoint(items.Count - 1, r, kw, kh), EdgePointSize / 2, EdgePointSize / 2);
        }

        if(IsMinMaxPointsVisible)
        {
            var brush = new SolidColorBrush(MinMaxPointColor);
            context.DrawEllipse(brush, null, CalcPoint(MinimumIndex, r, kw, kh), MinMaxPointSize / 2, MinMaxPointSize / 2);
            context.DrawEllipse(brush, null, CalcPoint(MaximumIndex, r, kw, kh), MinMaxPointSize / 2, MinMaxPointSize / 2);
        }
    }

    double CalcY(double value, Rect r, double kh)
    {
        return r.Bottom - (value - Minimum.Y) * kh;
    }
    
    Point CalcPoint(int index, Rect r, double kw, double kh)
    {
        return new Point(r.Left + (Arguments[index] - Arguments[0]) * kw, r.Bottom - (Values[index] - Minimum.Y) * kh);
    }

    private double GetValueCore(object item)
    {
        if(isValueType)
            return (double)item;
        if(valuePropertyInfo != null)
            return Convert.ToDouble(valuePropertyInfo.GetValue(item));
        if(valueFieldInfo != null)
            return Convert.ToDouble(valueFieldInfo.GetValue(item));
        return 0;
    }
    
    private double GetArgumentCore(object item, int i)
    {
        if(isValueType)
            return (double)i;
        if(argumentPropertyInfo != null)
            return Convert.ToDouble(argumentPropertyInfo.GetValue(item));
        if(argumentFieldInfo != null)
            return Convert.ToDouble(argumentFieldInfo.GetValue(item));
        return 0;
    }
}