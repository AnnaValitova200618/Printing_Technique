using Printing_Technique.VM;
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
using System.Windows.Shapes;

namespace Printing_Technique.Views
{
    /// <summary>
    /// Логика взаимодействия для EditRequest.xaml
    /// </summary>
    public partial class EditRequest_Window : Window
    {
        public EditRequest_Window(Models.Request selectRequest)
        {
            InitializeComponent();
            DataContext = new EditRequestVM(selectRequest, window);
        }
    }
}
