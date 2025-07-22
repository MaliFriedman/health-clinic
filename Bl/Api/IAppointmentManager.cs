using Webapi.models;

namespace Bl.Services
{
    public interface IAppointmentManager
    {
        AvailableAppointment CancleAnAppointment(NotAvailableAppointmentBl notAvailableAppointmentBl);
        NotAvailableAppointment CastingavailableTOnotavailable(AvailableAppointment availableAppointment);
        AvailableAppointment CastingnotavailableTOavailable(NotAvailableAppointment notAvailableAppointment);
        NotAvailableAppointmentBl MakeAnAppointment(AvailableAppointmentBl availableAppointmentBl);
        NotAvailableAppointment UpdateAnAppointment(NotAvailableAppointmentBl notAvailableAppointmentBl);
    }
}