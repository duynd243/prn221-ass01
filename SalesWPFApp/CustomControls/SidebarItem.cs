using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SalesWPFApp.Utils;

namespace SalesWPFApp.CustomControls;

public class SidebarItem : ListBoxItem
{
    static SidebarItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(SidebarItem),
            new FrameworkPropertyMetadata(typeof(SidebarItem)));
    }

    public PAGES Page
    {
        get => (PAGES) GetValue(PageProperty);
        set => SetValue(PageProperty, value);
    }

    public static readonly DependencyProperty PageProperty =
        DependencyProperty.Register(nameof(Page), typeof(PAGES), typeof(SidebarItem), new PropertyMetadata(null));


    public Geometry Icon
    {
        get => (Geometry) GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(Geometry), typeof(SidebarItem), new PropertyMetadata(null));
}