using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SQLImportData
{
    /// <summary>
    /// Interaction logic for ToDoTask.xaml
    /// </summary>
    public partial class ToDoTask : Window
    {
        public ToDoTask(Window owner, string message)
        {
            InitializeComponent();
            Owner = owner;
            txtMessage.Text = message;
        }

        public void CloseMessage()
        {
            if (Dispatcher.Thread != System.Threading.Thread.CurrentThread)
                Dispatcher.BeginInvoke(DispatcherPriority.Send, (System.Threading.SendOrPostCallback)delegate { CloseMessage(); }, null);
            else
                Close();
        }

        internal static void Run(Window owner,string message, Action action)
        {
            var view = new ToDoTask(owner, message);

            var thread = new Thread(new ParameterizedThreadStart(Execute));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(new Tuple<Action, ToDoTask>(action, view));

            view.ShowDialog();
        }

        private static void Execute(object obj)
        {
            var tuple = obj as Tuple<Action, ToDoTask>;
            tuple.Item1();
            (tuple.Item2 as ToDoTask).CloseMessage();
        }
    }
}