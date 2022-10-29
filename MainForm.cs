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
    // Mdi Parent form to house all child forms
    public partial class MainForm : Form
    {

        #region Variables
        private ProjectTrackingDataSet myProjects;
        #endregion

        #region Properties
        public ProjectTrackingDataSet MyProjects
        {
            // Read-only property to return dataset
            get { return myProjects; }
        }
        #endregion

        #region Events
        public MainForm()
        {
            InitializeComponent();
            // Instantiate dataset
            myProjects = new ProjectTrackingDataSet();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Fill dataset from database
            Read();
        }
        // TODO: Determine which forms need dataset access
        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new EmployeesForm(/*MyProjects*/));
        }

        private void projectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ProjectsForm(/*MyProjects*/));
        }

        private void tasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new TasksForm(/*MyProjects*/));
        }

        private void workToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new WorkForm(/*MyProjects*/));
        }

        private void projectAndTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ProjectAndTasksForm(/*MyProjects*/));
        }

        private void projectHoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ProjectHoursForm(MyProjects));
        }

        private void employeeHoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new EmployeeHoursForm(MyProjects));
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to save your data?", 
                "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Save();
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                e.Cancel = true;
                SetStatusLabel("");
            }
        }

        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void closeWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
                frm.Close();
            SetStatusLabel("");
        }
        #endregion

        #region Methods
        public void SetStatusLabel(string message)
        {
            tslReady.Text = message;
        }

        public void Read()
        {
            // Create table adapters for each table in the database
            ProjectTrackingDataSetTableAdapters.EmployeesTableAdapter taEmployees =
                new ProjectTrackingDataSetTableAdapters.EmployeesTableAdapter();
            ProjectTrackingDataSetTableAdapters.ProjectsTableAdapter taProjects =
                new ProjectTrackingDataSetTableAdapters.ProjectsTableAdapter();
            ProjectTrackingDataSetTableAdapters.StatusTableAdapter taStatus =
                new ProjectTrackingDataSetTableAdapters.StatusTableAdapter();
            ProjectTrackingDataSetTableAdapters.TasksTableAdapter taTasks =
                new ProjectTrackingDataSetTableAdapters.TasksTableAdapter();
            ProjectTrackingDataSetTableAdapters.WorkTableAdapter taWork =
                new ProjectTrackingDataSetTableAdapters.WorkTableAdapter();

            try
            {
                // Load data into the dataset tables
                taEmployees.Fill(myProjects.Employees);
                taProjects.Fill(myProjects.Projects);
                taStatus.Fill(myProjects.Status);
                taTasks.Fill(myProjects.Tasks);
                taWork.Fill(myProjects.Work);
                SetStatusLabel("Success: Data loaded from database");
            }
            catch (Exception ex)
            {
                SetStatusLabel("Error: Cannot load data from database");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Save()
        {
            // Create table adapters for each table in the dataset
            ProjectTrackingDataSetTableAdapters.EmployeesTableAdapter taEmployees =
                new ProjectTrackingDataSetTableAdapters.EmployeesTableAdapter();
            ProjectTrackingDataSetTableAdapters.ProjectsTableAdapter taProjects =
                new ProjectTrackingDataSetTableAdapters.ProjectsTableAdapter();
            ProjectTrackingDataSetTableAdapters.StatusTableAdapter taStatus =
                new ProjectTrackingDataSetTableAdapters.StatusTableAdapter();
            ProjectTrackingDataSetTableAdapters.TasksTableAdapter taTasks =
                new ProjectTrackingDataSetTableAdapters.TasksTableAdapter();
            ProjectTrackingDataSetTableAdapters.WorkTableAdapter taWork =
                new ProjectTrackingDataSetTableAdapters.WorkTableAdapter();

            // Create instance of table adapter manager to manage all tables
            ProjectTrackingDataSetTableAdapters.TableAdapterManager taManager =
                new ProjectTrackingDataSetTableAdapters.TableAdapterManager();

            // Assign each table adapter to the table adapter manager
            taManager.EmployeesTableAdapter = taEmployees;
            taManager.ProjectsTableAdapter = taProjects;
            taManager.StatusTableAdapter = taStatus;
            taManager.TasksTableAdapter = taTasks;
            taManager.WorkTableAdapter = taWork;

            try
            {
                // Using manager update all tables
                taManager.UpdateAll(myProjects);
                SetStatusLabel("Success: Changes saved to database");
            }
            catch (Exception ex)
            {
                SetStatusLabel("Error: Cannot save to database");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowChildForm(Form FormToShow)
        {
            FormToShow.MdiParent = this;
            FormToShow.Show();
            SetStatusLabel(FormToShow.Text + " is ready for use");
        }
        #endregion
    }
}
