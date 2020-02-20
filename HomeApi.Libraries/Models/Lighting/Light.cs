using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using HomeApi.Libraries.Annotations;

namespace HomeApi.Libraries.Models.Lighting
{
    public class Light : INotifyPropertyChanged
    {
        public byte Brightness
        {
            get => (byte) (255d / (100d / BrightnessPercentage));
            set => BrightnessPercentage = (int) (255d / 100d * value);
        }

        public int BrightnessPercentage
        {
            get => _brightnessPercentage;
            set
            {
                if (value <= 0)
                {
                    value = 0;
                }
                else if (value >= 100)
                {
                    value = 100;
                }

                if (_brightnessPercentage == value) return;

                _brightnessPercentage = value;

                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Brightness));

                Debug.WriteLine($"Changing brightness: {Brightness} | {BrightnessPercentage}%");
            }
        }

        public double[] ColourCoordinates
        {
            get => _colourCoordinates;
            set
            {
                if (Equals(value, _colourCoordinates)) return;
                _colourCoordinates = value;
                NotifyPropertyChanged();
            }
        }

        public string ColourMode
        {
            get => _colourMode;
            set
            {
                if (value == _colourMode) return;
                _colourMode = value;
                NotifyPropertyChanged();
            }
        }

        public int? ColourTemperature
        {
            get => _colourTemperature;
            set
            {
                if (value == _colourTemperature) return;
                _colourTemperature = value;
                NotifyPropertyChanged();
            }
        }

        public int? Hue
        {
            get => _hue;
            set
            {
                if (value == _hue) return;
                _hue = value;
                NotifyPropertyChanged();
            }
        }

        public string Id
        {
            get => _id;
            set
            {
                if (value == _id) return;
                _id = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsOn
        {
            get => _isOn;
            set
            {
                if (value == _isOn) return;
                _isOn = value;
                NotifyPropertyChanged();
            }
        }

        public bool? IsReachable
        {
            get => _isReachable;
            set
            {
                if (value == _isReachable) return;
                _isReachable = value;
                NotifyPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                NotifyPropertyChanged();
            }
        }

        public int? Saturation
        {
            get => _saturation;
            set
            {
                if (value == _saturation) return;
                _saturation = value;
                NotifyPropertyChanged();
            }
        }

        public int? TransitionMilliseconds
        {
            get => _transitionMilliseconds;
            set
            {
                if (value == _transitionMilliseconds) return;
                _transitionMilliseconds = value;
                NotifyPropertyChanged();
            }
        }

        private int _brightnessPercentage;
        private double[] _colourCoordinates;
        private string _colourMode;
        private int? _colourTemperature;
        private int? _hue;
        private string _id;
        private bool _isOn;
        private bool? _isReachable;
        private string _name;
        private int? _saturation;
        private int? _transitionMilliseconds;

        public event PropertyChangedEventHandler PropertyChanged;

        public void UpdateFrom(Light other, bool copyId = false)
        {
            if (copyId) Id = other.Id;

            Brightness = other.Brightness;
            ColourCoordinates = other.ColourCoordinates;
            ColourMode = other.ColourMode;
            ColourTemperature = other.ColourTemperature;
            Hue = other.Hue;
            IsOn = other.IsOn;
            IsReachable = other.IsReachable;
            Name = other.Name;
            Saturation = other.Saturation;
            TransitionMilliseconds = other.TransitionMilliseconds;
        }

        [NotifyPropertyChangedInvocator]
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}