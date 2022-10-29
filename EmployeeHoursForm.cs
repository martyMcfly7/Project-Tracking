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
        #region Variables
        ProjectTrackingDataSet projects;
        #endregion

        #region Events
        public EmployeeHoursForm(ProjectTrackingDataSet projectsDataSet)
        {
            InitializeComponent();
            projects = projectsDataSet;
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
            FillDataGridView(employeeID);
        }
        #endregion

        #region Methods
        // Calculate Employee hours worked (using query) by EmployeeID
        private void EmployeeHoursWorked(int employeeID)
        {
            ProjectTrackingDataSetTableAdapters.EmployeesTableAdapter taEmployees =
                new ProjectTrackingDataSetTableAdapters.EmployeesTableAdapter();
            // Query can return null result
            decimal? hoursWorked = (decimal?)taEmployees.GetEmployeeHoursWorked(employeeID);
            txtHoursWorked.Text = hoursWorked.ToString();
        }

        // Display results in DataGridView from selected employee
        private void FillDataGridView(int employeeID)
        {
            ProjectTrackingDataSetTableAdapters.WorkTableAdapter taWork = 
                new ProjectTrackingDataSetTableAdapters.WorkTableAdapter();
            ProjectTrackingDataSet.WorkDataTable table = new ProjectTrackingDataSet.WorkDataTable();
            try
            {
                taWork.FillEmployeeHoursByProjectID(table, employeeID);
            }
            catch (Exception)
            {
                Console.WriteLine(table.GetErrors()[0].RowError);
            }
           eHDataGridView.DataSource = table;
            // Format DGV columns
            eHDataGridView.Columns["WorkID"].Visible = false;
            eHDataGridView.Columns["EmployeeID"].Visible = false;
            eHDataGridView.Columns["TaskID"].Visible = false;
            eHDataGridView.Columns["ProjectName"].DisplayIndex = 1;
            eHDataGridView.Columns["TaskName"].DisplayIndex = 2;
            eHDataGridView.Columns["StatusType"].DisplayIndex = 3;
            eHDataGridView.Columns["DateWorked"].DisplayIndex = 4;
            eHDataGridView.Columns["HoursWorked"].DisplayIndex = 5;
            eHDataGridView.Columns["ProjectName"].HeaderText = "Project";
            eHDataGridView.Columns["TaskName"].HeaderText = "Task";
            eHDataGridView.Columns["StatusType"].HeaderText = "Status";
            eHDataGridView.Columns["DateWorked"].HeaderText = "Date Worked";
            eHDataGridView.Columns["HoursWorked"].HeaderText = "Hours Worked";
        }
        #endregion
    }
}
