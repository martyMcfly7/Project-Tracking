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
        // constants used for button text
        const string ADD = "&Add";
        const string UPDATE = "&Update";
        const string SAVE = "&Save";
        const string CANCEL = "&Cancel";

        // constant to display current row and total number of rows
        const string LABEL_ROWS = "{0} of {1} Total Projects";

        // class level variable to capture row index
        int currentRow;

        // enumeration for moving through rows
        enum MoveTo { First, Prior = -1, Next = 1, Last }
        #endregion

        #region Events
        public ProjectsForm()
        {
            InitializeComponent();
        }

        private void ProjectsForm_Load(object sender, EventArgs e)
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
                    lblProjectID.Text = (Projects.Rows.Count + 1).ToString();
                    btnAdd.Text = SAVE;
                    btnUpdate.Text = CANCEL;
                }
                else // complete the add process and save
                {
                    // create new Project row
                    DataRow projectRow = Projects.NewRow();
                    // add each control to correct Project row
                    projectRow[1] = txtName.Text;
                    projectRow[2] = txtDescription.Text;
                    projectRow[3] = (byte)cboStatus.SelectedValue;
                    projectRow[4] = dtpStartDate.Value;
                    projectRow[5] = dtpEndDate.Value;
                    projectRow[6] = (int)cboManager.SelectedValue;
                    Projects.Rows.Add(projectRow);

                    // create instance of Students row in the table
                    //Projects.AddProjectsRow(txtName.Text, txtDescription.Text,
                    //    (ProjectTrackingDataSet.StatusRow)(/*(byte)*/cboStatus.SelectedValue), dtpStartDate.Value,
                    //    dtpEndDate.Value, (ProjectTrackingDataSet.EmployeesRow)cboManager.SelectedValue);
                    
                    // mark location as the new last row in the table
                    CurrentRow = Projects.Count - 1;
                    // reset buttons to default
                    btnAdd.Text = ADD;
                    btnUpdate.Text = UPDATE;
                    // update label for current and total students
                    UpdateProjectsLabel();
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
                    if (Projects.Rows.Count > 0)
                    {
                        GetData();
                    }
                }
                else // if user decides to cancel the add process
                {
                    if (Projects.Rows.Count > 0)
                        ShowData();
                    else
                        ClearForm();
                    btnAdd.Text = ADD;
                    btnUpdate.Text = UPDATE;
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

        // get access to the Projects table
        private ProjectTrackingDataSet.ProjectsDataTable Projects
        {
            get { return MyProjects.Projects; }
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
                if (value >= 0 && value < Projects.Rows.Count)
                    currentRow = value;
            }
        }
        #endregion

        #region Methods
        private void SetupManagerComboBox()
        {
            cboManager.DropDownStyle = ComboBoxStyle.DropDownList;
            cboManager.DisplayMember = "FullName"; // calculated column using expression
            cboManager.ValueMember = "EmployeeID";
            cboManager.DataSource = MyProjects.Employees;
            cboManager.SelectedValue = Projects.Rows[CurrentRow][6]; // ProjectManager
        }

        private void SetupStatusComboBox()
        {
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.DisplayMember = "StatusType";
            cboStatus.ValueMember = "StatusID";
            cboStatus.DataSource = MyProjects.Status;
            cboStatus.SelectedValue = Projects.Rows[CurrentRow][3]; // ProjectStatus
        }

        private void SetupDateTimePicker(DateTimePicker dateTimePicker)
        {
            if (!dateTimePicker.Checked)
            {
                // hide date value since it's not set
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
            // if there are rows
            if (Projects.Rows.Count > 0)
            {
                if (WhichRow == MoveTo.Last) // last is count - 1
                    CurrentRow = Projects.Rows.Count - 1;
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
            UpdateProjectsLabel();
            ShowData();
            // change combo box entries according to currentRow
            SetupManagerComboBox();
            SetupStatusComboBox();
        }

        private void UpdateProjectsLabel()
        {
            // format label to show current row number and total count of rows
            lblRowsCounter.Text = string.Format(LABEL_ROWS, (CurrentRow + 1).ToString(),
                Projects.Rows.Count.ToString()); // add 1 to CurrentRow so first row displays as 1 not 0
        }

        private void ShowData()
        {
            // go to dataset, Projects table, go to current index in row and specified position
            lblProjectID.Text = Projects.Rows[CurrentRow][0].ToString();
            txtName.Text = Projects.Rows[CurrentRow][1].ToString();
            cboManager.SelectedValue = Projects.Rows[CurrentRow][6];
            cboStatus.SelectedValue = Projects.Rows[CurrentRow][3];
            // if null, uncheck date time picker and clear date
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
            // try/catch nullable values
            try
            {
                dtpStartDate.Checked = true;
                SetupDateTimePicker(dtpStartDate);
                dtpStartDate.Value = Convert.ToDateTime(Projects.Rows[CurrentRow][4]);
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
                dtpEndDate.Value = Convert.ToDateTime(Projects.Rows[CurrentRow][5]);
            }
            catch // catch null, reset date time picker
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
            SetReady("");
            txtName.Clear();
            cboManager.SelectedIndex = -1;
            cboStatus.SelectedIndex = -1;
            dtpStartDate.Checked = false;
            dtpEndDate.Checked = false;
            txtDescription.Clear();
            txtName.Focus();
        }

        private void SetReady(string message)
        {
            MyParent.SetReadyLabel(message);
        }
        #endregion
    }
}
