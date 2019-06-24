using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Dart.Base
{
	/// <summary>
	/// is for opening files an keep testing possible in viewmodels
	/// extend the functions if needed
	/// </summary>
	internal class FileDialogService
	{
		/// <summary>
		/// opens a SaveFileDialog for saving a file
		/// </summary>
		/// <param name="filter"></param>
		/// <param name="title"></param>
		/// <returns></returns>
		public string SaveFileDialog(string filter, string title)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog
			{
				Filter = filter,
				Title = title
			};
			saveFileDialog.ShowDialog();
			return saveFileDialog.FileName;
		}

		public string SaveFileDialog(SaveFileDialog saveFileDialog)
		{
			saveFileDialog.ShowDialog();
			return saveFileDialog.FileName;
		}

		/// <summary>
		/// opens a fileopen dialog to open an file
		/// </summary>
		/// <param name="filter"></param>
		/// <param name="title"></param>
		/// <param name="filePath"></param>
		/// <param name="defaultExtension"></param>
		/// <returns></returns>
		public bool OpenFileDialog(string filter, string title, out string filePath, string defaultExtension = null)
		{
			var dlg = new OpenFileDialog
			{
				CheckFileExists = true,

				Filter = filter,
				Title = title
			};
			if (!string.IsNullOrEmpty(defaultExtension))
				dlg.DefaultExt = defaultExtension;

			var check = dlg.ShowDialog();
			filePath = dlg.FileName;
			return check.HasValue && check.Value;
		}

		public bool? OpenFileDialog(OpenFileDialog openFileDialog)
		{
			return openFileDialog.ShowDialog();
		}
	}
}
