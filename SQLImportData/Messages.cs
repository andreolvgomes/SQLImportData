using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SQLImportData
{
    public static class Messages
    {
        public static bool Error(string message)
        {
            MessageBox.Show(System.Windows.Application.Current.MainWindow, message, "Atenção", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        public static bool Warning(string message)
        {
            MessageBox.Show(System.Windows.Application.Current.MainWindow, message, "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        public static bool Information(string message)
        {
            MessageBox.Show(System.Windows.Application.Current.MainWindow, message, "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
            return false;
        }
    }
}