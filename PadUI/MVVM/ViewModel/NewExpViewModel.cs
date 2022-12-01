using System;
using System.Windows;
using PadUI.Core;
using PadUI.MVVM.Model;
using System.Collections.Generic;
using System.Text.Json;

namespace PadUI.MVVM.ViewModel
{
    class NewExpViewModel : ObsevableObject
    {
        //定义按钮命令对象
        public RelayCommand? SubmitCmd { get; set; }
        public RelayCommand? ResetCmd { get; set; }

        /* 定义ViewModel属性值 */
        //样品编号
        private string _smpid;
        public string SmpId
        {
            get { return _smpid; }
            set { _smpid = value; OnPropertyChanged(); }
        }
        //试验编号
        private string _expid;
        public string ExpId
        {
            get { return _expid; }
            set { _expid = value; OnPropertyChanged(); }
        }
        //样品名称
        private string _smpname;
        public string SmpName
        {
            get { return _smpname; }
            set { _smpname = value; OnPropertyChanged(); }
        }
        //规格型号
        private string _smpspec;
        public string SmpSpec
        {
            get { return _smpspec; }
            set { _smpspec = value; OnPropertyChanged(); }
        }
        //密度
        private double _density;
        public double Density
        {
            get { return _density; }
            set { _density = value; OnPropertyChanged(); }
        }
        //面密度
        private double _sdensity;
        public double SDensity
        {
            get { return _sdensity; }
            set { _sdensity = value; OnPropertyChanged(); }
        }
        //备注
        private string _expmemo;
        public string ExpMemo
        {
            get { return _expmemo; }
            set { _expmemo = value; OnPropertyChanged(); }
        }

        private readonly WSClientHelper? _ws;
        public NewExpViewModel()
        {
            //初始化WebSocket客户端回调方法
            _ws = Application.Current.Properties["WSProxy"] as WSClientHelper;
            //初始化命令按钮
            SubmitCmd = new RelayCommand(o => { SubmitInfo(); });
            ResetCmd = new RelayCommand(o => { ResetInfo(); });

            //初始化数据字段
            SmpId = "";
            ExpId = "";
            SmpName = "";
            SmpSpec = "";
            Density = 0.0;
            SDensity = 0.0;
            ExpMemo = "";
        }
        private void SubmitInfo()
        {
            //向试验控制器提交新建试验信息
            Message _msg = new Message();
            _msg.Cmd = 0x01;
            _msg.Ret = 0;
            _msg.Msg = "";
            _msg.Param = new Dictionary<string, object>();
            _msg.Param.Add("smpid", SmpId);      //样品编号
            _msg.Param.Add("expid", ExpId);      //试验编号
            _msg.Param.Add("smpname", SmpName);  //样品名称
            _msg.Param.Add("spec", SmpSpec);     //规格型号
            _msg.Param.Add("density", Density);  //密度
            _msg.Param.Add("sdensity", SDensity);//面密度
            _msg.Param.Add("memo", ExpMemo);     //试验备注
            //序列化为JSON对象            
            //byte[] cmdbytes = JsonSerializer.SerializeToUtf8Bytes(_msg, new JsonSerializerOptions()
            //{
            //    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs)
            //});
            //string jsoncmd = Encoding.UTF8.GetString(cmdbytes);

            //string jsoncmd = JsonSerializer.Serialize(_msg, new JsonSerializerOptions()
            //{
            //    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs)
            //});
            string jsoncmd = JsonSerializer.Serialize(_msg);
            //发送WebSocket消息
            if (_ws != null) _ws.Send(jsoncmd);
        }

        private void ResetInfo()
        {
            //清空界面值
            SmpId = "";
            ExpId = "";
            SmpName = "";
            SmpSpec = "";
            Density = 0.0;
            SDensity = 0.0;
            ExpMemo = "";
        }
    }
}
