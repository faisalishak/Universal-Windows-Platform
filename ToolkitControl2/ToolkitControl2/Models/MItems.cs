using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolkitControl2.Common;
using Windows.UI.Xaml;

namespace ToolkitControl2.Models
{
    class MItems : BindableBase
    {
        private string _title = default(string);

        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        private bool? _isFavorite = default(bool);

       /*private Visibility? _isFavorites = default(Visibility);

        public Visibility? IsFavorites
        {
            get { return _isFavorites; }
            set { Set(ref _isFavorites, value); }
        }*/

        public bool? IsFavorite
        {
            get { return _isFavorite; }
            set { Set(ref _isFavorite, value); }
        }

        private DelegateCommand _toggleFavorite = default(DelegateCommand);

        public DelegateCommand ToggleFavorite => _toggleFavorite ?? (_toggleFavorite = new DelegateCommand(ExecuteToggleFavoriteCommand, CanExecuteToggleFavoriteCommand));

        private bool CanExecuteToggleFavoriteCommand()
        {
            return true;
        }

        private void ExecuteToggleFavoriteCommand()
        {
            IsFavorite = !IsFavorite;
        }
    }
}
