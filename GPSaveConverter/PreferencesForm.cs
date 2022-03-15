﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPSaveConverter
{
    public partial class PreferencesForm : Form
    {
        public PreferencesForm()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.R))
            {
                this.BeginInvoke((Action)ResetKeycode_Pressed);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ResetKeycode_Pressed()
        {
            DialogResult res = MessageBox.Show(this, "Remove all local configurations? (Software will then exit)", "Are you sure?", MessageBoxButtons.YesNoCancel);
            if(res == DialogResult.Yes)
            {
                Properties.Settings.Default.Reset();
                Application.Exit();
            }

        }

        private void PreferencesForm_Load(object sender, EventArgs e)
        {
            this.logLevelComboBox.Items.AddRange(NLog.LogLevel.AllLevels.ToArray());
            this.logLevelComboBox.SelectedIndex = Properties.Settings.Default.FileLogLevel.Ordinal;
            this.allowNetworkCheckbox.Checked = Properties.Settings.Default.AllowWebDataFetch;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.FileLogLevel = this.logLevelComboBox.SelectedItem as NLog.LogLevel;
            Properties.Settings.Default.AllowWebDataFetch = this.allowNetworkCheckbox.Checked;
            Properties.Settings.Default.Save();

            NLog.LogManager.Configuration.Variables["fileLogLevel"] = GPSaveConverter.Properties.Settings.Default.FileLogLevel.ToString();

            this.Hide();
        }
    }
}