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
    // Form to add & update Employee information
    public partial class EmployeesForm : Form
    {

        #region Constants, Variables & Enums
        // Button text
        const string ADD = "&Add";
        const string UPDATE = "&Update";
        const string SAVE = "&Save";
        const string CANCEL = "&Cancel";

        // Displays current row and total rows
        const string LABEL_ROWS = "{0} of {1} Total Employees";

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

        private ProjectTrackingDataSet.EmployeesDataTable Employees
        {
            get { return MyProjects.Employees; } // Access Employees table
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
                if (value >= 0 && value < Employees.Rows.Count)
                    currentRow = value;
            }
        }
        #endregion

        #region Events
        public EmployeesForm()
        {
            InitializeComponent();
        }

        private void EmployeesForm_Load(object sender, EventArgs e)
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
                    lblEmployeeID.Text = (Employees.Rows.Count + 1).ToString();
                    // Change buttons to show Save/Cancel
                    btnAdd.Text = SAVE;
                    btnUpdate.Text = CANCEL;
                }
                else // Button says Save, save new employee entry
                {
                    Employees.AddEmployeesRow(txtTitle.Text, txtFirstName.Text, txtLastName.Text);
                    // Mark location as the new last row in the table
                    CurrentRow = Employees.Count - 1;
                    btnAdd.Text = ADD;
                    btnUpdate.Text = UPDATE;
                    UpdateEmployeeLabel();
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
                    if (Employees.Rows.Count > 0)
                    {
                        GetData();
                        SetStatusLabel("Updating entry was successful");
                    }
                }
                else // User cancels the add process
                {
                    if (Employees.Rows.Count > 0)
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
        private void MoveToRow(MoveTo WhichRow)
        {
            SetStatusLabel("");
            // If there are rows
            if (Employees.Rows.Count > 0)
            {
                if (WhichRow == MoveTo.Last) // Last is count - 1
                    CurrentRow = Employees.Rows.Count - 1;
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
            UpdateEmployeeLabel();
            ShowData();
        }

        private void UpdateEmployeeLabel()
        {
            // Show current row number and total number of rows
            lblRowsCounter.Text = string.Format(LABEL_ROWS, (CurrentRow + 1).ToString(),
                Employees.Rows.Count.ToString());
        }

        private void ShowData()
        {
            lblEmployeeID.Text = Employees.Rows[CurrentRow][0].ToString();
            txtFirstName.Text = Employees.Rows[CurrentRow][2].ToString();
            txtLastName.Text = Employees.Rows[CurrentRow][3].ToString();
            txtTitle.Text = Employees.Rows[CurrentRow][1].ToString();
        }

        private void GetData()
        {
            Employees.Rows[CurrentRow][2] = txtFirstName.Text;
            Employees.Rows[CurrentRow][3] = txtLastName.Text;
            Employees.Rows[CurrentRow][1] = txtTitle.Text;
        }

        private void ClearForm()
        {
            SetStatusLabel("");
            txtFirstName.Clear();
            txtLastName.Clear();
            txtTitle.Clear();
            txtFirstName.Focus();
        }

        private void SetStatusLabel(string message)
        {
            MyParent.SetStatusLabel(message);
        }
        #endregion
    }
}
