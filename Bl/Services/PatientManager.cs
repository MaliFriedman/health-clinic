using AutoMapper;
using Bl.Api;
using Dal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webapi.models;

namespace Bl.Services
{
    internal class PatientManager : IPatientManager
    {
        private readonly IPatientDal patientDal;
        private readonly INotAvailableAppointmentDal notAvailableAppointment;
        private readonly IMapper mapper;

        public PatientManager(IDal dal, IMapper _mapper)
        {
            patientDal = dal.PatientDal;
            notAvailableAppointment = dal.NotAvailableAppointmentDal;
            mapper = _mapper;
        }
        public void AddPatient(PatientBl patientBl)
        {
            if (patientBl == null)
                throw new ArgumentNullException(nameof(patientBl));

            if (patientDal.Exists(patientBl.Id))
                throw new InvalidOperationException("Patient with this ID already exists.");

            var patient = mapper.Map<Patient>(patientBl);
            patientDal.Add(patient);

        }
        public void RemovePatient(string id, string phoneNumber)
        {

            patientDal.DeleteByIdAndPhoneNumber(id, phoneNumber);
        }

        public PatientBl GetPatient(string id)
        {
            return mapper.Map<PatientBl>(patientDal.GetById(id).Result);
        }
        public List<PatientBl> GetPatients()
        {
            var patientsList = patientDal.GetAll().Result ;

            return mapper.Map<List<PatientBl>>(patientsList);
        }
        public void UpdatePatient(PatientBl patientBl)
        {
            if (patientBl == null)
                throw new ArgumentNullException(nameof(patientBl));

            if (!patientDal.Exists(patientBl.Id))
                throw new InvalidOperationException("Patient does not exist.");

            var patient = mapper.Map<Patient>(patientBl);
            patientDal.Update(patient);
        }
        public List<PatientBl> GetPatientsAppointmentByDate(DateTime date)
        {
            List<NotAvailableAppointment> appointments = notAvailableAppointment.GetAppointmentsByDate(date).Result;

            List<Patient> patients = appointments.Select(a => a.Patient).Distinct().ToList();

            return mapper.Map<List<PatientBl>>(patients);
        }
        public List<PatientBl> GetPatientsByDoctorId(DateTime date, string doctorId)
        {
            List<NotAvailableAppointment> appointments = notAvailableAppointment.GetAppointmentsByDate(date).Result;
            List<NotAvailableAppointment> appointments1 = appointments.Where(a => a.DoctorId == doctorId).Distinct().ToList();

            List<Patient> patients = appointments1.Select(a => a.Patient).Distinct().ToList();

            return mapper.Map<List<PatientBl>>(patients);
        }

    }
}
