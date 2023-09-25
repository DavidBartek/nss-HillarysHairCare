using System.ComponentModel.DataAnnotations;

namespace HillarysHair.Models;

public class Appointment
{
    public int Id { get; set; }
    [Required]
    public int StylistId { get; set; }
    public Stylist Stylist { get; set; }
    [Required]
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
    public List<ServiceAppointment> ServiceAppointments { get; set; }
    public decimal? TotalCost
    {
        get
        {
            decimal totalCost = 0M;
            
            foreach (ServiceAppointment serviceAppointment in ServiceAppointments)
            {
                totalCost += serviceAppointment.Service.Cost;
            }

            return totalCost;
        }
    }
    
}