using System;
using System.Collections.Generic;

namespace Web.WhatsAppThirdIntegración.API.Infraestructure.Persistence.Entities;

public partial class Appointment
{
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }

    public Guid ProcessId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual Processor Process { get; set; } = null!;
}
