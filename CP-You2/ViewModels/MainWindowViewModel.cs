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
        public CpuPercentageManager CpuPercentage { get; } = new CpuPercentageManager();

        public WindowManager WindowManager { get; }

        public ReactiveCommand ModeChangeCommand { get; }

        public MainWindowViewModel()
        {
            WindowManager = new WindowManager();
            ModeChangeCommand = new ReactiveCommand(); 
            ModeChangeCommand.Subscribe(_ => WindowManager.ModeChange());
        }
    }
}
