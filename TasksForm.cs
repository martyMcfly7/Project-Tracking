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
    // Form to add & update Task information
    public partial class TasksForm : Form
    {
        
        #region Constants, Variables & Enums
        // Button text
        const string ADD = "&Add";
        const string UPDATE = "&Update";
        const string SAVE = "&Save";
        const string CANCEL = "&Cancel";

        // Display current row and total rows
        const string LABEL_ROWS = "{0} of {1} Total Tasks";

        // Captures row index
        int currentRow;

        // Navigation through rows
        enum MoveTo { First, Prior = -1, Next = 1, Last }
        #endregion

        #region Properties
        private ProjectTrackingDataSet MyProjects
        {
            get { return MyParent.MyProjects; } // Access Projects dataset
        }

        private ProjectTrackingDataSet.TasksDataTable Tasks
        {
            get { return MyProjects.Tasks; } // Access Tasks table
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
                if (value >= 0 && value < Tasks.Rows.Count)
                    currentRow = value;
            }
        }
        #endregion

        #region Events
        public TasksForm()
        {
            InitializeComponent();
        }

        private void TasksForm_Load(object sender, EventArgs e)
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
                // If button says add, start process of creating new entry
                if (btnAdd.Text == ADD)
                {
                    ClearForm();
                    lblTaskID.Text = (Tasks.Rows.Count + 1).ToString();
                    // Change buttons to show Save/Cancel
                    btnAdd.Text = SAVE;
                    btnUpdate.Text = CANCEL;
                }
                else // Button says Save, save new task entry
                {
                    DataRow taskRow = Tasks.NewRow();
                    // Add each control to correct Task row
                    taskRow[1] = (int)cboProject.SelectedValue;
                    taskRow[2] = txtName.Text;
                    taskRow[3] = txtDescription.Text;
                    taskRow[4] = (byte)cboStatus.SelectedValue;
                    taskRow[5] = dtpStartDate.Value;
                    taskRow[6] = dtpEndDate.Value;
                    Tasks.Rows.Add(taskRow);
                    // Mark location as the new last row in the table
                    CurrentRow = Tasks.Count - 1;
                    btnAdd.Text = ADD;
                    btnUpdate.Text = UPDATE;
                    UpdateTasksLabel();
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
            try
            {
                // Update data from controls and save
                if (btnUpdate.Text == UPDATE)
                {
                    if (Tasks.Rows.Count > 0)
                    {
                        GetData();
                        SetStatusLabel("Updating entry was successful");
                    }
                }
                else // User cancels the add process
                {
                    if (Tasks.Rows.Count > 0)
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
            cboProject.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProject.DisplayMember = "ProjectName";
            cboProject.ValueMember = "ProjectID";
            cboProject.DataSource = MyProjects.Projects;
            cboProject.SelectedValue = Tasks.Rows[CurrentRow][1]; // ProjectID
        }

        // Link Status combobox to StatusID using status types
        private void SetupStatusComboBox()
        {
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.DisplayMember = "StatusType";
            cboStatus.ValueMember = "StatusID";
            cboStatus.DataSource = MyProjects.Status;
            cboStatus.SelectedValue = Tasks.Rows[CurrentRow][4]; // TaskStatus
        }

        // TODO: Display blank DTP when unselected
        private void SetupDateTimePicker(DateTimePicker dateTimePicker)
        {
            if (!dateTimePicker.Checked)
            {
                // Hide date value since it's not set
                dateTimePicker.CustomFormat = "";
                dateTimePicker.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                dateTimePicker.CustomFormat = null;
                dateTimePicker.Format = DateTimePickerFormat.Short;
            }
        }

        private void MoveToRow(MoveTo WhichRow)
        {
            // If there are rows
            if (Tasks.Rows.Count > 0)
            {
                if (WhichRow == MoveTo.Last) // Last is count - 1
                    CurrentRow = Tasks.Rows.Count - 1;
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
            UpdateTasksLabel();
            ShowData();
            // Change combo box entries according to currentRow
            SetupManagerComboBox();
            SetupStatusComboBox();
        }

        private void UpdateTasksLabel()
        {
            // Show current row number and total number of rows
            lblRowsCounter.Text = string.Format(LABEL_ROWS, (CurrentRow + 1).ToString(),
                Tasks.Rows.Count.ToString());
        }

        private void ShowData()
        {
            lblTaskID.Text = Tasks.Rows[CurrentRow][0].ToString();
            txtName.Text = Tasks.Rows[CurrentRow][2].ToString();
            cboProject.SelectedValue = Tasks.Rows[CurrentRow][1];
            cboStatus.SelectedValue = Tasks.Rows[CurrentRow][4];
            // TODO: If DTP unselect display blank date (disable DTP)
            try
            {
                dtpStartDate.Checked = true;
                SetupDateTimePicker(dtpStartDate);
                dtpStartDate.Value = Convert.ToDateTime(Tasks.Rows[CurrentRow][5]);
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
                dtpEndDate.Value = Convert.ToDateTime(Tasks.Rows[CurrentRow][6]);
            }
            catch // Catch null, reset date time picker
            {
                dtpEndDate.Checked = false;
                SetupDateTimePicker(dtpEndDate);
            }
            txtDescription.Text = Tasks.Rows[CurrentRow][3].ToString();
        }

        private void GetData()
        {
            Tasks.Rows[CurrentRow][2] = txtName.Text;
            Tasks.Rows[CurrentRow][1] = cboProject.SelectedValue;
            Tasks.Rows[CurrentRow][4] = cboStatus.SelectedValue;
            // TODO: DTP as blank
            Tasks.Rows[CurrentRow][5] = dtpStartDate.Value.ToShortDateString();
            Tasks.Rows[CurrentRow][6] = dtpEndDate.Value.ToShortDateString();
            Tasks.Rows[CurrentRow][3] = txtDescription.Text;
        }

        private void ClearForm()
        {
            SetStatusLabel("");
            txtName.Clear();
            cboProject.SelectedIndex = -1;
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
