using Microsoft.EntityFrameworkCore;
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
    public class EditRequestVM : BaseVM
    {
        private Request selectRequest;
        private Technic selectedTechnic;
        private List<Consumable> consumables;
        private Consumable selectedConsumable;
        private Request editRequest;

        public CustomCommand Save { get; set; }
        public List<CrossConsTech> CrossConsTechs { get; set; }
        public Request EditRequest { get => editRequest; set => editRequest = value; }
        public Technic SelectedTechnic
        {
            get => selectedTechnic;
            set
            {
                selectedTechnic = value;
                EditRequest.IdCrossNavigation.IdTechNavigation = SelectedTechnic;
                Consumables = DBInstance.GetInstance().CrossConsTeches.
                    Where(d => d.IdTechNavigation == SelectedTechnic).
                    Select(s => s.IdConsNavigation).ToList();
                SelectedConsumable = null;
                SignalChanged();
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
        public List<Technic> Technics { get; set; }
        public List<Consumable> Consumables
        {
            get => consumables;
            set
            {
                consumables = value;
                SignalChanged();
            }
        }

        public EditRequestVM(Request selectRequest, Window window)
        {
            EditRequest = selectRequest;
            SelectedTechnic = EditRequest.IdCrossNavigation?.IdTechNavigation;
            SelectedConsumable = EditRequest.IdCrossNavigation.IdConsNavigation;
            Technics = DBInstance.GetInstance().Technics.ToList();
            CrossConsTechs = DBInstance.GetInstance().CrossConsTeches.Include(s => s.IdConsNavigation).Include(s => s.IdTechNavigation).ToList();

            Save = new CustomCommand(() =>
            {
                if (SelectedConsumable == null || SelectedTechnic == null)
                {
                    MessageBox.Show("Что-то плохое");
                    return;
                }

                var cross = DBInstance.GetInstance().CrossConsTeches.FirstOrDefault(s => s.IdCons == selectedConsumable.Id && s.IdTech == selectedTechnic.Id);
                selectRequest.IdCrossNavigation = cross ?? new CrossConsTech
                {
                    IdTechNavigation = SelectedTechnic,
                    IdConsNavigation = SelectedConsumable
                };
                selectRequest.Data = EditRequest.Data;
                selectRequest.Price = EditRequest.Price;

                DBInstance.GetInstance().Entry<Request>(selectRequest).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                DBInstance.GetInstance().SaveChanges();
                MessageBox.Show("Оке");
                window.Close();
            });
        }
    }
}
