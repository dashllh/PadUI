using System;
using System.Windows;
using System.Windows.Controls;
using PadUI.MVVM.ViewModel;

namespace PadUI.MVVM.View
{
    /// <summary>
    /// RealTimeView.xaml 的交互逻辑
    /// </summary>
    public partial class RealTimeView : UserControl
    {
        public RealTimeView()
        {
            InitializeComponent();
            //DataContext = Application.Current.Properties["RTVM"] as RealTimeViewModel;
        }
    }
}
