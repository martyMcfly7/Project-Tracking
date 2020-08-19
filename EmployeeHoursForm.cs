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
    // Form to view Employee and all matching Project/Task information & Hour worked
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

        // when current property changes get EmployeeID from datarow
        private void employeesBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView employee = (DataRowView)employeesBindingSource.Current;
            DataRow drEmployee = employee.Row;
            int employeeID = (int)drEmployee["EmployeeID"]; // get EmployeeID
            GetEmployeeHoursWorkedByID(employeeID); // call method with EmployeeID
        }
        #endregion

        #region Methods
        // method used to calculate Employee hours worked by EmployeeID
        private void GetEmployeeHoursWorkedByID(int employeeID)
        {
            ProjectTrackingDataSetTableAdapters.EmployeesTableAdapter taEmployees =
                new ProjectTrackingDataSetTableAdapters.EmployeesTableAdapter();
            // using query, calculate Employee hours worked
            decimal? hoursWorked = taEmployees.GetEmployeeHoursWorked(employeeID);
            txtHoursWorked.Text = hoursWorked.ToString();
        }
        #endregion
    }
}
