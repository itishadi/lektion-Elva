using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SmartHome.MVVM.ViewModels
{
    public partial class DataTimeViewModel : ObservableObject
    {
        private string _currentTime;
        private string _currentDate;
        private CancellationTokenSource _cancellationTokenSource;

        public string CurrentTime
        {
            get => _currentTime;
            set => SetProperty(ref _currentTime, value);
        }

        public string CurrentDate
        {
            get => _currentDate;
            set => SetProperty(ref _currentDate, value);
        }

        public DataTimeViewModel()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            UpdateTimePeriodically(_cancellationTokenSource.Token);
        }

        private async Task UpdateTimePeriodically(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var currentTime = DateTime.Now;
                    CurrentTime = currentTime.ToString("HH:mm");
                    CurrentDate = currentTime.ToString("dd MMMM yyyy");
                }
                catch (Exception ex)
                {
                    CurrentTime = "N/A";
                    CurrentDate = "N/A";
                    Console.WriteLine($"Error: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        public void StopUpdatingTime()
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}
