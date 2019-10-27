using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CP_You2.Interfaces;
using Prism.Mvvm;
using Utf8Json;

namespace CP_You2.Models
{
    public class WindowManager : BindableBase, IDataService
    {
        private static readonly string DIRECTORY_PATH =
            $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\ProjectATR\CP-You2";

        private const string FILE_NAME = @"\Window.json";

        private const string ERR_LOG_FILE = @"./Error.log";

        private WindowData _windowData = new WindowData();

        public WindowData WindowData
        {
            get => _windowData;
            set => this.SetProperty(ref _windowData, value);
        }

        public WindowManager()
        {
            this.Load();
            Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                    h => h.Invoke,
                    handler => this.WindowData.PropertyChanged += handler,
                    handler => this.WindowData.PropertyChanged -= handler)
                .Throttle(TimeSpan.FromMilliseconds(100))
                .Subscribe( async _ => await this.SaveAsync());
        }

        public void ModeChange()
        {
            WindowData.IsTotalMode = !WindowData.IsTotalMode;
        }

        public async Task SaveAsync()
        {
            if (!Directory.Exists(DIRECTORY_PATH)) Directory.CreateDirectory(DIRECTORY_PATH);
            try
            {
                await using var stream = File.OpenWrite(DIRECTORY_PATH + FILE_NAME);
                await JsonSerializer.SerializeAsync(stream, this.WindowData);
            }
            catch (Exception e)
            {
                await using var stream = new StreamWriter(ERR_LOG_FILE, true, Encoding.UTF8);
                await stream.WriteLineAsync($"{DateTime.Now} : {e.Message}");
            }

        }

        public async Task LoadAsync()
        {
            if (!Directory.Exists(DIRECTORY_PATH) || !File.Exists(DIRECTORY_PATH + FILE_NAME)) return;
            try
            {
                await using var stream = File.OpenRead(DIRECTORY_PATH + FILE_NAME);
                var windowData = await JsonSerializer.DeserializeAsync<WindowData>(stream);
                this.WindowData.Left = windowData.Left;
                this.WindowData.Top = windowData.Top;
                this.WindowData.IsTotalMode = windowData.IsTotalMode;
            }
            catch (Exception e)
            {
                await using var stream = new StreamWriter(ERR_LOG_FILE, true, Encoding.UTF8);
                await stream.WriteLineAsync($"{DateTime.Now} : {e.Message}");
            }
        }

        public void Load()
        {
            if (!Directory.Exists(DIRECTORY_PATH) || !File.Exists($"{DIRECTORY_PATH}{FILE_NAME}")) return;

            try
            {
                using var stream = File.OpenRead(DIRECTORY_PATH + FILE_NAME);
                var windowData = JsonSerializer.Deserialize<WindowData>(stream);
                this.WindowData.Left = windowData.Left;
                this.WindowData.Top = windowData.Top;
                this.WindowData.IsTotalMode = windowData.IsTotalMode;
            }
            catch (Exception e)
            {
                using var stream = new StreamWriter(ERR_LOG_FILE, true, Encoding.UTF8);
                stream.WriteLine($"{DateTime.Now} : {e.Message}");
            }
        }
    }
}