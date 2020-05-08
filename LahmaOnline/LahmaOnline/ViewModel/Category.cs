using LahmaOnline.Property;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace LahmaOnline.ViewModel
{
    public class Category: BLL.M.Mobile.Category, INotifyPropertyChanged
    {
        private bool _IsSelected;

        public bool IsSelected
        {
            get { return _IsSelected; }
            set {
                _IsSelected = value;
                if (value)
                {
                    BackgroundColor = Color.FromHex("#72B0A49F");
                    BorderColor = Color.FromHex("#3A1D0F");
                    TextColor = Color.FromHex("#3A1D0F");
                }
                else
                {
                    BackgroundColor = Color.Transparent;
                    BorderColor = Color.FromHex("#707070");
                    TextColor = Color.FromHex("#95A0B6");
                };
                OnPropertyChanged(); }
        }

        private Color _BackgroundColor = Color.Transparent;

        public Color BackgroundColor
        {
            get { return _BackgroundColor; }
            set { _BackgroundColor = value; OnPropertyChanged(); }
        }
        private Color _TextColor = Color.FromHex("#95A0B6");
        public Color TextColor
        {
            get { return _TextColor; }
            set { _TextColor = value; OnPropertyChanged(); }
        }
        private Color _BorderColor = Color.FromHex("#707070");
        public Color BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; OnPropertyChanged(); }
        }
        /// <summary>         
        /// /// Sets the property.         
        /// /// </summary>         
        /// /// <returns><c>true</c>, if property was set, <c>false</c> otherwise.</returns>         
        /// /// <param name="backingStore">Backing store.</param>         
        /// /// <param name="value">Value.</param>         
        /// /// <param name="propertyName">Property name.</param>         
        /// /// <param name="onChanged">On changed.</param>         
        /// /// <typeparam name="T">The 1st type parameter.</typeparam>         
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value; onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        /// <summary>         
        /// /// Occurs when property changed.         
        /// /// </summary>         
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>         
        /// /// Raises the property changed event.         
        /// /// </summary>         
        /// /// <param name="propertyName">Property name.</param>         
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            var changed = PropertyChanged;
            changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
