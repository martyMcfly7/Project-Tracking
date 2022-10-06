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
    // Form to add & update Project information
    public partial class ProjectsForm : Form
    {
        
        #region Constants, Variables & Enums
        // Button text
        const string ADD = "&Add";
        const string UPDATE = "&Update";
        const string SAVE = "&Save";
        const string CANCEL = "&Cancel";

        // Display current row and total rows
        const string LABEL_ROWS = "{0} of {1} Total Projects";

        // Captures row index
        int currentRow;

        // Navigation through rows
        enum MoveTo { First, Prior = -1, Next = 1, Last }
        #endregion

        #region Properties
        private ProjectTrackingDataSet MyProjects
        {
            get { return MyParent.MyProjects; } // Accesss Projects dataset
        }

        private ProjectTrackingDataSet.ProjectsDataTable Projects
        {
            get { return MyProjects.Projects; } // Access Projects table
        }

        MainForm MyParent
        {
            get { return (MainForm)MdiParent; } // Access Main form
        }

        private int CurrentRow
        {
            get { return currentRow; }
            set
            {
                // Current row must be between 0 and total number of rows
                if (value >= 0 && value < Projects.Rows.Count)
                    currentRow = value;
            }
        }
        #endregion

        #region Events
        public ProjectsForm()
        {
            InitializeComponent();
        }

        private void ProjectsForm_Load(object sender, EventArgs e)
        {
            // Setup Add and Update buttons
            btnAdd.Text = ADD;
            btnUpdate.Text = UPDATE;
            // Setup navigation buttons
            btnFirst.Tag = MoveTo.First;
            btnPrior.Tag = MoveTo.Prior;
            btnNext.Tag = MoveTo.Next;
            btnLast.Tag = MoveTo.Last;
            // Show first row in dataset
            MoveToRow(MoveTo.First);
            // Display name instead of ID
            SetupManagerComboBox();
            SetupStatusComboBox();
            // Display date if active/blank if disabled
            SetupDateTimePicker(dtpStartDate);
            SetupDateTimePicker(dtpEndDate);
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            SetupDateTimePicker(dtpStartDate);
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            SetupDateTimePicker(dtpEndDate);
        }

        private void NavigationButtons_Click(object sender, EventArgs e)
        {
            // Treat button as MoveTo object/set to control tag
            MoveToRow((MoveTo)((Button)sender).Tag);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            MoveToRow(MoveTo.First); // Button |<
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            MoveToRow(MoveTo.Prior); // Button <<
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            MoveToRow(MoveTo.Next); // Button >>
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            MoveToRow(MoveTo.Last); // Button >|
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetStatusLabel("");
            try
            {
                // If button says Add, start process of creating new entry
                if (btnAdd.Text == ADD)
                {
                    ClearForm();
                    lblProjectID.Text = (Projects.Rows.Count + 1).ToString();
                    // Change buttons to show Save/Cancel
                    btnAdd.Text = SAVE;
                    btnUpdate.Text = CANCEL;
                }
                else // Button says Save, save new project entry
                {
                    DataRow projectRow = Projects.NewRow();
                    // Add each control to correct Project row
                    projectRow[1] = txtName.Text;
                    projectRow[2] = txtDescription.Text;
                    projectRow[3] = (byte)cboStatus.SelectedValue;
                    projectRow[4] = dtpStartDate.Value;
                    projectRow[5] = dtpEndDate.Value;
                    projectRow[6] = (int)cboManager.SelectedValue;
                    Projects.Rows.Add(projectRow);
                    // Mark location as the new last row in the table
                    CurrentRow = Projects.Count - 1;
                    btnAdd.Text = ADD;
                    btnUpdate.Text = UPDATE;
                    UpdateProjectsLabel();
                    SetStatusLabel("Adding entry was successful");
                }
            }
            catch (Exception ex)
            {
                SetStatusLabel("Error: Adding entry has failed");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SetStatusLabel("");
            try
            {
                // Update data from controls and save
				if (btnUpdate.Text == UPDATE)
                {
                    if (Projects.Rows.Count > 0)
                    {
                        GetData();
                        SetStatusLabel("Updating entry was successful");
                    }
                }
                else // User cancels the add process
                {
                    if (Projects.Rows.Count > 0)
                        ShowData();
                    else
                        ClearForm();
                    btnAdd.Text = ADD;
                    btnUpdate.Text = UPDATE;
                    SetStatusLabel("Adding entry was cancelled");
                }
            }
            catch (Exception ex)
            {
                SetStatusLabel("Error: Updating entry has failed");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            SetStatusLabel("");
            this.Close();
        }
        #endregion

        #region Methods
        // Link Manager combobox to EmployeeID using employee name
        private void SetupManagerComboBox()
        {
            cboManager.DropDownStyle = ComboBoxStyle.DropDownList;
            cboManager.DisplayMember = "FullName";
            cboManager.ValueMember = "EmployeeID";
            cboManager.DataSource = MyProjects.Employees;
            cboManager.SelectedValue = Projects.Rows[CurrentRow][6]; // ProjectManager
        }

        // Link Status combobox to StatusID using status types
        private void SetupStatusComboBox()
        {
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.DisplayMember = "StatusType";
            cboStatus.ValueMember = "StatusID";
            cboStatus.DataSource = MyProjects.Status;
            cboStatus.SelectedValue = Projects.Rows[CurrentRow][3]; // ProjectStatus
        }
        
        // TODO: Display blank DTP when unselected
        private void SetupDateTimePicker(DateTimePicker dateTimePicker)
        {
            if (!dateTimePicker.Checked)
            {
                // Hide date value since it's not set
                dateTimePicker.CustomFormat = "";
                dateTimePicker.Format = DateTimePickerFormat.Custom;
                //dateTimePicker.Value = DBNull.Value;
            }
            else
            {
                dateTimePicker.CustomFormat = null;
                dateTimePicker.Format = DateTimePickerFormat.Short;
            }
        }

        private void MoveToRow(MoveTo WhichRow)
        {
            SetStatusLabel("");
            // If there are rows
            if (Projects.Rows.Count > 0)
            {
                if (WhichRow == MoveTo.Last) // Last is count - 1
                    CurrentRow = Projects.Rows.Count - 1;
                else if (WhichRow == MoveTo.First) // First is 0
                    CurrentRow = 0;
                else // Prior or Next append index
                    CurrentRow += (int)WhichRow;
                FormChange();
            }
            else
                ClearForm();
        }

        private void FormChange()
        {
            UpdateProjectsLabel();
            ShowData();
            // Change combo box entries according to currentRow
            SetupManagerComboBox();
            SetupStatusComboBox();
        }

        private void UpdateProjectsLabel()
        {
            // Show current row number and total number of rows
            lblRowsCounter.Text = string.Format(LABEL_ROWS, (CurrentRow + 1).ToString(),
                Projects.Rows.Count.ToString());
        }

        private void ShowData()
        {
            lblProjectID.Text = Projects.Rows[CurrentRow][0].ToString();
            txtName.Text = Projects.Rows[CurrentRow][1].ToString();
            cboManager.SelectedValue = Projects.Rows[CurrentRow][6];
            cboStatus.SelectedValue = Projects.Rows[CurrentRow][3];
            // TODO: If DTP unselect display blank date (disable DTP)
            //if (Projects.Rows[CurrentRow][4] == DBNull.Value)
            //{
            //    dtpStartDate.Checked = false;
            //    SetupDateTimePicker(dtpStartDate);
            //}
            //else // set date to date time picker value
            //{
            //    dtpStartDate.Checked = true;
            //    SetupDateTimePicker(dtpStartDate);
            //    dtpStartDate.Value = Convert.ToDateTime(Projects.Rows[CurrentRow][4]);
            //}
            try
            {
                dtpStartDate.Checked = true;
                SetupDateTimePicker(dtpStartDate);
                dtpStartDate.Value = Convert.ToDateTime(Projects.Rows[CurrentRow][4]);
            }
            catch // Catch null, reset date time picker
            {
                dtpStartDate.Checked = false;
                SetupDateTimePicker(dtpStartDate);
            }
            try
            {
                dtpEndDate.Checked = true;
                SetupDateTimePicker(dtpEndDate);
                dtpEndDate.Value = Convert.ToDateTime(Projects.Rows[CurrentRow][5]);
            }
            catch // Catch null, reset date time picker
            {
                dtpEndDate.Checked = false;
                SetupDateTimePicker(dtpEndDate);
            }
            txtDescription.Text = Projects.Rows[CurrentRow][2].ToString();
        }

        private void GetData()
        {
            Projects.Rows[CurrentRow][1] = txtName.Text;
            Projects.Rows[CurrentRow][6] = cboManager.SelectedValue;
            Projects.Rows[CurrentRow][3] = cboStatus.SelectedValue;
            Projects.Rows[CurrentRow][4] = dtpStartDate.Value.ToShortDateString();
            // TODO: DTP as blank
            //if (dtpStartDate.Checked)
            //{
            //    Projects.Rows[CurrentRow][4] = dtpStartDate.Value.ToShortDateString();
            //}
            //else if (dtpStartDate.Checked == false)
            //{
            //    Projects.Rows[CurrentRow][4] = DBNull.Value;
            //}
            Projects.Rows[CurrentRow][5] = dtpEndDate.Value.ToShortDateString();
            Projects.Rows[CurrentRow][2] = txtDescription.Text;
        }

        private void ClearForm()
        {
            SetStatusLabel("");
            txtName.Clear();
            cboManager.SelectedIndex = -1;
            cboStatus.SelectedIndex = -1;
            dtpStartDate.Checked = false;
            dtpEndDate.Checked = false;
            txtDescription.Clear();
            txtName.Focus();
        }

        private void SetStatusLabel(string message)
        {
            MyParent.SetStatusLabel(message);
        }
        #endregion
    }
}
