using System;
using System.Windows;
using System.Windows.Controls;

namespace Container
{
    public class CustomContainer : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = new Size();
            foreach (UIElement child in InternalChildren)
            {
                child.Measure(availableSize);
                size.Width = Math.Max(size.Width, child.DesiredSize.Width);
                size.Height += child.DesiredSize.Height;
            }
            return size;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double yOffset = (finalSize.Height - InternalChildren.Count * InternalChildren[0].DesiredSize.Height) / 2;
            double y = yOffset;
            foreach (UIElement child in InternalChildren)
            {
                child.Arrange(new Rect((finalSize.Width - child.DesiredSize.Width) / 2, y, child.DesiredSize.Width, child.DesiredSize.Height));
                y += child.DesiredSize.Height;
            }
            return finalSize;
        }
    }
}
