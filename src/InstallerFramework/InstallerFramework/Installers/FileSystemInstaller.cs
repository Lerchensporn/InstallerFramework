// <copyright file="FileSystemInstaller.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>19.05.2009</date>
// <summary></summary>

namespace InstallerFramework.Installers
{
   using System.Collections.Generic;
   using System.IO;
   using InstallerFramework.Base;

   /// <summary>
   /// Installiert Dateien und Verzeichnisse auf dem System.
   /// </summary>
   public class FileSystemInstaller : AdvancedInstaller
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="FileSystemInstaller"/>-Klasse.
      /// </summary>
      public FileSystemInstaller()
      {
         this.MaxSteps = new StepInfo(2, 2);
         this.Directories = new List<InstallerDirectory>();
         this.Files = new List<InstallerFile>();
      }

      /// <summary>
      /// Gibt an oder legt fest, welche Verzeichnisse installiert werden.
      /// </summary>
      public IList<InstallerDirectory> Directories { get; private set; }

      /// <summary>
      /// Gibt an oder legt fest, welche Dateinen installiert werden.
      /// </summary>
      public IList<InstallerFile> Files { get; private set; }

      /// <summary>
      /// Führt die Installation aus. 
      /// </summary>
      /// <param name="stateSaver">Ein <see cref="System.Collections.IDictionary"/>, in dem die zum Ausführen eines Commit-, Rollback- oder Deinstallationsvorgangs erforderlichen Daten gespeichert werden.</param>
      public override void Install(System.Collections.IDictionary stateSaver)
      {
         this.Log.LogMessage("FileSystemInstaller hat die Installation gestartet.");
         base.Install(stateSaver);
         this.Log.LogMessage(" Dateisystemeinträge werden erstellt...");
         List<InstallerDirectory> dirs = new List<InstallerDirectory>();
         List<InstallerFile> files = new List<InstallerFile>();

         this.Log.LogMessage(" Verzeichnisse werden erstellt...");
         foreach (InstallerDirectory item in this.Directories)
         {
            if (!Directory.Exists(item.FullPath))
            {
               DirectoryInfo info = Directory.CreateDirectory(item.FullPath);
               if (item.Attributes != FileAttributes.Normal)
               {
                  info.Attributes = item.Attributes;
               }

               if (item.AccessControl != null)
               {
                  info.SetAccessControl(item.AccessControl);
               }
            }

            dirs.Add(item);
         }

         this.CurrentSteps = 1;
         this.Log.LogMessage(" Dateien werden geschrieben...");
         foreach (InstallerFile item in this.Files)
         {
            if (Directory.Exists(item.ParentDirectory))
            {
               FileStream writer = File.Create(item.FullPath);
               int maxBufferSize = 1048576; // 1 Megabyte
               long bufferSize = item.ValueStream.Length > maxBufferSize ? maxBufferSize : item.ValueStream.Length;
               byte[] buffer = new byte[bufferSize];
               int bytesRead;
               do
               {
                  bytesRead = item.ValueStream.Read(buffer, 0, buffer.Length);
                  writer.Write(buffer, 0, buffer.Length);
               }
               while (bytesRead != 0);
               writer.Close();
               FileInfo info = new FileInfo(item.FullPath);
               if (item.Attributes != FileAttributes.Normal)
               {
                  info.Attributes = item.Attributes;
               }

               if (item.AccessControl != null)
               {
                  info.SetAccessControl(item.AccessControl);
               }

               files.Add(item);
            }
         }

         stateSaver.Add("dirs", dirs);
         stateSaver.Add("files", files);
         this.Log.LogMessage("FileSystemInstaller hat die Installation abgeschlossen.");
         this.CurrentSteps = 2;
      }

      /// <summary>
      /// Führt die Deinstallation aus.
      /// </summary>
      /// <param name="savedState">Ein <see cref="System.Collections.IDictionary"/> mit Informationen über den Zustand, in dem sich der Computer nach Abschluss der Installation befindet.</param>
      public override void Uninstall(System.Collections.IDictionary savedState)
      {
         this.Log.LogMessage("FileSystemInstaller hat die Deinstallation gestartet.");
         base.Uninstall(savedState);

         this.Log.LogMessage(" Dateien werden entfernt...");
         foreach (InstallerFile item in (List<InstallerFile>)savedState["files"])
         {
            File.Delete(item.FullPath);
         }

         this.CurrentSteps = 1;
         this.Log.LogMessage(" Verzeichnisse werden entfernt...");
         foreach (InstallerDirectory item in (List<InstallerDirectory>)savedState["dirs"])
         {
            if (item.DeleteMode == DirectoryDeleteMode.DeleteNever || !Directory.Exists(item.FullPath))
            {
               continue;
            }
            else if (item.DeleteMode == DirectoryDeleteMode.DeleteIfNotContainsFiles)
            {
               if (Directory.GetFiles(item.FullPath, string.Empty, SearchOption.AllDirectories).Length > 0)
               {
                  continue;
               }
            }

            Directory.Delete(item.FullPath, true);
         }

         this.Log.LogMessage("FileSystemInstaller hat die Deinstallation abgeschlossen.");
         this.CurrentSteps = 2;
      }
   }
}