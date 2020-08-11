using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class CreatePatientViewModel : ViewModelBase
    {
        CreatePatient createPat;

        public CreatePatientViewModel(CreatePatient patOpen)
        {
            createPat = patOpen;
        }
    }
}
