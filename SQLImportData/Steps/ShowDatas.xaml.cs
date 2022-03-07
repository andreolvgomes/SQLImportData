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
    /// Interaction logic for ShowDatas.xaml
    /// </summary>
    public partial class ShowDatas : UserControl, IControlSteps
    {
        private ContextController _controller;

        public ShowDatas()
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
            _controller.DataTable = ConvertCSV.GetDataTable(_controller.File);
            dgvData.ItemsSource = _controller.DataTable.DefaultView;
        }

        public bool Valida()
        {
            return true;
        }
    }
}