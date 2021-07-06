// <copyright file="EventLogInstaller.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>16.05.2009</date>
// <summary></summary>

namespace InstallerFramework.Installers
{
   using System.Diagnostics;
   using InstallerFramework.Base;

   /// <summary>
   /// Installiert EventLogs auf dem System.
   /// </summary>
   public class EventLogInstaller : AdvancedInstaller
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="EventLogInstaller"/>-Klasse.
      /// </summary>
      public EventLogInstaller()
      {
         this.MaxSteps = new StepInfo(1, 1);
      }

      /// <summary>
      /// Gibt an oder legt fest, welche Informationen zum Erstellen des EventLogs verwendet werden.
      /// </summary>
      public EventSourceCreationData CreationData { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, ob bei der Deinstallation die Ereignisquelle gelöscht wird oder nur der EventLog.
      /// Ereignisquellen können nur gelöscht werden, wenn ihr Name nicht mit dem Namen des Ereignisprotokolls übereinstimmt.
      /// </summary>
      public bool DeleteSourceOnUninstall { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welche maximale Größe der EventLog hat.
      /// </summary>
      public long? MaximumKilobytes { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welche Aktion ausgeführt wird, wenn die maximale Größe des EventLogs erreicht ist.
      /// </summary>
      public OverflowAction Overflow { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, für wieviele Tage die Einträge im EventLog aufbewahrt werden, wenn Overflow auf OverwriteOlder gesetzt ist.
      /// </summary>
      public int MinimumRetentionDays { get; set; }

      /// <summary>
      /// Führt die Installation aus. 
      /// </summary>
      /// <param name="stateSaver">Ein <see cref="System.Collections.IDictionary"/>, in dem die zum Ausführen eines Commit-, Rollback- oder Deinstallationsvorgangs erforderlichen Daten gespeichert werden.</param>
      public override void Install(System.Collections.IDictionary stateSaver)
      {
         this.Log.LogMessage("EventLogInstaller hat die Installation gestartet.");
         base.Install(stateSaver);
         this.Log.LogMessage(" Die Ereignisquelle wird erstellt...");
         if (!EventLog.Exists(this.CreationData.LogName, this.CreationData.MachineName))
         {
            EventLog.CreateEventSource(this.CreationData);
         }

         this.Log.LogMessage(" Die Eigenschaften des EventLogs werden festgelegt...");
         EventLog log = new EventLog(this.CreationData.LogName, this.CreationData.MachineName);
         log.ModifyOverflowPolicy(this.Overflow, this.MinimumRetentionDays);
         if (this.MaximumKilobytes != null)
         {
            log.MaximumKilobytes = (long)this.MaximumKilobytes;
         }

         stateSaver.Add("source", this.CreationData.Source);
         stateSaver.Add("log", this.CreationData.LogName);
         stateSaver.Add("delsource", this.DeleteSourceOnUninstall);
         stateSaver.Add("machine", this.CreationData.MachineName);
         this.Log.LogMessage("EventLogInstaller hat die Installation abgeschlossen");
         this.CurrentSteps = 1;
      }

      /// <summary>
      /// Führt die Deinstallation aus.
      /// </summary>
      /// <param name="savedState">Ein <see cref="System.Collections.IDictionary"/> mit Informationen über den Zustand, in dem sich der Computer nach Abschluss der Installation befindet.</param>
      public override void Uninstall(System.Collections.IDictionary savedState)
      {
         this.Log.LogMessage("EventLogInstaller hat die Deinstallation gestartet.");
         base.Uninstall(savedState);
         if ((bool)savedState["delsource"] && (string)savedState["source"] != (string)savedState["machine"])
         {
            this.Log.LogMessage(" Die Ereignisquelle wird entfernt...");
            EventLog.DeleteEventSource((string)savedState["source"], (string)savedState["machine"]);
         }
         else
         {
            this.Log.LogMessage(" Der EventLog wird gelöscht...");
            EventLog.Delete((string)savedState["log"], (string)savedState["machine"]);
         }

         this.Log.LogMessage("EventLogInstaller hat die Deinstallation abgeschlossen.");
         this.CurrentSteps = 1;
      }
   }
}