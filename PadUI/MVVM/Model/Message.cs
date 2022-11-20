using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadUI.MVVM.Model
{
    /* 定义用于与服务器通信的消息对象 */
    internal class Message
    {
        public int Cmd { get; set; }
        public int Ret { get; set; }
        public string? Msg { get; set; }
        public Dictionary<string, object>? Param { get; set; }
    }
}
