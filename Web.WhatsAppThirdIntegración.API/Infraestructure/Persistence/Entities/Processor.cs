using System;
using System.Collections.Generic;

namespace Web.WhatsAppThirdIntegración.API.Infraestructure.Persistence.Entities;

public partial class Processor
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public bool Active { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
