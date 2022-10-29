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
    // Form to view Project and (all matching) Employee/Task information & hours worked
    public partial class ProjectHoursForm : Form
    {
        #region Variables
        ProjectTrackingDataSet projects;
        #endregion

        #region Events
        public ProjectHoursForm(ProjectTrackingDataSet projectsDataSet)
        {
            InitializeComponent();
            projects = projectsDataSet;
        }

        private void ProjectHoursForm_Load(object sender, EventArgs e)
        {
            this.workTableAdapter.Fill(this.projectTrackingDataSet.Work);
            this.projectsTableAdapter.Fill(this.projectTrackingDataSet.Projects);
        }

        // When current property changes get ProjectID from datarow
        private void projectsBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView project = (DataRowView)projectsBindingSource.Current;
            DataRow drProject = project.Row;
            int projectID = (int)drProject["ProjectID"];
            ProjectHoursWorked(projectID);
            FillDataGridView(projectID);
        }
        #endregion

        #region Methods
        // Calculate Project hours worked (using query) by ProjectID
        private void ProjectHoursWorked(int projectID)
        {
            ProjectTrackingDataSetTableAdapters.ProjectsTableAdapter taProjects =
                new ProjectTrackingDataSetTableAdapters.ProjectsTableAdapter();
            // Query can return null result
            decimal? hoursWorked = (decimal?)taProjects.GetProjectHoursWorked(projectID);
            txtHoursWorked.Text = hoursWorked.ToString();
        }

        // Display results in DataGridView from selected project
        private void FillDataGridView(int projectID)
        {
            ProjectTrackingDataSetTableAdapters.WorkTableAdapter taWork =
                new ProjectTrackingDataSetTableAdapters.WorkTableAdapter();
            ProjectTrackingDataSet.WorkDataTable table = new ProjectTrackingDataSet.WorkDataTable();
            try
            {
                taWork.FillProjectHoursByProjectID(table, projectID);
            }
            catch (Exception)
            {
                Console.WriteLine(table.GetErrors()[0].RowError);
            }
            pHDataGridView.DataSource = table;
            // Format DGV columns
            pHDataGridView.Columns["WorkID"].Visible = false;
            pHDataGridView.Columns["EmployeeID"].Visible = false;
            pHDataGridView.Columns["TaskID"].Visible = false;
            pHDataGridView.Columns["FullName"].DisplayIndex = 1;
            pHDataGridView.Columns["TaskName"].DisplayIndex = 2;
            pHDataGridView.Columns["StatusType"].DisplayIndex = 3;
            pHDataGridView.Columns["DateWorked"].DisplayIndex = 4;
            pHDataGridView.Columns["HoursWorked"].DisplayIndex = 5;
            pHDataGridView.Columns["FullName"].HeaderText = "Employee";
            pHDataGridView.Columns["TaskName"].HeaderText = "Task";
            pHDataGridView.Columns["StatusType"].HeaderText = "Status";
            pHDataGridView.Columns["DateWorked"].HeaderText = "Date Worked";
            pHDataGridView.Columns["HoursWorked"].HeaderText = "Hours Worked";
        }
        #endregion
    }
}
