using System.ComponentModel.DataAnnotations;

namespace HillarysHair.Models;

public class ServiceAppointment
{
    public int Id { get; set; }
    [Required]
    public int ServiceId { get; set; }
    public Service Service { get; set; }
    [Required]
    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; }
}