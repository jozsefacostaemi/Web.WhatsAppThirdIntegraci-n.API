﻿using System;
using System.Collections.Generic;

namespace Web.WhatsAppThirdIntegración.API.Infraestructure.Persistence.Entities;

public partial class Plan
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public bool Acrive { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
