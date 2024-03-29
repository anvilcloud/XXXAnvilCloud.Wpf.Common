﻿using System.Windows;
using System.Windows.Interactivity;

namespace AnvilCloud.WPF.Common.Behaviors
{
    /// <summary>
    /// Provides a simple interface for windows that should be closeable from their view model.
    /// </summary>
    public class CloseableBehavior : Behavior<Window>
    {
        private ICloseableViewModel _closeable;

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            AssociatedObject.Closing += AssociatedObject_Closing;
            AssociatedObject.DataContextChanged += AssociatedObject_DataContextChanged;
            AssociatedObject.Closed += AssociatedObject_Closed;

            _closeable = this.AssociatedObject.DataContext as ICloseableViewModel;

            if (_closeable != null)
                _closeable.Close += Closeable_Close;

            base.OnAttached();
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        /// Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.Closing -= AssociatedObject_Closing;
            AssociatedObject.DataContextChanged -= AssociatedObject_DataContextChanged;
            AssociatedObject.Closed -= AssociatedObject_Closed;
        }

        void AssociatedObject_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_closeable != null)
                _closeable.Close -= Closeable_Close;

            _closeable = e.NewValue as ICloseableViewModel;

            if (_closeable != null)
                _closeable.Close += Closeable_Close;
        }

        void Closeable_Close(object sender, CloseEventArgs e)
        {
            AssociatedObject.DialogResult = e.DialogResult;
            AssociatedObject.Close();
        }

        void AssociatedObject_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_closeable != null)
            {
                if (!_closeable.CanClose())
                {
                    e.Cancel = true;
                }
            }
        }

        void AssociatedObject_Closed(object sender, System.EventArgs e)
        {
            if (_closeable != null)
            {
                _closeable.Closed();
            }
        }
    }
}
