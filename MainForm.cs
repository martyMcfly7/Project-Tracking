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
        // create variable name for dataset
        private ProjectTrackingDataSet myProjects;
        #endregion

        #region Properties
        // read-only property to return dataset
        public ProjectTrackingDataSet MyProjects
        {
            get { return myProjects; }
        }
        #endregion

        #region Events
        public MainForm()
        {
            InitializeComponent();
            // instantiate dataset
            myProjects = new ProjectTrackingDataSet();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // fill tables from dataset on form load
            Read();
            SetReadyLabel("Ready");
        }

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new EmployeesForm());
        }

        private void projectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ProjectsForm());
        }

        private void tasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new TasksForm());
        }

        private void workToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new WorkForm());
        }

        private void projectAndTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ProjectAndTasksForm());
        }

        private void projectHoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ProjectHoursForm());
        }

        private void employeeHoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new EmployeeHoursForm());
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        // ask user to save all changes made to database
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to save your data?", 
                "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Save(); // call Save()
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                e.Cancel = true; // cancel FormClosingEvent
                SetReadyLabel("Ready");
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
        }
        #endregion

        #region Methods
        // method to read data from dataset
        public void Read()
        {
            // create table adapters for each table in the dataset
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
                // load the data into the tables
                taEmployees.Fill(myProjects.Employees);
                taProjects.Fill(myProjects.Projects);
                taStatus.Fill(myProjects.Status);
                taTasks.Fill(myProjects.Tasks);
                taWork.Fill(myProjects.Work);
            }
            catch (Exception ex)
            {
                SetReadyLabel("Error loading data from database");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // method to save data to dataset
        public void Save()
        {
            // create table adapters for each table in the dataset
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

            // create instance of table adapter manager to manage all tables
            ProjectTrackingDataSetTableAdapters.TableAdapterManager taManager =
                new ProjectTrackingDataSetTableAdapters.TableAdapterManager();

            // assign each table table adapter to the table adapter manager
            taManager.EmployeesTableAdapter = taEmployees;
            taManager.ProjectsTableAdapter = taProjects;
            taManager.StatusTableAdapter = taStatus;
            taManager.TasksTableAdapter = taTasks;
            taManager.WorkTableAdapter = taWork;

            try
            {
                // using manager, update all of the tables
                taManager.UpdateAll(myProjects);
            }
            catch (Exception ex)
            {
                SetReadyLabel("Error saving to database");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // method to set status label on main form
        public void SetReadyLabel(string message)
        {
            tslReady.Text = message;
        }

        // method to show child form
        private void ShowChildForm(Form FormToShow)
        {
            FormToShow.MdiParent = this;
            FormToShow.Show();
            SetReadyLabel(FormToShow.Text + " is ready for use");
        }
        #endregion
    }
}
