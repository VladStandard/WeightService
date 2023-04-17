// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ControlsExamples;

public partial class FormMain : Form
{

	#region Methods

	public FormMain()
	{
		InitializeComponent();
	}

	private void FormMain_Load(object sender, EventArgs e)
	{
		GetFonts(fieldFontFamily);
		SetFont(fieldFontFamily);
	}


	private void GetFonts(ComboBox comboBoxFontFamily)
	{
		comboBoxFontFamily.Items.Clear();
		using InstalledFontCollection fontCollection = new();
		foreach (FontFamily fontFamily in fontCollection.Families)
		{
			comboBoxFontFamily.Items.Add(fontFamily.Name);
		}
	}

	private void SetFont(ComboBox comboBoxFontFamily)
	{
		if (comboBoxFontFamily?.Items?.Count > 0)
		{
			for (var i = 0; i < comboBoxFontFamily.Items.Count; i++)
			{
				if (comboBoxFontFamily.Items[i].Equals(Font.FontFamily.Name))
				{
					comboBoxFontFamily.SelectedIndex = i;
					break;
				}
			}
		}
	}

	#endregion
}
