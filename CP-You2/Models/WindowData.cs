using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace CP_You2.Models
{
    public class WindowData : BindableBase
    {
        private bool _isTotalMode = true;

        public bool IsTotalMode
        {
            get => _isTotalMode;
            set => this.SetProperty(ref _isTotalMode, value);
        }

        private double _left;

        public double Left
        {
            get => _left;
            set => this.SetProperty(ref _left, value);
        }

        private double _top;

        public double Top
        {
            get => _top;
            set => this.SetProperty(ref _top, value);
        }

        private bool _isTopMost;

        public bool IsTopMost
        {
            get => _isTopMost;
            set => this.SetProperty(ref _isTopMost, value);
        }
    }
}
