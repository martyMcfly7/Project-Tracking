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
        // constants used for button text
        const string ADD = "&Add";
        const string UPDATE = "&Update";
        const string SAVE = "&Save";
        const string CANCEL = "&Cancel";

        // constant to display current row and total number of rows
        const string LABEL_ROWS = "{0} of {1} Total Work Submissions";

        // class level variable to capture row index
        int currentRow;

        // enumeration for moving through rows
        enum MoveTo { First, Prior = -1, Next = 1, Last }
        #endregion

        #region Events
        public WorkForm()
        {
            InitializeComponent();
        }

        private void WorkForm_Load(object sender, EventArgs e)
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
            // link Employee combobox to EmployeeID using employee name
            SetupEmployeeComboBox();
            // link Task combobox to TaskID using task name
            SetupTaskComboBox();
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
                    lblWorkID.Text = (Work.Rows.Count + 1).ToString();
                    btnAdd.Text = SAVE;
                    btnUpdate.Text = CANCEL;
                }
                else // complete the add process and save
                {
                    // create new Project row
                    DataRow workRow = Work.NewRow();
                    // add each control to correct Project row
                    workRow[1] = (int)cboEmployeeName.SelectedValue;
                    workRow[2] = (int)cboTaskWorked.SelectedValue;
                    workRow[3] = dtpDateWorked.Value;
                    workRow[4] = spnHoursWorked.Value;
                    Work.Rows.Add(workRow);

                    // create instance of Students row in the table
                    //Work.AddWorkRow((ProjectTrackingDataSet.EmployeesRow)cboEmployeeName.SelectedValue,
                    //    (ProjectTrackingDataSet.TasksRow)cboTaskWorked.SelectedValue, dtpDateWorked.Value,
                    //    spnHoursWorked.Value);

                    // mark location as the new last row in the table
                    CurrentRow = Work.Count - 1;
                    // reset buttons to default
                    btnAdd.Text = ADD;
                    btnUpdate.Text = UPDATE;
                    // update label for current and total students
                    UpdateWorkLabel();
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
                    if (Work.Rows.Count > 0)
                    {
                        GetData();
                    }
                }
                else // if user decides to cancel the add process
                {
                    if (Work.Rows.Count > 0)
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

        // prevent selection of Task that is not Underway
        private void cboTaskWorked_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTaskWorked.SelectedIndex == -1)
            {
                
            }
            else
            {
                int taskID = (int)cboTaskWorked.SelectedValue;
                foreach (DataRow drTasks in MyProjects.Tasks.Rows)
                {
                    if ((int)drTasks["TaskID"] == taskID)
                    {
                        // if Underway "2"
                        if ((byte)drTasks["TaskStatus"] == 2) // cast as byte since TaskStatus is TinyInt
                        {
                            SetReady(""); // clear ready message
                            btnAdd.Enabled = true;
                        }
                        else
                        {
                            SetReady("Task must have a status of Underway. Please select another Task.");
                            btnAdd.Enabled = false;
                        }
                        break; // break out of the foreach loop
                    }
                }
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
        private ProjectTrackingDataSet.WorkDataTable Work
        {
            get { return MyProjects.Work; }
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
                if (value >= 0 && value < Work.Rows.Count)
                    currentRow = value;
            }
        }
        #endregion

        #region Methods
        private void SetupEmployeeComboBox()
        {
            cboEmployeeName.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEmployeeName.DisplayMember = "FullName"; // calculated column using expression
            cboEmployeeName.ValueMember = "EmployeeID";
            cboEmployeeName.DataSource = MyProjects.Employees;
            cboEmployeeName.SelectedValue = Work.Rows[CurrentRow][1]; // EmployeeID
        }

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
            // if there are rows
            if (Work.Rows.Count > 0)
            {
                if (WhichRow == MoveTo.Last) // last is count - 1
                    CurrentRow = Work.Rows.Count - 1;
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
            UpdateWorkLabel();
            ShowData();
            // change combo box entries according to currentRow
            SetupEmployeeComboBox();
            SetupTaskComboBox();
        }

        private void UpdateWorkLabel()
        {
            // format label to show current row number and total count of rows
            lblRowsCounter.Text = string.Format(LABEL_ROWS, (CurrentRow + 1).ToString(),
                Work.Rows.Count.ToString()); // add 1 to CurrentRow so first row displays as 1 not 0
        }

        private void ShowData()
        {
            // go to dataset, Work table, go to current index in row and specified position
            lblWorkID.Text = Work.Rows[CurrentRow][0].ToString();
            cboEmployeeName.SelectedValue = Work.Rows[CurrentRow][1];
            cboTaskWorked.SelectedValue = Work.Rows[CurrentRow][2];
            dtpDateWorked.Value = Convert.ToDateTime(Work.Rows[CurrentRow][3]);
            spnHoursWorked.Value = Convert.ToDecimal(Work.Rows[CurrentRow][4]);
        }

        private void GetData()
        {
            Work.Rows[CurrentRow][1] = cboEmployeeName.SelectedValue;
            Work.Rows[CurrentRow][2] = cboTaskWorked.SelectedValue;
            Work.Rows[CurrentRow][3] = dtpDateWorked.Value.ToShortDateString();
            Work.Rows[CurrentRow][4] = spnHoursWorked.Value;
        }

        private void ClearForm()
        {
            SetReady("");
            cboEmployeeName.SelectedIndex = -1;
            cboTaskWorked.SelectedIndex = -1;
            dtpDateWorked.Value = DateTime.Now;
            spnHoursWorked.Value = 0;
            cboEmployeeName.Focus();
        }

        private void SetReady(string message)
        {
            MyParent.SetReadyLabel(message);
        }
        #endregion
    }
}
