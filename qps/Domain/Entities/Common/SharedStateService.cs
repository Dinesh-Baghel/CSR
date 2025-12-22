using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common
{
    public class SharedStateService
    {
        public event Action? OnChange;

        private string? _value;
        public string? Value
        {
            get => _value;
            set
            {
                _value = value;
                NotifyStateChanged();
            }
        }

        public void NotifyStateChanged() => OnChange?.Invoke();
    }

}
