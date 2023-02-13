using Printing_Technique.DB;
using Printing_Technique.Models;
using Printing_Technique.Tools;
using Printing_Technique.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Printing_Technique.VM
{
    public class TechnicEditVM : BaseVM
    {
        private MainWinVM mainWinVM;
        Technic original;
        private Technic technic;
       

        public List<Сabinet> ListCabinet { get; set; }
        public CustomCommand Save { get; set; }
        public CustomCommand OpenAddConsum { get; set; }
        public Technic EditTechnic { get => technic; set => technic = value; }
        public Department SelectedCabinet { get; set; }
        

        public TechnicEditVM(MainWinVM mainWinVM, Technic editTechnic)
        {
            this.mainWinVM = mainWinVM;
            ListCabinet = DBInstance.GetInstance().Сabinets.ToList();

            EditTechnic = editTechnic;
            /*new()
            {
                Name = editTechnic.Name,
                Category = editTechnic.Category,
                IdCabinetNavigation = editTechnic.IdCabinetNavigation
            };*/

            Save = new CustomCommand(() =>
            {
                
                    editTechnic.Name = EditTechnic.Name;
                    editTechnic.Category = EditTechnic.Category;
                    editTechnic.IdCabinetNavigation = EditTechnic.IdCabinetNavigation;
                    if(editTechnic.Id == 0 )
                        DBInstance.GetInstance().Technics.Add(editTechnic);
                    else
                        DBInstance.GetInstance().Entry<Technic>(editTechnic).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    DBInstance.GetInstance().SaveChanges();
                    MessageBox.Show("Оке");
                    mainWinVM.CurrentPage = new ListTechnics(mainWinVM);
                
                
            });

            OpenAddConsum = new CustomCommand(() =>
            {
                new AddConsumables(EditTechnic).Show();
            });
        }
    }
}
