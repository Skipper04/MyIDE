namespace MyIDE
{
    partial class MainWindow
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.fileMainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newMainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.openMainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveMainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.buildMainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.runMainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.buildSolutionMainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tcResults = new System.Windows.Forms.TabControl();
            this.tabOutput = new System.Windows.Forms.TabPage();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.tabErrors = new System.Windows.Forms.TabPage();
            this.dgvErrors = new System.Windows.Forms.DataGridView();
            this.columnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnInitialIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabVariables = new System.Windows.Forms.TabPage();
            this.dgvVariables = new System.Windows.Forms.DataGridView();
            this.columnVariable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.rtbProgram = new System.Windows.Forms.RichTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.msMain.SuspendLayout();
            this.tcResults.SuspendLayout();
            this.tabOutput.SuspendLayout();
            this.tabErrors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrors)).BeginInit();
            this.tabVariables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVariables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMainMenu,
            this.buildMainMenu});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.msMain.Size = new System.Drawing.Size(584, 24);
            this.msMain.TabIndex = 0;
            this.msMain.Text = "Main menu";
            // 
            // fileMainMenu
            // 
            this.fileMainMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMainMenu,
            this.openMainMenu,
            this.toolStripSeparator1,
            this.saveMainMenu,
            this.toolStripSeparator2,
            this.exitMainMenu});
            this.fileMainMenu.Name = "fileMainMenu";
            this.fileMainMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMainMenu.Text = "&File";
            // 
            // newMainMenu
            // 
            this.newMainMenu.Name = "newMainMenu";
            this.newMainMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newMainMenu.Size = new System.Drawing.Size(146, 22);
            this.newMainMenu.Text = "&New";
            this.newMainMenu.Click += new System.EventHandler(this.OnFileNewMainMenuClick);
            // 
            // openMainMenu
            // 
            this.openMainMenu.Name = "openMainMenu";
            this.openMainMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openMainMenu.Size = new System.Drawing.Size(146, 22);
            this.openMainMenu.Text = "&Open";
            this.openMainMenu.Click += new System.EventHandler(this.OnOpenFileClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // saveMainMenu
            // 
            this.saveMainMenu.Name = "saveMainMenu";
            this.saveMainMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveMainMenu.Size = new System.Drawing.Size(146, 22);
            this.saveMainMenu.Text = "&Save";
            this.saveMainMenu.Click += new System.EventHandler(this.OnSaveMainMenuClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(143, 6);
            // 
            // exitMainMenu
            // 
            this.exitMainMenu.Name = "exitMainMenu";
            this.exitMainMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitMainMenu.Size = new System.Drawing.Size(146, 22);
            this.exitMainMenu.Text = "E&xit";
            this.exitMainMenu.Click += new System.EventHandler(this.OnExitMainMenuClick);
            // 
            // buildMainMenu
            // 
            this.buildMainMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runMainMenu,
            this.buildSolutionMainMenu});
            this.buildMainMenu.Name = "buildMainMenu";
            this.buildMainMenu.Size = new System.Drawing.Size(46, 20);
            this.buildMainMenu.Text = "&Build";
            // 
            // runMainMenu
            // 
            this.runMainMenu.Name = "runMainMenu";
            this.runMainMenu.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.runMainMenu.Size = new System.Drawing.Size(166, 22);
            this.runMainMenu.Text = "&Run";
            this.runMainMenu.Click += new System.EventHandler(this.OnRunMainMenuClick);
            // 
            // buildSolutionMainMenu
            // 
            this.buildSolutionMainMenu.Name = "buildSolutionMainMenu";
            this.buildSolutionMainMenu.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.buildSolutionMainMenu.Size = new System.Drawing.Size(166, 22);
            this.buildSolutionMainMenu.Text = "&Build solution";
            this.buildSolutionMainMenu.Click += new System.EventHandler(this.OnBuildSolutionMainMenuClick);
            // 
            // tcResults
            // 
            this.tcResults.Controls.Add(this.tabOutput);
            this.tcResults.Controls.Add(this.tabErrors);
            this.tcResults.Controls.Add(this.tabVariables);
            this.tcResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tcResults.Location = new System.Drawing.Point(0, 0);
            this.tcResults.Margin = new System.Windows.Forms.Padding(4);
            this.tcResults.Name = "tcResults";
            this.tcResults.SelectedIndex = 0;
            this.tcResults.Size = new System.Drawing.Size(584, 214);
            this.tcResults.TabIndex = 1;
            // 
            // tabOutput
            // 
            this.tabOutput.Controls.Add(this.rtbOutput);
            this.tabOutput.Location = new System.Drawing.Point(4, 24);
            this.tabOutput.Margin = new System.Windows.Forms.Padding(4);
            this.tabOutput.Name = "tabOutput";
            this.tabOutput.Padding = new System.Windows.Forms.Padding(4);
            this.tabOutput.Size = new System.Drawing.Size(576, 186);
            this.tabOutput.TabIndex = 0;
            this.tabOutput.Text = "Output";
            this.tabOutput.UseVisualStyleBackColor = true;
            // 
            // rtbOutput
            // 
            this.rtbOutput.BackColor = System.Drawing.SystemColors.Window;
            this.rtbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOutput.Location = new System.Drawing.Point(4, 4);
            this.rtbOutput.Margin = new System.Windows.Forms.Padding(4);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.ReadOnly = true;
            this.rtbOutput.Size = new System.Drawing.Size(568, 178);
            this.rtbOutput.TabIndex = 0;
            this.rtbOutput.Text = "";
            // 
            // tabErrors
            // 
            this.tabErrors.Controls.Add(this.dgvErrors);
            this.tabErrors.Location = new System.Drawing.Point(4, 24);
            this.tabErrors.Margin = new System.Windows.Forms.Padding(4);
            this.tabErrors.Name = "tabErrors";
            this.tabErrors.Padding = new System.Windows.Forms.Padding(4);
            this.tabErrors.Size = new System.Drawing.Size(576, 186);
            this.tabErrors.TabIndex = 1;
            this.tabErrors.Text = "Errors";
            this.tabErrors.UseVisualStyleBackColor = true;
            // 
            // dgvErrors
            // 
            this.dgvErrors.AllowUserToAddRows = false;
            this.dgvErrors.AllowUserToDeleteRows = false;
            this.dgvErrors.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvErrors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnDescription,
            this.columnLine,
            this.columnColumn,
            this.columnInitialIndex,
            this.columnLength});
            this.dgvErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvErrors.Location = new System.Drawing.Point(4, 4);
            this.dgvErrors.Margin = new System.Windows.Forms.Padding(4);
            this.dgvErrors.Name = "dgvErrors";
            this.dgvErrors.ReadOnly = true;
            this.dgvErrors.RowHeadersVisible = false;
            this.dgvErrors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvErrors.Size = new System.Drawing.Size(568, 178);
            this.dgvErrors.TabIndex = 0;
            this.dgvErrors.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvErrors_MouseDoubleClick);
            // 
            // columnDescription
            // 
            this.columnDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnDescription.FillWeight = 200F;
            this.columnDescription.HeaderText = "Description";
            this.columnDescription.Name = "columnDescription";
            this.columnDescription.ReadOnly = true;
            this.columnDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnLine
            // 
            this.columnLine.HeaderText = "Line";
            this.columnLine.Name = "columnLine";
            this.columnLine.ReadOnly = true;
            this.columnLine.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnColumn
            // 
            this.columnColumn.HeaderText = "Column";
            this.columnColumn.Name = "columnColumn";
            this.columnColumn.ReadOnly = true;
            this.columnColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.columnColumn.Width = 88;
            // 
            // columnInitialIndex
            // 
            this.columnInitialIndex.HeaderText = "StartIndex";
            this.columnInitialIndex.Name = "columnInitialIndex";
            this.columnInitialIndex.ReadOnly = true;
            this.columnInitialIndex.Visible = false;
            // 
            // columnLength
            // 
            this.columnLength.HeaderText = "Length";
            this.columnLength.Name = "columnLength";
            this.columnLength.ReadOnly = true;
            this.columnLength.Visible = false;
            // 
            // tabVariables
            // 
            this.tabVariables.Controls.Add(this.dgvVariables);
            this.tabVariables.Location = new System.Drawing.Point(4, 24);
            this.tabVariables.Margin = new System.Windows.Forms.Padding(4);
            this.tabVariables.Name = "tabVariables";
            this.tabVariables.Padding = new System.Windows.Forms.Padding(4);
            this.tabVariables.Size = new System.Drawing.Size(576, 186);
            this.tabVariables.TabIndex = 2;
            this.tabVariables.Text = "Variables";
            this.tabVariables.UseVisualStyleBackColor = true;
            // 
            // dgvVariables
            // 
            this.dgvVariables.AllowUserToAddRows = false;
            this.dgvVariables.AllowUserToDeleteRows = false;
            this.dgvVariables.AllowUserToResizeColumns = false;
            this.dgvVariables.AllowUserToResizeRows = false;
            this.dgvVariables.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVariables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnVariable,
            this.columnValue});
            this.dgvVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVariables.Location = new System.Drawing.Point(4, 4);
            this.dgvVariables.Margin = new System.Windows.Forms.Padding(4);
            this.dgvVariables.MultiSelect = false;
            this.dgvVariables.Name = "dgvVariables";
            this.dgvVariables.ReadOnly = true;
            this.dgvVariables.RowHeadersVisible = false;
            this.dgvVariables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVariables.Size = new System.Drawing.Size(568, 178);
            this.dgvVariables.TabIndex = 0;
            // 
            // columnVariable
            // 
            this.columnVariable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnVariable.HeaderText = "Variable";
            this.columnVariable.Name = "columnVariable";
            this.columnVariable.ReadOnly = true;
            this.columnVariable.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnValue
            // 
            this.columnValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnValue.HeaderText = "Value";
            this.columnValue.Name = "columnValue";
            this.columnValue.ReadOnly = true;
            this.columnValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.rtbProgram);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tcResults);
            this.splitContainer.Size = new System.Drawing.Size(584, 438);
            this.splitContainer.SplitterDistance = 219;
            this.splitContainer.SplitterWidth = 5;
            this.splitContainer.TabIndex = 3;
            this.splitContainer.TabStop = false;
            // 
            // rtbProgram
            // 
            this.rtbProgram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbProgram.EnableAutoDragDrop = true;
            this.rtbProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbProgram.Location = new System.Drawing.Point(0, 0);
            this.rtbProgram.Margin = new System.Windows.Forms.Padding(4);
            this.rtbProgram.Name = "rtbProgram";
            this.rtbProgram.Size = new System.Drawing.Size(584, 219);
            this.rtbProgram.TabIndex = 0;
            this.rtbProgram.Text = "";
            this.rtbProgram.TextChanged += new System.EventHandler(this.OnRtbProgramTextChanged);
            this.rtbProgram.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbProgram_KeyDown);
            this.rtbProgram.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtbProgram_KeyPress);
            this.rtbProgram.KeyUp += new System.Windows.Forms.KeyEventHandler(this.rtbProgram_KeyUp);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.msMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMenuStrip = this.msMain;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainWindow";
            this.Text = "My IDE";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tcResults.ResumeLayout(false);
            this.tabOutput.ResumeLayout(false);
            this.tabErrors.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrors)).EndInit();
            this.tabVariables.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVariables)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem fileMainMenu;
        private System.Windows.Forms.ToolStripMenuItem newMainMenu;
        private System.Windows.Forms.ToolStripMenuItem openMainMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveMainMenu;
        private System.Windows.Forms.ToolStripMenuItem exitMainMenu;
        private System.Windows.Forms.ToolStripMenuItem buildMainMenu;
        private System.Windows.Forms.ToolStripMenuItem runMainMenu;
        private System.Windows.Forms.ToolStripMenuItem buildSolutionMainMenu;
        private System.Windows.Forms.TabControl tcResults;
        private System.Windows.Forms.TabPage tabOutput;
        private System.Windows.Forms.TabPage tabErrors;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.RichTextBox rtbProgram;
        private System.Windows.Forms.TabPage tabVariables;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView dgvErrors;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.DataGridView dgvVariables;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnVariable;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnInitialIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnLength;
        private System.Windows.Forms.RichTextBox rtbOutput;
    }
}

