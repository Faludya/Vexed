﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vexed.Models;

namespace Vexed.Data
{
    public class VexedDbContext : IdentityDbContext<IdentityUser>
    {
        public VexedDbContext(DbContextOptions<VexedDbContext> options) : base(options) { }

        public DbSet<ContactInfo>? ContactsInfo { get; set; }
        public DbSet<EmergencyContact>? EmergencyContacts { get; set; }
        public DbSet<UserDetails>? UsersDetails{ get; set; }
        public DbSet<UserEmployment>? UsersEmployments{ get; set; }
        public DbSet<TimeCard>? TimeCards{ get; set; }
        public DbSet<LeaveRequest>? LeaveRequests{ get; set; }
    }
}