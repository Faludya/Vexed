using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DataModels;

namespace DataAccess
{
    public class VexedDbContext : IdentityDbContext<IdentityUser>
    {
        public VexedDbContext(DbContextOptions<VexedDbContext> options) : base(options) { }

        public DbSet<ContactInfo>? ContactsInfo { get; set; }
        public DbSet<EmergencyContact>? EmergencyContacts { get; set; }
        public DbSet<LeaveRequest>? LeaveRequests{ get; set; }
        public DbSet<Project>? Projects{ get; set; }
        public DbSet<ProjectTeam>? ProjectTeams{ get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<TimeCard>? TimeCards{ get; set; }
        public DbSet<ToDo>? ToDos{ get; set; }
        public DbSet<UserDetails>? UsersDetails{ get; set; }
        public DbSet<UserEmployment>? UsersEmployments{ get; set; }
    }
}
