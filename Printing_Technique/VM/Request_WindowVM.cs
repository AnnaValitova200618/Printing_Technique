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
    public class Request_WindowVM : BaseVM
    {
        private List<Technic> technics;
        private List<Consumable> consumables;
        private Technic selectedTechnic;
        private Consumable selectedConsumable;

        public CustomCommand Save { get; set; }

        public Technic SelectedTechnic
        {
            get => selectedTechnic;
            set
            {
                selectedTechnic = value;
                Consumables = DBInstance.GetInstance().Consumables.Where(s => s.CrossConsTeches.FirstOrDefault(d => d.IdTechNavigation == SelectedTechnic) != null).ToList();
                SignalChanged(nameof(Consumables));
            }
        }

        public Consumable SelectedConsumable
        {
            get => selectedConsumable;
            set
            {
                selectedConsumable = value;
                SignalChanged();
            }
        }
        public List<Technic> Technics
        {
            get => technics;
            set
            {
                technics = value;
                SignalChanged();
            }
        }
        public List<Consumable> Consumables
        {
            get => consumables;
            set
            {
                consumables = value;
                SignalChanged();
            }
        }

        public Request Request { get; set; } = new Request();

        public Request_WindowVM(Action close)
        {
            Technics = DBInstance.GetInstance().Technics.ToList();

            Save = new CustomCommand(() =>
            {
                if (SelectedTechnic != null && SelectedConsumable != null)
                {
                    Request.IdCrossNavigation = DBInstance.GetInstance().CrossConsTeches.FirstOrDefault(s => s.IdTech == SelectedTechnic.Id && s.IdCons == selectedConsumable.Id);
                    DBInstance.GetInstance().Requests.Add(Request);
                    DBInstance.GetInstance().SaveChanges();
                    close();
                }
                    
                else
                    MessageBox.Show("Необходимо выбрать что-то");

            });
        }

    }
}
