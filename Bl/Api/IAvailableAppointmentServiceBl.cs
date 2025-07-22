using Webapi.models;

namespace Bl.Api
{
    public interface IAvailableAppointmentServiceBl
    {
        Task Add(AvailableAppointmentBl appointment);
        Task Delete(AvailableAppointment appointment);
        Task<List<AvailableAppointment>> GetAll();
        Task<List<AvailableAppointment>> GetAllAvailableAppointments();
        Task<List<AvailableAppointment>> GetAllAvailableAppointmentsOfSpecialization(string specialization);
        Task<List<AvailableAppointment>> GetAppointmentsByDate(DateTime date);
        Task Update(AvailableAppointment appointment);
    }
}