namespace ProjectTracking
{
    partial class ProjectHoursForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label projectNameLabel;
            System.Windows.Forms.Label projectDescriptionLabel;
            System.Windows.Forms.Label projectStatusLabel;
            System.Windows.Forms.Label projectStartDateLabel;
            System.Windows.Forms.Label projectEndDateLabel;
            System.Windows.Forms.Label projectManagerLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectHoursForm));
            this.projectsBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.projectsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.projectTrackingDataSet = new ProjectTracking.ProjectTrackingDataSet();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.projectsBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.txtProjectDescription = new System.Windows.Forms.TextBox();
            this.txtProjectStatus = new System.Windows.Forms.TextBox();
            this.txtProjectStartDate = new System.Windows.Forms.TextBox();
            this.txtProjectEndDate = new System.Windows.Forms.TextBox();
            this.txtProjectManager = new System.Windows.Forms.TextBox();
            this.pHDataGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHoursWorked = new System.Windows.Forms.TextBox();
            this.projectsTableAdapter = new ProjectTracking.ProjectTrackingDataSetTableAdapters.ProjectsTableAdapter();
            this.tableAdapterManager = new ProjectTracking.ProjectTrackingDataSetTableAdapters.TableAdapterManager();
            this.workBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.workTableAdapter = new ProjectTracking.ProjectTrackingDataSetTableAdapters.WorkTableAdapter();
            projectNameLabel = new System.Windows.Forms.Label();
            projectDescriptionLabel = new System.Windows.Forms.Label();
            projectStatusLabel = new System.Windows.Forms.Label();
            projectStartDateLabel = new System.Windows.Forms.Label();
            projectEndDateLabel = new System.Windows.Forms.Label();
            projectManagerLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.projectsBindingNavigator)).BeginInit();
            this.projectsBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.projectsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectTrackingDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.workBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // projectNameLabel
            // 
            projectNameLabel.AutoSize = true;
            projectNameLabel.Location = new System.Drawing.Point(26, 50);
            projectNameLabel.Name = "projectNameLabel";
            projectNameLabel.Size = new System.Drawing.Size(92, 16);
            projectNameLabel.TabIndex = 1;
            projectNameLabel.Text = "Project &Name:";
            // 
            // projectDescriptionLabel
            // 
            projectDescriptionLabel.AutoSize = true;
            projectDescriptionLabel.Location = new System.Drawing.Point(40, 134);
            projectDescriptionLabel.Name = "projectDescriptionLabel";
            projectDescriptionLabel.Size = new System.Drawing.Size(78, 16);
            projectDescriptionLabel.TabIndex = 11;
            projectDescriptionLabel.Text = "&Description:";
            // 
            // projectStatusLabel
            // 
            projectStatusLabel.AutoSize = true;
            projectStatusLabel.Location = new System.Drawing.Point(341, 78);
            projectStatusLabel.Name = "projectStatusLabel";
            projectStatusLabel.Size = new System.Drawing.Size(47, 16);
            projectStatusLabel.TabIndex = 5;
            projectStatusLabel.Text = "S&tatus:";
            // 
            // projectStartDateLabel
            // 
            projectStartDateLabel.AutoSize = true;
            projectStartDateLabel.Location = new System.Drawing.Point(47, 106);
            projectStartDateLabel.Name = "projectStartDateLabel";
            projectStartDateLabel.Size = new System.Drawing.Size(69, 16);
            projectStartDateLabel.TabIndex = 7;
            projectStartDateLabel.Text = "&Start Date:";
            // 
            // projectEndDateLabel
            // 
            projectEndDateLabel.AutoSize = true;
            projectEndDateLabel.Location = new System.Drawing.Point(322, 106);
            projectEndDateLabel.Name = "projectEndDateLabel";
            projectEndDateLabel.Size = new System.Drawing.Size(66, 16);
            projectEndDateLabel.TabIndex = 9;
            projectEndDateLabel.Text = "&End Date:";
            // 
            // projectManagerLabel
            // 
            projectManagerLabel.AutoSize = true;
            projectManagerLabel.Location = new System.Drawing.Point(55, 78);
            projectManagerLabel.Name = "projectManagerLabel";
            projectManagerLabel.Size = new System.Drawing.Size(64, 16);
            projectManagerLabel.TabIndex = 3;
            projectManagerLabel.Text = "&Manager:";
            // 
            // projectsBindingNavigator
            // 
            this.projectsBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.projectsBindingNavigator.BindingSource = this.projectsBindingSource;
            this.projectsBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.projectsBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.projectsBindingNavigator.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.projectsBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.projectsBindingNavigatorSaveItem});
            this.projectsBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.projectsBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.projectsBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.projectsBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.projectsBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.projectsBindingNavigator.Name = "projectsBindingNavigator";
            this.projectsBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.projectsBindingNavigator.Size = new System.Drawing.Size(632, 27);
            this.projectsBindingNavigator.TabIndex = 0;
            this.projectsBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Enabled = false;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorAddNewItem.Text = "Add";
            this.bindingNavigatorAddNewItem.Visible = false;
            // 
            // projectsBindingSource
            // 
            this.projectsBindingSource.DataMember = "Projects";
            this.projectsBindingSource.DataSource = this.projectTrackingDataSet;
            this.projectsBindingSource.CurrentChanged += new System.EventHandler(this.projectsBindingSource_CurrentChanged);
            // 
            // projectTrackingDataSet
            // 
            this.projectTrackingDataSet.DataSetName = "ProjectTrackingDataSet";
            this.projectTrackingDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(45, 24);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Enabled = false;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorDeleteItem.Text = "Cancel";
            this.bindingNavigatorDeleteItem.Visible = false;
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorMoveFirstItem.Text = "First";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorMovePreviousItem.Text = "Prior";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 27);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorMoveNextItem.Text = "Next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorMoveLastItem.Text = "Last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // projectsBindingNavigatorSaveItem
            // 
            this.projectsBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.projectsBindingNavigatorSaveItem.Enabled = false;
            this.projectsBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("projectsBindingNavigatorSaveItem.Image")));
            this.projectsBindingNavigatorSaveItem.Name = "projectsBindingNavigatorSaveItem";
            this.projectsBindingNavigatorSaveItem.Size = new System.Drawing.Size(29, 24);
            this.projectsBindingNavigatorSaveItem.Text = "Save";
            this.projectsBindingNavigatorSaveItem.Visible = false;
            // 
            // txtProjectName
            // 
            this.txtProjectName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.projectsBindingSource, "ProjectName", true));
            this.txtProjectName.Location = new System.Drawing.Point(134, 47);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.ReadOnly = true;
            this.txtProjectName.Size = new System.Drawing.Size(425, 22);
            this.txtProjectName.TabIndex = 2;
            // 
            // txtProjectDescription
            // 
            this.txtProjectDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.projectsBindingSource, "ProjectDescription", true));
            this.txtProjectDescription.Location = new System.Drawing.Point(134, 131);
            this.txtProjectDescription.Multiline = true;
            this.txtProjectDescription.Name = "txtProjectDescription";
            this.txtProjectDescription.ReadOnly = true;
            this.txtProjectDescription.Size = new System.Drawing.Size(425, 100);
            this.txtProjectDescription.TabIndex = 12;
            // 
            // txtProjectStatus
            // 
            this.txtProjectStatus.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.projectsBindingSource, "ProjectStatus", true));
            this.txtProjectStatus.Location = new System.Drawing.Point(404, 75);
            this.txtProjectStatus.Name = "txtProjectStatus";
            this.txtProjectStatus.ReadOnly = true;
            this.txtProjectStatus.Size = new System.Drawing.Size(155, 22);
            this.txtProjectStatus.TabIndex = 6;
            // 
            // txtProjectStartDate
            // 
            this.txtProjectStartDate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.projectsBindingSource, "ProjectStartDate", true));
            this.txtProjectStartDate.Location = new System.Drawing.Point(134, 103);
            this.txtProjectStartDate.Name = "txtProjectStartDate";
            this.txtProjectStartDate.ReadOnly = true;
            this.txtProjectStartDate.Size = new System.Drawing.Size(155, 22);
            this.txtProjectStartDate.TabIndex = 8;
            // 
            // txtProjectEndDate
            // 
            this.txtProjectEndDate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.projectsBindingSource, "ProjectEndDate", true));
            this.txtProjectEndDate.Location = new System.Drawing.Point(404, 103);
            this.txtProjectEndDate.Name = "txtProjectEndDate";
            this.txtProjectEndDate.ReadOnly = true;
            this.txtProjectEndDate.Size = new System.Drawing.Size(155, 22);
            this.txtProjectEndDate.TabIndex = 10;
            // 
            // txtProjectManager
            // 
            this.txtProjectManager.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.projectsBindingSource, "ProjectManager", true));
            this.txtProjectManager.Location = new System.Drawing.Point(134, 75);
            this.txtProjectManager.Name = "txtProjectManager";
            this.txtProjectManager.ReadOnly = true;
            this.txtProjectManager.Size = new System.Drawing.Size(155, 22);
            this.txtProjectManager.TabIndex = 4;
            // 
            // pHDataGridView
            // 
            this.pHDataGridView.AllowUserToAddRows = false;
            this.pHDataGridView.AllowUserToDeleteRows = false;
            this.pHDataGridView.AllowUserToOrderColumns = true;
            this.pHDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pHDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pHDataGridView.Location = new System.Drawing.Point(12, 251);
            this.pHDataGridView.Name = "pHDataGridView";
            this.pHDataGridView.ReadOnly = true;
            this.pHDataGridView.RowHeadersWidth = 51;
            this.pHDataGridView.RowTemplate.Height = 24;
            this.pHDataGridView.Size = new System.Drawing.Size(608, 240);
            this.pHDataGridView.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(261, 513);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Total Project &Hours Worked:";
            // 
            // txtHoursWorked
            // 
            this.txtHoursWorked.Location = new System.Drawing.Point(459, 510);
            this.txtHoursWorked.Name = "txtHoursWorked";
            this.txtHoursWorked.ReadOnly = true;
            this.txtHoursWorked.Size = new System.Drawing.Size(100, 22);
            this.txtHoursWorked.TabIndex = 14;
            this.txtHoursWorked.Text = "0.00";
            // 
            // projectsTableAdapter
            // 
            this.projectsTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.EmployeesTableAdapter = null;
            this.tableAdapterManager.ProjectsTableAdapter = this.projectsTableAdapter;
            this.tableAdapterManager.StatusTableAdapter = null;
            this.tableAdapterManager.TasksTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = ProjectTracking.ProjectTrackingDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.WorkTableAdapter = null;
            // 
            // workBindingSource
            // 
            this.workBindingSource.DataMember = "Work";
            this.workBindingSource.DataSource = this.projectTrackingDataSet;
            // 
            // workTableAdapter
            // 
            this.workTableAdapter.ClearBeforeFill = true;
            // 
            // ProjectHoursForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 553);
            this.Controls.Add(this.txtHoursWorked);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pHDataGridView);
            this.Controls.Add(projectNameLabel);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(projectDescriptionLabel);
            this.Controls.Add(this.txtProjectDescription);
            this.Controls.Add(projectStatusLabel);
            this.Controls.Add(this.txtProjectStatus);
            this.Controls.Add(projectStartDateLabel);
            this.Controls.Add(this.txtProjectStartDate);
            this.Controls.Add(projectEndDateLabel);
            this.Controls.Add(this.txtProjectEndDate);
            this.Controls.Add(projectManagerLabel);
            this.Controls.Add(this.txtProjectManager);
            this.Controls.Add(this.projectsBindingNavigator);
            this.Name = "ProjectHoursForm";
            this.Text = "Project Hours Form";
            this.Load += new System.EventHandler(this.ProjectHoursForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.projectsBindingNavigator)).EndInit();
            this.projectsBindingNavigator.ResumeLayout(false);
            this.projectsBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.projectsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectTrackingDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.workBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ProjectTrackingDataSet projectTrackingDataSet;
        private System.Windows.Forms.BindingSource projectsBindingSource;
        private ProjectTrackingDataSetTableAdapters.ProjectsTableAdapter projectsTableAdapter;
        private ProjectTrackingDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator projectsBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton projectsBindingNavigatorSaveItem;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.TextBox txtProjectDescription;
        private System.Windows.Forms.TextBox txtProjectStatus;
        private System.Windows.Forms.TextBox txtProjectStartDate;
        private System.Windows.Forms.TextBox txtProjectEndDate;
        private System.Windows.Forms.TextBox txtProjectManager;
        private System.Windows.Forms.BindingSource workBindingSource;
        private ProjectTrackingDataSetTableAdapters.WorkTableAdapter workTableAdapter;
        private System.Windows.Forms.DataGridView pHDataGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHoursWorked;
    }
}