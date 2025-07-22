//using Dal.Models;
using System;
using System.Collections.Generic;

namespace Webapi.models;

public partial class AvailableAppointment
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public string DoctorId { get; set; } = null!;

    public string? PatientId { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
