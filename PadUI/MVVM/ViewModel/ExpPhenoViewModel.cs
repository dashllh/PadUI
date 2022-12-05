using System;
using System.Collections.Generic;
using System.Windows;
using PadUI.Core;
using PadUI.MVVM.Model;
using System.Text.Json;
using System.Windows.Controls;
using PadUI.Theme;

namespace PadUI.MVVM.ViewModel
{
    internal class ExpPhenoViewModel : ObsevableObject
    {
        //测试代码: 静态属性-当前获得键盘焦点的控件引用
        public static UserControl? FocusedElement = null;

        /* 火焰时间属性定义 */
        //Timer
        public int Timer { get; set; }

        //60mm
        private int _Time60mm;
        public int Time60mm
        {
            get { return _Time60mm; }
            set { _Time60mm = value; OnPropertyChanged(); }
        }
        //110mm
        private int _Time110mm;
        public int Time110mm
        {
            get { return _Time110mm; }
            set { _Time110mm = value; OnPropertyChanged(); }
        }
        //160mm
        private int _Time160mm;
        public int Time160mm
        {
            get { return _Time160mm; }
            set { _Time160mm = value; OnPropertyChanged(); }
        }
        //210mm
        private int _Time210mm;
        public int Time210mm
        {
            get { return _Time210mm; }
            set { _Time210mm = value; OnPropertyChanged(); }
        }
        //260mm
        private int _Time260mm;
        public int Time260mm
        {
            get { return _Time260mm; }
            set { _Time260mm = value; OnPropertyChanged(); }
        }

        //310mm
        private int _Time310mm;
        public int Time310mm
        {
            get { return _Time310mm; }
            set { _Time310mm = value; OnPropertyChanged(); }
        }
        //360mm
        private int _Time360mm;
        public int Time360mm
        {
            get { return _Time360mm; }
            set { _Time360mm = value; OnPropertyChanged(); }
        }
        //410mm
        private int _Time410mm;
        public int Time410mm
        {
            get { return _Time410mm; }
            set { _Time410mm = value; OnPropertyChanged(); }
        }
        //460mm
        private int _Time460mm;
        public int Time460mm
        {
            get { return _Time460mm; }
            set { _Time460mm = value; OnPropertyChanged(); }
        }
        //510mm
        private int _Time510mm;
        public int Time510mm
        {
            get { return _Time510mm; }
            set { _Time510mm = value; OnPropertyChanged(); }
        }

        //560mm
        private int _Time560mm;
        public int Time560mm
        {
            get { return _Time560mm; }
            set { _Time560mm = value; OnPropertyChanged(); }
        }
        //610mm
        private int _Time610mm;
        public int Time610mm
        {
            get { return _Time610mm; }
            set { _Time610mm = value; OnPropertyChanged(); }
        }
        //660mm
        private int _Time660mm;
        public int Time660mm
        {
            get { return _Time660mm; }
            set { _Time660mm = value; OnPropertyChanged(); }
        }
        //710mm
        private int _Time710mm;
        public int Time710mm
        {
            get { return _Time710mm; }
            set { _Time710mm = value; OnPropertyChanged(); }
        }
        //760mm
        private int _Time760mm;
        public int Time760mm
        {
            get { return _Time760mm; }
            set { _Time760mm = value; OnPropertyChanged(); }
        }

        //810mm
        private int _Time810mm;
        public int Time810mm
        {
            get { return _Time810mm; }
            set { _Time810mm = value; OnPropertyChanged(); }
        }
        //860mm
        private int _Time860mm;
        public int Time860mm
        {
            get { return _Time860mm; }
            set { _Time860mm = value; OnPropertyChanged(); }
        }
        //910mm
        private int _Time910mm;
        public int Time910mm
        {
            get { return _Time910mm; }
            set { _Time910mm = value; OnPropertyChanged(); }
        }
        //960mm
        private int _Time960mm;
        public int Time960mm
        {
            get { return _Time960mm; }
            set { _Time960mm = value; OnPropertyChanged(); }
        }
        //1010mm
        private int _Time1010mm;
        public int Time1010mm
        {
            get { return _Time1010mm; }
            set { _Time1010mm = value; OnPropertyChanged(); }
        }

        //火焰熄灭时间
        private int _flameOutTime;
        public int FlameOutTime
        {
            get { return _flameOutTime; }
            set { _flameOutTime = value; OnPropertyChanged(); }
        }

        /* 火焰距离辐射值属性定义 */
        //10Min
        private double _FlameDist10min;
        public double FlameDist10min
        {
            get { return _FlameDist10min; }
            set { _FlameDist10min = value; OnPropertyChanged(); }
        }
        //20Min
        private double _FlameDist20min;
        public double FlameDist20min
        {
            get { return _FlameDist20min; }
            set { _FlameDist20min = value; OnPropertyChanged(); }
        }
        //30Min
        private double _FlameDist30min;
        public double FlameDist30min
        {
            get { return _FlameDist30min; }
            set { _FlameDist30min = value; OnPropertyChanged(); }
        }
        //HF-10
        private double _HF10min;
        public double HF10min
        {
            get { return _HF10min; }
            set { _HF10min = value; OnPropertyChanged(); }
        }
        //HF-20
        private double _HF20min;
        public double HF20min
        {
            get { return _HF20min; }
            set { _HF20min = value; OnPropertyChanged(); }
        }
        //HF-30
        private double _HF30min;
        public double HF30min
        {
            get { return _HF30min; }
            set { _HF30min = value; OnPropertyChanged(); }
        }

        /* 火焰熄灭方式 */
        //自然熄灭
        private bool _ZiXiMie;
        public bool ZiXiMie
        {
            get { return _ZiXiMie; }
            set { _ZiXiMie = value; OnPropertyChanged(); }
        }
        //强制熄灭
        private bool _QiangXiMie;
        public bool QiangXiMie
        {
            get { return _QiangXiMie; }
            set { _QiangXiMie = value; OnPropertyChanged(); }
        }

        //火焰最远传播距离
        private double _FlameMaxDist;
        public double FlameMaxDist
        {
            get { return _FlameMaxDist; }
            set { _FlameMaxDist = value; OnPropertyChanged(); }
        }
        //CHF
        private double _CHF;
        public double CHF
        {
            get { return _CHF; }
            set { _CHF = value; OnPropertyChanged(); }
        }

        /* 试验现象属性定义 */
        //闪燃
        private bool _IsShanRan;
        public bool IsShanRan
        {
            get { return _IsShanRan; }
            set { _IsShanRan = value; OnPropertyChanged(); }
        }
        //熔化
        private bool _IsRongHua;
        public bool IsRongHua
        {
            get { return _IsRongHua; }
            set { _IsRongHua = value; OnPropertyChanged(); }
        }
        //起泡
        private bool _IsQiPao;
        public bool IsQiPao
        {
            get { return _IsQiPao; }
            set { _IsQiPao = value; OnPropertyChanged(); }
        }
        //烧穿
        private bool _IsShaoChuan;
        public bool IsShaoChuan
        {
            get { return _IsShaoChuan; }
            set { _IsShaoChuan = value; OnPropertyChanged(); }
        }
        //再燃
        private bool _IsZaiRan;
        public bool IsZaiRan
        {
            get { return _IsZaiRan; }
            set { _IsZaiRan = value; OnPropertyChanged(); }
        }
        //再燃时间
        private int _ZaiRanTime;
        public int ZaiRanTime
        {
            get { return _ZaiRanTime; }
            set { _ZaiRanTime = value; OnPropertyChanged(); }
        }
        //再燃位置
        private int _ZaiRanLoc;
        public int ZaiRanLoc
        {
            get { return _ZaiRanLoc; }
            set { _ZaiRanLoc = value; OnPropertyChanged(); }
        }
        //其他现象
        private string? _OtherMemo = String.Empty;
        public string? OtherMemo 
        {
            get { return _OtherMemo; }
            set { _OtherMemo = value; OnPropertyChanged(); }
        }

        /* 命令对象 */
        public RelayCommand RefreshCmd { get; set; }
        public RelayCommand SubmitCmd { get; set; }
        public RelayCommand RecordTimeCmd { get; set; }

        //WebSocket对象
        private readonly WSClientHelper? _ws;

        //构造函数
        public ExpPhenoViewModel()
        {
            //属性初始化
            IsShanRan = false;
            ZiXiMie = true;
            QiangXiMie = true;

            //初始化WebSocket客户端回调方法
            _ws = Application.Current.Properties["WSProxy"] as WSClientHelper;

            RecordTimeCmd = new RelayCommand(o => { RecordTime(); });
            RefreshCmd = new RelayCommand(o => { Refresh(); });
            SubmitCmd = new RelayCommand(o => { Submit(); });
        }

        //清空界面显示
        public void ClearUI()
        {
            Time60mm = 0;
            Time110mm = 0;
            Time160mm = 0;
            Time210mm = 0;
            Time260mm = 0;
            Time310mm = 0;
            Time360mm = 0;
            Time410mm = 0;
            Time460mm = 0;
            Time510mm = 0;
            Time560mm = 0;
            Time610mm = 0;
            Time660mm = 0;
            Time710mm = 0;
            Time760mm = 0;
            Time810mm = 0;
            Time860mm = 0;
            Time910mm = 0;
            Time960mm = 0;
            Time1010mm = 0;
            FlameOutTime = 0;
            FlameDist10min = 0;
            FlameDist20min = 0;
            FlameDist30min = 0;
            HF10min = 0;
            HF20min = 0;
            HF30min = 0;
            ZiXiMie = true;
            QiangXiMie = false;
            FlameMaxDist = 0;
            CHF = 0;
            IsShanRan = false;
            IsRongHua = false;
            IsQiPao = false;
            IsShaoChuan = false;
            IsZaiRan = false;
            ZaiRanTime = 0;
            ZaiRanLoc = 0;
            OtherMemo = "";
        }

        //功能: 将当前计时值赋值给焦点所在文本框对应的变量
        private void RecordTime()
        {
            //获取当前计时值并赋值
            if(FocusedElement != null)
                ((FlameTimeItem)FocusedElement).Value = this.Timer;
        }
        private void Refresh()
        {
            /* 从服务端获取最新试验记录信息 */
            Message _msg = new Message();
            _msg.Cmd = 0x06;
            _msg.Ret = 0;
            _msg.Msg = "";
            _msg.Param = new Dictionary<string, object>();
            string jsoncmd = JsonSerializer.Serialize(_msg);
            //发送WebSocket消息
            if (_ws != null) _ws.Send(jsoncmd);
        }
        private void Submit()
        {
            //向试验控制器提交新建试验信息
            Message _msg = new Message();
            _msg.Cmd = 0x05;
            _msg.Ret = 0;
            _msg.Msg = "";
            _msg.Param = new Dictionary<string, object>();
            /* 添加火焰时间参数 */
            _msg.Param.Add("60mm",  _Time60mm);       //60mm
            _msg.Param.Add("110mm", _Time110mm);      //110mm
            _msg.Param.Add("160mm", _Time160mm);      //160mm
            _msg.Param.Add("210mm", _Time210mm);      //210mm
            _msg.Param.Add("260mm", _Time260mm);      //260mm
            _msg.Param.Add("310mm", _Time310mm);      //310mm
            _msg.Param.Add("360mm", _Time360mm);      //360mm
            _msg.Param.Add("410mm", _Time410mm);      //410mm
            _msg.Param.Add("460mm", _Time460mm);      //460mm
            _msg.Param.Add("510mm", _Time510mm);      //510mm
            _msg.Param.Add("560mm", _Time560mm);      //560mm
            _msg.Param.Add("610mm", _Time610mm);      //610mm
            _msg.Param.Add("660mm", _Time660mm);      //660mm
            _msg.Param.Add("710mm", _Time710mm);      //710mm
            _msg.Param.Add("760mm", _Time760mm);      //760mm
            _msg.Param.Add("810mm", _Time810mm);      //810mm
            _msg.Param.Add("860mm", _Time860mm);      //860mm
            _msg.Param.Add("910mm", _Time910mm);      //910mm
            _msg.Param.Add("960mm", _Time960mm);      //960mm
            _msg.Param.Add("1010mm",_Time1010mm);     //1010mm
            _msg.Param.Add("flameouttime", _flameOutTime);  //火焰熄灭时间
            /* 添加火焰熄灭方式参数 */
            _msg.Param.Add("ziximie",    _ZiXiMie);    //自然熄灭
            _msg.Param.Add("qiangximie", _QiangXiMie); //强制熄灭
            /* 添加火焰距离,辐射参数 */
            _msg.Param.Add("dist10min",  _FlameDist10min); //10min火焰传播距离
            _msg.Param.Add("dist20min",  _FlameDist20min); //20min火焰传播距离
            _msg.Param.Add("dist30min",  _FlameDist30min); //30min火焰传播距离
            _msg.Param.Add("maxdist",    _FlameMaxDist);   //最远传播距离
            _msg.Param.Add("hf10min",    _HF10min);        //HF10Min
            _msg.Param.Add("hf20min",    _HF20min);        //HF20Min
            _msg.Param.Add("hf30min",    _HF30min);        //HF30Min            
            _msg.Param.Add("chf",        _CHF);            //CHF
            /* 添加试验现象参数 */
            _msg.Param.Add("shanran",    _IsShanRan);   //闪燃
            _msg.Param.Add("ronghua",    _IsRongHua);   //熔化
            _msg.Param.Add("qipao",      _IsQiPao);     //起泡
            _msg.Param.Add("shaochuan",  _IsShaoChuan); //烧穿
            _msg.Param.Add("zairan",     _IsZaiRan);    //再燃
            _msg.Param.Add("zairantime", _ZaiRanTime);  //再燃时间
            _msg.Param.Add("zairanloc",  _ZaiRanLoc);   //再燃位置
            _msg.Param.Add("othermemo",  _OtherMemo == null ? "": _OtherMemo);   //其他现象描述

            string jsoncmd = JsonSerializer.Serialize(_msg);
            //发送WebSocket消息
            if (_ws != null) _ws.Send(jsoncmd);            
        }
    }
}
