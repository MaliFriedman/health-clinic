using Webapi.models;

namespace Bl.Api
{
    public interface IDoctorManager
    {
        void AddDoctor(DoctorBl doctorBl);
        List<DoctorBl> GetDoctor();
        DoctorBl GetDoctor(string id);
        void RemoveDoctor(string id, string phoneNumber);
        void UpdateDoctor(DoctorBl doctorBl);

        Task AddWeeklyAvailableAppointments(string doctorId, DateTime startDate, int slotsPerDay);

    }
}