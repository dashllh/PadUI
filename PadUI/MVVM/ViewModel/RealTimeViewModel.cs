using PadUI.Core;
using PadUI.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Text.Json;

namespace PadUI.MVVM.ViewModel
{
    /* 定义实时值视图的显示属性集 */
    class RealTimeViewModel : ObsevableObject
    {
        //计时器
        private int _timer;
        public int Timer
        {
            get { return _timer; }
            set { _timer = value; OnPropertyChanged("timer"); }
        }
        //光信号
        private double _light;
        public double Light
        {
            get { return _light; }
            set { _light = value; OnPropertyChanged("light"); }
        }
        //箱体温度
        private double _cht;
        public double CHT
        {
            get { return _cht; }
            set { _cht = value; OnPropertyChanged("cht"); }
        }
        //黑体温度
        private double _bbt;
        public double BBT
        {
            get { return _bbt; }
            set { _bbt = value; OnPropertyChanged("bbt"); }
        }
        //管道温度
        private double _ductt;
        public double DuctT
        {
            get { return _ductt; }
            set { _ductt = value; OnPropertyChanged("ductt"); }
        }
        //管道风速
        private double _ductf;
        public double DuctF
        {
            get { return _ductf; }
            set { _ductf = value; OnPropertyChanged("ductf"); }
        }        
        //热辐射值
        private double _hf;
        public double HF
        {
            get { return _hf; }
            set { _hf = value; OnPropertyChanged("hf"); }
        }

        //光通量曲线数据模型
        public PlotModel? lightModel { get; set; }
        //温度曲线数据模型
        public PlotModel? tempModel { get; set; }
        //开始计时命令
        public RelayCommand? StartTimerCmd { get; set; }
        //停止计时命令
        public RelayCommand? StopTimerCmd { get; set; }
        //websocket客户端对象
        private readonly WSClientHelper? _ws;
        //控制器对象
        public readonly ExpMaster? _master;
        public RealTimeViewModel()
        {
            _ws     = Application.Current.Properties["WSProxy"] as WSClientHelper;
            _master = Application.Current.Properties["Controller"] as ExpMaster;
            /* 配置光通量图表 */
            lightModel = new PlotModel();
            lightModel.Title = "光通量曲线(%)";
            lightModel.TitleFontSize = 14;
            lightModel.Background = OxyColor.FromRgb(250, 250, 250);

            //添加第一Y轴
            //给坐标轴添加标签
            //lightModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "光通量(%)", Minimum = 0, Maximum = 100 });
            lightModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left,MajorGridlineStyle= LineStyle.Solid, Minimum = 0, Maximum = 100 });
            //添加第二Y轴
            lightModel.Axes.Add(new LinearAxis { Position = AxisPosition.Right, Minimum = 0, Maximum = 100 });
            //添加序列
            lightModel.Series.Add(new LineSeries { LineStyle = LineStyle.Solid });

            //配置温度图表
            tempModel = new PlotModel();
            tempModel.Title = "温度曲线(℃)";
            tempModel.TitleFontSize = 14;
            tempModel.Background = OxyColor.FromRgb(250, 250, 250);
            //添加第一Y轴
            tempModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MajorGridlineStyle = LineStyle.Solid, Minimum = 0, Maximum = 100 });
            //添加第二Y轴
            tempModel.Axes.Add(new LinearAxis { Position = AxisPosition.Right, Minimum = 0, Maximum = 100 });
            //添加序列
            tempModel.Series.Add(new LineSeries { LineStyle = LineStyle.Solid });

            /* 初始化命令按钮 */
            StartTimerCmd = new RelayCommand(o => { StartTimer(); });
            StopTimerCmd = new RelayCommand(o => { StopTimer(); });            
        }

        //向曲线图添加一个点
        public void AddNewData(int timer)
        {
            var s = (LineSeries)lightModel.Series[0];
            s.Points.Add(new DataPoint(timer, 20));
            lightModel.InvalidatePlot(true);
        }

        //清空曲线显示
        public void ClearChart()
        {
            var s = (LineSeries)lightModel.Series[0];
            s.Points.Clear();
        }

        private void StartTimer()
        {
            ExpMaster? master = Application.Current.Properties["Controller"] as ExpMaster;
            //判断控制器当前状态,若不处于可计时状态则返回
            if (master != null && master.Status == MasterStatus.Working)
            {
                //已处于试验进行中,不能重复开始计时
                MessageBox.Show("试验已在进行中。", "服务端消息", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            if (master != null && master.Status != MasterStatus.Preparing)
            {
                //设备尚未准备就绪,不能开始计时
                MessageBox.Show("请先新建试验任务。", "客户端消息", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_ws != null)
            {
                //向服务器发送开始计时命令
                Message _msg = new Message();
                _msg.Cmd = 0x03;
                _msg.Ret = 0;
                _msg.Msg = "";

                string jsoncmd = JsonSerializer.Serialize(_msg);
                //发送WebSocket消息
                if (_ws != null) _ws.Send(jsoncmd);
            }
        }

        private void StopTimer()
        {
            //判断控制器当前状态,若不处于可停止计时状态则返回
            ExpMaster? master = Application.Current.Properties["Controller"] as ExpMaster;
            if(master != null && master.WorkMode == MasterWorkMode.Experiment && 
                master.Status == MasterStatus.Working)
            {
                if(MessageBoxResult.No == MessageBox.Show("试验正在进行中，是否终止计时?","客户端消息",MessageBoxButton.YesNo,MessageBoxImage.Question))
                {
                    return;
                }
            }

            if (_ws != null)
            {
                //向服务器发送开始计时命令
                Message _msg = new Message();
                _msg.Cmd = 0x04;
                _msg.Ret = 0;
                _msg.Msg = "";

                string jsoncmd = JsonSerializer.Serialize(_msg);
                //发送WebSocket消息
                if (_ws != null) _ws.Send(jsoncmd);
            }
        }
    }
}
