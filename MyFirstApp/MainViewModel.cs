using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace MyFirstApp
{
    class MainViewModel : ViewModelBase
    {
        DispatcherTimer timer;
        public MainViewModel()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();
            PropertyChanged += UpdateProperties;
            SeletedDate = new DateTime(2015, 12, 18, 16, 0, 0);
            //LoadSettings();
        }

        private void UpdateProperties(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(SeletedDate)))
            {
                CalculateRemainingTime();
            }
        }

        private void Timer_Tick(object sender, object e)
        {
            CalculateRemainingTime();
        }

        private string _timeToChristmas;
        public string TimeToChristmas
        {
            get { return _timeToChristmas; }
            set
            {
                _timeToChristmas = value;
                NotifyPropertyChanged(nameof(TimeToChristmas));
            }
        }

        private string _timeToChristmasRings;

        public string TimeToChristmasRings
        {
            get { return _timeToChristmasRings; }
            set { _timeToChristmasRings = value; NotifyPropertyChanged(nameof(TimeToChristmasRings)); }
        }

        private DateTime _selectedDate;

        public DateTime SeletedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; NotifyPropertyChanged(nameof(SeletedDate)); }
        }

        private void CalculateRemainingTime()
        {

            var restTimespan = SeletedDate - DateTime.Now;

            var days = restTimespan.Days;
            var hours = restTimespan.Hours;
            var mins = restTimespan.Minutes;
            var seks = restTimespan.Seconds;
            TimeToChristmas = $"{days}D {hours}H {mins}m {seks}s";
            var dRestTimeSpan = new DateTime(2015, 12, 24, 16, 0, 0) - DateTime.Now; 
            var ddays = dRestTimeSpan.Days;
            var dhours = dRestTimeSpan.Hours;
            var dmins = dRestTimeSpan.Minutes;
            var dseks = dRestTimeSpan.Seconds;
            TimeToChristmasRings = $"{ddays}D {dhours}H {dmins}m {dseks}s";

        }
        private  void SaveData()
        {
            var settings  = Windows.Storage.ApplicationData.Current.LocalSettings;
            var date = settings.CreateContainer("SelectedDate", Windows.Storage.ApplicationDataCreateDisposition.Always);
            date.Values["SelectedDate"] = SeletedDate;
           
        }
        private void LoadSettings()
        {
            var settings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var data = settings.Containers["SelectedDate"];
            if (data == null) return;
            var dataobject = data.Values["SelectedDate"];
            if (dataobject == null) return;
            SeletedDate = (DateTime)dataobject;
        }

    }
}
