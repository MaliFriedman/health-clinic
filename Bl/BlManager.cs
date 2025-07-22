using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bl.Api;
using Bl.Services;
using Dal.Api;
using Dal;
using Microsoft.Extensions.DependencyInjection;


namespace Bl
{
    public class BlManager : IBlManager
    {

        public IDoctorManager DoctorManager { get; }
        public IPatientManager PatientManager { get; }
        public IAppointmentManager AppointmentManager { get; }
        public IAvailableAppointmentServiceBl AvailableAppointmentServiceBl { get; }

        public BlManager(IDal dal)
        {
            
            ServiceCollection services = new ServiceCollection();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddSingleton<IDoctorManager, DoctorManager>();
            services.AddSingleton<IDal, DalManager>();
            services.AddSingleton<IPatientManager, PatientManager>();
            services.AddSingleton<IAppointmentManager, AppointmentManager>();
            services.AddSingleton<IAvailableAppointmentServiceBl, AvailableAppointmentServiceBl>();




            ServiceProvider serviceProvider = services.BuildServiceProvider();
            DoctorManager = serviceProvider.GetService<IDoctorManager>();
            PatientManager = serviceProvider.GetService<IPatientManager>();
            AppointmentManager = serviceProvider.GetService<IAppointmentManager>();
            AvailableAppointmentServiceBl = serviceProvider.GetService<IAvailableAppointmentServiceBl>();


        }

    }
}


