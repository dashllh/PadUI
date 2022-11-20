using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadUI.Core
{
    //定义控制器任务模式枚举
    enum MasterWorkMode : int
    {
        None = 0,
        Adjust,
        Calibration,
        Experiment
    }
    //定义控制器状态枚举
    enum MasterStatus : int
    {
        Idle = 0,
        Preparing,
        Read,
        Working,
        Exception
    }
    internal class ExpMaster : ObsevableObject
    {
        //控制器当前任务模式
        private MasterWorkMode _workmode;
        public MasterWorkMode WorkMode
        {
            get { return _workmode; }
            set 
            {     _workmode = value;
                  OnPropertyChanged();
            }
        }

        //控制器当前状态
        private MasterStatus _status;
        public MasterStatus Status
        {
            get { return _status; }
            set 
            {     _status = value;
                  OnPropertyChanged();
            }
        }

        public ExpMaster()
        {
            _workmode = MasterWorkMode.None;
            _status = MasterStatus.Idle;
        }
    }
}
