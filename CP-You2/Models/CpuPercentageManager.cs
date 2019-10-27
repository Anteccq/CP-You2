using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using Reactive;
using Reactive.Bindings;
using Reactive.Bindings.Binding;
using Prism.Mvvm;

namespace CP_You2.Models
{
    public class CpuPercentageManager : BindableBase
    {
        private const string CATEGORY_NAME = "Processor";
        private const string COUNTER_NAME = "% Processor Time";
        private const string INSTANCE_NAME = "_Total";
        private static readonly PerformanceCounter TotalCounter = new PerformanceCounter(CATEGORY_NAME, COUNTER_NAME, INSTANCE_NAME);
        private static readonly PerformanceCounter[] Counters = new PerformanceCounter[Environment.ProcessorCount];
        private static IObservable<long> PerSecTimer = null;


        private int _totalPercentage = -1;
        public int TotalPercentage
        {
            get
            {
                if (_totalPercentage != -1) return _totalPercentage;
                _totalPercentage = 0;

                if (PerSecTimer == null) PerSecTimer = Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(1000));
                PerSecTimer.Subscribe(_ => this.SetProperty(ref _totalPercentage, (int)TotalCounter.NextValue() ));
                return _totalPercentage;
            }
        }

        private ReactiveCollection<int> _cpuPercentageCollection = null;

        public ReactiveCollection<int> CpuPercentageCollection
        {
            get
            {
                if (_cpuPercentageCollection == null)
                {
                    _cpuPercentageCollection = new ReactiveCollection<int>();
                    for (int i = 0; i < Environment.ProcessorCount; i++)
                    {
                        _cpuPercentageCollection.Add(0);
                    }
                }

                if (PerSecTimer == null) PerSecTimer = Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(1000));
                PerSecTimer.ObserveOn(SynchronizationContext.Current)
                    .Subscribe(_ =>
                    {
                        for (var i = 0; i < Counters.Length; i++)
                        {
                            _cpuPercentageCollection[i] = (int)Counters[i].NextValue();
                        }
                    });

                return _cpuPercentageCollection;
            }
        }

        public CpuPercentageManager()
        {
            for (var i = 0; i < Environment.ProcessorCount; i++)
            {
                Counters[i] = new PerformanceCounter(CATEGORY_NAME, COUNTER_NAME, i.ToString());
            }
        }

    }
}
