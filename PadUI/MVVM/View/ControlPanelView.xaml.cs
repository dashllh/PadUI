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
using PadUI.Core;

namespace PadUI.MVVM.View
{
    /// <summary>
    /// ControlPanelView.xaml 的交互逻辑
    /// </summary>
    public partial class ControlPanelView : UserControl
    {
        public ControlPanelView()
        {
            InitializeComponent();
        }
        private void btnStartTimer_Click(object sender, RoutedEventArgs e)
        {
            //构造命令对象
            //获得ws全局对象
            WSClientHelper _wsClient = (WSClientHelper)App.Current.Properties["WSProxy"];
            if (_wsClient != null)
            {
                _wsClient.Send("{\"Cmd\":3}");
            }
    }
    }
}
