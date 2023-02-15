using Printing_Technique.DB;
using Printing_Technique.Models;
using Printing_Technique.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Printing_Technique.VM
{
    public class AddConsumablesVM : BaseVM
    {
        private List<Consumable> consumables;
        private ObservableCollection<Consumable> selectConsumbles = new();
        private readonly ListView list;

        public CustomCommand Save { get; set; }
        public CustomCommand Select { get; set; }
        public CustomCommand Del { get; set; }

        public List<Consumable> Consumables
        {
            get => consumables;
            set
            {
                consumables = value;
                SignalChanged();
            }
        }

        public ObservableCollection<Consumable> SelectedCons
        {
            get => selectConsumbles;
            set
            {
                selectConsumbles = value;
                SignalChanged();
            }
        }



        public AddConsumablesVM(Technic editTechnic, System.Windows.Controls.ListView list, ListView listSelected, Window window)
        {
            SelectedCons = new ObservableCollection<Consumable>(DBInstance.GetInstance().CrossConsTeches.Where(s => s.IdTech == editTechnic.Id).Select(s=>s.IdConsNavigation));
            Consumables = DBInstance.GetInstance().Consumables.ToList();
            this.list = list;

            Select = new CustomCommand(() => {
                foreach (Consumable d in list.SelectedItems)
                {
                    if (!SelectedCons.Contains(d))
                        SelectedCons.Add(d);
                }
            });

            Save = new CustomCommand(() =>
            {
                editTechnic.CrossConsTeches.Clear();
                SelectedCons.ToList().ForEach(s => 
                {
                    editTechnic.CrossConsTeches.Add(new CrossConsTech { IdCons = s.Id, IdTech = editTechnic.Id });
                    DBInstance.GetInstance().SaveChanges();
                });
                MessageBox.Show("Оке");
                window.Close();
            });
            Del = new CustomCommand(() =>
            {
                // костыль
                var sssr = new ObservableCollection<Consumable>(SelectedCons);
                foreach (Consumable d in listSelected.SelectedItems)
                {
                    if (sssr.Contains(d))
                        sssr.Remove(d);
                }
                SelectedCons = sssr;
            });
        }
    }
}
