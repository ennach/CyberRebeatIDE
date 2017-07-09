using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace CyberRebeatIDE
{
    public class WindowCustomize
    {
        private readonly Border parentBorder = new Border();
        private readonly Image imagecontrol = new Image();
        public WindowCustomize(Window window)
        {
            loadImage(@"Images\header.png");

            if (window.IsLoaded) Window_Loaded(window, null);
            window.Loaded += Window_Loaded;
        }

        /// <summary>
        /// 背景画像セット　右上寄せ
        /// </summary>
        /// <param name="path"></param>
        private void loadImage(string path)
        {
            var imagesource = new BitmapImage(new Uri(path, UriKind.Relative));
            imagesource.Freeze();
            imagecontrol.Source = imagesource;
            imagecontrol.Stretch = Stretch.None;
            imagecontrol.HorizontalAlignment = HorizontalAlignment.Right;
            imagecontrol.VerticalAlignment = VerticalAlignment.Top;
            imagecontrol.Opacity = 1;
            imagecontrol.Effect = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var mainwindow = (Window)sender;

            // 背景画像挿入
            parentBorder.Child = imagecontrol;
            Grid.SetRowSpan(parentBorder, 4);
            var rootgrid = (Grid)mainwindow.Template.FindName("RootGrid", mainwindow);
            rootgrid.Children.Insert(0, parentBorder);

            // タイトルバー　透過
            // 型が取れないので、dynamic
            dynamic title = mainwindow.Template.FindName("MainWindowTitleBar", mainwindow);

            // title.Backgroundは読み取り専用のため
            SolidColorBrush backgorund = title.Background;
            title.Background = new SolidColorBrush(backgorund.Color) { Opacity = 0 };

            // メニューバー　透過
            dynamic dock = mainwindow.Template.FindName("PART_MenuBarFrameControlContainer", mainwindow);
            dock.Parent.Background = new SolidColorBrush(Colors.Gray) { Opacity = 0 };

            DockPanel panel = dock.Parent;
            var vsMenuParent = (System.Windows.Controls.ContentPresenter)panel.Children[1];
            dynamic vsMenu = VisualTreeHelper.GetChild(vsMenuParent, 0);
            vsMenu.Background = new SolidColorBrush(Colors.Gray) { Opacity = 0 };

            // ツールバー　透過
            var third = (DependencyObject)mainwindow.Template.FindName("PART_ToolBarHost", mainwindow);
            var thirdGrid = (Grid)VisualTreeHelper.GetChild(third, 0);
            dynamic thirdTarget = VisualTreeHelper.GetChild(thirdGrid, 0);
            var newThirdBg = new LinearGradientBrush() { Opacity = 0 };
            thirdTarget.Background = newThirdBg;
        }
    }
}
