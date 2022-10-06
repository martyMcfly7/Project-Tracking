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
        #region Events
        public ProjectHoursForm()
        {
            InitializeComponent();
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
            //FillDataGridView(projectID);
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

        private void FillDataGridView(int projectID)
        {
            //ProjectTrackingDataSetTableAdapters.WorkTableAdapter taWork =
            //    new ProjectTrackingDataSetTableAdapters.WorkTableAdapter();
            //// using query, get related rows by ProjectID
            //var dataTable = new DataTable();
            //dataTable = taWork.GetDataByProjectID(projectID);
            //workDataGridView.DataSource = dataTable;
        }
        #endregion
    }
}
