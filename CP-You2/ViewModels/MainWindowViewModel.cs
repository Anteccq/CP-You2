using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        public ReactiveCommand WindowDataLoadAsyncCommand { get; }

        public MainWindowViewModel()
        {
            WindowManager = new WindowManager();
            ModeChangeCommand = new ReactiveCommand();
            ModeChangeCommand.Subscribe(_ => WindowManager.ModeChange());
            WindowDataLoadAsyncCommand = new ReactiveCommand();
            WindowDataLoadAsyncCommand.Subscribe(async _ => await WindowManager.LoadAsync());
        }

        public async Task WindowLoadAsync() => await WindowManager.LoadAsync();
    }
}