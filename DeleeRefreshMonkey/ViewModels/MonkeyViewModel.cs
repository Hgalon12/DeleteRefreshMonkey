using DeleeRefreshMonkey.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DeleeRefreshMonkey.Services;

namespace DeleeRefreshMonkey.ViewModels
{
    public class MonkeyViewModel:ViewModelBase
    {
       
        private ObservableCollection<Monkey> monkeys;
        public ObservableCollection<Monkey> Monkeys
        {
            get
            {
                return this.monkeys;
            }
            set
            {
                this.monkeys = value;
                OnPropertyChanged();
            }
        }
        private MonkeyService monkeyService;
        public MonkeyViewModel(MonkeyService service)
        {
            this.monkeyService = service;
            monkeys = new ObservableCollection<Monkey>();
            ReadMonkeys();
            
        }
        public MonkeyViewModel()
        {
            monkeys = new ObservableCollection<Monkey>();
            IsRefreshing = false;
            ReadMonkeys();
            Loc = new ObservableCollection<LocationModel>();
            Fillloc();
        }

        private async void ReadMonkeys()
        {
            MonkeyService service = new MonkeyService();
            List<Monkey> list = await service.GetMonkeys();
            this.Monkeys = new ObservableCollection<Monkey>(list);
           
        }
        private ObservableCollection<LocationModel> loc;
        private ObservableCollection<LocationModel> Loc
        {
            get
            {
                return this.loc;
            }
            set
            {
                this.loc = value;
                OnPropertyChanged();
            }
        }
        private LocationModel selectedLoc;
        public LocationModel SelectedLoc
        {
            get { return this.selectedLoc; }
            set
            {
                this.selectedLoc = value;
                OnPickerChanged();
                OnPropertyChanged();
            }
        }

        private void OnPickerChanged()
        {
            ReadMonkeys();
            if (SelectedMonkey != null)
            {
                List<Monkey> tobeRemoved = monkeys.Where(s => s.Location != SelectedLoc.LocationM).ToList();
                foreach (Monkey monkey in tobeRemoved)
                {
                    Monkeys.Remove(monkey);
                }
            }
        }
        private void Fillloc()
        {
            Loc.Add(new LocationModel { Id = 0, LocationM = "Asia"});
            Loc.Add(new LocationModel { Id = 0, LocationM = "America"});
            Loc.Add(new LocationModel { Id = 0, LocationM = "China"});
            Loc.Add(new LocationModel { Id = 0, LocationM = "Brazil"});
        }

        public ICommand DeleteCommand => new Command<Monkey>(RemoveMonkey);

        void RemoveMonkey(Monkey st)
        {
            if (Monkeys.Contains(st))
            {
                Monkeys.Remove(st);
            }
        }
        private Object selectedMonkey;
        public Object SelectedMonkey
        {
            get
            {
                return this.selectedMonkey;
            }
            set
            {
                this.selectedMonkey = value;
                OnPropertyChanged();
            }
        }

        public ICommand SingleSelectCommand => new Command(OnSingleSelectStudent);

        async void OnSingleSelectStudent()
        {
            if (SelectedMonkey != null)
            {
                var navParam = new Dictionary<string, object>()
                {
                    { "selectedMonkey",SelectedMonkey}
                };
                //Add goto here to show details
                await Shell.Current.GoToAsync("monkeyDetails", navParam);

                SelectedMonkey = null;
            }
        }
        



        #region Refresh View
        public ICommand RefreshCommand => new Command(Refresh);
        private async void Refresh()
        {

            ReadMonkeys();

            IsRefreshing = false;
        }
      

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get
            {
                return this.isRefreshing;
            }
            set
            {
                this.isRefreshing = value;
                OnPropertyChanged();
            }
        }
        #endregion
       

    }
}
