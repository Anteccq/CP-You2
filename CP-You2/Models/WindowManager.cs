using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Prism.Mvvm;

namespace CP_You2.Models
{
    public class WindowManager : BindableBase
    {
        private bool _isTotalMode = true;

        public bool IsTotalMode
        {
            get => _isTotalMode;
            set => this.SetProperty(ref _isTotalMode, value);
        }

        public void ModeChange()
        {
            IsTotalMode = !IsTotalMode;
        }
    }
}