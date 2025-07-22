using Bl.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webapi.models;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        public IDoctorManager DoctorManager;
        public DoctorController(IBlManager blManager)
        {
            DoctorManager = blManager.DoctorManager;
        }
        [HttpPost]
        public void AddDoctor([FromBody] DoctorBl doctor)
        {
            DoctorManager.AddDoctor(doctor);
        }
        [HttpGet("{id}")]
        public DoctorBl GetDoctor(string id)
        {
            return DoctorManager.GetDoctor(id);
        }
        [HttpGet]
        public List<DoctorBl> GetDoctors()
        {
            return DoctorManager.GetDoctor();
        }
        [HttpPut]
        public void UpdateDoctor([FromBody] DoctorBl doctor)
        {
            DoctorManager.UpdateDoctor(doctor);
        }
        [HttpDelete("{id}/{phoneNumber}")]
        public void DeleteDoctor(string id, string phoneNumber)
        {
            DoctorManager.RemoveDoctor(id, phoneNumber);
        }
        [HttpGet("all")]
        public List<DoctorBl> GetAllDoctors()
        {
            return DoctorManager.GetDoctor();
        }
        [HttpPost("add-weekly-appointments")]
        public async Task<IActionResult> AddWeeklyAvailableAppointments([FromQuery] string doctorId, [FromQuery] DateTime startDate, [FromQuery] int slotsPerDay)
        {
            await DoctorManager.AddWeeklyAvailableAppointments(doctorId, startDate, slotsPerDay);
            return Ok("Weekly appointments added successfully.");
        }

    }
}
