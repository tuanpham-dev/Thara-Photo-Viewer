namespace TharaStudio.PhotoViewer {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.mainToolStrip = new System.Windows.Forms.ToolStrip();
			this.toolFile = new System.Windows.Forms.ToolStripSplitButton();
			this.toolOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.toolCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.toolDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolOpenFitToWin = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.toolExit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.toolFullscreen = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolActualSize = new System.Windows.Forms.ToolStripButton();
			this.toolZoomIn = new System.Windows.Forms.ToolStripButton();
			this.toolZoomOut = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.toolRotateCounterClockWise = new System.Windows.Forms.ToolStripButton();
			this.toolRotateClockWise = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.toolNextImage = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.toolPrevImage = new System.Windows.Forms.ToolStripButton();
			this.toolDragCopy = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
			this.toolPhotoshop = new System.Windows.Forms.ToolStripButton();
			this.panelMain = new System.Windows.Forms.Panel();
			this.labelNoImageMessage = new System.Windows.Forms.Label();
			this.labelErrorMessage = new System.Windows.Forms.Label();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.contextOpenWith = new System.Windows.Forms.ToolStripMenuItem();
			this.contextPhotoshop = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.contextOpenExplorer = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.contextCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.contextDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
			this.contextFileProperties = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.timerScroll = new System.Windows.Forms.Timer(this.components);
			this.mainToolStrip.SuspendLayout();
			this.panelMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.contextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainToolStrip
			// 
			this.mainToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolFile,
			this.toolStripSeparator7,
			this.toolFullscreen,
			this.toolStripSeparator3,
			this.toolActualSize,
			this.toolZoomIn,
			this.toolZoomOut,
			this.toolStripSeparator4,
			this.toolRotateCounterClockWise,
			this.toolRotateClockWise,
			this.toolStripSeparator5,
			this.toolNextImage,
			this.toolStripSeparator6,
			this.toolPrevImage,
			this.toolDragCopy,
			this.toolStripSeparator12,
			this.toolPhotoshop});
			this.mainToolStrip.Location = new System.Drawing.Point(0, 536);
			this.mainToolStrip.Name = "mainToolStrip";
			this.mainToolStrip.Size = new System.Drawing.Size(784, 25);
			this.mainToolStrip.TabIndex = 0;
			// 
			// toolFile
			// 
			this.toolFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolOpen,
			this.toolSaveAs,
			this.toolStripSeparator8,
			this.toolCopy,
			this.toolDelete,
			this.toolStripSeparator1,
			this.toolOpenFitToWin,
			this.toolStripSeparator2,
			this.toolHelp,
			this.toolExit});
			this.toolFile.Image = global::TharaStudio.PhotoViewer.Properties.Resources.file;
			this.toolFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolFile.Name = "toolFile";
			this.toolFile.Size = new System.Drawing.Size(32, 22);
			this.toolFile.ButtonClick += new System.EventHandler(this.toolFile_ButtonClick);
			// 
			// toolOpen
			// 
			this.toolOpen.Image = global::TharaStudio.PhotoViewer.Properties.Resources.open;
			this.toolOpen.Name = "toolOpen";
			this.toolOpen.ShortcutKeyDisplayString = "O";
			this.toolOpen.Size = new System.Drawing.Size(193, 22);
			this.toolOpen.Text = "Open";
			this.toolOpen.Click += new System.EventHandler(this.toolOpen_Click);
			// 
			// toolSaveAs
			// 
			this.toolSaveAs.Enabled = false;
			this.toolSaveAs.Image = global::TharaStudio.PhotoViewer.Properties.Resources.save;
			this.toolSaveAs.Name = "toolSaveAs";
			this.toolSaveAs.ShortcutKeyDisplayString = "Ctrl + S";
			this.toolSaveAs.Size = new System.Drawing.Size(193, 22);
			this.toolSaveAs.Text = "Save as...";
			this.toolSaveAs.Click += new System.EventHandler(this.toolSaveAs_Click);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(190, 6);
			// 
			// toolCopy
			// 
			this.toolCopy.Enabled = false;
			this.toolCopy.Image = global::TharaStudio.PhotoViewer.Properties.Resources.copy;
			this.toolCopy.Name = "toolCopy";
			this.toolCopy.ShortcutKeyDisplayString = "Ctrl + C";
			this.toolCopy.Size = new System.Drawing.Size(193, 22);
			this.toolCopy.Text = "Copy";
			this.toolCopy.Click += new System.EventHandler(this.toolCopy_Click);
			// 
			// toolDelete
			// 
			this.toolDelete.Enabled = false;
			this.toolDelete.Image = global::TharaStudio.PhotoViewer.Properties.Resources.delete;
			this.toolDelete.Name = "toolDelete";
			this.toolDelete.ShortcutKeyDisplayString = "Del";
			this.toolDelete.Size = new System.Drawing.Size(193, 22);
			this.toolDelete.Text = "Delete";
			this.toolDelete.Click += new System.EventHandler(this.toolDelete_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(190, 6);
			// 
			// toolOpenFitToWin
			// 
			this.toolOpenFitToWin.CheckOnClick = true;
			this.toolOpenFitToWin.Image = global::TharaStudio.PhotoViewer.Properties.Resources.fit_to_win;
			this.toolOpenFitToWin.Name = "toolOpenFitToWin";
			this.toolOpenFitToWin.ShortcutKeyDisplayString = "F";
			this.toolOpenFitToWin.Size = new System.Drawing.Size(193, 22);
			this.toolOpenFitToWin.Text = "Open Fit to Window";
			this.toolOpenFitToWin.CheckedChanged += new System.EventHandler(this.toolOpenFitToWin_CheckedChanged);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(190, 6);
			// 
			// toolHelp
			// 
			this.toolHelp.Image = global::TharaStudio.PhotoViewer.Properties.Resources.help;
			this.toolHelp.Name = "toolHelp";
			this.toolHelp.ShortcutKeyDisplayString = "H";
			this.toolHelp.Size = new System.Drawing.Size(193, 22);
			this.toolHelp.Text = "Help";
			this.toolHelp.Click += new System.EventHandler(this.toolHelp_Click);
			// 
			// toolExit
			// 
			this.toolExit.Image = global::TharaStudio.PhotoViewer.Properties.Resources.exit;
			this.toolExit.Name = "toolExit";
			this.toolExit.ShortcutKeyDisplayString = "Esc";
			this.toolExit.Size = new System.Drawing.Size(193, 22);
			this.toolExit.Text = "Exit";
			this.toolExit.Click += new System.EventHandler(this.toolExit_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
			// 
			// toolFullscreen
			// 
			this.toolFullscreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolFullscreen.Image = global::TharaStudio.PhotoViewer.Properties.Resources.fullscreen;
			this.toolFullscreen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolFullscreen.Name = "toolFullscreen";
			this.toolFullscreen.Size = new System.Drawing.Size(23, 22);
			this.toolFullscreen.Click += new System.EventHandler(this.toolFullscreen_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// toolActualSize
			// 
			this.toolActualSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolActualSize.Enabled = false;
			this.toolActualSize.Image = global::TharaStudio.PhotoViewer.Properties.Resources.actual_size;
			this.toolActualSize.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolActualSize.Name = "toolActualSize";
			this.toolActualSize.Size = new System.Drawing.Size(23, 22);
			this.toolActualSize.Click += new System.EventHandler(this.toolActualSize_Click);
			// 
			// toolZoomIn
			// 
			this.toolZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolZoomIn.Enabled = false;
			this.toolZoomIn.Image = global::TharaStudio.PhotoViewer.Properties.Resources.zoom_in;
			this.toolZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolZoomIn.Name = "toolZoomIn";
			this.toolZoomIn.Size = new System.Drawing.Size(23, 22);
			this.toolZoomIn.Click += new System.EventHandler(this.toolZoomIn_Click);
			// 
			// toolZoomOut
			// 
			this.toolZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolZoomOut.Enabled = false;
			this.toolZoomOut.Image = global::TharaStudio.PhotoViewer.Properties.Resources.zoom_out;
			this.toolZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolZoomOut.Name = "toolZoomOut";
			this.toolZoomOut.Size = new System.Drawing.Size(23, 22);
			this.toolZoomOut.Click += new System.EventHandler(this.toolZoomOut_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// toolRotateCounterClockWise
			// 
			this.toolRotateCounterClockWise.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolRotateCounterClockWise.Enabled = false;
			this.toolRotateCounterClockWise.Image = global::TharaStudio.PhotoViewer.Properties.Resources.rotate_counterclockwise;
			this.toolRotateCounterClockWise.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolRotateCounterClockWise.Name = "toolRotateCounterClockWise";
			this.toolRotateCounterClockWise.Size = new System.Drawing.Size(23, 22);
			this.toolRotateCounterClockWise.Click += new System.EventHandler(this.toolRotateCounterClockWise_Click);
			// 
			// toolRotateClockWise
			// 
			this.toolRotateClockWise.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolRotateClockWise.Enabled = false;
			this.toolRotateClockWise.Image = global::TharaStudio.PhotoViewer.Properties.Resources.rotate_clockwise;
			this.toolRotateClockWise.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolRotateClockWise.Name = "toolRotateClockWise";
			this.toolRotateClockWise.Size = new System.Drawing.Size(23, 22);
			this.toolRotateClockWise.Click += new System.EventHandler(this.toolRotateClockWise_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			// 
			// toolNextImage
			// 
			this.toolNextImage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolNextImage.AutoToolTip = false;
			this.toolNextImage.Enabled = false;
			this.toolNextImage.Image = global::TharaStudio.PhotoViewer.Properties.Resources.next;
			this.toolNextImage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolNextImage.Name = "toolNextImage";
			this.toolNextImage.Size = new System.Drawing.Size(51, 22);
			this.toolNextImage.Text = "Next";
			this.toolNextImage.Click += new System.EventHandler(this.toolNextImage_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
			// 
			// toolPrevImage
			// 
			this.toolPrevImage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolPrevImage.AutoToolTip = false;
			this.toolPrevImage.Enabled = false;
			this.toolPrevImage.Image = global::TharaStudio.PhotoViewer.Properties.Resources.prev;
			this.toolPrevImage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolPrevImage.Name = "toolPrevImage";
			this.toolPrevImage.Size = new System.Drawing.Size(72, 22);
			this.toolPrevImage.Text = "Previous";
			this.toolPrevImage.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.toolPrevImage.Click += new System.EventHandler(this.toolPrevImage_Click);
			// 
			// toolDragCopy
			// 
			this.toolDragCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolDragCopy.Enabled = false;
			this.toolDragCopy.Image = ((System.Drawing.Image)(resources.GetObject("toolDragCopy.Image")));
			this.toolDragCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolDragCopy.Name = "toolDragCopy";
			this.toolDragCopy.Size = new System.Drawing.Size(23, 22);
			this.toolDragCopy.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolDragCopy_MouseDown);
			// 
			// toolStripSeparator12
			// 
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
			// 
			// toolPhotoshop
			// 
			this.toolPhotoshop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolPhotoshop.Image = global::TharaStudio.PhotoViewer.Properties.Resources.photoshop;
			this.toolPhotoshop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolPhotoshop.Name = "toolPhotoshop";
			this.toolPhotoshop.Size = new System.Drawing.Size(23, 22);
			this.toolPhotoshop.Visible = false;
			this.toolPhotoshop.Click += new System.EventHandler(this.toolPhotoshop_Click);
			this.toolPhotoshop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolPhotoshop_MouseDown);
			// 
			// panelMain
			// 
			this.panelMain.Controls.Add(this.labelNoImageMessage);
			this.panelMain.Controls.Add(this.labelErrorMessage);
			this.panelMain.Controls.Add(this.pictureBox);
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(784, 536);
			this.panelMain.TabIndex = 1;
			this.panelMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMain_MouseDown);
			// 
			// labelNoImageMessage
			// 
			this.labelNoImageMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelNoImageMessage.Location = new System.Drawing.Point(0, 0);
			this.labelNoImageMessage.Name = "labelNoImageMessage";
			this.labelNoImageMessage.Size = new System.Drawing.Size(784, 536);
			this.labelNoImageMessage.TabIndex = 2;
			this.labelNoImageMessage.Text = "No image found in the folder.";
			this.labelNoImageMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labelNoImageMessage.Visible = false;
			// 
			// labelErrorMessage
			// 
			this.labelErrorMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelErrorMessage.Location = new System.Drawing.Point(0, 0);
			this.labelErrorMessage.Name = "labelErrorMessage";
			this.labelErrorMessage.Size = new System.Drawing.Size(784, 536);
			this.labelErrorMessage.TabIndex = 1;
			this.labelErrorMessage.Text = "Thara Photo Viewer can\'t open this picture because file appears to be damanged, c" +
	"orrupted, or is too large.";
			this.labelErrorMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labelErrorMessage.Visible = false;
			// 
			// pictureBox
			// 
			this.pictureBox.ContextMenuStrip = this.contextMenu;
			this.pictureBox.Location = new System.Drawing.Point(0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(0, 0);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.pictureBox.Visible = false;
			this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
			this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// contextMenu
			// 
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.contextOpenWith,
			this.contextPhotoshop,
			this.toolStripSeparator9,
			this.contextOpenExplorer,
			this.toolStripSeparator10,
			this.contextCopy,
			this.contextDelete,
			this.toolStripSeparator11,
			this.contextFileProperties});
			this.contextMenu.Name = "contextMenu";
			this.contextMenu.Size = new System.Drawing.Size(195, 154);
			// 
			// contextOpenWith
			// 
			this.contextOpenWith.Image = global::TharaStudio.PhotoViewer.Properties.Resources.open_with;
			this.contextOpenWith.Name = "contextOpenWith";
			this.contextOpenWith.Size = new System.Drawing.Size(194, 22);
			this.contextOpenWith.Text = "Open With...";
			this.contextOpenWith.Click += new System.EventHandler(this.contextOpenWith_Click);
			// 
			// contextPhotoshop
			// 
			this.contextPhotoshop.Image = global::TharaStudio.PhotoViewer.Properties.Resources.photoshop;
			this.contextPhotoshop.Name = "contextPhotoshop";
			this.contextPhotoshop.ShortcutKeyDisplayString = "Ctrl + E";
			this.contextPhotoshop.Size = new System.Drawing.Size(194, 22);
			this.contextPhotoshop.Text = "Open PSD File";
			this.contextPhotoshop.Visible = false;
			this.contextPhotoshop.Click += new System.EventHandler(this.contextPhotoshop_Click);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(191, 6);
			// 
			// contextOpenExplorer
			// 
			this.contextOpenExplorer.Image = global::TharaStudio.PhotoViewer.Properties.Resources.open_explorer;
			this.contextOpenExplorer.Name = "contextOpenExplorer";
			this.contextOpenExplorer.ShortcutKeyDisplayString = "F8";
			this.contextOpenExplorer.Size = new System.Drawing.Size(194, 22);
			this.contextOpenExplorer.Text = "Open File Location";
			this.contextOpenExplorer.Click += new System.EventHandler(this.contextOpenExplorer_Click);
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(191, 6);
			// 
			// contextCopy
			// 
			this.contextCopy.Image = global::TharaStudio.PhotoViewer.Properties.Resources.copy;
			this.contextCopy.Name = "contextCopy";
			this.contextCopy.ShortcutKeyDisplayString = "Ctrl + C";
			this.contextCopy.Size = new System.Drawing.Size(194, 22);
			this.contextCopy.Text = "Copy";
			this.contextCopy.Click += new System.EventHandler(this.contextCopy_Click);
			// 
			// contextDelete
			// 
			this.contextDelete.Image = global::TharaStudio.PhotoViewer.Properties.Resources.delete;
			this.contextDelete.Name = "contextDelete";
			this.contextDelete.ShortcutKeyDisplayString = "Del";
			this.contextDelete.Size = new System.Drawing.Size(194, 22);
			this.contextDelete.Text = "Delete";
			this.contextDelete.Click += new System.EventHandler(this.contextDelete_Click);
			// 
			// toolStripSeparator11
			// 
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(191, 6);
			// 
			// contextFileProperties
			// 
			this.contextFileProperties.Image = global::TharaStudio.PhotoViewer.Properties.Resources.file_properties;
			this.contextFileProperties.Name = "contextFileProperties";
			this.contextFileProperties.Size = new System.Drawing.Size(194, 22);
			this.contextFileProperties.Text = "File Properties";
			this.contextFileProperties.Click += new System.EventHandler(this.contextFileProperties_Click);
			// 
			// timerScroll
			// 
			this.timerScroll.Tick += new System.EventHandler(this.timerScroll_Tick);
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
			this.ClientSize = new System.Drawing.Size(784, 561);
			this.Controls.Add(this.panelMain);
			this.Controls.Add(this.mainToolStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
			this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainForm_PreviewKeyDown);
			this.mainToolStrip.ResumeLayout(false);
			this.mainToolStrip.PerformLayout();
			this.panelMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.contextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip mainToolStrip;
		private System.Windows.Forms.ToolStripSplitButton toolFile;
		private System.Windows.Forms.ToolStripMenuItem toolOpen;
		private System.Windows.Forms.ToolStripMenuItem toolSaveAs;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolOpenFitToWin;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem toolHelp;
		private System.Windows.Forms.ToolStripMenuItem toolExit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton toolActualSize;
		private System.Windows.Forms.ToolStripButton toolZoomIn;
		private System.Windows.Forms.ToolStripButton toolZoomOut;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton toolRotateCounterClockWise;
		private System.Windows.Forms.ToolStripButton toolRotateClockWise;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripButton toolNextImage;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripButton toolPrevImage;
		private System.Windows.Forms.Panel panelMain;
		private System.Windows.Forms.Label labelErrorMessage;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.Timer timerScroll;
		private System.Windows.Forms.ToolStripButton toolFullscreen;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripMenuItem toolCopy;
		private System.Windows.Forms.ToolStripMenuItem toolDelete;
		private System.Windows.Forms.ContextMenuStrip contextMenu;
		private System.Windows.Forms.ToolStripMenuItem contextOpenWith;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		private System.Windows.Forms.ToolStripMenuItem contextOpenExplorer;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
		private System.Windows.Forms.ToolStripMenuItem contextCopy;
		private System.Windows.Forms.ToolStripMenuItem contextDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
		private System.Windows.Forms.ToolStripMenuItem contextFileProperties;
		private System.Windows.Forms.ToolStripButton toolDragCopy;
		private System.Windows.Forms.Label labelNoImageMessage;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
		private System.Windows.Forms.ToolStripButton toolPhotoshop;
		private System.Windows.Forms.ToolStripMenuItem contextPhotoshop;
	}
}