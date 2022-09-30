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
        // constants used for button text
        const string ADD = "&Add";
        const string UPDATE = "&Update";
        const string SAVE = "&Save";
        const string CANCEL = "&Cancel";

        // constant to display current row and total number of rows
        const string LABEL_ROWS = "{0} of {1} Total Tasks";

        // class level variable to capture row index
        int currentRow;

        // enumeration for moving through rows
        enum MoveTo { First, Prior = -1, Next = 1, Last }
        #endregion

        #region Events
        public TasksForm()
        {
            InitializeComponent();
        }

        private void TasksForm_Load(object sender, EventArgs e)
        {
            // setup Add and Update buttons
            btnAdd.Text = ADD;
            btnUpdate.Text = UPDATE;
            // setup navigation buttons
            btnFirst.Tag = MoveTo.First;
            btnPrior.Tag = MoveTo.Prior;
            btnNext.Tag = MoveTo.Next;
            btnLast.Tag = MoveTo.Last;
            // show first row in dataset
            MoveToRow(MoveTo.First);
            // link Manager combobox to EmployeeID using employee name
            SetupManagerComboBox();
            // link Status combobox to StatusID using status types
            SetupStatusComboBox();
            // set dateTimePickers to null, when clicked on set value to date selected
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
            // treat button as MoveTo object, set to tag
            MoveToRow((MoveTo)((Button)sender).Tag);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            MoveToRow(MoveTo.First); // |<
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            MoveToRow(MoveTo.Prior); // <<
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            MoveToRow(MoveTo.Next); // >>
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            MoveToRow(MoveTo.Last); // >|
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // if button says add start process of creating new entry
                if (btnAdd.Text == ADD)
                {
                    ClearForm();
                    btnAdd.Text = SAVE;
                    btnUpdate.Text = CANCEL;
                }
                else // complete the add process and save
                {
                    // create new Project row
                    DataRow taskRow = Tasks.NewRow();
                    // add each control to correct Project row
                    taskRow[1] = (int)cboProject.SelectedValue;
                    taskRow[2] = txtName.Text;
                    taskRow[3] = txtDescription.Text;
                    taskRow[4] = (byte)cboStatus.SelectedValue;
                    taskRow[5] = dtpStartDate.Value;
                    taskRow[6] = dtpEndDate.Value;
                    Tasks.Rows.Add(taskRow);

                    // create instance of Students row in the table
                    //Tasks.AddTasksRow((ProjectTrackingDataSet.ProjectsRow)cboProject.SelectedValue, txtName.Text,
                    //    txtDescription.Text, (ProjectTrackingDataSet.StatusRow)cboStatus.SelectedItem,
                    //    dtpStartDate.Value, dtpEndDate.Value);

                    // mark location as the new last row in the table
                    CurrentRow = Tasks.Count - 1;
                    // reset buttons to default
                    btnAdd.Text = ADD;
                    btnUpdate.Text = UPDATE;
                    // update label for current and total students
                    UpdateTasksLabel();
                }
            }
            catch (Exception ex)
            {
                SetReady("Error adding entry");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // if user decides to update, get data and save
                if (btnUpdate.Text == UPDATE)
                {
                    if (Tasks.Rows.Count > 0)
                    {
                        GetData();
                    }
                }
                else // if user decides to cancel the add process
                {
                    if (Tasks.Rows.Count > 0)
                        ShowData();
                    else
                        ClearForm();
                    btnAdd.Text = ADD;
                }
            }
            catch (Exception ex)
            {
                SetReady("Error updating entry");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Properties
        private ProjectTrackingDataSet MyProjects
        {
            get { return MyParent.MyProjects; }
        }

        // get access to the Students table
        private ProjectTrackingDataSet.TasksDataTable Tasks
        {
            get { return MyProjects.Tasks; }
        }

        // to access the Main Form
        MainForm MyParent
        {
            get { return (MainForm)MdiParent; }
        }

        private int CurrentRow
        {
            get { return currentRow; }
            set
            {
                // current row must be larger or equal to 0 & less than number of rows
                if (value >= 0 && value < Tasks.Rows.Count)
                    currentRow = value;
            }
        }
        #endregion

        #region Methods
        private void SetupManagerComboBox()
        {
            cboProject.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProject.DisplayMember = "ProjectName";
            cboProject.ValueMember = "ProjectID";
            cboProject.DataSource = MyProjects.Projects;
            cboProject.SelectedValue = Tasks.Rows[CurrentRow][1]; // ProjectID
        }

        private void SetupStatusComboBox()
        {
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.DisplayMember = "StatusType";
            cboStatus.ValueMember = "StatusID";
            cboStatus.DataSource = MyProjects.Status;
            cboStatus.SelectedValue = Tasks.Rows[CurrentRow][4]; // TaskStatus
        }

        private void SetupDateTimePicker(DateTimePicker dateTimePicker)
        {
            if (!dateTimePicker.Checked)
            {
                // hide date value since it's not set
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
            // if there are rows
            if (Tasks.Rows.Count > 0)
            {
                if (WhichRow == MoveTo.Last) // last is count - 1
                    CurrentRow = Tasks.Rows.Count - 1;
                else if (WhichRow == MoveTo.First) // first is 0
                    CurrentRow = 0;
                else
                    CurrentRow += (int)WhichRow; // prior or next append the index
                // call method to update/change form controls
                FormChange();
            }
            else
                ClearForm();
        }

        private void FormChange()
        {
            UpdateTasksLabel();
            ShowData();
            // change combo box entries according to currentRow
            SetupManagerComboBox();
            SetupStatusComboBox();
        }

        private void UpdateTasksLabel()
        {
            // format label to show current row number and total count of rows
            lblRowsCounter.Text = string.Format(LABEL_ROWS, (CurrentRow + 1).ToString(),
                Tasks.Rows.Count.ToString()); // add 1 to CurrentRow so first row displays as 1 not 0
        }

        private void ShowData()
        {
            // go to dataset, Tasks table, go to current index in row and specified position
            lblTaskID.Text = Tasks.Rows[CurrentRow][0].ToString();
            txtName.Text = Tasks.Rows[CurrentRow][2].ToString();
            cboProject.SelectedValue = Tasks.Rows[CurrentRow][1];
            cboStatus.SelectedValue = Tasks.Rows[CurrentRow][4];
            // try/catch nullable values
            try
            {
                dtpStartDate.Checked = true;
                SetupDateTimePicker(dtpStartDate);
                dtpStartDate.Value = Convert.ToDateTime(Tasks.Rows[CurrentRow][5]);
            }
            catch // catch null, reset date time picker
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
            catch // catch null, reset date time picker
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
            Tasks.Rows[CurrentRow][5] = dtpStartDate.Value.ToShortDateString();
            Tasks.Rows[CurrentRow][6] = dtpEndDate.Value.ToShortDateString();
            Tasks.Rows[CurrentRow][3] = txtDescription.Text;
        }

        private void ClearForm()
        {
            SetReady("");
            txtName.Clear();
            cboProject.SelectedIndex = -1;
            cboStatus.SelectedIndex = -1;
            dtpStartDate.Checked = false;
            dtpEndDate.Checked = false;
            txtDescription.Clear();
            txtName.Clear();
        }

        private void SetReady(string message)
        {
            MyParent.SetReadyLabel(message);
        }
        #endregion
    }
}
