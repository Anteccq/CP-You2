using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CP_You2.Interfaces;
using Prism.Mvvm;
using Utf8Json;

namespace CP_You2.Models
{
    public class WindowManager : BindableBase, IDataService
    {
        private static readonly string DIRECTORY_PATH =
            $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\ProjectATR\CP-You2";

        private const string FILE_NAME = @"\Window.config";

        private const string ERR_LOG_FILE = @"./Error.log";

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

        public WindowManager()
        {
            this.PropertyChanged += async (a, e) => await this.SaveAsync();
        }

        public void ModeChange()
        {
            IsTotalMode = !IsTotalMode;
        }

        public async Task SaveAsync()
        {
            if (!Directory.Exists(DIRECTORY_PATH)) Directory.CreateDirectory(DIRECTORY_PATH);
            try
            {
                await using var stream = File.OpenWrite(DIRECTORY_PATH + FILE_NAME);
                await JsonSerializer.SerializeAsync(stream, this);
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
                var windowData = await JsonSerializer.DeserializeAsync<WindowManager>(stream);
                this.Left = windowData.Left;
                this.Top = windowData.Top;
                this.IsTotalMode = windowData.IsTotalMode;
            }
            catch (Exception e)
            {
                await using var stream = new StreamWriter(ERR_LOG_FILE, true, Encoding.UTF8);
                await stream.WriteLineAsync($"{DateTime.Now} : {e.Message}");
            }
        }
    }
}