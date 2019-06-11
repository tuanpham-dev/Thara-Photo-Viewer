using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Reflection;
using System.IO;
using System.Security.Principal;
using System.Windows.Forms;

namespace TharaStudio.PhotoViewer {
	public static class FileAssociation {
		public static void registerToDefaultPrograms() {
			WindowsIdentity CurrentIdentity = WindowsIdentity.GetCurrent();
			WindowsPrincipal CurrentPrincipal = new WindowsPrincipal(CurrentIdentity);

			if (!CurrentPrincipal.IsInRole(WindowsBuiltInRole.Administrator)) {
				return;
			}

			try {
				var appName = "Thara Photo Viewer";
				var path = Assembly.GetEntryAssembly().Location;
				var file = Path.GetFileName(path);
				var cmdPath = String.Format("\"{0}\" \"%1\"", path);

				// register app path
				var appPaths = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\App Paths", true).CreateSubKey(file);
				appPaths.SetValue("", path);

				// set application capabilities
				var capabitities = Registry.LocalMachine.OpenSubKey("Software", true).CreateSubKey(appName).CreateSubKey("Capabilities");
				capabitities.SetValue("AppcationDescription", appName);
				capabitities.SetValue("ApplicationName", appName);

				var jpg = capabitities.CreateSubKey("FileAssociations");
				jpg.SetValue(".jpeg", appName + ".FileAssoc.Jpeg");

				// register to default programs
				var registeredApplications = Registry.LocalMachine.OpenSubKey("Software\\RegisteredApplications", true);
				registeredApplications.SetValue(appName, "Software\\" + appName + "\\Capabilities");

				// extension handler
				var handler = Registry.LocalMachine.OpenSubKey("Software\\Classes\\", true).CreateSubKey(appName + ".FileAssoc.Jpeg");
				handler.SetValue("", "JPEG File");
				handler.SetValue("FriendlyTypeName", "JPEG File");

				var command = handler.CreateSubKey("shell\\open\\command");
				command.SetValue("", cmdPath);

				// Show Mesage Box
				MessageBox.Show("Register for Default Apps Successfully!", appName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			} catch (Exception) { }
		}
	}
}
