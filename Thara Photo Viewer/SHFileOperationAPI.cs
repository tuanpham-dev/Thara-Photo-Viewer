using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TharaStudio.PhotoViewer {
	public static class SHFileOperationAPI {
		[Flags]
		public enum FileOperationFlags : ushort {
			FOF_SILENT = 0x0004,
			FOF_NOCONFIRMATION = 0x0010,
			FOF_ALLOWUNDO = 0x0040,
			FOF_SIMPLEPROGRESS = 0x0100,
			FOF_NOERRORUI = 0x0400,
			FOF_WANTNUKEWARNING = 0x4000,
		}

		public enum FileOperationType : uint {
			FO_MOVE = 0x0001,
			FO_COPY = 0x0002,
			FO_DELETE = 0x0003,
			FO_RENAME = 0x0004,
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private struct SHFILEOPSTRUCT {

			public IntPtr hwnd;
			[MarshalAs(UnmanagedType.U4)]
			public FileOperationType wFunc;
			public string pFrom;
			public string pTo;
			public FileOperationFlags fFlags;
			[MarshalAs(UnmanagedType.Bool)]
			public bool fAnyOperationsAborted;
			public IntPtr hNameMappings;
			public string lpszProgressTitle;
		}

		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		private static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);

		public static bool moveToTrash(string path) {
			try {
				var shf = new SHFILEOPSTRUCT {
					wFunc = FileOperationType.FO_DELETE,
					fFlags = FileOperationFlags.FOF_ALLOWUNDO | FileOperationFlags.FOF_NOCONFIRMATION,
					pFrom = path + '\0' + '\0'
				};

				return SHFileOperation(ref shf) == 0;
			} catch (Exception) { }

			return false;
		}
	}
}
