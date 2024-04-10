using System.ComponentModel.DataAnnotations.Schema;

namespace LiL.TimeTracking.Models;

public class TimeEntry
{
    public Guid Id  { get; set; }

    public int EmployeeId { get; set; }

    [ForeignKey(nameof(EmployeeId))]
    public virtual Employee Employee { get; set; }

    public int ProjectId { get; set; }

    [ForeignKey(nameof(ProjectId))] 
    public virtual Project Project {get;set;}

    public DateOnly DateWorked { get; set; }
    public decimal HoursWorked { get; set; }
}