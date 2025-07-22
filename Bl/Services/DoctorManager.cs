using Dal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Webapi.models;
using Bl.Api;

namespace Bl.Services
{
    internal class DoctorManager : IDoctorManager
    {
        private readonly IDoctorDal doctorDal;
        private readonly IAvailableAppointmentDal availableAppointmentDal;
        private readonly IMapper mapper;

        public DoctorManager(IDal dal, IMapper _mapper)
        {
            doctorDal = dal.DoctorDal;
            availableAppointmentDal = dal.AvailableAppointmentDal;
            mapper = _mapper;
        }

        public void AddDoctor(DoctorBl doctorBl)
        {
            if (doctorBl == null)
                throw new ArgumentNullException(nameof(doctorBl));

            if (doctorDal.Exists(doctorBl.Id))
                throw new InvalidOperationException("Doctor with this ID already exists.");

            var doctor = mapper.Map<Doctor>(doctorBl);
            doctorDal.Add(doctor);
        }

        public void RemoveDoctor(string id, string phoneNumber)
        {
            // doctorDal.DeleteByIdAndPhoneNumber(id, phoneNumber);
        }

        public DoctorBl GetDoctor(string id)
        {
            return mapper.Map<DoctorBl>(doctorDal.GetById(id).Result);
        }

        public List<DoctorBl> GetDoctor()
        {
            var doctorList = doctorDal.GetAll().Result;
            return mapper.Map<List<DoctorBl>>(doctorList);
        }

        public void UpdateDoctor(DoctorBl doctorBl)
        {
            if (doctorBl == null)
                throw new ArgumentNullException(nameof(doctorBl));

            if (!doctorDal.Exists(doctorBl.Id))
                throw new InvalidOperationException("Doctor does not exist.");

            var doctor = mapper.Map<Doctor>(doctorBl);
            doctorDal.Update(doctor);
        }

        // New function to add available appointments for a week
        public async Task AddWeeklyAvailableAppointments(string doctorId, DateTime startDate, int slotsPerDay)
        {
            for (int i = 0; i < 7; i++)
            {
                var date = startDate.Date.AddDays(i);
                for (int slot = 0; slot < slotsPerDay; slot++)
                {
                    var appointment = new AvailableAppointment
                    {
                        DoctorId = doctorId,
                        Date = date.AddHours(9 + slot), // Example: slots start at 9AM
                    };
                    await availableAppointmentDal.Add(appointment);
                }
            }
        }
    }
}