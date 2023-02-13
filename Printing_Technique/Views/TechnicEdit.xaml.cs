using Printing_Technique.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Printing_Technique.Views
{
    /// <summary>
    /// Логика взаимодействия для Technics.xaml
    /// </summary>
    public partial class TechnicEdit : Page
    {
        public TechnicEdit(MainWinVM mainWinVM, Technic editTechnic)
        {
            InitializeComponent();
            DataContext = new TechnicEditVM(mainWinVM, editTechnic);
        }
    }
}
