using System.ComponentModel.DataAnnotations;

namespace HillarysHair.Models;

public class Service
{
    public int Id { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public decimal Cost { get; set; }
}