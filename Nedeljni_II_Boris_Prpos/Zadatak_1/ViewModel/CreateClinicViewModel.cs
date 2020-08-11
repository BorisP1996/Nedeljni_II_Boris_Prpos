using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class CreateClinicViewModel : ViewModelBase
    {
        CreateClinic createClinic;

        public CreateClinicViewModel(CreateClinic clinicOpen)
        {
            createClinic = clinicOpen;
        }
    }
}
