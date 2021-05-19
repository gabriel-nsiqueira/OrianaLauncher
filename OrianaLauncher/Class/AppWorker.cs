﻿using Octokit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrianaLauncher.Class
{
    public class AppWorker
    {
        public OrianaLauncher orianaLauncher;
        public BackgroundWorker backgroundWorker;

        public AppWorker(OrianaLauncher orianaLauncher)
        {
            this.orianaLauncher = orianaLauncher;
        }

        public void installApp()
        {
            this.backgroundWorker = new BackgroundWorker();
            this.backgroundWorker.WorkerReportsProgress = true;
            this.InitializeBackgroundWorker();
            this.backgroundWorker.RunWorkerAsync();
        }

        private void InitializeBackgroundWorker()
        {
            this.backgroundWorker.DoWork += new DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            this.backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
        }

        public void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var backgroundWorkerCode = sender as BackgroundWorker;
            App activeApp = this.orianaLauncher.appList.apps[this.orianaLauncher.activeApp];

            ReleaseAsset asset = new ReleaseAsset();

            backgroundWorker.ReportProgress(0);

            foreach (ReleaseAsset ra in activeApp.release.Assets)
            {
                if (ra.Name.Contains("zip"))
                {
                    asset = ra;
                    break;
                }
            }

            string path = this.orianaLauncher.tempPath + "\\" + asset.Name;
            this.orianaLauncher.utils.FileDelete(path);

            using (var client = new WebClient())
            {
                client.DownloadFile(asset.BrowserDownloadUrl, path);
            }

            backgroundWorker.ReportProgress(50);

            string appPath = this.orianaLauncher.appPath + "\\OrianaApps\\" + activeApp.name;

            this.orianaLauncher.utils.DirectoryDelete(appPath);
            Directory.CreateDirectory(appPath);

            ZipFile.ExtractToDirectory(path, appPath);

            if (this.orianaLauncher.config.containsApp(activeApp.name))
            {
                this.orianaLauncher.config.removeApp(activeApp.name);
            }
            this.orianaLauncher.config.installedApp.Add(new InstalledApp(activeApp.name, Version.Parse(activeApp.release.TagName)));
            this.orianaLauncher.config.update(this.orianaLauncher);

            backgroundWorker.ReportProgress(100);
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Panel AppDownloadPanel = (Panel) this.orianaLauncher.componentList.get("AppDownload").getControl("AppDownloadPanel");
            ProgressBar AppDownloadBar = (ProgressBar)AppDownloadPanel.Controls["AppDownloadBar"];
            AppDownloadBar.Value = e.ProgressPercentage;
            System.Windows.Forms.Label AppDownloadPercent = (System.Windows.Forms.Label)AppDownloadPanel.Controls["AppDownloadPercent"];
            AppDownloadPercent.Text = e.ProgressPercentage + "%";

            System.Windows.Forms.Label AppDownloadStatus = (System.Windows.Forms.Label)AppDownloadPanel.Controls["AppDownloadStatus"];
            
            if (e.ProgressPercentage == 0)
            {
                AppDownloadStatus.Text = this.orianaLauncher.translator.lg("Downloading ...");
            }

            if (e.ProgressPercentage == 50)
            {
                AppDownloadStatus.Text = this.orianaLauncher.translator.lg("Extracting ...");
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Panel AppDownloadPanel = (Panel)this.orianaLauncher.componentList.get("AppDownload").getControl("AppDownloadPanel");
            ProgressBar AppDownloadBar = (ProgressBar)AppDownloadPanel.Controls["AppDownloadBar"];
            AppDownloadBar.Value = 100;
            System.Windows.Forms.Label AppDownloadPercent = (System.Windows.Forms.Label)AppDownloadPanel.Controls["AppDownloadPercent"];
            AppDownloadPercent.Text = "100%";
            System.Windows.Forms.Label AppDownloadStatus = (System.Windows.Forms.Label)AppDownloadPanel.Controls["AppDownloadStatus"];
            AppDownloadStatus.Text = this.orianaLauncher.translator.lg("Completed !");

        }
    }
}