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

namespace SQLImportData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ContextController controller;

        public MainWindow()
        {
            InitializeComponent();
            controller = new ContextController();
            DataContext = controller;

            SetStepsStypes(StepsStypes.FromDataSource);
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if ((int)controller.StepCurrent > 1)
            {
                var stepCurrent = (StepsStypes)((int)controller.StepCurrent - 1);
                if ((int)stepCurrent >= 1)
                    SetStepsStypes(stepCurrent);
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidStepCurrent())
                {
                    if ((int)controller.StepCurrent < Enum.GetValues(typeof(StepsStypes)).Cast<int>().Max())
                    {
                        var stepCurrent = (StepsStypes)((int)controller.StepCurrent + 1);
                        while (!CheckStep(stepCurrent))
                            stepCurrent = (StepsStypes)((int)stepCurrent + 1);
                        SetStepsStypes(stepCurrent);
                    }
                    else
                    {
                        // to do something
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.Error(ex.Message);
            }
        }

        private bool ValidStepCurrent()
        {
            var control = gridControl.Children[0] as UserControl;

            var controlSteps = control as IControlSteps;
            if (!controlSteps.Valida()) return false;
            if (!controlSteps.Next()) return false;
            return true;
        }

        private void SetStepsStypes(StepsStypes stepsStypes)
        {
            UserControl userControlLast = null;
            if (gridControl.Children.Count > 0)
                userControlLast = gridControl.Children[0] as UserControl;
            try
            {
                gridControl.Children.Clear();

                var userControl = GetUserControl(stepsStypes);
                if (userControl != null)
                {
                    (userControl as IControlSteps).SetController(controller);
                    gridControl.Children.Add(userControl);
                }
                controller.StepCurrent = stepsStypes;
                Buttons();
            }
            catch (Exception ex)
            {
                if (userControlLast != null)
                    gridControl.Children.Add(userControlLast);
                MessageBox.Show(ex.Message);
            }
        }

        private void Buttons()
        {
            btnFinish.Visibility = Visibility.Collapsed;
            btnNext.Visibility = Visibility.Collapsed;
            btnPrevious.Visibility = Visibility.Collapsed;

            if((int)controller.StepCurrent == Enum.GetValues(typeof(StepsStypes)).Cast<int>().Max())
            {
                btnFinish.Visibility = Visibility.Visible;
                btnPrevious.Visibility = Visibility.Visible;
            }
            else if ((int)controller.StepCurrent == 1)
            {
                btnNext.Visibility = Visibility.Visible;
            }
            else
            {
                btnNext.Visibility = Visibility.Visible;
                btnPrevious.Visibility = Visibility.Visible;
            }
        }

        private UserControl GetUserControl(StepsStypes stepsStypes)
        {
            switch (stepsStypes)
            {
                case StepsStypes.FromDataSource:
                    return new Steps.FromDataSource();
                case StepsStypes.ShowData:
                    return new Steps.ShowDatas();
                case StepsStypes.ToDataSource:
                    return new Steps.ToDataSource();
                case StepsStypes.Finish:
                    return new Steps.Finish();
            }
            return null;
        }

        private bool CheckStep(StepsStypes novoPasso)
        {
            return true;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                controller.Import();
                Messages.Information("Importação concluída com sucesso!!!");
                SetStepsStypes(StepsStypes.FromDataSource);
            }
            catch (Exception ex)
            {
                Messages.Error(ex.Message);
            }
        }
    }
}