using System.Windows;
using System.Windows.Controls;
using PadUI.MVVM.ViewModel;

namespace PadUI.Theme
{
    /// <summary>
    /// FlameTimeItem.xaml 的交互逻辑
    /// </summary>
    public partial class FlameTimeItem : UserControl
    {
        //火焰位置
        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }
        //到达时间
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        //注册用户组件属性
        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register("LabelText", typeof(string), typeof(FlameTimeItem));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(FlameTimeItem));
        public FlameTimeItem()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ExpPhenoViewModel.FocusedElement = this;
        }
    }
}
