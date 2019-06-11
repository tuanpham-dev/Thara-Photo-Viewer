using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TharaStudio.PhotoViewer {
	public class Settings {
		private bool settingsChanged;
		private int x, y, width, height;

		public int X {
			get {
				return x;
			}

			set {
				x = value;
				settingsChanged = true;
			}
		}

		public int Y {
			get {
				return y;
			}

			set {
				y = value;
				settingsChanged = true;
			}
		}

		public int Width {
			get {
				return width;
			}

			set {
				width = value;
				settingsChanged = true;
			}
		}

		public int Height {
			get {
				return height;
			}

			set {
				height = value;
				settingsChanged = true;
			}
		}

		private bool maximized, openFitToWindow;

		public bool Maximized {
			get {
				return maximized;
			}

			set {
				maximized = value;
				settingsChanged = true;
			}
		}

		public bool OpenFitToWindow {
			get {
				return openFitToWindow;
			}

			set {
				openFitToWindow = value;
				settingsChanged = true;
			}
		}

		private string photoshop;

		public string Photoshop {
			get {
				return photoshop;
			}

			set {
				photoshop = value;
				settingsChanged = true;
			}
		}

		public bool SaveSettings() {
			if (settingsChanged) {
				using (StreamWriter writer = new StreamWriter(Application.LocalUserAppDataPath + "/config.xml", false)) {
					var xml = new XmlSerializer(typeof(Settings));
					xml.Serialize(writer, this);
				}
			}

			return settingsChanged;
		}

		public static Settings LoadSettings() {
			FileInfo fi = null;

			try {
				fi = new FileInfo(Application.LocalUserAppDataPath + "/config.xml");
			} catch (Exception) { }

			if (fi == null || !fi.Exists) {
				var settings = new Settings();

				settings.X = -88;
				settings.Y = -88;
				settings.Width = 800;
				settings.Height = 600;
				settings.Maximized = false;
				settings.OpenFitToWindow = false;
				settings.Photoshop = "";

				return settings;
			}

			using (var file = fi.OpenRead()) {
				var xml = new XmlSerializer(typeof(Settings));

				try {
					return (Settings) xml.Deserialize(file);
				} catch (Exception) {
					var settings = new Settings();

					settings.X = -88;
					settings.Y = -88;
					settings.Width = 800;
					settings.Height = 600;
					settings.Maximized = false;
					settings.OpenFitToWindow = false;
					settings.Photoshop = "";

					return settings;
				}
			}
		}
	}
}
