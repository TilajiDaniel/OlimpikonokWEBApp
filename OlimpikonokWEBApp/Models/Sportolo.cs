using System;
using System.Collections.Generic;

namespace OlimpikonokWEBApp.Models;

public partial class Sportolo
{
    public int Id { get; set; }

    public string? Nev { get; set; }

    public bool? Neme { get; set; }

    public DateTime? SzulDatum { get; set; }

    public int? SportagId { get; set; }

    public int? Ermek { get; set; }

    public int? OrszagId { get; set; }

    public byte[]? IndexKep { get; set; }

    public byte[]? Kep { get; set; }

    public virtual Orszag? Orszag { get; set; }

    public virtual Sportag? Sportag { get; set; }
}
