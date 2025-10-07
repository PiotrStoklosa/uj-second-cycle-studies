using System;
using System.Collections.Generic;
using System.Linq;

namespace CharityApp
{
    public class SuperAdmin
    {
        public UserCredentials Credentials { get; private set; }
        public List<Admin> Admins { get; private set; }
        public List<Goal> Goals { get; private set; }
        public double TotalAmount { get; private set; }


        public SuperAdmin(string username, string password)
        {
            Credentials = new UserCredentials(username, password);
            Admins = new List<Admin>();
            Goals = new List<Goal>();
            TotalAmount = 0;
        }

        public void AddAdmin(Admin admin)
        {
            Admins.Add(admin);
        }

        public void AddGoal(Goal goal)
        {
            if (goal != null)
            {
                Goals.Add(goal);
            }
        }

        public void AddToTotalAmount(double amount)
        {
            TotalAmount += amount;
        }

        public Goal GetGoalById(Guid id)
        {
            return Goals.FirstOrDefault(g => g.Id == id);
        }
        
        public void DeleteGoal(Goal goal)
        {
            Goals.Remove(goal);
        }
    }
}
