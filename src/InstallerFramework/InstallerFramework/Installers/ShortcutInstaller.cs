// <copyright file="ShortcutInstaller.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>15.05.2009</date>
// <summary></summary>

namespace InstallerFramework.Installers
{
   using System;
   using System.Collections;
   using System.IO;
   using System.Runtime.InteropServices.ComTypes;
   using System.Windows.Forms;
   using InstallerFramework.Base;

   /// <summary>
   /// Installiert Verknüpfungen zu Dateien und Verzeichnissen.
   /// </summary>
   public partial class ShortcutInstaller : AdvancedInstaller
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="ShortcutInstaller"/>-Klasse.
      /// </summary>
      public ShortcutInstaller()
      {
         this.MaxSteps = new StepInfo(1, 1);
      }

      /// <summary>
      /// Gibt an oder legt fest, welchen Pfad das Ziel der Verknüpfung hat.
      /// </summary>
      public string TargetPath { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welchen Dateipfad die Verknüpfung hat.
      /// </summary>
      public string LinkPath { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welchen Anzeigestatus das Fensters hat, das durch die Vreknüpfung geöffnet wird.
      /// </summary>
      public ShowWindow? WindowState { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welche Kommadozeilenargumente übergeben werden.
      /// </summary>
      public string Arguments { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welches das Arbeitsverzeichnis des Ziels der Verknüpfung ist.
      /// </summary>
      public string WorkingDirectory { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welche Beschreibung der Verknüpfung zugeordnet wird.
      /// </summary>
      public string Description { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welchen Pfad das Icons hat, das der Verknüpfung zugewiesen wird.
      /// </summary>
      public string IconLocation { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welche Tastenkombination der Verknüpfung zugeordnet wird. Es muss eine Modifier-Flag enthalten sein.
      /// </summary>
      public Keys? HotKey { get; set; }

      /// <summary>
      /// Installiert die Verknüpfung.
      /// </summary>
      /// /// <param name="stateSaver">Ein System.Collections.IDictionary, in dem zum Ausführen des Commit-, Rollback- oder Deinstallationsvorgangs erforderliche Daten gespeichert werden.</param>
      public override void Install(IDictionary stateSaver)
      {
         this.Log.LogMessage("ShortcutInstaller hat die Installation gestartet.");
         base.Install(stateSaver);
         this.Log.LogMessage(" Die Verknüpfungen werden installiert...");
         if (!Directory.Exists(Path.GetDirectoryName(this.LinkPath)))
         {
            throw new InstallException("Der Pfad existiert nicht.");
         }

         this.CreateShortcut();

         stateSaver.Add("linkpath", this.LinkPath);
         this.Log.LogMessage(" Die Verknüpfung mit dem Pfad '" + this.LinkPath + "' wurde erstellt.");
         this.Log.LogMessage("ShortcutInstaller hat die Installation abgeschlossen.");
         this.CurrentSteps = 1;
      }

      /// <summary>
      /// Löscht die Verknüpfung.
      /// </summary>
      /// <param name="savedState">Ein System.Collections.IDictionary mit AnInformationen über den Zustand, in dem sich der Computer nach Abschluss der Installation befindet.</param>
      public override void Uninstall(IDictionary savedState)
      {
         this.Log.LogMessage("ShortcutInstaller hat die Deinstallation gestartet.");
         base.Uninstall(savedState);

         if (savedState.Contains("linkpath"))
         {
            this.Log.LogMessage(" Die Verknüpfungen werden deinstalliert...");
            string path = savedState["linkpath"].ToString();
            if (File.Exists(path))
            {
               File.Delete(path);
               this.Log.LogMessage(" Die Verknüpfung '" + path + "' wurde entfernt.");
            }
            else
            {
               this.Log.LogMessage(" Die Verknüpfung mit dem Pfad '" + path + "' wurde nicht gefunden.");
            }
         }
         else
         {
            this.Log.LogMessage(" Der notwendige Parameter in savedState war nicht vorhanden. Die Deinsallation kann nicht durchgeführt werden.");
         }

         this.Log.LogMessage("ShortcutInstaller hat die Deinstallation abgeschlossen.");
         this.CurrentSteps = 1;
      }

      /// <summary>
      /// Erstellt eine Verknüpfung auf dem System.
      /// </summary>
      private void CreateShortcut()
      {
         NativeMethods.ShellLink shellLink = new NativeMethods.ShellLink();
         NativeMethods.IShellLink link = (NativeMethods.IShellLink)shellLink;

         link.SetPath(this.TargetPath);
         if (this.Arguments != null)
         {
            link.SetArguments(this.Arguments);
         }

         if (this.WindowState != null)
         {
            link.SetShowCmd((ShowWindow)this.WindowState);
         }

         if (this.HotKey != null)
         {
            if ((this.HotKey & Keys.Modifiers) != 0)
            {
               throw new ArgumentException("Der Hotkey hat keinen Modifier.");
            }

            // IShellLink: 0xMMVK
            // Keys:  0x00MM00VK        
            //   MM = Modifier (Alt, Control, Shift)
            //   VK = Virtual key code
            int modifier = (int)(this.HotKey & Keys.Modifiers);
            int keyCode = (int)(this.HotKey & Keys.KeyCode);
            int hotkey = modifier >> 8 | keyCode;
            link.SetHotkey((short)hotkey);
         }

         IPersistFile file = (IPersistFile)shellLink;
         file.Save(this.LinkPath, true);
      }
   }
}
