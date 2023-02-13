using Printing_Technique.Tools;
using Printing_Technique.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Printing_Technique.VM
{
    public class MainWinVM : BaseVM
    {
        public CustomCommand OpenList { get; set; }
        public CustomCommand AddCabinet { get; set; }
        public CustomCommand OpenReqest { get; set; }
        public CustomCommand OpenListRequest { get; set; }

        private Page currentPage;

        public Page CurrentPage 
        { 
            get => currentPage;
            set
            {
                currentPage = value;
                SignalChanged();
            }
        }

        public MainWinVM()
        {
            CurrentPage = new ListTechnics(this);

            OpenList = new CustomCommand(() =>
            {
                CurrentPage = new ListTechnics(this);
            });
            OpenReqest = new CustomCommand(() =>
            {
                 new Request_Window().Show();
            });
            OpenListRequest = new CustomCommand(() =>
            {
                CurrentPage = new ListRequest(this);
            });
            AddCabinet = new CustomCommand(() =>
            {
                new Cabinet_Window().Show();
            });

        }
    }
}
