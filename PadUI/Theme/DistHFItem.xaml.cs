using System.Windows;
using System.Windows.Controls;

namespace PadUI.Theme
{
    /// <summary>
    /// DistHFItem.xaml 的交互逻辑
    /// </summary>
    public partial class DistHFItem : UserControl
    {
        //名称标签
        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }
        //值
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        //物理单位标签
        public string Unit
        {
            get { return (string)GetValue(UnitProperty); }
            set { SetValue(UnitProperty, value); }
        }
        //注册用户组件属性
        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register("LabelText", typeof(string), typeof(DistHFItem));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(DistHFItem));

        public static readonly DependencyProperty UnitProperty =
            DependencyProperty.Register("Unit", typeof(string), typeof(DistHFItem));
        public DistHFItem()
        {
            InitializeComponent();
        }
    }
}
