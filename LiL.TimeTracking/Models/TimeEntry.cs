namespace LiL.TimeTracking.Models;

public class TimeEntry
{
    public Guid Id  { get; set; }

    public virtual Employee Employee { get; set; }

    public virtual Project Project {get;set;}

    public DateOnly DateWorked { get; set; }
    public decimal HoursWorked { get; set; }
}