using Printing_Technique.DB;
using Printing_Technique.Models;
using Printing_Technique.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Printing_Technique.VM
{
    public class CabinetWinVM : BaseVM
    {
        private Сabinet cabinet;

        public CustomCommand CabinetSave { get; set; }
        public List<Department> Departments { get; set; }
        public Department SelectedDepartment { get; set; }
        public Сabinet Cabinet { get => cabinet; set => cabinet = value; }

        public CabinetWinVM()
        {
            Departments = DBInstance.GetInstance().Departments.ToList();
            this.Cabinet = new Сabinet();
            

            CabinetSave = new CustomCommand(() =>
            {
                Cabinet.IdDepartmentNavigation = SelectedDepartment;
                DBInstance.GetInstance().Сabinets.Add(Cabinet);
                DBInstance.GetInstance().SaveChanges();
                MessageBox.Show("Кабинет добавлен успешно!");
            });
        }
    }
}
