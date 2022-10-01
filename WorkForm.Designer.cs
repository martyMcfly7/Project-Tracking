namespace ProjectTracking
{
    partial class WorkForm
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
            this.cboEmployeeName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboTaskWorked = new System.Windows.Forms.ComboBox();
            this.dtpDateWorked = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.spnHoursWorked = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.lblRowsCounter = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblWorkID = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnPrior = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.spnHoursWorked)).BeginInit();
            this.SuspendLayout();
            // 
            // cboEmployeeName
            // 
            this.cboEmployeeName.FormattingEnabled = true;
            this.cboEmployeeName.Location = new System.Drawing.Point(138, 107);
            this.cboEmployeeName.Name = "cboEmployeeName";
            this.cboEmployeeName.Size = new System.Drawing.Size(200, 24);
            this.cboEmployeeName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "&Employee:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "&Task:";
            // 
            // cboTaskWorked
            // 
            this.cboTaskWorked.FormattingEnabled = true;
            this.cboTaskWorked.Location = new System.Drawing.Point(138, 146);
            this.cboTaskWorked.Name = "cboTaskWorked";
            this.cboTaskWorked.Size = new System.Drawing.Size(200, 24);
            this.cboTaskWorked.TabIndex = 3;
            this.cboTaskWorked.SelectedIndexChanged += new System.EventHandler(this.cboTaskWorked_SelectedIndexChanged);
            // 
            // dtpDateWorked
            // 
            this.dtpDateWorked.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateWorked.Location = new System.Drawing.Point(138, 188);
            this.dtpDateWorked.Name = "dtpDateWorked";
            this.dtpDateWorked.Size = new System.Drawing.Size(155, 22);
            this.dtpDateWorked.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "&Date:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(74, 231);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "&Hours:";
            // 
            // spnHoursWorked
            // 
            this.spnHoursWorked.DecimalPlaces = 2;
            this.spnHoursWorked.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.spnHoursWorked.Location = new System.Drawing.Point(138, 230);
            this.spnHoursWorked.Name = "spnHoursWorked";
            this.spnHoursWorked.Size = new System.Drawing.Size(70, 22);
            this.spnHoursWorked.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(79, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Work:";
            // 
            // lblRowsCounter
            // 
            this.lblRowsCounter.AutoSize = true;
            this.lblRowsCounter.Location = new System.Drawing.Point(135, 31);
            this.lblRowsCounter.Name = "lblRowsCounter";
            this.lblRowsCounter.Size = new System.Drawing.Size(38, 16);
            this.lblRowsCounter.TabIndex = 9;
            this.lblRowsCounter.Text = "0 of 0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(62, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 16);
            this.label7.TabIndex = 10;
            this.label7.Text = "Work ID:";
            // 
            // lblWorkID
            // 
            this.lblWorkID.AutoSize = true;
            this.lblWorkID.Location = new System.Drawing.Point(135, 71);
            this.lblWorkID.Name = "lblWorkID";
            this.lblWorkID.Size = new System.Drawing.Size(14, 16);
            this.lblWorkID.TabIndex = 11;
            this.lblWorkID.Text = "0";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(386, 44);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(93, 34);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "&Add";
            this.toolTip1.SetToolTip(this.btnAdd, "Add/Save New Work Submission");
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(386, 95);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(93, 34);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "&Update";
            this.toolTip1.SetToolTip(this.btnUpdate, "Update/Cancel Current Work Submission");
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(386, 146);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(93, 34);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "C&lose";
            this.toolTip1.SetToolTip(this.btnClose, "Close Window");
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(77, 293);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(61, 34);
            this.btnFirst.TabIndex = 15;
            this.btnFirst.Text = "|<";
            this.toolTip1.SetToolTip(this.btnFirst, "First");
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrior
            // 
            this.btnPrior.Location = new System.Drawing.Point(180, 293);
            this.btnPrior.Name = "btnPrior";
            this.btnPrior.Size = new System.Drawing.Size(61, 34);
            this.btnPrior.TabIndex = 16;
            this.btnPrior.Text = "<<";
            this.toolTip1.SetToolTip(this.btnPrior, "Prior");
            this.btnPrior.UseVisualStyleBackColor = true;
            this.btnPrior.Click += new System.EventHandler(this.btnPrior_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(283, 293);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(61, 34);
            this.btnNext.TabIndex = 17;
            this.btnNext.Text = ">>";
            this.toolTip1.SetToolTip(this.btnNext, "Next");
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(386, 293);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(61, 34);
            this.btnLast.TabIndex = 18;
            this.btnLast.Text = ">|";
            this.toolTip1.SetToolTip(this.btnLast, "Last");
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // WorkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 358);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrior);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblWorkID);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblRowsCounter);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.spnHoursWorked);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpDateWorked);
            this.Controls.Add(this.cboTaskWorked);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboEmployeeName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "WorkForm";
            this.Text = "Work Form";
            this.Load += new System.EventHandler(this.WorkForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spnHoursWorked)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboEmployeeName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboTaskWorked;
        private System.Windows.Forms.DateTimePicker dtpDateWorked;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown spnHoursWorked;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblRowsCounter;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblWorkID;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnPrior;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}