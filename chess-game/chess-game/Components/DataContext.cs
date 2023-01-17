using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_game.Components
{
    /// <summary>
    /// Base DataContext class with PropertyChanged event.
    /// </summary>
    internal class DataContext : INotifyPropertyChanged
    {
        #region Event handlers

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notify a property has changed.
        /// </summary>
        /// <param name="propertyName">Name of the changed property.</param>
        public void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
