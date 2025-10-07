using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kontrolka
{
    public class CustomControl : Control
    {
        static CustomControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomControl), new FrameworkPropertyMetadata(typeof(CustomControl)));
        }

        public static readonly DependencyProperty CircleColorProperty = DependencyProperty.Register(
            nameof(CircleColor), typeof(Brush), typeof(CustomControl), new PropertyMetadata(Brushes.Red));

        public Brush CircleColor
        {
            get => (Brush)GetValue(CircleColorProperty);
            set => SetValue(CircleColorProperty, value);
        }

    }
}
