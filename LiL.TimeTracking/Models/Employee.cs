using Microsoft.Build.Evaluation;

namespace LiL.TimeTracking.Models;

public class Employee 
{
    public int Id  { get; set; }
    public string Name  { get; set; }

    public DateOnly StartDate { get; set; }

    public virtual ICollection<Project> Projects {get;set;}

}