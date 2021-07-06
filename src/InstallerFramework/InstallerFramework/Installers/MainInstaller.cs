// <copyright file="MainInstaller.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>11.04.2009</date>
// <summary></summary>

namespace InstallerFramework.Installers
{
   using System.Collections;
   using System.IO;
   using System.Runtime.Serialization.Formatters.Binary;
   using InstallerFramework.Base;

   /// <summary>
   /// Ein übergeordneter Installer, der andere Installationsvorgänge koordiniert und Informationen für die Deinstallation speichert.
   /// </summary>
   public class MainInstaller : AdvancedInstaller
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="MainInstaller"/>-Klasse.
      /// </summary>
      public MainInstaller()
         : base()
      {
         this.MaxSteps = new StepInfo(2, 2);
         this.UninstallDataFile = "unins000.dat";
      }

      /// <summary>
      /// Gibt an oder legt fest, welche Datei die für die Deinstallation notwendingen Daten enthält.
      /// </summary>
      public string UninstallDataFile { get; set; }

      /// <summary>
      /// Installiert die Software auf dem System.
      /// </summary>
      public void Install()
      {
         IDictionary stateSaver = new Hashtable();
         stateSaver = new Hashtable();
         this.Log.LogMessage("---Die Installation wurde vom MainInstaller gestartet---");
         this.Log.LogMessage(" Die Software wird installiert...");
         base.Install(stateSaver);

         this.CurrentSteps = 1;
         this.Log.LogMessage(" Die Daten für die Deinstallation werden in der Datei '" + this.UninstallDataFile + "'gespeichert...");
         BinaryFormatter bin = new BinaryFormatter();
         StreamWriter writer = new StreamWriter(this.UninstallDataFile);
         bin.Serialize(writer.BaseStream, stateSaver);
         writer.Close();

         this.Log.LogMessage("---Die Installation wurde vom MainInstaller abgeschlossen---");
         this.CurrentSteps = 2;
      }

      /// <summary>
      /// Führt die Deinstallation aus.
      /// </summary>
      public void Uninstall()
      {
         this.Log.LogMessage("---Die Deinstallation wurde vom MainInstaller gestartet---");
         this.Log.LogMessage(" Die Daten für die Deinstallation werden gelesen...");
         IDictionary savedState = new Hashtable();
         StreamReader reader = new StreamReader(this.UninstallDataFile);
         BinaryFormatter bin = new BinaryFormatter();
         savedState = (IDictionary)bin.Deserialize(reader.BaseStream);
         reader.Close();
         this.CurrentSteps = 1;
         this.Log.LogMessage(" Die Software wird deinstalliert...");
         base.Uninstall(savedState);
         File.Delete(this.UninstallDataFile);
         this.Log.LogMessage("---Die Deinstallation wurde vom MainInstaller abgeschlossen---");
         this.CurrentSteps = 2;
      }
   }
}
