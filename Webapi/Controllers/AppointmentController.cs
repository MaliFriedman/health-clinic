using Microsoft.AspNetCore.Mvc;
using Bl;
using Webapi.models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bl.Api;
using Bl.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentManager _appointmentManager;
        private readonly IAvailableAppointmentServiceBl _availableAppointmentServiceBl;

        public AppointmentController(IBlManager blManager)
        {
            _appointmentManager = blManager.AppointmentManager;
            _availableAppointmentServiceBl = blManager.AvailableAppointmentServiceBl;
        }

        //// הזמנת תור
        //[HttpPost("Book")]
        //public async Task<IActionResult> Book([FromQuery] int availableAppointmentId, [FromQuery] string patientId)
        //{
        //    try
        //    {
        //        await _appointmentManager.BookAppointment(availableAppointmentId, patientId);
        //        return Ok("Appointment booked successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Booking failed: {ex.Message}");
        //    }
        //}

        // קבלת כל התורים הזמינים
        [HttpGet("Available")]
        public async Task<ActionResult<List<AvailableAppointment>>> GetAllAvailable()
        {
            var appointments = await _availableAppointmentServiceBl.GetAll();
            return Ok(appointments);
        }


        

        // קבלת תורים לפי תאריך
        [HttpGet("ByDate")]
        public async Task<ActionResult<List<AvailableAppointment>>> GetByDate([FromQuery] DateTime date)
        {
            var appointments = await _availableAppointmentServiceBl.GetAppointmentsByDate(date);
            return Ok(appointments);
        }

        

        // עדכון תור
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] AvailableAppointmentBl appointment)
        {
            var a = _appointmentManager.MakeAnAppointment(appointment);
            return Ok("Appointment updated.");
        }
    }
}
