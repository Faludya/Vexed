﻿namespace DataModels.ViewModels
{
    public class SalaryVM
    {
        public List<UserNameVM> UserNameVMs { get; set; }
        public Guid SelectedUserId { get; set; }
        public Salary Salary { get; set; }
    }
}
