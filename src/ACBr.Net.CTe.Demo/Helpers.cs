using System.Windows.Forms;

namespace ACBr.Net.CTe.Demo
{
	public static class Helpers
	{
		public static string OpenFile(string filters, string title = "Abrir")
		{
			using (var ofd = new OpenFileDialog())
			{
				ofd.CheckPathExists = true;
				ofd.CheckFileExists = true;
				ofd.Multiselect = false;
				ofd.Filter = filters;
				ofd.Title = title;

				return ofd.ShowDialog().Equals(DialogResult.Cancel) ? null : ofd.FileName;
			}
		}

		public static string[] OpenFiles(string filters, string title = "Abrir")
		{
			using (var ofd = new OpenFileDialog())
			{
				ofd.CheckPathExists = true;
				ofd.CheckFileExists = true;
				ofd.Multiselect = true;
				ofd.Filter = filters;
				ofd.Title = title;

				return ofd.ShowDialog().Equals(DialogResult.Cancel) ? null : ofd.FileNames;
			}
		}

		public static string SelectFolder()
		{
			using (var fbd = new FolderBrowserDialog())
			{
				fbd.ShowNewFolderButton = true;
				return fbd.ShowDialog().Equals(DialogResult.Cancel) ? string.Empty : fbd.SelectedPath;
			}
		}
	}
}