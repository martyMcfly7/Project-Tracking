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
    // Form to add & update Work information
    public partial class WorkForm : Form
    {

        #region Constants, Variables & Enums
        // Button text
        const string ADD = "&Add";
        const string UPDATE = "&Update";
        const string SAVE = "&Save";
        const string CANCEL = "&Cancel";

        // Display current row and total rows
        const string LABEL_ROWS = "{0} of {1} Total Work Submissions";

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

        private ProjectTrackingDataSet.WorkDataTable Work
        {
            get { return MyProjects.Work; } // Access Projects table
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
                if (value >= 0 && value < Work.Rows.Count)
                    currentRow = value;
            }
        }
        #endregion

        #region Events
        public WorkForm()
        {
            InitializeComponent();
        }

        private void WorkForm_Load(object sender, EventArgs e)
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
            SetupEmployeeComboBox();
            SetupTaskComboBox();
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
                    lblWorkID.Text = (Work.Rows.Count + 1).ToString();
                    // Change buttons to show Save/Cancel
                    btnAdd.Text = SAVE;
                    btnUpdate.Text = CANCEL;
                }
                else // Button says Save, save new work entry
                {
                    DataRow workRow = Work.NewRow();
                    // Add each control to correct Project row
                    workRow[1] = (int)cboEmployeeName.SelectedValue;
                    workRow[2] = (int)cboTaskWorked.SelectedValue;
                    workRow[3] = dtpDateWorked.Value;
                    workRow[4] = spnHoursWorked.Value;
                    Work.Rows.Add(workRow);
                    // Mark location as the new last row in the table
                    CurrentRow = Work.Count - 1;
                    btnAdd.Text = ADD;
                    btnUpdate.Text = UPDATE;
                    UpdateWorkLabel();
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
                    if (Work.Rows.Count > 0)
                    {
                        GetData();
                        SetStatusLabel("Updating entry was successful");
                    }
                }
                else // User cancels the add process
                {
                    if (Work.Rows.Count > 0)
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

        // Prevent selection of Task that is not Underway (Valdiation)
        private void cboTaskWorked_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTaskWorked.SelectedIndex == -1)
            {
                // HACK: Fix if/else
            }
            else
            {
                int taskID = (int)cboTaskWorked.SelectedValue;
                foreach (DataRow drTasks in MyProjects.Tasks.Rows)
                {
                    if ((int)drTasks["TaskID"] == taskID)
                    {
                        // If status is 2 (TaskStatus == Underway)
                        if ((byte)drTasks["TaskStatus"] == 2)
                        {
                            SetStatusLabel("");
                            btnAdd.Enabled = true;
                        }
                        else
                        {
                            SetStatusLabel("Task must have a status of Underway. Please select another Task.");
                            btnAdd.Enabled = false;
                        }
                        break;
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            SetStatusLabel("");
            this.Close();
        }
        #endregion

        #region Methods
        // Link Employee combobox to EmployeeID using employee name
        private void SetupEmployeeComboBox()
        {
            cboEmployeeName.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEmployeeName.DisplayMember = "FullName";
            cboEmployeeName.ValueMember = "EmployeeID";
            cboEmployeeName.DataSource = MyProjects.Employees;
            cboEmployeeName.SelectedValue = Work.Rows[CurrentRow][1]; // EmployeeID
        }

        // Link Task combobox to TaskID using task name
        private void SetupTaskComboBox()
        {
            cboTaskWorked.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTaskWorked.DisplayMember = "TaskName";
            cboTaskWorked.ValueMember = "TaskID";
            cboTaskWorked.DataSource = MyProjects.Tasks;
            cboTaskWorked.SelectedValue = Work.Rows[CurrentRow][2]; // TaskID
        }

        private void MoveToRow(MoveTo WhichRow)
        {
            // If there are rows
            if (Work.Rows.Count > 0)
            {
                if (WhichRow == MoveTo.Last) // Last is count - 1
                    CurrentRow = Work.Rows.Count - 1;
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
            UpdateWorkLabel();
            ShowData();
            // Change combo box entries according to currentRow
            SetupEmployeeComboBox();
            SetupTaskComboBox();
        }

        private void UpdateWorkLabel()
        {
            // Show current row number and total number of rows
            lblRowsCounter.Text = string.Format(LABEL_ROWS, (CurrentRow + 1).ToString(),
                Work.Rows.Count.ToString());
        }

        private void ShowData()
        {
            lblWorkID.Text = Work.Rows[CurrentRow][0].ToString();
            cboEmployeeName.SelectedValue = Work.Rows[CurrentRow][1];
            cboTaskWorked.SelectedValue = Work.Rows[CurrentRow][2];
            // TODO: If DTP unselect display blank date (disable DTP)
            dtpDateWorked.Value = Convert.ToDateTime(Work.Rows[CurrentRow][3]);
            spnHoursWorked.Value = Convert.ToDecimal(Work.Rows[CurrentRow][4]);
        }

        private void GetData()
        {
            Work.Rows[CurrentRow][1] = cboEmployeeName.SelectedValue;
            Work.Rows[CurrentRow][2] = cboTaskWorked.SelectedValue;
            // TODO: DTP as blank
            Work.Rows[CurrentRow][3] = dtpDateWorked.Value.ToShortDateString();
            Work.Rows[CurrentRow][4] = spnHoursWorked.Value;
        }

        private void ClearForm()
        {
            SetStatusLabel("");
            cboEmployeeName.SelectedIndex = -1;
            cboTaskWorked.SelectedIndex = -1;
            dtpDateWorked.Value = DateTime.Now;
            spnHoursWorked.Value = 0;
            cboEmployeeName.Focus();
        }

        private void SetStatusLabel(string message)
        {
            MyParent.SetStatusLabel(message);
        }
        #endregion
    }
}
