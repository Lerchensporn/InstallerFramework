// <copyright file="RegistryKeyInstaller.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>28.04.2009</date>
// <summary></summary>

namespace InstallerFramework.Installers
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using InstallerFramework.Base;
   using Microsoft.Win32;

   /// <summary>
   /// Erstellt Registrierungseinträge in der Registry.
   /// </summary>
   public class RegistryKeyInstaller : AdvancedInstaller
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="RegistryKeyInstaller"/>-Klasse.
      /// </summary>
      public RegistryKeyInstaller()
      {
         this.RegistryValues = new List<RegistryValue>();
         this.MaxSteps = new StepInfo(1, 1);
      }

      /// <summary>
      /// Gibt an oder legt fest, welche Registrierungseinträge erstellt werden.
      /// </summary>
      public IList<RegistryValue> RegistryValues { get; private set; }

      /// <summary>
      /// Führt die Installation aus. 
      /// </summary>
      /// <param name="stateSaver">Ein <see cref="System.Collections.IDictionary"/>, in dem die zum Ausführen eines Commit-, Rollback- oder Deinstallationsvorgangs erforderlichen Daten gespeichert werden.</param>
      public override void Install(IDictionary stateSaver)
      {
         this.Log.LogMessage("RegistryKeyInstaller hat die Installation gestartet...");
         base.Install(stateSaver);
         this.Log.LogMessage("Die Registrierungseinträge werden erstellt...");

         foreach (RegistryValue item in this.RegistryValues)
         {
            if (item.ValueName == null)
            {
               item.ParentKey.CreateSubKey(item.SubKeyPath).Close();
            }
            else
            {
               Registry.SetValue(item.SubKeyPath, item.ValueName, item.Value);
            }
         }

         stateSaver.Add("keys", this.RegistryValues);

         this.Log.LogMessage("RegistryKeyInstaller hat die Installation abgeschlossen.");
         this.CurrentSteps = 1;
      }

      /// <summary>
      /// Entfernt die erstellten Einträge aus der Registry.
      /// </summary>
      /// <param name="savedState">Ein <see cref="System.Collections.IDictionary"/> mit Informationen über den Zustand, in dem sich der Computer nach Abschluss der Installation befindet.</param>
      public override void Uninstall(IDictionary savedState)
      {
         this.Log.LogMessage("RegistryKeyInstaller hat die Deinstallation gestartet.");
         base.Uninstall(savedState);
         this.Log.LogMessage("Die Registrierungseinträge werden entfernt...");
         IList<RegistryValue> values = (IList<RegistryValue>)savedState["keys"];
         foreach (RegistryValue item in values)
         {
            if (item.ValueName == null)
            {
               item.ParentKey.DeleteSubKey(item.SubKeyPath, false);
            }
            else
            {
               RegistryKey key = item.ParentKey.OpenSubKey(item.SubKeyPath, true);
               key.DeleteValue(item.ValueName);
               key.Close();
            }
         }

         this.Log.LogMessage("RegistryKeyInstaller hat die Deinstallation abgeschlossen.");
         this.CurrentSteps = 1;
      }
   }
}