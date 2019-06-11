using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TharaStudio.PhotoViewer {
	public partial class MainForm : Form {
		private string appTitle = "Thara Photo Viewer";

		// application settings
		private Settings settings;
		private bool componentLoaded = false;

		// window state for fullscreen function.
		private bool windowMaximized;

		// open image
		private string currentFile;
		private string currentDirectory;
		private List<string> images;
		private int currentFileIndex = -1;

		// scroll picture box
		private Point mouseLastPosition;
		private int scrollOffsetX;
		private int scrollOffsetY;

		// zoom image
		enum Zoom { NoZoom, NoZoomTop, NoZoomBottom, ActualSize, In, Out, FitToWin };
		enum ViewMode { FitWidth, FitHeight, ActualSize };
		private float zoomScale = 1;
		private List<float> zoomLevels;

		public MainForm() {
			InitializeComponent();

			// subscribe picture box mousewheel event.
			pictureBox.MouseWheel += pictureBox_MouseWheel;
			// set picture box hand grab cursor.
			pictureBox.Cursor = new Cursor(Properties.Resources.grab.Handle);

			Text = appTitle;

			// load settings.
			settings = Settings.LoadSettings();

			// apply settings
			Width = settings.Width;
			Height = settings.Height;

			if (settings.X != -88 || settings.Y != -88) {
				StartPosition = FormStartPosition.Manual;
				Location = new Point(settings.X, settings.Y);
			}

			WindowState = settings.Maximized ? FormWindowState.Maximized : FormWindowState.Normal;
			windowMaximized = settings.Maximized;

			toolOpenFitToWin.Checked = settings.OpenFitToWindow;
		}

		private void MainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
			// handle hotkey.
			if (!e.Alt && !e.Control && !e.Shift) {
				switch (e.KeyCode) {
					case Keys.F:
					case Keys.Space:
						// toggle open fit to window option.
						toolOpenFitToWin.Checked = !toolOpenFitToWin.Checked;
						break;

					case Keys.H:
						// show help.
						showHelp();
						break;

					case Keys.Escape:
						if (FormBorderStyle == FormBorderStyle.None) {
							// exit fullscreen.
							toggleFullScreen();
						} else {
							// exit application.
							exit();
						}

						break;

					case Keys.F11:
						// toggle fullscreen.
						toggleFullScreen();
						break;

					case Keys.O:
						// open image.
						openImage();
						break;

					case Keys.F5:
						// reload image.
						reloadImage();
						break;

					case Keys.Right:
					case Keys.NumPad6:
					case Keys.BrowserBack:
						// next image.
						navigateImage();
						break;

					case Keys.Left:
					case Keys.NumPad4:
					case Keys.BrowserForward:
						// previous image.
						navigateImage(false);
						break;

					case Keys.Home:
						// first image.
						navigateFirstImage();
						break;

					case Keys.End:
						// last image.
						navigateLastImage();
						break;

					case Keys.Up:
					case Keys.NumPad8:
						// scoll up.
						repositionPictureBox(0, 100);
						break;

					case Keys.Down:
					case Keys.NumPad2:
						// scroll down.
						repositionPictureBox(0, -100);
						break;

					case Keys.OemMinus:
					case Keys.Subtract:
						// zoom out.
						zoomImage(Zoom.Out, true);
						break;

					case Keys.Oemplus:
					case Keys.Add:
						// zoom in.
						zoomImage(Zoom.In, true);
						break;

					case Keys.D0:
						// actual size.
						zoomImage(Zoom.ActualSize, true);
						break;

					case Keys.Enter:
						// actual size.
						zoomImage(Zoom.ActualSize);
						break;

					case Keys.PageUp:
						// scroll top top.
						zoomImage(Zoom.NoZoomTop);
						break;

					case Keys.PageDown:
						// scroll to bottom.
						zoomImage(Zoom.NoZoomBottom);
						break;

					case Keys.F1:
						// associate image files.
						FileAssociation.registerToDefaultPrograms();
						break;

					case Keys.Delete:
						// move image to trash.
						deleteImage();
						break;

					case Keys.F8:
						// open image in explorer.
						openImageLocation();
						break;
				}
			} else if (!e.Alt && e.Control && !e.Shift) {
				switch (e.KeyCode) {
					case Keys.O:
						openImage();
						break;

					case Keys.S:
						saveImage();
						break;

					case Keys.D0:
						// actual size.
						zoomImage(Zoom.ActualSize, true);
						break;

					case Keys.Up:
					case Keys.NumPad8:
					case Keys.Oemplus:
					case Keys.Add:
						// zoom in.
						zoomImage(Zoom.In, true);
						break;

					case Keys.Down:
					case Keys.NumPad2:
					case Keys.OemMinus:
					case Keys.Subtract:
						// zoom out.
						zoomImage(Zoom.Out, true);
						break;

					case Keys.C:
						// copy to clipboard.
						copyToClipboard();
						break;

					case Keys.V:
						// open from clipboard.
						openFromClipBoard();
						break;

					case Keys.E:
						// open photoshop.
						openPhotoshop();
						break;

					case Keys.P:
						// show file properties.
						showImageProperties();
						break;
					case Keys.L:
						// open in explorer.
						openImageLocation();
						break;
				}
			} else if (!e.Alt && !e.Control && e.Shift) {
				switch (e.KeyCode) {
					case Keys.Up:
					case Keys.NumPad8:
						// scroll left.
						repositionPictureBox(100, 0);
						break;

					case Keys.Down:
					case Keys.NumPad2:
						// scroll right.
						repositionPictureBox(-100, 0);
						break;
				}
			} else if (!e.Alt && e.Control && e.Shift) {
				switch (e.KeyCode) {
					case Keys.Right:
					case Keys.NumPad6:
						// rotate clockwise image.
						rotate();
						break;

					case Keys.Left:
					case Keys.NumPad4:
						// rotate counterclockwise image.
						rotate(false);
						break;
				}
			} else if (e.Alt && !e.Control && !e.Shift) {
				switch (e.KeyCode) {
					case Keys.Enter:
						toggleFullScreen();
						break;
				}
			}
		}

		private void MainForm_Load(object sender, EventArgs e) {
			// support open image that is set in arguments.
			var args = Environment.GetCommandLineArgs();

			if (args.Length > 1) {
				var filePath = args[args.Length - 1];

				if (File.Exists(filePath)) {
					loadImage(filePath);
				}
			}

			// prevent changing settings before applying.
			componentLoaded = true;
		}

		private void MainForm_SizeChanged(object sender, EventArgs e) {
			if (WindowState != FormWindowState.Minimized) {
				try {
					var image = pictureBox.Image;
	
					// recalculate zoom levels
					recalculateZoomLevels();
	
					zoomImage(settings.OpenFitToWindow ? Zoom.FitToWin : Zoom.NoZoom);
				} catch (Exception) { }
			}

			// save size, location and state settings.
			if (componentLoaded) {
				if (WindowState == FormWindowState.Maximized) {
					settings.Maximized = true;
				} else {
					settings.Maximized = false;

					if (WindowState != FormWindowState.Minimized) {
						settings.Width = Width;
						settings.Height = Height;
						settings.X = Location.X;
						settings.Y = Location.Y;
					}
				}
			}
		}

		private void toolOpenFitToWin_CheckedChanged(object sender, EventArgs e) {
			// save open fit to window setting.
			settings.OpenFitToWindow = toolOpenFitToWin.Checked;

			zoomImage(settings.OpenFitToWindow ? Zoom.FitToWin : Zoom.ActualSize);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
			// save settings.
			settings.SaveSettings();
		}

		#region Show Help Message
		private void showHelp() {
			MessageBox.Show("Thara Photo Viewer v1.0.0 made with love by Tuan Pham" + Environment.NewLine
				+ "*********************************************************************" + Environment.NewLine + Environment.NewLine
				+ "Usage:" + Environment.NewLine
				+ "    o, ctrl + o: Open image" + Environment.NewLine
				+ "    ctrl + s: Save image" + Environment.NewLine
				+ "-----------------------------------------------------------" + Environment.NewLine
				+ "    ←: Previous image" + Environment.NewLine
				+ "    →: Next image" + Environment.NewLine
				+ "-----------------------------------------------------------" + Environment.NewLine
				+ "    +, ctrl + mouse wheel up: Zoom in" + Environment.NewLine
				+ "    -, ctrl + mouse wheel down: Zoom out" + Environment.NewLine
				+ "    0, enter: Reset zoom" + Environment.NewLine
				+ "    f, space: Change zoom mode (fit to window or actual size)" + Environment.NewLine
				+ "-----------------------------------------------------------" + Environment.NewLine
				+ "    middle mouse, left mouse: Scroll Image" + Environment.NewLine
				+ "    ↑, mouse wheel up: Scroll Up" + Environment.NewLine
				+ "    ↓, mouse wheel down: Scroll Down" + Environment.NewLine
				+ "    shift + ↑, shift + mouse wheel up: Scroll Left" + Environment.NewLine
				+ "    shift + ↓, shift + mouse wheel down: Scroll Right" + Environment.NewLine
				+ "-----------------------------------------------------------" + Environment.NewLine
				+ "    ctrl + c: Copy image" + Environment.NewLine
				+ "    delete: Move image to trash" + Environment.NewLine
				+ "    f8, ctrl + l: Open image in explorer" + Environment.NewLine
				+ "    ctrl + p: Show image properties" + Environment.NewLine
				+ "    ctrl + e: Open psd file (same name as image) with Photoshop" + Environment.NewLine
				+ "-----------------------------------------------------------" + Environment.NewLine
				+ "    f11, alt + enter: Toggle Fullscreen" + Environment.NewLine
				+ "    escape: Exit Fullscreen/Exit" + Environment.NewLine
				+ "    f1: Register to default app Photo Viewer (run as administrator)" + Environment.NewLine
				+ "-----------------------------------------------------------" + Environment.NewLine
			, appTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void toolHelp_Click(object sender, EventArgs e) {
			showHelp();
		}
		#endregion

		#region Exit Application
		private void exit() {
			Application.Exit();
		}

		private void toolExit_Click(object sender, EventArgs e) {
			exit();
		}
		#endregion

		#region FullScreen
		private void toggleFullScreen() {
			if (mainToolStrip.Visible == false) {
				mainToolStrip.Visible = true;
				FormBorderStyle = FormBorderStyle.Sizable;
				TopMost = false;
				WindowState = windowMaximized ? FormWindowState.Maximized : FormWindowState.Normal;
			} else {
				windowMaximized = WindowState == FormWindowState.Maximized;
				mainToolStrip.Visible = false;
				FormBorderStyle = FormBorderStyle.None;
				TopMost = true;
				WindowState = FormWindowState.Normal;
				WindowState = FormWindowState.Maximized;
			}
		}
		#endregion

		private void disableControls() {
			pictureBox.Visible = false;
			toolSaveAs.Enabled = false;
			toolActualSize.Enabled = false;
			toolZoomIn.Enabled = false;
			toolZoomOut.Enabled = false;
			toolRotateCounterClockWise.Enabled = false;
			toolRotateClockWise.Enabled = false;

			if (images.Count < 2) {
				toolNextImage.Enabled = false;
				toolPrevImage.Enabled = false;
				toolDragCopy.Enabled = false;
				toolCopy.Enabled = false;
				toolDelete.Enabled = false;
			}
		}

		private void enableControl() {
			pictureBox.Visible = true;

			// enable toolbar buttons.
			toolSaveAs.Enabled = true;
			toolActualSize.Enabled = true;
			toolZoomIn.Enabled = true;
			toolZoomOut.Enabled = true;
			toolRotateCounterClockWise.Enabled = true;
			toolRotateClockWise.Enabled = true;
			toolNextImage.Enabled = true;
			toolPrevImage.Enabled = true;
			toolDragCopy.Enabled = true;
			toolCopy.Enabled = true;
			toolDelete.Enabled = true;
		}

		private void openImage() {
			openFileDialog.Filter = "Bitmap Files (*.bmp, *.dib)|*.bmp;*.dib|JPEG (*.jpg, *.jpeg, *.jpe, *.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|GIF (*.gif)|*.gif|TIFF (*.tif, *.tiff)|*.tif;*.tiff|PNG (*.png)|*.png|ICO (*.ico)|*.ico|All Picture Files (*.jpg, *.png,...)|*.bmp;*.dib;*.jpg;*.jpeg;*.jpe;*.jfif;*.gif;*.tif;*.tiff;*.png;*.ico|All Files|*.*";
			openFileDialog.FilterIndex = 7;

			if (openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.FileName.Length > 0) {
				loadImage(openFileDialog.FileName);
			}
		}

		private void loadImage(string image, bool reloadDirectory = false) {
			try {
				// save the memory.
				if (pictureBox.Image != null) {
					pictureBox.Image.Dispose();
				}

				currentFile = Path.GetFileName(image);

				// get all images from current image's directory.
				var directory = Path.GetDirectoryName(image);

				if (currentDirectory != directory || reloadDirectory) {
					currentDirectory = directory;

					var di = new DirectoryInfo(currentDirectory);
					images = new List<string>();

					foreach (var file in di.GetFiles()) {
						if (isImageExtension(file.Extension)) {
							images.Add(file.Name);
						}
					}

					images.Sort(new NaturalComparer());

					currentFileIndex = images.IndexOf(currentFile);
				}

				if (!File.Exists(image) && images.Count == 0) {
					// show message.
					labelErrorMessage.Visible = false;
					labelNoImageMessage.Visible = true;
					toolPhotoshop.Visible = false;
					contextPhotoshop.Visible = false;

					disableControls();

					Text = appTitle;

					return;
				}

				// set form title.
				Text = currentFile + " - " + appTitle;

				// try to load image.
				using (var stream = new FileStream(image, FileMode.Open, FileAccess.Read)) {
					var memStream = new MemoryStream();

					stream.CopyTo(memStream);
					memStream.Position = 0;

					pictureBox.Image = Image.FromStream(memStream);
				}

				// show picture.
				labelErrorMessage.Visible = false;
				labelNoImageMessage.Visible = false;

				enableControl();

				// recalculate zoom levels.
				recalculateZoomLevels();

				// resize and repostion picture box.
				zoomImage(settings.OpenFitToWindow ? Zoom.FitToWin : Zoom.ActualSize);

				// check if has photoshop file with same name.
				if (File.Exists(Path.Combine(currentDirectory, Path.GetFileNameWithoutExtension(currentFile) + ".psd"))) {
					toolPhotoshop.Visible = true;
					contextPhotoshop.Visible = true;
				}
			} catch (Exception) {
				// show message.
				labelErrorMessage.Visible = true;
				labelNoImageMessage.Visible = false;
				toolPhotoshop.Visible = false;
				contextPhotoshop.Visible = false;

				disableControls();
			}
		}

		private void reloadImage() {
			loadImage(Path.Combine(currentDirectory, currentFile));
		}

		private void toolFile_ButtonClick(object sender, EventArgs e) {
			openImage();
		}

		private void toolOpen_Click(object sender, EventArgs e) {
			openImage();
		}

		private bool isImageExtension(string extension) {
			switch (extension.ToLowerInvariant()) {
				case ".jpg":
				case ".jpeg":
				case ".jpe":
				case ".jfif":
				case ".png":
				case ".gif":
				case ".bmp":
				case ".dib":
				case ".tif":
				case ".tiff":
				case ".ico":
//				case ".svg":
					return true;
			}

			return false;
		}

		private void navigateImage(bool next = true, bool reloadDirectory = false) {
			if (images != null && images.Count > 1) {
				if (currentFileIndex < 0) {
					currentFileIndex = 0;
				}

				if (next) {
					currentFileIndex = currentFileIndex >= images.Count - 1 ? 0 : currentFileIndex + 1;
				} else {
					currentFileIndex = currentFileIndex == 0 ? images.Count - 1 : currentFileIndex - 1;
				}

				loadImage(Path.Combine(currentDirectory, images[currentFileIndex]), reloadDirectory);
			}
		}

		private void navigateFirstImage() {
			if (images != null && images.Count > 1) {
				currentFileIndex = 0;

				loadImage(Path.Combine(currentDirectory, images[currentFileIndex]));
			}
		}

		private void navigateLastImage() {
			if (images != null && images.Count > 1) {
				currentFileIndex = images.Count - 1;

				loadImage(Path.Combine(currentDirectory, images[currentFileIndex]));
			}
		}

		private void toolNextImage_Click(object sender, EventArgs e) {
			navigateImage();
		}

		private void toolPrevImage_Click(object sender, EventArgs e) {
			navigateImage(false);
		}

		private void panelMain_MouseDown(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.XButton1) {
				navigateImage(false);
			} else if (e.Button == MouseButtons.XButton2) {
				navigateImage();
			}
		}

		// save image.
		private void saveImage() {
			saveFileDialog.Filter = "Bitmap (*.bmp, *.dib)|*.bmp|JPEG (*.jpg, *.jpeg, *.jpe, *.jfif)|*.jpg|GIF (*.gif)|*.gif|TIFF (*.tif, *.tiff)|*.tif|PNG (*.png)|*.png|ICO (*.ico)|*.ico|All Files|*.*";
			saveFileDialog.FilterIndex = 5;
			saveFileDialog.DefaultExt = ".png";
			saveFileDialog.AddExtension = true;

			if (saveFileDialog.ShowDialog() == DialogResult.OK) {
				try {
					var format = ImageFormat.Png;
					var extension = Path.GetExtension(saveFileDialog.FileName);

					switch (extension) {
						case ".jpg":
						case ".jpeg":
						case ".jpe":
						case ".jfif":
							format = ImageFormat.Jpeg;
							break;

						case ".gif":
							format = ImageFormat.Gif;
							break;

						case ".bmp":
						case ".dib":
							format = ImageFormat.Bmp;
							break;

						case ".tif":
						case ".tiff":
							format = ImageFormat.Tiff;
							break;

						case ".ico":
							format = ImageFormat.Icon;
							break;
					}

					var image = pictureBox.Image;
					image.Save(saveFileDialog.FileName, format);

					//reloadImage();
				} catch (Exception) { }
			}
		}

		private void toolSaveAs_Click(object sender, EventArgs e) {
			saveImage();
		}

		// rotate image.
		private void rotate(bool clockwise = true) {
			try {
				var image = pictureBox.Image;
				var imagePath = Path.Combine(currentDirectory, currentFile);

				image.RotateFlip(clockwise ? RotateFlipType.Rotate90FlipNone : RotateFlipType.Rotate270FlipNone);

				File.Delete(imagePath);

				image.Save(imagePath);

				reloadImage();
			} catch (Exception) { }
		}

		private void toolRotateClockWise_Click(object sender, EventArgs e) {
			rotate();
		}

		private void toolRotateCounterClockWise_Click(object sender, EventArgs e) {
			rotate(false);
		}

		// scroll picture box.
		private void repositionPictureBox(int offsetX = 0, int offsetY = 0) {
			var newX = pictureBox.Location.X;
			var newY = pictureBox.Location.Y;

			if (offsetX > 0) {
				if (pictureBox.Location.X < 0) {
					newX += offsetX;
					newX = newX > 0 ? 0 : newX;
				}
			} else if (offsetX < 0) {
				if (pictureBox.Location.X + pictureBox.Width > panelMain.Width) {
					newX += offsetX;
					newX = newX + pictureBox.Width < panelMain.Width ? panelMain.Width - pictureBox.Width : newX;
				}
			}

			if (offsetY > 0) {
				if (pictureBox.Location.Y < 0) {
					newY += offsetY;
					newY = newY > 0 ? 0 : newY;
				}
			} else if (offsetY < 0) {
				if (pictureBox.Location.Y + pictureBox.Height > panelMain.Height) {
					newY += offsetY;
					newY = newY + pictureBox.Height < panelMain.Height ? panelMain.Height - pictureBox.Height : newY;
				}
			}

			// update last mouse position for mouse3 scroll.
			if (timerScroll.Enabled) {
				mouseLastPosition = new Point(mouseLastPosition.X + pictureBox.Location.X - newX, mouseLastPosition.Y + pictureBox.Location.Y - newY);
			}

			// update picture box position.
			pictureBox.Location = new Point(newX, newY);
		}

		private void pictureBox_MouseWheel(object sender, MouseEventArgs e) {
			if (ModifierKeys != Keys.Alt) {
				if (ModifierKeys != Keys.Control) {
					if (ModifierKeys != Keys.Shift) {
						// scroll vertical
						repositionPictureBox(0, e.Delta);
					} else {
						// scroll horizontal
						repositionPictureBox(e.Delta, 0);
					}
				} else if (ModifierKeys != Keys.Shift) {
					zoomImage(e.Delta > 0 ? Zoom.In : Zoom.Out, true, new Point(e.X + pictureBox.Location.X, e.Y + pictureBox.Location.Y));
				}
			}
		}

		private void pictureBox_MouseDown(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				if (ModifierKeys == Keys.Control) {
					doDragDropImage();
				} else {
					// save last mouse position.
					mouseLastPosition = e.Location;

					// set mouse cursor to grabbing.
					pictureBox.Cursor.Dispose();
					pictureBox.Cursor = new Cursor(Properties.Resources.grabbing.Handle);
				}
			} else if (e.Button == MouseButtons.Middle) {
				// save last mouse position.
				mouseLastPosition = e.Location;

				// set mouse cursor to mouse3
				pictureBox.Cursor.Dispose();
				pictureBox.Cursor = new Cursor(Properties.Resources.mouse3.Handle);
			} else if (e.Button == MouseButtons.XButton1) {
				navigateImage(false);
			} else if (e.Button == MouseButtons.XButton2) {
				navigateImage();
			}
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle) {
				// restore mouse cursor to grab.
				pictureBox.Cursor.Dispose();
				pictureBox.Cursor = new Cursor(Properties.Resources.grab.Handle);

				if (e.Button == MouseButtons.Middle) {
					// disable scroll timer
					timerScroll.Enabled = false;
				}
			}
		}

		private void pictureBox_MouseMove(object sender, MouseEventArgs e) {
			if ((MouseButtons & MouseButtons.Left) == MouseButtons.Left) {
				// scroll picture box.
				repositionPictureBox(e.X - mouseLastPosition.X, e.Y - mouseLastPosition.Y);
			} else if ((MouseButtons & MouseButtons.Middle) == MouseButtons.Middle) {
				var offsetX = e.X - mouseLastPosition.X;
				var offsetY = e.Y - mouseLastPosition.Y;

				if (Math.Abs(offsetX) <= 10 && Math.Abs(offsetY) <= 10) {
					// disable scroll timer
					timerScroll.Enabled = false;

					pictureBox.Cursor = new Cursor(Properties.Resources.mouse3.Handle);
				} else {
					// enable scroll timer
					timerScroll.Enabled = true;
					scrollOffsetX = offsetX;
					scrollOffsetY = offsetY;

					// set mouse cursor
					pictureBox.Cursor.Dispose();

					if (offsetX > 10) {
						if (offsetY > 10) {
							pictureBox.Cursor = new Cursor(Properties.Resources.mouse3_down_right.Handle);
						} else if (offsetY < -10) {
							pictureBox.Cursor = new Cursor(Properties.Resources.mouse3_up_right.Handle);
						} else {
							pictureBox.Cursor = new Cursor(Properties.Resources.mouse3_right.Handle);
						}
					} else if (offsetX < -10) {
						if (offsetY > 10) {
							pictureBox.Cursor = new Cursor(Properties.Resources.mouse3_down_left.Handle);
						} else if (offsetY < -10) {
							pictureBox.Cursor = new Cursor(Properties.Resources.mouse3_up_left.Handle);
						} else {
							pictureBox.Cursor = new Cursor(Properties.Resources.mouse3_left.Handle);
						}
					} else {
						if (offsetY > 10) {
							pictureBox.Cursor = new Cursor(Properties.Resources.mouse3_down.Handle);
						} else if (offsetY < -10) {
							pictureBox.Cursor = new Cursor(Properties.Resources.mouse3_up.Handle);
						}
					}
				}
			}
		}

		private void timerScroll_Tick(object sender, EventArgs e) {
			var offsetX = Math.Abs(scrollOffsetX) <= 10 ? 0 : scrollOffsetX;
			var offsetY = Math.Abs(scrollOffsetY) <= 10 ? 0 : scrollOffsetY;

			repositionPictureBox(-offsetX, -offsetY);
		}

		// recalculate zoom levels
		private void recalculateZoomLevels() {
			try {
				var minStep = 0.1f;
				var maxScale = 5f;
				var minScale = 1f;

				zoomLevels = new List<float>();

				zoomLevels.Add(1);
				zoomLevels.Add(maxScale);

				// calculate min scale
				var image = pictureBox.Image;
				var imageWidth = image.Width;
				var imageHeight = image.Height;
				var panelWidth = panelMain.Width;
				var panelHeight = panelMain.Height;

				if (imageWidth > panelWidth || imageHeight > panelHeight) {
					var scaleWidth = (float) panelWidth / imageWidth;
					var scaleHeight = (float) panelHeight / imageHeight;

					minScale = Math.Min(scaleWidth, scaleHeight);
				}

				// calculate min step
				if (minStep < (maxScale - minScale) / 16) {
					minStep = (maxScale - minScale) / 16;
				}

				while (minScale < maxScale) {
					if (minScale != 1) {
						zoomLevels.Add(minScale);
					}

					minScale += minStep;
				}

				zoomLevels.Sort();
			} catch (Exception) { }
		}

		// zoom image.
		private void zoomImage(Zoom type, bool zooming = false, Point focusPosition = default(Point)) {
			try {
				var image = pictureBox.Image;
				var imageWidth = image.Width;
				var imageHeight = image.Height;
				var panelWidth = panelMain.Width;
				var panelHeight = panelMain.Height;
				var oldScale = zoomScale;

				switch (type) {
					case Zoom.ActualSize:
						zoomScale = 1;
						break;

					case Zoom.FitToWin:
						if (imageWidth > panelWidth || imageHeight > panelHeight) {
							var scaleWidth = (float) panelWidth / imageWidth;
							var scaleHeight = (float) panelHeight / imageHeight;

//							zoomScale = Math.Min(scaleWidth, scaleHeight);
							zoomScale = scaleWidth;
						} else {
							zoomScale = 1;
						}

						break;

					case Zoom.In:
						for (var i = 0; i < zoomLevels.Count; i++) {
							if (zoomScale < zoomLevels[i]) {
								zoomScale = zoomLevels[i];
								break;
							}
						}

						break;

					case Zoom.Out:
						for (var i = 0; i < zoomLevels.Count; i++) {
							if (i == zoomLevels.Count - 1 && zoomScale > zoomLevels[i] || i < zoomLevels.Count - 1 && zoomScale > zoomLevels[i] && zoomScale <= zoomLevels[i + 1]) {
								zoomScale = zoomLevels[i];
								break;
							}
						}

						break;
					default:
						if (settings.OpenFitToWindow) {
							if (!toolZoomOut.Enabled) {
								zoomScale = zoomLevels.Min();
							}
			
							if (!toolZoomIn.Enabled) {
								zoomScale = zoomLevels.Max();
							}
						}
						
						break;
				}

				// enable/disable zoom button.
				toolZoomIn.Enabled = zoomScale < zoomLevels.Max();
				toolZoomOut.Enabled = zoomScale > zoomLevels.Min();

				// resize and reposion picture box.
				var newX = pictureBox.Location.X;
				var newY = pictureBox.Location.Y;

				var pictureBoxWidth = (int) (zoomScale * imageWidth);
				var pictureBoxHeight = (int) (zoomScale * imageHeight);

				if (zooming) {
					if (focusPosition != default(Point)) {
						// calculate focus by position.
						newX = (int) (zoomScale / oldScale * (pictureBox.Location.X - focusPosition.X) + focusPosition.X);
						newY = (int) (zoomScale / oldScale * (pictureBox.Location.Y - focusPosition.Y) + focusPosition.Y);
					} else {
						// calculate focus by center of panel.
						newX = (int) (zoomScale / oldScale * (pictureBox.Location.X - panelWidth / 2) + panelWidth / 2);
						newY = (int) (zoomScale / oldScale * (pictureBox.Location.Y - panelHeight / 2) + panelHeight / 2);
					}

					newX = newX > 0 ? 0 : newX;
					newY = newY > 0 ? 0 : newY;
				} else {
					if (type == Zoom.NoZoomBottom) {
						newY = panelHeight - pictureBoxHeight;
					} else if (type == Zoom.NoZoomTop) {
						newY = 0;
					}
				}
				
				if (panelWidth >= pictureBoxWidth) {
					newX = (panelWidth - pictureBoxWidth) / 2;
				} else if (newX > 0) {
					newX = 0;
				}

				if (panelHeight >= pictureBoxHeight) {
					newY = (panelHeight - pictureBoxHeight) / 2;
				} else if (newY > 0) {
					newY = 0;
				}

				pictureBox.Width = pictureBoxWidth;
				pictureBox.Height = pictureBoxHeight;
				pictureBox.Location = new Point(newX, newY);
			} catch (Exception) { }
		}

		private void toolActualSize_Click(object sender, EventArgs e) {
			zoomImage(Zoom.ActualSize, true);
		}

		private void toolZoomIn_Click(object sender, EventArgs e) {
			zoomImage(Zoom.In, true);
		}

		private void toolZoomOut_Click(object sender, EventArgs e) {
			zoomImage(Zoom.Out, true);
		}

		private void toolFullscreen_Click(object sender, EventArgs e) {
			toggleFullScreen();
		}

		// drag and drop to copy image.
		private void doDragDropImage() {
			string filePath = Path.Combine(currentDirectory, currentFile);

			if (File.Exists(filePath)) {
				string[] paths = { filePath };

				DoDragDrop(new DataObject(DataFormats.FileDrop, paths), DragDropEffects.Copy);
			}
		}

		private void toolDragCopy_MouseDown(object sender, MouseEventArgs e) {
			doDragDropImage();
		}

		// copy to clipboard.
		private void copyToClipboard() {
			string filePath = Path.Combine(currentDirectory, currentFile);

			if (File.Exists(filePath)) {
				var paths = new StringCollection();

				paths.Add(filePath);
				Clipboard.SetFileDropList(paths);
			}
		}

		private void toolCopy_Click(object sender, EventArgs e) {
			copyToClipboard();
		}

		private void contextCopy_Click(object sender, EventArgs e) {
			copyToClipboard();
		}

		// open file from clipboard.
		private void openFromClipBoard() {
			var filePath = "";

			if (Clipboard.ContainsText()) {
				filePath = Clipboard.GetText();
			} else if (Clipboard.ContainsFileDropList()) {
				var files = Clipboard.GetFileDropList();

				if (files.Count > 0) {
					filePath = files[files.Count - 1];
				}
			}

			if (File.Exists(filePath) && (currentDirectory == null || currentDirectory == null || filePath != Path.Combine(currentDirectory, currentFile))) {
				loadImage(filePath);
			}
		}

		// send image to trash.
		private void deleteImage() {
			if (currentDirectory == null || currentDirectory == null) {
				return;
			}

			string filePath = Path.Combine(currentDirectory, currentFile);

			if (File.Exists(filePath)) {
				if (SHFileOperationAPI.moveToTrash(filePath)) {
					if (images.Count > 1) {
						navigateImage(true, true);
					} else {
						loadImage(filePath, true);
					}
				}
			}
		}

		private void toolDelete_Click(object sender, EventArgs e) {
			deleteImage();
		}

		private void contextDelete_Click(object sender, EventArgs e) {
			deleteImage();
		}

		// open image in explorer.
		private void openImageLocation() {
			try {
				var filePath = Path.Combine(currentDirectory, currentFile);

				if (File.Exists(filePath)) {
					var args = "/select, \"" + filePath + "\"";

					Process.Start("explorer.exe", args);
				}
			} catch (Exception) { }
		}

		private void contextOpenExplorer_Click(object sender, EventArgs e) {
			openImageLocation();
		}

		// show image properties.
		private void showImageProperties() {
			try {
				var filePath = Path.Combine(currentDirectory, currentFile);

				ShellExecuteExAPI.ShowFileProperties(filePath);
			} catch (Exception) { }
		}

		private void contextFileProperties_Click(object sender, EventArgs e) {
			showImageProperties();
		}

		// photoshop.
		private void configPhotoshopPath() {
			var findPhotoshopDialog = new OpenFileDialog();

			findPhotoshopDialog.Title = "Choose Photoshop Location";
			findPhotoshopDialog.Filter = "EXE File|*.exe";

			if (findPhotoshopDialog.ShowDialog() == DialogResult.OK) {
				settings.Photoshop = findPhotoshopDialog.FileName;
			}
		}

		private void openPhotoshop() {
			try {
				var filePath = Path.Combine(currentDirectory, Path.GetFileNameWithoutExtension(currentFile) + ".psd");

				if (File.Exists(filePath)) {
					if (settings.Photoshop != "" && settings.Photoshop != null) {
						Process.Start(settings.Photoshop, "\"" + filePath + "\"");
					} else {
						if (MessageBox.Show("You didn't configure photoshop location. Do you want to configure now?", appTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
							configPhotoshopPath();

							if (settings.Photoshop != "" && settings.Photoshop != null) {
								Process.Start(settings.Photoshop, "\"" + filePath + "\"");
							}
						}
					}
				}
			} catch (Exception) { }
		}

		private void toolPhotoshop_Click(object sender, EventArgs e) {
			openPhotoshop();
		}

		private void toolPhotoshop_MouseDown(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Right) {
				configPhotoshopPath();
			}
		}

		private void contextPhotoshop_Click(object sender, EventArgs e) {
			openPhotoshop();
		}

		// support drag & drop file from explorer to open it.
		private void MainForm_DragEnter(object sender, DragEventArgs e) {
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
				e.Effect = DragDropEffects.Copy;
			}
		}

		private void MainForm_DragDrop(object sender, DragEventArgs e) {
			string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);

			if (files.Length > 0) {
				var filePath = files[files.Length - 1];

				if (currentDirectory == null || currentDirectory == null || filePath != Path.Combine(currentDirectory, currentFile)) {
					loadImage(files[files.Length - 1]);
				}
			}
		}

		// show open with dialog
		private void showOpenWith(string filePath) {
			var args = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "shell32.dll");
			args += ",OpenAs_RunDLL " + filePath;

			Process.Start("rundll32.exe", args);
		}

		private void contextOpenWith_Click(object sender, EventArgs e) {
			try {
				showOpenWith(Path.Combine(currentDirectory, currentFile));
			} catch (Exception) { }
		}
	}
}
