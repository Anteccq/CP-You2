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

        public DisplayModeManager DisplayMode { get; }

        public ReactiveCommand ModeChangeCommand { get; }

        public MainWindowViewModel()
        {
            DisplayMode = new DisplayModeManager();
            ModeChangeCommand = new ReactiveCommand(); 
            ModeChangeCommand.Subscribe(_ => DisplayMode.ModeChange());
        }
    }
}
