using System;
using System.Collections.Generic;

namespace Web.WhatsAppThirdIntegración.API.Infraestructure.Persistence.Entities;

public partial class Patient
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Identification { get; set; } = null!;

    public string City { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
