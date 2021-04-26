using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WPF_MVVM_Basic.Utility
{
    /// <summary>
    /// Helper class for dispatcher operations on the UI thread.
    /// </summary>
    public static class DispatcherHelper
    {
        /// <summary>
        /// Gets a reference to the UI thread's dispatcher, after the <see cref="M:GalaSoft.MvvmLight.Threading.DispatcherHelper.Initialize" /> method has been called on the UI thread.
        /// </summary>
        public static Dispatcher UIDispatcher
        {
            get;
            private set;
        }

        public static void CheckBeginInvokeOnUI(Action action)
        {
            if(action != null)
            {
                CheckDispatcher();
                if(UIDispatcher.CheckAccess())
                {
                    action();
                }
                else
                {
                    UIDispatcher.BeginInvoke(action);
                }
            }
        }

        private static void CheckDispatcher()
        {
            if(UIDispatcher == null)
            {
                StringBuilder error = new StringBuilder("The DispatcherHelper is not initialized.");
                error.AppendLine();
                error.Append("Call DispatcherHelper.Initialize() in the static App constructor.");
                throw new InvalidOperationException(error.ToString());
            }
        }

        /// <summary>
        /// Invokes an action asynchronously on the UI thread.
        /// </summary>
        /// <param name="action">The action that must be executed.</param>
        /// <returns>An object, which is returned immediately after BeginInvoke is called, that can be used to 
        /// interact with the delegate as it is pending execution in the event queue.</returns>
        public static DispatcherOperation RunAsync(Action action)
        {
            CheckDispatcher();
            return UIDispatcher.BeginInvoke(action);
        }

        /// <summary>
        /// This method should be called once on the UI thread to ensure that the <see cref="P:GalaSoft.MvvmLight.Threading.DispatcherHelper.UIDispatcher" /> property is
        /// initialized.
        /// <para>In a Silverlight application, call this method in the Application_Startup event handler, after the MainPage is constructed.</para>
        /// <para>In WPF, call this method on the static App() constructor.</para>
        /// </summary>
        public static void Initialize()
        {
            if(UIDispatcher == null || !UIDispatcher.Thread.IsAlive)
            {
                UIDispatcher = Dispatcher.CurrentDispatcher;
            }
        }

        /// <summary>
        /// Resets the class by deleting the <see cref="P:GalaSoft.MvvmLight.Threading.DispatcherHelper.UIDispatcher" />
        /// </summary>
        public static void Reset()
        {
            UIDispatcher = null;
        }
    }
}
