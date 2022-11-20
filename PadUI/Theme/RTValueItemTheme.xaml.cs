using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PadUI.Theme
{
    /// <summary>
    /// RTValueItemTheme.xaml 的交互逻辑
    /// </summary>
    public partial class RTValueItemTheme : UserControl
    {
        //属性定义
        public string ValueName { get; set; }
        public double Value { get; set; }
        public RTValueItemTheme()
        {
            InitializeComponent();
        }

        //注册控件属性,以便在XAML中能够访问
        public static DependencyProperty ValueNameProperty =
            DependencyProperty.Register("ValueName", typeof(string), typeof(RTValueItemTheme));

        public static DependencyProperty ValueProperty =
                DependencyProperty.Register("Value", typeof(double), typeof(RTValueItemTheme));
    }
}
