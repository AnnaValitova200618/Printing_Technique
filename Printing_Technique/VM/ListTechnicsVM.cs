using Microsoft.EntityFrameworkCore;
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
    public class ListTechnicsVM : BaseVM
    {
        public CustomCommand AddTechnics { get; set; }
        public CustomCommand EditConsumables { get; set; }
        public CustomCommand DelTechnic { get; set; }

        private MainWinVM mainWinVM;
        private List<Consumable> consumables;
        private List<Technic> technics;
        private Technic selectedTechnic;
        private string search;
        

        public List<Consumable> Consumables
        {
            get => consumables;
            set
            {
                consumables = value;
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
       

        public string Search
        {
            get => search;
            set
            {
                search = value;
                DoSearch();
            }
        }

        private void DoSearch()
        {
            var serch = DBInstance.GetInstance().Technics.Include(s=>s.CrossConsTeches).Where(s =>
            s.Name.Contains(Search) ||
            s.Category.Contains(Search) ||
            s.IdCabinetNavigation.Namber.Contains(Search) ||
            s.IdCabinetNavigation.IdDepartmentNavigation.Name.Contains(Search)

            );
            var technic = serch.ToList();
            Technics = technic;
        }
        public ListTechnicsVM()
        {
            Technics = DBInstance.GetInstance().Technics.Include("IdCabinetNavigation").Include("IdCabinetNavigation.IdDepartmentNavigation").ToList();

            AddTechnics = new CustomCommand(() =>
            {
                mainWinVM.CurrentPage = new TechnicEdit(mainWinVM, new Technic());
            });

            EditConsumables = new CustomCommand(() =>
            {
                if (SelectedTechnic == null)
                {
                    MessageBox.Show("Необходимо выбрать технику!!!!!!!! ВЫ чёёёё дурак????????", "ОШИБКА", MessageBoxButton.YesNo, MessageBoxImage.Error);
                }
                else
                    mainWinVM.CurrentPage = new TechnicEdit(mainWinVM, SelectedTechnic);
            });

            DelTechnic = new CustomCommand(() =>
            {
                if (SelectedTechnic == null)
                {
                    MessageBox.Show("Необходимо выбрать технику!!!!!!!! ВЫ чёёёё дурак????????", "ОШИБКА", MessageBoxButton.YesNo, MessageBoxImage.Error);
                }
                else
                {
                    if (MessageBox.Show("Удалить технику?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                        return;
                    else
                        DBInstance.GetInstance().Technics.Remove(SelectedTechnic);
                    DBInstance.GetInstance().SaveChanges();
                    Technics = DBInstance.GetInstance().Technics.Include("IdCabinetNavigation").Include("IdCabinetNavigation.IdDepartmentNavigation").ToList();
                }
            });
        }


        public void SetMainWM(MainWinVM mainWinVM)
        {
            this.mainWinVM = mainWinVM;
        }
    }
}
