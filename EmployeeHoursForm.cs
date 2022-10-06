using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Created by Victor Ortiz
 * TermProject - Project Tracking
 * CIS266 6/1/2019
 */

/*
 * Purpose: Create an application to track projects, tasks,
 * and the time employees spend working on each task.
 */

namespace ProjectTracking
{
    // Form to view Employee and (all matching) Project/Task information & hours worked
    public partial class EmployeeHoursForm : Form
    {

        #region Events
        public EmployeeHoursForm()
        {
            InitializeComponent();
        }

        private void EmployeeHoursForm_Load(object sender, EventArgs e)
        {
            this.workTableAdapter.Fill(this.projectTrackingDataSet.Work);
            this.tasksTableAdapter.Fill(this.projectTrackingDataSet.Tasks);
            this.employeesTableAdapter.Fill(this.projectTrackingDataSet.Employees);
        }

        // When current property changes get EmployeeID from datarow
        private void employeesBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView employee = (DataRowView)employeesBindingSource.Current;
            DataRow drEmployee = employee.Row;
            int employeeID = (int)drEmployee["EmployeeID"];
            EmployeeHoursWorked(employeeID);
        }
        #endregion

        #region Methods
        // Calculate Employee hours worked (using query) by EmployeeID
        private void EmployeeHoursWorked(int employeeID)
        {
            ProjectTrackingDataSetTableAdapters.EmployeesTableAdapter taEmployees =
                new ProjectTrackingDataSetTableAdapters.EmployeesTableAdapter();
            // Query can return null result
            decimal? hoursWorked = taEmployees.GetEmployeeHoursWorked(employeeID);
            txtHoursWorked.Text = hoursWorked.ToString();
        }
        #endregion
    }
}
