using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LahmaOnline.Property
{
    /// <summary>     /// Observable object with INotifyPropertyChanged implemented     /// </summary>     
    public class ObservableObject : INotifyPropertyChanged/*, IDisposable*/
    {
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); OnPropertyChanged(); }
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