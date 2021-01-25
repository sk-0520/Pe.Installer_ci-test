using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pe.Installer
{
    public partial class InstallerForm: Form
    {
        public InstallerForm()
        {
            InitializeComponent();
            Font = SystemFonts.MessageBoxFont;
            LoggerFactory = new InternalLoggerFactory(this.listLog);
            Logger = LoggerFactory.CreateLogger(GetType());

            Logger.LogInfo("START");
        }


        #region proeprty

        ILoggerFactory LoggerFactory { get; }
        ILogger Logger { get; }

        bool Installed { get; set; }

        Task CurrentTask { get; set; }
        CancellationTokenSource CancellationTokenSource { get; set; }

        string ExecutePath { get; set; }

        #endregion

        #region function

        #endregion

        private void InstallerForm_Load(object sender, EventArgs e)
        {
            // ディレクトリ設定
            this.inputDirectoryPath.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Pe", "application");

            // プラットフォーム設定
            if(Environment.Is64BitOperatingSystem) {
                this.listPlatform.Items.Add(new PlatformListItem(Properties.Resources.String_Platform_64bit, "x64"));
            }
            this.listPlatform.Items.Add(new PlatformListItem(Properties.Resources.String_Platform_32bit, "x86"));
            this.listPlatform.SelectedIndex = 0;

            // リソース増えるとしんどいので手で頑張る
            Text = Properties.Resources.String_Caption;
            this.labelDirectoryPath.Text = Properties.Resources.String_Label_DirectoryPath_A;
            this.labelPlatform.Text = Properties.Resources.String_Label_Platform_A;
            this.labelTotalProgress.Text = Properties.Resources.String_Label_TotalProgress_A;
            this.labelCurrentProgress.Text = Properties.Resources.String_Label_CurrentProgress_A;
            this.commandDirectoryPath.Text = Properties.Resources.String_SelectDirectoryPath_A;
            this.commandExecute.Text = Properties.Resources.String_Execute_Install_A;
            this.commandClose.Text = Properties.Resources.String_Close_A;

        }

        private void commandDirectoryPath_Click(object sender, EventArgs e)
        {
            using(var dialog = new FolderBrowserDialog() {
                SelectedPath = this.inputDirectoryPath.Text,
                ShowNewFolderButton = true,
            }) {
                if(dialog.ShowDialog() == DialogResult.OK) {
                    this.inputDirectoryPath.Text = dialog.SelectedPath;
                }
            }

        }

        private void commandClose_Click(object sender, EventArgs e)
        {
            if(CancellationTokenSource != null) {
                CancellationTokenSource.Cancel();
                this.commandClose.Text = Properties.Resources.String_Close_A;
                CancellationTokenSource = null;
            } else {
                Close();
            }
        }

        private async void commandExecute_Click(object sender, EventArgs e)
        {
            if(Installed) {

            } else {
                Logger.LogInfo("INSTALL");

                this.commandExecute.Enabled = false;
                this.commandClose.Text = Properties.Resources.String_Cancel_A;
                CancellationTokenSource = new CancellationTokenSource();

                try {
                    var selectedItem = (PlatformListItem)this.listPlatform.SelectedItem;

                    var downloader = new Downloader(LoggerFactory);
                    var donwloadUri = await downloader.GetArchiveUriAsync(Constants.UpdateFileUri, selectedItem.Value, CancellationTokenSource.Token);
                    Logger.LogDebug(donwloadUri.ToString());

                } finally {
                    this.commandExecute.Enabled = true;
                }
            }
        }

    }
}
