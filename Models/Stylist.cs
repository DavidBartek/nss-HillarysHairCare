using System.ComponentModel.DataAnnotations;

namespace HillarysHair.Models;

public class Stylist
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string Lastname { get; set; }
    // [Required]
    public bool IsActive { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Phone { get; set; }
    public List<Appointment> Appointments { get; set; }
}