using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webapi.models;
using Dal.Api;
using Bl.Models;
using AutoMapper;

namespace Bl.Services
{
    public class AppointmentManager : IAppointmentManager
    {
        private readonly IAvailableAppointmentDal availableAppointmentdal;
        private readonly INotAvailableAppointmentDal notAvailableAppointmentdal;
        private readonly IPassedAppointmentDal passedAppointmentDal;
        private readonly IMapper mapper;


        public AppointmentManager(IDal dal, IMapper _mapper)
        {
            availableAppointmentdal = dal.AvailableAppointmentDal;
            notAvailableAppointmentdal = dal.NotAvailableAppointmentDal;
            passedAppointmentDal = dal.PassedAppointmentDal;
            mapper = _mapper;
        }
        public NotAvailableAppointment CastingavailableTOnotavailable(AvailableAppointment availableAppointment)
        {
            var notAvailableAppointment = new NotAvailableAppointment()
            {
                Id = availableAppointment.Id,
                Date = availableAppointment.Date,
                DoctorId = availableAppointment.DoctorId,
                PatientId = availableAppointment.PatientId,
            };

            return notAvailableAppointment;
        }
        public NotAvailableAppointmentBl MakeAnAppointment(AvailableAppointmentBl availableAppointmentBl)
        {
            var availableAppointment = mapper.Map<AvailableAppointment>(availableAppointmentBl);
            var notAvailableAppointment = CastingavailableTOnotavailable(availableAppointment);
            notAvailableAppointmentdal.Add(notAvailableAppointment);
            availableAppointmentdal.Delete(availableAppointment);
            return mapper.Map<NotAvailableAppointmentBl>(notAvailableAppointment);

        }

        public AvailableAppointment CastingnotavailableTOavailable(NotAvailableAppointment notAvailableAppointment)
        {
            var availableAppointment = new AvailableAppointment()
            {
                Id = notAvailableAppointment.Id,
                Date = notAvailableAppointment.Date,
                DoctorId = notAvailableAppointment.DoctorId,
                PatientId = notAvailableAppointment.PatientId,
            };

            return availableAppointment;
        }

        public AvailableAppointment CancleAnAppointment(NotAvailableAppointmentBl notAvailableAppointmentBl)
        {
            var notAvailableAppointment = mapper.Map<NotAvailableAppointment>(notAvailableAppointmentBl);
            availableAppointmentdal.Add(
            CastingnotavailableTOavailable(notAvailableAppointment)
            );
            notAvailableAppointmentdal.Delete(notAvailableAppointment);

            return CastingnotavailableTOavailable(notAvailableAppointment);
        }
        public NotAvailableAppointment UpdateAnAppointment(NotAvailableAppointmentBl notAvailableAppointmentBl)
        {
            var availableAppointment = mapper.Map<AvailableAppointment>(notAvailableAppointmentBl);
            notAvailableAppointmentdal.Add(
            CastingavailableTOnotavailable(availableAppointment)
            );
            availableAppointmentdal.Delete(availableAppointment);

            return CastingavailableTOnotavailable(availableAppointment);
        }



    }
}


