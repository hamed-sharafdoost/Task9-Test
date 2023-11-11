using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Task9.ViewModel
{
    public class ValidationViewModelBase : ViewModelBase,INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> errorsByPropertyName = new Dictionary<string, List<string>>();
        public bool HasErrors => errorsByPropertyName.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return propertyName != null && errorsByPropertyName.ContainsKey(propertyName)
                ? errorsByPropertyName[propertyName]
                : Enumerable.Empty<string>();
        }
        protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs args)
        {
            ErrorsChanged?.Invoke(this, args);
        }
        protected void AddError(string error, string propertyName)
        {
            if (propertyName == null) return;
            if (!errorsByPropertyName.ContainsKey(propertyName))
            {
                errorsByPropertyName[propertyName] = new List<string>();
            }
            if (!errorsByPropertyName[propertyName].Contains(error))
            {
                errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
                OnPropertyChanged(nameof(HasErrors));
            }
        }
        protected void ClearErrors(string propertyName)
        {
            if (propertyName is null) return;
            if (errorsByPropertyName.ContainsKey(propertyName))
            {
                errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
                OnPropertyChanged(nameof(HasErrors));
            }
        }
        protected void ClearAllErrors()
        {
            if(errorsByPropertyName.Any())
            {
                errorsByPropertyName.Clear();
                OnPropertyChanged(nameof(HasErrors));
            }
        }
    }
}
