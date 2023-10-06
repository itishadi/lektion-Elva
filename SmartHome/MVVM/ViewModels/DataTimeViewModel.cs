//using System;
//using System.Net.Http;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.Maui.Controls;
//using CommunityToolkit.Mvvm.ComponentModel;
//using Newtonsoft.Json;
//using System.ComponentModel;
//using System.Diagnostics;
//using System.Windows.Input;
//using CommunityToolkit.Mvvm.Input;

//namespace SmartHome.MVVM.ViewModels;

//public partial class DataTimeViewModel : ObservableObject, INotifyPropertyChanged
//{

//    [RelayCommand]
//    async Task GoBack() => await Shell.Current.GoToAsync("..");

//    private string _currentTime;
//    private string _currentDate;
//    private string _temperature;
//    private string _condition;
//    private CancellationTokenSource _cancellationTokenSource;

//    public string CurrentTime
//    {
//        get => _currentTime;
//        set => SetProperty(ref _currentTime, value);
//    }

//    public string CurrentDate
//    {
//        get => _currentDate;
//        set => SetProperty(ref _currentDate, value);
//    }

//    public string Temperature
//    {
//        get { return _temperature; }
//        set { _temperature = value; OnPropertyChanged(nameof(Temperature)); }
//    }

//    public string Condition
//    {
//        get { return _condition; }
//        set { _condition = value; OnPropertyChanged(nameof(Condition)); }
//    }


//    [Obsolete]
//    public DataTimeViewModel()
//    {

//        Device.StartTimer(TimeSpan.FromMinutes(15), () =>
//        {
//            GetWeatherAsync();
//            return true;
//        });

//        GetWeatherAsync();

//        _cancellationTokenSource = new CancellationTokenSource();
//        _ = UpdateTimePeriodically(_cancellationTokenSource.Token);
//    }



//    public event PropertyChangedEventHandler PropertyChanged;
//    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

//    private async void GetWeatherAsync()
//    {
//        using var http = new HttpClient();
//        try
//        {
//            var data = JsonConvert.DeserializeObject<dynamic>(await http.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?lat=59.1875&lon=18.1232&appid=b4a3119e986341f8f3a4d159c5787679"));
//            Temperature = (data!.main.temp - 273.15).ToString("#,##0.0");

//            switch (data!.weather[0].description.ToString())
//            {
//                case "clear sky":
//                    Condition = "\ue28f";
//                    break;
//                case "few clouds":
//                    Condition = "\uf6c4";
//                    break;
//                case "overcast clouds":
//                    Condition = "\uf744";
//                    break;
//                case "scattered clouds":
//                    Condition = "\uf0c2";
//                    break;
//                case "broken clouds":
//                    Condition = "\uf744";
//                    break;
//                case "shower rain":
//                    Condition = "\uf738";
//                    break;
//                case "rain":
//                    Condition = "\uf740";
//                    break;
//                case "thunderstorm":
//                    Condition = "\uf76c";
//                    break;
//                case "snow":
//                    Condition = "\uf742";
//                    break;
//                case "mist":
//                    Condition = "\uf74e";
//                    break;

//                default:
//                    Condition = "\ue137";
//                    break;
//            }
//        }
//        catch (Exception ex)
//        {
//            Debug.WriteLine($"Failed to get weather data. Error: {ex.Message}");
//            Temperature = "--";
//        }
//    }


//    private async Task UpdateTimePeriodically(CancellationToken cancellationToken)
//    {
//        while (!cancellationToken.IsCancellationRequested)
//        {
//            try
//            {
//                var currentTime = DateTime.Now;
//                CurrentTime = currentTime.ToString("HH:mm");
//                CurrentDate = currentTime.ToString("dd MMMM yyyy");
//            }
//            catch (Exception ex)
//            {
//                CurrentTime = "N/A";
//                CurrentDate = "N/A";
//                Debug.WriteLine($"Error: {ex.Message}");
//            }

//            await Task.Delay(TimeSpan.FromSeconds(1));
//        }
//    }


//    public void StopUpdatingTime()
//    {
//        _cancellationTokenSource?.Cancel();
//    }
//}

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using CommunityToolkit.Mvvm.Input;

namespace SmartHome.MVVM.ViewModels
{
    public partial class DataTimeViewModel : ObservableObject, INotifyPropertyChanged
    {
        [RelayCommand]
        async Task GoBack() => await Shell.Current.GoToAsync("..");

        private string _currentTime;
        private string _currentDate;
        private string _temperature;
        private string _condition;
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

        public string Temperature
        {
            get { return _temperature; }
            set { _temperature = value; OnPropertyChanged(nameof(Temperature)); }
        }

        public string Condition
        {
            get { return _condition; }
            set { _condition = value; OnPropertyChanged(nameof(Condition)); }
        }

        [Obsolete]
        public DataTimeViewModel()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                UpdateTime();
                return true;
            });

            Device.StartTimer(TimeSpan.FromMinutes(15), () =>
            {
                GetWeatherAsync();
                return true;
            });

            GetWeatherAsync();

            _cancellationTokenSource = new CancellationTokenSource();
            _ = UpdateTimePeriodically(_cancellationTokenSource.Token);
        }

        private void UpdateTime()
        {
            var currentTime = DateTime.Now;
            CurrentTime = currentTime.ToString("HH:mm");
            CurrentDate = currentTime.ToString("dd MMMM yyyy");
        }

        private async void GetWeatherAsync()
        {
            using var http = new HttpClient();
            try
            {
                var data = JsonConvert.DeserializeObject<dynamic>(await http.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?lat=59.1875&lon=18.1232&appid=b4a3119e986341f8f3a4d159c5787679"));
                Temperature = (data!.main.temp - 273.15).ToString("#,##0.0");

                switch (data!.weather[0].description.ToString())
                {
                    case "clear sky":
                        Condition = "\ue28f";
                        break;
                    case "few clouds":
                        Condition = "\uf6c4";
                        break;
                    case "overcast clouds":
                        Condition = "\uf744";
                        break;
                    case "scattered clouds":
                        Condition = "\uf0c2";
                        break;
                    case "broken clouds":
                        Condition = "\uf744";
                        break;
                    case "shower rain":
                        Condition = "\uf738";
                        break;
                    case "rain":
                        Condition = "\uf740";
                        break;
                    case "thunderstorm":
                        Condition = "\uf76c";
                        break;
                    case "snow":
                        Condition = "\uf742";
                        break;
                    case "mist":
                        Condition = "\uf74e";
                        break;

                    default:
                        Condition = "\ue137";
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to get weather data. Error: {ex.Message}");
                Temperature = "--";
            }
        }

        private async Task UpdateTimePeriodically(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    UpdateTime();
                }
                catch (Exception ex)
                {
                    CurrentTime = "N/A";
                    CurrentDate = "N/A";
                    Debug.WriteLine($"Error: {ex.Message}");
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

