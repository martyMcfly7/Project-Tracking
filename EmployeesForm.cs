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
        // constants used for button text
        const string ADD = "&Add";
        const string UPDATE = "&Update";
        const string SAVE = "&Save";
        const string CANCEL = "&Cancel";

        // constant to display current row and total number of rows
        const string LABEL_ROWS = "{0} of {1} Total Employees";

        // class level variable to capture row index
        int currentRow;

        // enumeration for moving through rows
        enum MoveTo { First, Prior = -1, Next = 1, Last }
        #endregion

        #region Events
        public EmployeesForm()
        {
            InitializeComponent();
        }

        private void EmployeesForm_Load(object sender, EventArgs e)
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
                    // create instance of Employees row in the table
                    Employees.AddEmployeesRow(txtTitle.Text, txtFirstName.Text, txtLastName.Text);
                    // mark location as the new last row in the table
                    CurrentRow = Employees.Count - 1;
                    // reset buttons to default
                    btnAdd.Text = ADD;
                    btnUpdate.Text = UPDATE;
                    // update label for current and total employees
                    UpdateEmployeeLabel();
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
                    if (Employees.Rows.Count > 0)
                    {
                        GetData();
                    }
                }
                else // if user decides to cancel the add process
                {
                    if (Employees.Rows.Count > 0)
                        ShowData();
                    else
                        ClearForm();
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

        // get access to the Employees table
        private ProjectTrackingDataSet.EmployeesDataTable Employees
        {
            get { return MyProjects.Employees; }
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
                if (value >= 0 && value < Employees.Rows.Count)
                    currentRow = value;
            }
        }
        #endregion

        #region Methods
        private void MoveToRow(MoveTo WhichRow)
        {
            // if there are rows
            if (Employees.Rows.Count > 0)
            {
                if (WhichRow == MoveTo.Last) // last is count - 1
                    CurrentRow = Employees.Rows.Count - 1;
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
            UpdateEmployeeLabel();
            ShowData();
        }

        private void UpdateEmployeeLabel()
        {
            // format label to show current row number and total count of rows
            lblRowsCounter.Text = string.Format(LABEL_ROWS, (CurrentRow + 1).ToString(),
                Employees.Rows.Count.ToString()); // add 1 to CurrentRow so first row displays as 1 not 0
        }

        private void ShowData()
        {
            // go to dataset, Employees table, go to current index in row and specified position
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
            SetReady("");
            txtFirstName.Clear();
            txtLastName.Clear();
            txtTitle.Clear();
            txtFirstName.Focus();
        }

        private void SetReady(string message)
        {
            MyParent.SetReadyLabel(message);
        }
        #endregion
    }
}
