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
    /// Interaction logic for Finish.xaml
    /// </summary>
    public partial class Finish : UserControl, IControlSteps
    {
        private ContextController _controller;

        public Finish()
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
            return true;
        }
    }
}