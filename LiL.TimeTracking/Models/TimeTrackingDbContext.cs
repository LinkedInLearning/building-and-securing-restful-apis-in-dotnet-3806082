using Microsoft.EntityFrameworkCore;

namespace LiL.TimeTracking.Models;

public class TimeTrackingDbContext : DbContext
{
    public TimeTrackingDbContext()
    {}    

    public TimeTrackingDbContext(DbContextOptions options ) :base(options)
    {}

    public DbSet<Employee> Employees {get;set;}
    public DbSet<Project> Projects { get; set; }

    public DbSet<TimeEntry> TimeEntries {get;set;}
    
}