using PaymentOrder.AdminPanel.Commands;
using PaymentOrder.AdminPanel.Enums;
using PaymentOrder.AdminPanel.Infrastructure;
using PaymentOrder.AdminPanel.Models;
using PaymentOrder.AdminPanel.Utils;
using PaymentOrder.AdminPanel.Views.Components;
using PaymentOrder.Core.Domain.Abstract;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace PaymentOrder.AdminPanel.ViewModels
{
    public abstract class BaseControlViewModel<T> : BaseViewModel where T : BaseModel, new()
    {
        public IUnitOfWork DB { get; }
        public DataProvider DataProvider { get; }
        public BaseControlViewModel(IUnitOfWork db)
        {
            DB = db;
            DataProvider = new DataProvider(db);
        }

        public abstract void OnSearch();
        public abstract string Header { get; }
        public ExportExcelCommand<T> ExportExcel => new ExportExcelCommand<T>(this);
        public RejectCommand<T> Reject => new RejectCommand<T>(this);

        #region properties

        public List<T> AllValues { get; set; }

        private ObservableCollection<T> values;
        public ObservableCollection<T> Values
        {
            get
            {
                return values;
            }
            set
            {
                values = value;
                OnPropertyChanged(nameof(Values));
            }
        }

        private T selectedValue;
        public T SelectedValue
        {
            get
            {
                return selectedValue;
            }
            set
            {
                selectedValue = value;

                CurrentValue = (T)SelectedValue?.Clone();
                CurrentSituation = SelectedValue != null ? (byte)Situations.SELECTED : (byte)Situations.NORMAL;

                OnPropertyChanged(nameof(SelectedValue));
            }
        }

        private T currentValue;
        public T CurrentValue
        {
            get
            {
                return currentValue;
            }
            set
            {
                currentValue = value;
                OnPropertyChanged(nameof(CurrentValue));
            }
        }

        private string searchText;
        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                searchText = value;
                OnSearch();
            }
        }

        private byte currentSituation = (byte)Situations.NORMAL;
        public byte CurrentSituation
        {
            get => currentSituation;
            set
            {
                currentSituation = value;
                OnPropertyChanged(nameof(CurrentSituation));
            }
        }

        #endregion

        #region methods

        public void Initialize()
        {
            Values = new ObservableCollection<T>(AllValues);
            CurrentSituation = (int)Situations.NORMAL;
            CurrentValue = new T();
        }

        protected bool IsCompatibleWithFilter(string value)
        {
            if (value != null && value.ToLower().Contains(SearchText.ToLower()))
                return true;

            return false;
        }

        #endregion

        #region error dialog

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
                OnPropertyChanged(nameof(TextColor));
            }
        }

        public Brush TextColor => Message == Constants.OperationSuccessMessage ? Brushes.Green : Brushes.Red;
        public ErrorDialog ErrorDialog { get; set; }

        #endregion
    }
}
