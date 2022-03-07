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
using System.Windows.Threading;

namespace SQLImportData.Steps
{
    /// <summary>
    /// Interaction logic for ToSource.xaml
    /// </summary>
    public partial class ToDataSource : UserControl, IControlSteps
    {
        private ContextController _controller;

        public ToDataSource()
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
            if (_controller.DataSource.NullOrEmpty())
                return Messages.Warning("Informe a instância do SQL Server");
            if (_controller.Table.NullOrEmpty())
                return Messages.Warning("Informe o nome da Tabela temporária");
            if (!TestCnn()) return false;
            if (!VerificaSeExistaTabela()) return false;
            return true;
        }

        private bool VerificaSeExistaTabela()
        {
            try
            {
                if (_controller.CheckTable())
                {
                    Messages.Warning($"Já existe uma tabela com o mesmo nome \"{_controller.Table}\"");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Messages.Error(ex.Message);
            }
            return true;
        }

        private bool TestCnn()
        {
            try
            {
                var success = false;
                Exception exception = null;

                ToDoTask.Run(Window.GetWindow(this), "Estabelecendo conexão com o SQL Server ....", new Action(() =>
                {
                    try
                    {
                        success = Connection.Test(_controller.DataSource);
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                    }
                }));

                if (exception != null)
                    throw exception;

                return success;
            }
            catch (Exception ex)
            {
                Messages.Error(ex.Message);
            }
            finally
            {
            }
            return false;
        }
    }
}