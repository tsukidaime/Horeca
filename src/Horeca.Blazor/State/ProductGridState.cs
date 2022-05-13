using System;

namespace Horeca.Blazor.State
{
    public class ProductGridState
    {
        private Guid? categoryId;

        public Guid? CategoryId
        {
            get => categoryId;
            set
            {
                categoryId = value;
                NotifyStateChanged();
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
