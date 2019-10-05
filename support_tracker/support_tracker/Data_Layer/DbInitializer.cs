using support_tracker.Models;
using System.Collections.Generic;
using System.Data.Entity;


namespace support_tracker.DbLayer
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            IList<Department> departments = new List<Department>();
            departments.Add(new Department { DepartmentName = "Technicians" });
            departments.Add(new Department { DepartmentName = "Repairmans" });
            departments.Add(new Department { DepartmentName = "Support" });
            context.Departments.AddRange(departments);
            base.Seed(context);
        }
    }
}