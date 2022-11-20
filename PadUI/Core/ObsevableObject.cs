using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PadUI.Core
{
    public class ObsevableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
