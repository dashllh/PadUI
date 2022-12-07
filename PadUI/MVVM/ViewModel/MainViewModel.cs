using System;
using System.Windows;
using PadUI.Core;
using PadUI.MVVM.Model;
using System.Text.Json;
using System.Windows.Media;

namespace PadUI.MVVM.ViewModel
{
    class MainViewModel : ObsevableObject
    {
        public RelayCommand NewExpCmd { get; set; }
        public RelayCommand RealTimeCmd { get; set; }
        public RelayCommand AboutCmd { get; set; }
        public RelayCommand CtrlPanelCmd { get; set; }
        public RelayCommand QuitCmd { get; set; }
        public RelayCommand CaliCmd { get; set; }
        public RelayCommand ExpPhenoCmd { get; set; }
        public NewExpViewModel NewExpVM { get; set; }
        public RealTimeViewModel RealTimeVM { get; set; }
        public ControlPanelViewModel ControlPanelVM { get; set; }
        public CalibrationViewModel CalibrationVM { get; set; } 
        public ExpPhenoViewModel ExpPhenoVM { get; set; }

        private readonly WSClientHelper _ws;        

        //视图属性声明
        private object _currentView;
        public object CurrentView 
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }   
        /* 
         * 功能: 客户端WebSocket响应函数 
         * 参数:
         *       data - 来自服务器的消息(JSON).格式定义参见通信协议文档
         */
        private void WSocketClient_OnMessage(object sender, string data)
        {
            ExpMaster master = Application.Current.Properties["Controller"] as ExpMaster;
            try
            {
                //响应服务器消息,更新对应UI
                if (data != null)
                {
                    Message message = JsonSerializer.Deserialize<Message>(data);
                    switch (message.Cmd)
                    {
                        case 0x00:  //显示实时数据
                            RealTimeVM.Timer = int.Parse(message.Param["Timer"].ToString());     //试验计时
                            RealTimeVM.Light = double.Parse(message.Param["Light"].ToString());  //光通量
                            RealTimeVM.HF = double.Parse(message.Param["HF"].ToString());        //热辐射值
                            RealTimeVM.DuctF = double.Parse(message.Param["DuctF"].ToString());  //烟道风速
                            RealTimeVM.DuctT = double.Parse(message.Param["DuctT"].ToString());  //烟道温度
                            RealTimeVM.CHT = double.Parse(message.Param["CHT"].ToString());      //箱体温度
                            RealTimeVM.BBT = double.Parse(message.Param["BBT"].ToString());      //黑体温度
                            //更新试验界面曲线图
                            RealTimeVM.RefreshChart(RealTimeVM.Timer, RealTimeVM.Light, 
                                RealTimeVM.CHT,RealTimeVM.BBT);
                            //同步更新试验现象记录ViewModel对象的Timer属性
                            ExpPhenoVM.Timer = RealTimeVM.Timer;
                            break;
                        case 0x11:  //服务端发生异常
                            break;
                        case 0x12:  //更新遥控端本地控制器状态(服务端主动更新)
                            if(master != null)
                            {
                                master.WorkMode = (MasterWorkMode)Enum.Parse(typeof(MasterWorkMode), message.Param["WorkMode"].ToString());
                                master.Status = (MasterStatus)Enum.Parse(typeof(MasterStatus), message.Param["Status"].ToString());
                            }
                            break;
                        case 0x01:  //新建试验任务
                            //判断是否执行成功
                            if (message.Ret == 0)
                            {
                                //更新本地控制器状态
                                master.WorkMode = MasterWorkMode.Experiment;
                                master.Status = MasterStatus.Preparing;
                                //切换本地视图
                                CurrentView = RealTimeVM;  
                            }                                
                            else
                                MessageBox.Show(message.Msg);
                            break;
                        case 0x02:  //新建标定任务
                            break;
                        case 0x03:  //开始计时
                            if(message.Ret == -1) //开始计时命令执行错误                            
                                MessageBox.Show(message.Msg, "服务端消息", MessageBoxButton.OK, MessageBoxImage.Error);
                            else if (master != null)
                            {
                                //更新试验控制器本地状态及工作模式
                                master.WorkMode = (MasterWorkMode)Enum.Parse(typeof(MasterWorkMode), message.Param["WorkMode"].ToString());
                                master.Status = (MasterStatus)Enum.Parse(typeof(MasterStatus), message.Param["Status"].ToString());
                            }
                            break;
                        case 0x04:  //停止计时
                            if (message.Ret == -1) //停止计时命令执行错误                            
                                MessageBox.Show(message.Msg, "服务端消息", MessageBoxButton.OK, MessageBoxImage.Error);
                            else if (master != null)
                            {
                                //更新试验控制器本地状态及工作模式
                                master.WorkMode = (MasterWorkMode)Enum.Parse(typeof(MasterWorkMode), message.Param["WorkMode"].ToString());
                                master.Status = (MasterStatus)Enum.Parse(typeof(MasterStatus), message.Param["Status"].ToString());
                                //刷新本地界面显示(更新ViewModel)
                                RealTimeVM.Timer = 0;    //试验计时
                                RealTimeVM.Light = 0.0;  //光通量
                                RealTimeVM.HF    = 0.0;  //热辐射值
                                RealTimeVM.DuctF = 0.0;  //烟道风速
                                RealTimeVM.DuctT = 0.0;  //烟道温度
                                RealTimeVM.CHT   = 0.0;  //箱体温度
                                RealTimeVM.BBT   = 0.0;  //黑体温度
                                //清空本地试验现象界面显示
                                ExpPhenoVM?.ClearUI();

                                //清空图表显示
                                RealTimeVM.ClearChart();
                            }
                            break;
                        case 0x05:  //记录试验现象(由客户端提交,服务器响应)
                            if(message.Ret == -1)                            
                                MessageBox.Show(message.Msg, "服务端消息", MessageBoxButton.OK, MessageBoxImage.Error);    
                            else
                            {
                                MessageBox.Show("提交成功!", "服务端消息", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            break;
                        case 0x06: //同步服务器端记录的试验现象
                            if (message.Ret == -1) //开始计时命令执行错误                            
                                MessageBox.Show(message.Msg, "服务端消息", MessageBoxButton.OK, MessageBoxImage.Error);
                            else if (master != null)
                            {
                                /* 添加火焰时间参数 */
                                ExpPhenoVM.Time60mm  = int.Parse(message.Param["60mm"].ToString());
                                ExpPhenoVM.Time110mm = int.Parse(message.Param["110mm"].ToString());
                                ExpPhenoVM.Time160mm = int.Parse(message.Param["160mm"].ToString());
                                ExpPhenoVM.Time210mm = int.Parse(message.Param["210mm"].ToString());
                                ExpPhenoVM.Time260mm = int.Parse(message.Param["260mm"].ToString());
                                ExpPhenoVM.Time310mm = int.Parse(message.Param["310mm"].ToString());
                                ExpPhenoVM.Time360mm = int.Parse(message.Param["360mm"].ToString());
                                ExpPhenoVM.Time410mm = int.Parse(message.Param["410mm"].ToString());
                                ExpPhenoVM.Time460mm = int.Parse(message.Param["460mm"].ToString());
                                ExpPhenoVM.Time510mm = int.Parse(message.Param["510mm"].ToString());
                                ExpPhenoVM.Time560mm = int.Parse(message.Param["560mm"].ToString());
                                ExpPhenoVM.Time610mm = int.Parse(message.Param["610mm"].ToString());
                                ExpPhenoVM.Time660mm = int.Parse(message.Param["660mm"].ToString());
                                ExpPhenoVM.Time710mm = int.Parse(message.Param["710mm"].ToString());
                                ExpPhenoVM.Time760mm = int.Parse(message.Param["760mm"].ToString());
                                ExpPhenoVM.Time810mm = int.Parse(message.Param["810mm"].ToString());
                                ExpPhenoVM.Time860mm = int.Parse(message.Param["860mm"].ToString());
                                ExpPhenoVM.Time910mm = int.Parse(message.Param["910mm"].ToString());
                                ExpPhenoVM.Time960mm = int.Parse(message.Param["960mm"].ToString());
                                ExpPhenoVM.Time1010mm = int.Parse(message.Param["1010mm"].ToString());
                                /* 添加火焰熄灭方式参数 */
                                int iXimieType = int.Parse(message.Param["ximietype"].ToString());
                                if (iXimieType == 0)
                                {
                                    ExpPhenoVM.ZiXiMie = true;
                                    ExpPhenoVM.QiangXiMie = false;
                                }                                    
                                else
                                {
                                    ExpPhenoVM.ZiXiMie = false;
                                    ExpPhenoVM.QiangXiMie = true;
                                }
                                /* 添加火焰距离,辐射参数 */
                                ExpPhenoVM.FlameDist10min = double.Parse(message.Param["dist10min"].ToString());
                                ExpPhenoVM.FlameDist20min = double.Parse(message.Param["dist20min"].ToString());
                                ExpPhenoVM.FlameDist30min = double.Parse(message.Param["dist30min"].ToString());
                                ExpPhenoVM.FlameMaxDist = double.Parse(message.Param["maxdist"].ToString());
                                ExpPhenoVM.HF10min = double.Parse(message.Param["hf10min"].ToString());
                                ExpPhenoVM.HF20min = double.Parse(message.Param["hf10min"].ToString());
                                ExpPhenoVM.HF30min = double.Parse(message.Param["hf10min"].ToString());
                                ExpPhenoVM.CHF = double.Parse(message.Param["chf"].ToString());
                                /* 添加试验现象参数 */
                                ExpPhenoVM.IsShanRan = bool.Parse(message.Param["shanran"].ToString());
                                ExpPhenoVM.IsRongHua = bool.Parse(message.Param["ronghua"].ToString());
                                ExpPhenoVM.IsQiPao = bool.Parse(message.Param["qipao"].ToString());
                                ExpPhenoVM.IsShaoChuan = bool.Parse(message.Param["shaochuan"].ToString());
                                ExpPhenoVM.IsZaiRan = bool.Parse(message.Param["zairan"].ToString());
                                if(ExpPhenoVM.IsZaiRan)
                                {
                                    ExpPhenoVM.ZaiRanTime = int.Parse(message.Param["zairantime"].ToString());
                                    ExpPhenoVM.ZaiRanLoc = int.Parse(message.Param["zairanloc"].ToString());
                                }
                                ExpPhenoVM.OtherMemo = message.Param["othermemo"].ToString();
                            }
                            break;
                        case 0x07: //取消当前任务
                            break;
                        case 0x08: //自动更新火焰时间及火焰位置的显示(由服务器主动发起)
                            string item = message.Param["item"].ToString();
                            int value = int.Parse(message.Param["value"].ToString());
                            switch (item)
                            {
                                case "time60":
                                    ExpPhenoVM.Time60mm = value;
                                    break;
                                case "time110":
                                    ExpPhenoVM.Time110mm = value;
                                    break;
                                case "time160":
                                    ExpPhenoVM.Time160mm = value;
                                    break;
                                case "time210":
                                    ExpPhenoVM.Time210mm = value;
                                    break;
                                case "time260":
                                    ExpPhenoVM.Time260mm = value;
                                    break;
                                case "time310":
                                    ExpPhenoVM.Time310mm = value;
                                    break;
                                case "time360":
                                    ExpPhenoVM.Time360mm = value;
                                    break;
                                case "time410":
                                    ExpPhenoVM.Time410mm = value;
                                    break;
                                case "time460":
                                    ExpPhenoVM.Time460mm = value;
                                    break;
                                case "time510":
                                    ExpPhenoVM.Time510mm = value;
                                    break;
                                case "time560":
                                    ExpPhenoVM.Time560mm = value;
                                    break;
                                case "time610":
                                    ExpPhenoVM.Time610mm = value;
                                    break;
                                case "time660":
                                    ExpPhenoVM.Time660mm = value;
                                    break;
                                case "time710":
                                    ExpPhenoVM.Time710mm = value;
                                    break;
                                case "time760":
                                    ExpPhenoVM.Time760mm = value;
                                    break;
                                case "time810":
                                    ExpPhenoVM.Time810mm = value;
                                    break;
                                case "time860":
                                    ExpPhenoVM.Time860mm = value;
                                    break;
                                case "time910":
                                    ExpPhenoVM.Time910mm = value;
                                    break;
                                case "time960":
                                    ExpPhenoVM.Time960mm = value;
                                    break;
                                case "time1010":
                                    ExpPhenoVM.Time1010mm = value;
                                    break;
                                case "dist10min":
                                    ExpPhenoVM.FlameDist10min = value;
                                    break;
                                case "dist20min":
                                    ExpPhenoVM.FlameDist20min = value;   
                                    break;
                                case "dist30min":
                                    ExpPhenoVM.FlameDist30min = value;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public MainViewModel()
        {
            //初始化WebSocket客户端回调方法
            _ws = Application.Current.Properties["WSProxy"] as WSClientHelper;
            if(_ws != null) _ws.OnMessage += WSocketClient_OnMessage;
            //初始化新建试验命令按钮
            NewExpCmd = new RelayCommand(o => { CurrentView = NewExpVM; });
            //初始化标定视图命令按钮
            CaliCmd = new RelayCommand(o => { CurrentView = CalibrationVM; });
            //初始化实时数据命令按钮
            RealTimeCmd = new RelayCommand(o => { CurrentView = RealTimeVM; });
            //初始化控制视图命令按钮
            CtrlPanelCmd = new RelayCommand(o => { CurrentView = ControlPanelVM; });
            //初始化现象记录视图命令按钮
            ExpPhenoCmd = new RelayCommand(o => { CurrentView = ExpPhenoVM; });
            //初始化关于系统命令按钮
            AboutCmd = new RelayCommand(o =>
            {

            });
            //初始化退出系统命令按钮
            QuitCmd = new RelayCommand(o => { QuitApp(); });
            //初始化视图模型
            NewExpVM = new NewExpViewModel();  //新建试验视图
            CalibrationVM = new CalibrationViewModel(); //标定视图
            ExpPhenoVM = new ExpPhenoViewModel(); //现象记录视图
            ControlPanelVM = new ControlPanelViewModel();
            RealTimeVM = Application.Current.Properties["RTVM"] as RealTimeViewModel;
            CurrentView = NewExpVM;
        }

        private void QuitApp()
        {
            //判断试验控制器状态,若为【工作中】("工作中"表示正在计时,
            //服务端正同步向遥控端发送实时消息,退出遥控端会导致服务端崩溃),则不允许退出
            ExpMaster? master = Application.Current.Properties["Controller"] as ExpMaster;
            if (master != null)
            {
                if(master.Status == MasterStatus.Working)
                {
                    MessageBox.Show("请先终止计时,再退出客户端程序。", "客户端消息", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    if (_ws != null)
                        _ws.Close();

                    App.Current.Shutdown();
                }
            }            
        }
    }
}
