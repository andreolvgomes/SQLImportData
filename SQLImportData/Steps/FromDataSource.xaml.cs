using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SQLImportData.Steps
{
    /// <summary>
    /// Interaction logic for LoadFile.xaml
    /// </summary>
    public partial class FromDataSource : UserControl, IControlSteps
    {
        private ContextController _controller;

        public FromDataSource()
        {
            InitializeComponent();
        }

        public bool Next()
        {
            return true;
        }

        public void SetController(ContextController controller)
        {
            _controller = controller;
            DataContext = _controller;
        }

        public bool Valida()
        {
            if (_controller.File.NullOrEmpty())
                return Messages.Warning("Informe o arquivo CSV");
            return true;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            using (var openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.Filter = "CSV Files (*.csv)|*.csv";

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    _controller.File = openFileDialog.FileName;
            }
        }
    }
}