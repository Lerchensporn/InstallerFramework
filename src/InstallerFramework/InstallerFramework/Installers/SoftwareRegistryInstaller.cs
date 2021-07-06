// <copyright file="SoftwareRegistryInstaller.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>15.05.2009</date>
// <summary></summary>

namespace InstallerFramework.Installers
{
   using System;
   using System.Collections.Generic;
   using InstallerFramework.Base;
   using Microsoft.Win32;

   /// <summary>
   /// Trägt die Software-Eigenschaften in die Registry ein und ermöglicht so die automatische Deinstallation mit Windows Installer.
   /// </summary>
   public class SoftwareRegistryInstaller : AdvancedInstaller
   {
      /// <summary>
      /// Initialisiert eine neue Instaz der <see cref="SoftwareRegistryInstaller"/>-Klasse.
      /// </summary>
      public SoftwareRegistryInstaller()
      {
         this.Info = new SoftwareInfo();
         this.MaxSteps = new StepInfo(3, 1);
      }

      /// <summary>
      /// Informationen über die zu installierende Software.
      /// </summary>
      public SoftwareInfo Info { get; set; }

      /// <summary>
      /// Installiert die Software auf dem Computer, indem REgistryeinträge erstellt werden.
      /// </summary>
      /// <param name="stateSaver">Ein System.Collections.IDictionary, in dem zum Ausführen des Commit-, Rollback- oder Deinstallationsvorgangs erforderliche Daten gespeichert werden.</param>
      /// <exception cref="InstallException"></exception>
      public override void Install(System.Collections.IDictionary stateSaver)
      {
         this.Log.LogMessage("SoftwareRegistryInstaller hat die Installation gestartet.");
         base.Install(stateSaver);

         if (this.Info.Publisher == null)
         {
            this.Log.LogMessage(" Es ist ein Fehler im Installer aufgetreten: Die Publisher-Eigenschaft ist null. Der Installer wurde vom Hersteller falsch konfiguriert.");
            throw new InstallException("Die Pulisher-Eigenschaft ist null.");
         }

         if (this.Info.DisplayName == null)
         {
            this.Log.LogMessage(" Es ist ein Fehler im Installer aufgetreten: Die DisplayName-Eigenschaft ist null. Der Installer wurde vom Hersteller falsch konfiguriert.");
            throw new InstallException("Die DisplayName-Eigenschaft ist null.");
         }

         if (this.Info.DisplayVersion == null)
         {
            this.Log.LogMessage(" Es ist ein Fehler im Installer aufgetreten: Die DisplayVersion-Eigenschaft ist null. Der Installer wurde vom Hersteller falsch konfiguriert.");
            throw new InstallException("Die DisplayVersion-Eigenschaft ist null.");
         }

         if (this.Info.ProductGuid == null)
         {
            this.Log.LogMessage(" Es ist ein Fehler im Installer aufgetreten: Die ProductGuid-Eigenschaft ist null. Der Installer wurde vom Hersteller falsch konfiguriert.");
            throw new InstallException("Die ProductGuid-Eigenschaft ist null.");
         }

         this.Log.LogMessage(" Die Software wird registriert...");
         RegistryKey baseKey = this.Info.InstalledForAllUsers ? Registry.LocalMachine : Registry.CurrentUser;

         Microsoft.Win32.RegistryKey key = baseKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall", true).CreateSubKey(this.Info.ProductGuid.ToString(), RegistryKeyPermissionCheck.ReadWriteSubTree);
         stateSaver.Add("guid", this.Info.ProductGuid);
         stateSaver.Add("all", this.Info.InstalledForAllUsers);

         this.CurrentSteps = 1;

         key.SetValue("Publisher", this.Info.Publisher);
         key.SetValue("UninstallString", this.Info.UninstallString);
         key.SetValue("InstallDate", DateTime.Now.ToString());
         key.SetValue("DisplayVersion", this.Info.DisplayVersion);
         key.SetValue("DisplayName", this.Info.DisplayName);

         this.CurrentSteps = 2;

         foreach (KeyValuePair<string, object> item in this.Info.OtherProperties)
         {
            key.SetValue(item.Key, item.Value);
         }

         key.Close();

         this.Log.LogMessage("SoftwareRegistryInstaller hat die Installation abgeschlossen");
         this.CurrentSteps = 3;
      }

      /// <summary>
      /// Deinstalliert die Software, indem die Registryeinträge gelöscht werden.
      /// </summary>
      /// <param name="savedState">Ein System.Collections.IDictionary mit Informationen über den Zustand, in dem sich der Computer nach Abschluss der Installation befindet.</param>
      public override void Uninstall(System.Collections.IDictionary savedState)
      {
         this.Log.LogMessage("SoftwareRegistryInstaller hat die Deinstallation gestartet.");
         base.Uninstall(savedState);

         this.Log.LogMessage(" Die Registrierung der Software in der Systemsteuerung wird entfernt...");
         RegistryKey baseKey = (bool)savedState["all"] ? Registry.LocalMachine : Registry.CurrentUser;
         Microsoft.Win32.RegistryKey key = baseKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall", true);
         key.DeleteSubKey((string)savedState["guid"]);
         key.Close();

         this.Log.LogMessage("SoftwareRegistryInstaller hat die Deinstallation abgeschlossen");
         this.CurrentSteps = 1;
      }
   }
}
