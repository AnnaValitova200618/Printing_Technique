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
    public class ListRequestVM : BaseVM
    {
        private MainWinVM mainWinVM;
        private List<Request> requests;
        private string search;
        private DateTime startSelect = DateTime.Now;
        private DateTime endSelect = DateTime.Now.AddDays(1);
        private Request selectRequest;

        public CustomCommand EditRequest { get; set; }

        public List<Request> Requests
        {
            get => requests;
            set
            {
                requests = value;
                SignalChanged();
            }
        }

        internal void SetMainWM(MainWinVM mainWinVM)
        {
            this.mainWinVM = mainWinVM;
        }

        public Request SelectRequest 
        { 
            get => selectRequest;
            set
            {
                selectRequest = value;
                SignalChanged();
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
        public DateTime StartSelect
        {
            get => startSelect;
            set
            {
                startSelect = value;
                DoDateSearch();
            }
        }
        public DateTime EndSelect
        {
            get => endSelect;
            set
            {
                endSelect = value;
                DoDateSearch();
            }
        }

        private void DoDateSearch()
        {
            Requests = DBInstance.GetInstance().Requests.Where(s => s.Data.CompareTo(StartSelect) >= 0 && s.Data.CompareTo(EndSelect) <= 0).ToList();
        }

        private void DoSearch()
        {
            var serch = DBInstance.GetInstance().Requests.Where(s =>

           s.IdCrossNavigation.IdTechNavigation.Name.Contains(Search) ||
           s.IdCrossNavigation.IdTechNavigation.Category.Contains(Search) ||
           s.IdCrossNavigation.IdConsNavigation.Name.Contains(Search)


           );
            var requests = serch.ToList();
            Requests = requests;
        }

        public ListRequestVM()
        {

            Requests = DBInstance.GetInstance().Requests.Include(s => s.IdCrossNavigation.IdTechNavigation).Include(s => s.IdCrossNavigation.IdConsNavigation).ToList();

            EditRequest = new CustomCommand(() =>
            {
                if (SelectRequest == null)
                {
                    MessageBox.Show("Необходимо выбрать заявку!!!!!!!! ВЫ чёёёё дурак????????", "ОШИБКА", MessageBoxButton.YesNo, MessageBoxImage.Error);
                }
                else
                    new EditRequest_Window(SelectRequest).Show();
            });
        }
    }
}
