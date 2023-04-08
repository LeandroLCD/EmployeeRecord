using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;

namespace EmployeeRecord.ViewModels
{
    /// <summary>
    /// Este modelo de vista se extiende en otros modelos de vista.
    /// </summary>
    [DataContract]
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Fields

        private string _menssage;
        private bool _isLook;

        private static bool isCompletet { get; set; }

        #endregion

        #region Event handler

        /// <summary>
        /// Ocurre cuando un propiedad cambia.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties
        /// <summary>
        /// Combierte un Stream en un Byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public byte[] StreamToByteArray(Stream stream)
        {
            stream.Position = 0L;
            byte[] array = new byte[stream.Length];
            stream.Read(array, 0, array.Length);
            return array;
        }
        /// <summary>
        /// Notifica que una tarea en cursa ha sido completada
        /// </summary>
        public static bool IsCompletet
        {
            get
            {
                return isCompletet;
            }

            set
            {
                if (isCompletet == value)
                {
                    return;
                }

                isCompletet = value;
            }
        }
        /// <summary>
        /// Bloquea la actividad principal del usuario, con un activity indicator.
        /// </summary>
        public bool IsLoading
        {
            get
            {
                return _isLook;
            }

            set
            {
                this.SetProperty(ref this._isLook, value);
            }
        }
        /// <summary>
        /// Muestra un mensaje durando la ejecución de una tarea.
        /// </summary>
        public string StatusMessage
        {
            get
            {
                return _menssage;
            }

            set
            {
                this.SetProperty(ref this._menssage, value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Notifica a la vista cuando una propiedad ha cambiado.
        /// </summary>
        /// <param name="propertyName">The PropertyName</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Establece el nuevo valor de una propiedad ej: SetProperty(ref _property, value)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            this.NotifyPropertyChanged(propertyName);

            return true;
        }
       

        #endregion
    }
}
