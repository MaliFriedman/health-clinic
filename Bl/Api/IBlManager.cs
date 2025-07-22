using Bl.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bl.Api
{
    public interface IBlManager
    {
        IDoctorManager DoctorManager { get; }
        IPatientManager PatientManager { get; }
        IAppointmentManager AppointmentManager { get; }
        IAvailableAppointmentServiceBl AvailableAppointmentServiceBl { get; }
    }
}
