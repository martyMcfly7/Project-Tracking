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
    // Form to view Project and (all matching) Task information
    public partial class ProjectAndTasksForm : Form
    {

        #region Events
        public ProjectAndTasksForm()
        {
            InitializeComponent();
        }

        private void ProjectAndTasksForm_Load(object sender, EventArgs e)
        {
                this.tasksTableAdapter.Fill(this.projectTrackingDataSet.Tasks);
                this.projectsTableAdapter.Fill(this.projectTrackingDataSet.Projects);
        }
        #endregion
    }
}
