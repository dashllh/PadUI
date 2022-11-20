using System;
using System.Windows;
using PadUI.Core;
using PadUI.MVVM.ViewModel;


namespace PadUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly WSClientHelper _wsClient;     // WebSocket辅助类
        private readonly ExpMaster _controller;        //试验控制器本地同步对象
        private RealTimeViewModel _realTimeViewModel;  //传感器实时数据类
        public App()
        {
            //初始化网络连接
            _wsClient = new WSClientHelper("ws://192.168.8.2:9090");
            _wsClient.OnOpen += WSocketClient_OnOpen;
            _wsClient.OnClose += WSocketClient_OnClose;
            _wsClient.OnError += WSocketClient_OnError;
            //添加网路通信代理至应用程序全局
            Properties.Add("WSProxy",_wsClient);
            //初始化试验视图对象(实时数据视图)
            _realTimeViewModel = new RealTimeViewModel();
            //添加实时数据视图至应用程序全局
            Properties.Add("RTVM",_realTimeViewModel);
            //初始化全局控制器对象
            _controller = new ExpMaster();
            //添加控制器对象至本地全局
            Properties.Add("Controller", _controller);
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //连接试验控制主机
            _wsClient.Open();
        }
        //WebSocket消息客户端响应函数
        private void WSocketClient_OnOpen(object? sender, EventArgs e)
        {
            
        }
        private void WSocketClient_OnClose(object? sender, EventArgs e)
        {
            
        }
        private void WSocketClient_OnError(object? sender, Exception ex)
        {
            switch (ex.HResult)
            {
                case -2147467259: //对应HResult值: E_FAIL(Unspecified failure)
                    MessageBox.Show("Error: "+ ex.Message + "\n服务器无响应或意外中断,请恢复服务端程序后重新启动。", "客户端消息", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                default:                    
                    MessageBox.Show("发生未知错误,请重新启动客户端程序。\n如果依然无法使用,请联系系统管理员。", "客户端消息", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }
    }
}
