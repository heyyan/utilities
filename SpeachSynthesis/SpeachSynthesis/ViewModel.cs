using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpeachSynthesis
{
    public class ViewModel:ViewModelBase
    {
        public ICommand SpeakCommand { get;  }

        public ViewModel()
        {
            SpeakCommand = new RelayCommand(Speak);
        }

        private void Speak()
        {
            throw new NotImplementedException();
        }
    }
}
