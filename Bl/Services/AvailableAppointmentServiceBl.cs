using AutoMapper;
using Bl.Api;
using Dal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webapi.models;

namespace Bl.Services
{
    public class AvailableAppointmentServiceBl : IAvailableAppointmentServiceBl
    {
        private readonly IAvailableAppointmentDal availableAppointmentDal;
        private readonly IMapper mapper;

        public AvailableAppointmentServiceBl(IDal dal, IMapper _mapper)
        {
            availableAppointmentDal = dal.AvailableAppointmentDal;
            mapper = _mapper;
        }

        public async Task Add(AvailableAppointmentBl appointment)
        {
            if (appointment == null)
                throw new ArgumentNullException(nameof(appointment));
            if (appointment.Date < DateTime.Now)
                throw new ArgumentException("Cannot add appointment in the past");

            await availableAppointmentDal.Add(mapper.Map<AvailableAppointment>(appointment));
        }

        public async Task Delete(AvailableAppointment appointment)
        {
            if (appointment == null)
                throw new ArgumentNullException(nameof(appointment));
            await availableAppointmentDal.Delete(appointment);
        }

        public async Task Update(AvailableAppointment appointment)
        {
            if (appointment == null)
                throw new ArgumentNullException(nameof(appointment));
            if (appointment.Date < DateTime.Now)
                throw new ArgumentException("Cannot update appointment to past date");

            await availableAppointmentDal.Update(appointment);
        }

        public async Task<List<AvailableAppointment>> GetAll()
        {
            return await availableAppointmentDal.GetAll();
        }

        public async Task<List<AvailableAppointment>> GetAllAvailableAppointments()
        {
            var all = await availableAppointmentDal.GetAll();
            return all
                .Where(a => a.Date > DateTime.Now)
                .OrderBy(a => a.Date)
                .ToList();
        }

        public async Task<List<AvailableAppointment>> GetAppointmentsByDate(DateTime date)
        {
            var all = await availableAppointmentDal.GetAll();
            return all
                .Where(a => a.Date.Date == date.Date)
                .OrderBy(a => a.Date)
                .ToList();
        }

        public async Task<List<AvailableAppointment>> GetAllAvailableAppointmentsOfSpecialization(string specialization)
        {
            if (string.IsNullOrWhiteSpace(specialization))
                throw new ArgumentException("Specialization is required");

            var all = await availableAppointmentDal.GetAll();
            return all
                .Where(a => a.Date > DateTime.Now && a.Doctor.Specialization == specialization)
                .OrderBy(a => a.Date)
                .ToList();
        }
    }
}
