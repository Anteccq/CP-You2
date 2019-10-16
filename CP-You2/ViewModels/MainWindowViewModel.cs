using System;
using System.Collections.Generic;
using System.Text;
using CP_You2.Models;
using Reactive.Bindings;
using Prism.Mvvm;

namespace CP_You2.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public ReactiveProperty<CpuPercentageManager> CpuPercentage { get; }

        public MainWindowViewModel()
        {
            CpuPercentage = new ReactiveProperty<CpuPercentageManager>(new CpuPercentageManager());
        }
    }
}
