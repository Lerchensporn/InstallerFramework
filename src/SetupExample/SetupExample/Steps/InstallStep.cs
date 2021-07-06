// <copyright file="InstallStep.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>07.06.2009</date>
// <summary></summary>

namespace SetupExample.Steps
{
   using System;
   using System.IO;
   using System.Reflection;
   using System.Windows.Forms;
   using InstallerFramework.Base;
   using InstallerFramework.Dialog;
   using InstallerFramework.Installers;
   using InstallerFramework.Installers.Service;

   /// <summary>
   /// Installiert die Software auf dem System.
   /// </summary>
   public partial class InstallStep : DialogStep
   {
      /// <summary>
      /// Der übergeordnete Dialog.
      /// </summary>
      private DialogForm parentDialog;

      /// <summary>
      /// Der Installer, der die Software installiert.
      /// </summary>
      private MainInstaller installer;

      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="InstallStep"/>-Klasse.
      /// </summary>
      public InstallStep()
      {
         this.InitializeComponent();
      }

      /// <summary>
      /// Wird ausgeführt, wenn dieses UserControl geladen wird.
      /// </summary>
      /// <param name="sender">Die Ereignisquelle.</param>
      /// <param name="e">Die <see cref="System.EventArgs"/>-Instanz, die die Ereignisdaten enthält.</param>
      private void InstallStep_Load(object sender, EventArgs e)
      {
         this.FocusChanged += new EventHandler<FocusChangedEventArgs>(this.InstallStep_FocusChanged);
         this.InitializeInstallers();

         StepCounter counter = new StepCounter();
         counter.StateChanged += new EventHandler<StepChangedEventArgs>(this.Counter_StateChanged);
         counter.Installers.Add(this.installer);
         counter.Start();

         if (StaticProperties.InstallMode == InstallAction.Install)
         {
            this.label1.Text = StringResource.InstallStep_Install;
            this.progressBar1.Maximum = counter.MaximumSteps.InstallSteps;
            this.installer.Install();
         }
         else
         {
            this.label1.Text = StringResource.InstallStep_Uninstall;
            this.progressBar1.Maximum = counter.MaximumSteps.UninstallSteps;
            this.installer.Uninstall();
         }

         this.parentDialog.GotoNext();
      }

      /// <summary>
      /// Initialisiert die Installer. 
      /// </summary>
      private void InitializeInstallers()
      {
         // Initialisierung
         ShortcutInstaller shortcutInstaller1 = new ShortcutInstaller();
         SoftwareRegistryInstaller softwareRegistryInstaller1 = new SoftwareRegistryInstaller();
         FileSystemInstaller fileSystemInstaller1 = new FileSystemInstaller();
         ServiceInstaller serviceInstaller1 = new ServiceInstaller();
         EventLogInstaller eventLogInstaller1 = new EventLogInstaller();
         ////
         // shortcutInstaller1
         ////
         shortcutInstaller1.Description = "Verknüpfung zu 'Software.exe'";
         shortcutInstaller1.LinkPath = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + "\\SetupExample\\SetupExample.lnk";
         shortcutInstaller1.TargetPath = StaticProperties.InstallPath + "\\SetupExample\\Software.exe";
         ////
         // softwareRegistryInstaller1
         ////
         softwareRegistryInstaller1.Info.DisplayName = "SetupExample";
         softwareRegistryInstaller1.Info.UninstallString = Assembly.GetExecutingAssembly().Location;
         softwareRegistryInstaller1.Info.Publisher = "Andreas Becker";
         softwareRegistryInstaller1.Info.ProductGuid = "SetupExample";
         softwareRegistryInstaller1.Info.InstalledForAllUsers = true;
         softwareRegistryInstaller1.Info.DisplayVersion = "1.0.0.0";
         ////
         // fileSystemInstaller1
         ////
         fileSystemInstaller1.Files.Add(new InstallerFile
         {
            ParentDirectory = StaticProperties.InstallPath + "\\SetupExample",
            FileName = "Service.exe",
            ValueStream = new MemoryStream(Properties.Resources.Service)
         });
         fileSystemInstaller1.Files.Add(new InstallerFile
         {
            ParentDirectory = StaticProperties.InstallPath + "\\SetupExample",
            FileName = "Software.exe",
            ValueStream = new MemoryStream(Properties.Resources.Software)
         });
         fileSystemInstaller1.Files.Add(new InstallerFile
         {
            ParentDirectory = StaticProperties.InstallPath + "\\SetupExample",
            FileName = "SetupExample.txt",
            ValueStream = new MemoryStream(Properties.Resources.SetupExample)
         });

         fileSystemInstaller1.Directories.Add(new InstallerDirectory
         {
            DirectoryName = "SetupExample",
            ParentDirectory = StaticProperties.InstallPath
         });
         fileSystemInstaller1.Directories.Add(new InstallerDirectory
         {
            DirectoryName = "SetupExample",
            ParentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu),
            DeleteMode = DirectoryDeleteMode.DeleteAlways
         });
         ////
         // serviceInstaller1
         ////
         ServiceDescription description = new ServiceDescription();
         ServiceFailureActions failureAction = new ServiceFailureActions();
         SCAction action = new SCAction();
         ServiceFailureActionsFlag failureActionsFlag = new ServiceFailureActionsFlag();

         description.Description = "Beispieldienst für das Testen des InstallerFrameworks.";
         failureActionsFlag.FailureActionsOnNonCrashFailures = true;

         action.Delay = 100;
         action.ActionType = SCActionType.SCActionRestart;

         failureAction.Actions = action.ToPointer();
         failureAction.CountActions = 1;
         failureAction.RebootMsg = "Ein Fehler ist aufgetreten.";

         serviceInstaller1.DisplayName = "SetupExample";
         serviceInstaller1.ServiceName = "SetupExample";
         serviceInstaller1.TargetPathName = StaticProperties.InstallPath + "\\SetupExample\\Service.exe";
         serviceInstaller1.StartType = ServiceStart.ServiceDemandStart;
         serviceInstaller1.ServiceType = ServiceType.ServiceWin32OwnProcess;
         serviceInstaller1.AdditionalProperties.Description = description;
         serviceInstaller1.AdditionalProperties.FailureActions = failureAction;
         serviceInstaller1.AdditionalProperties.FailureActionsFlag = failureActionsFlag;
         ////
         // eventLogInstaller1
         ////
         eventLogInstaller1.CreationData = new System.Diagnostics.EventSourceCreationData("SetupExample", "SetupExample");
         eventLogInstaller1.DeleteSourceOnUninstall = false;
         ////
         // installer
         ////
         this.installer = new MainInstaller();
         this.installer.Log = new InstallLog("InstallLog.log");
         this.installer.UninstallDataFile = Directory.GetParent(Assembly.GetExecutingAssembly().Location) + "\\unins000.dat";
         this.installer.Installers.Add(softwareRegistryInstaller1);
         this.installer.Installers.Add(fileSystemInstaller1);
         this.installer.Installers.Add(serviceInstaller1);
         this.installer.Installers.Add(eventLogInstaller1);
         this.installer.Installers.Add(shortcutInstaller1);
      }

      /// <summary>
      /// Wird ausgeführt, wenn die Installation vom Benutzer abgebrochen wird.
      /// </summary>
      /// <param name="sender">Die Ereignisquelle.</param>
      /// <param name="e">Eine <see cref="FocusChangedEventArgs"/>-Instanz, die die Ereignisdaten enthält.</param>
      private void InstallStep_FocusChanged(object sender, FocusChangedEventArgs e)
      {
         if (e.NaviType == NavigationType.Cancel)
         {
            if (MessageBox.Show(StringResource.InstallStep_CancelText, StringResource.InstallStep_CancelCaption, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.Yes)
            {
               this.installer.Uninstall();
               e.ParentDialogForm.GotoNext();
            }
         }

         this.parentDialog = e.ParentDialogForm;
      }

      /// <summary>
      /// Wird ausgeführt, wenn sich die Stufe des Vorgangs ändert.
      /// </summary>
      /// <param name="sender">Die Ereignisquelle.</param>
      /// <param name="e">Die <see cref="StepChangedEventArgs"/>-Instanz, die die Ereignisdaten enthält.</param>
      private void Counter_StateChanged(object sender, StepChangedEventArgs e)
      {
         this.progressBar1.Value = e.OldStep;
      }
   }
}
