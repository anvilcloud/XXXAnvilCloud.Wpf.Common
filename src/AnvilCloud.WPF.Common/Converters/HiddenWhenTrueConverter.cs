﻿using System;
using System.Windows;
using System.Windows.Data;

namespace AnvilCloud.WPF.Common.Converters
{
    /// <summary>
    /// Converts the boolean to Hidden when true, Visible otherwise.
    /// </summary>
    public class HiddenWhenTrueConverter : IValueConverter
    {
        /// <summary>
        /// Converts
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? Visibility.Hidden : Visibility.Visible;
        }

        /// <summary>
        /// Converts back.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();            
        }
    }
}
