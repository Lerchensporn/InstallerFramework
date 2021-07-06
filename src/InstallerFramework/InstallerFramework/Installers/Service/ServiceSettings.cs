// <copyright file="ServiceSettings.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>26.04.2009</date>
// <summary></summary>

namespace InstallerFramework.Installers.Service
{
   using System;
   using System.Runtime.InteropServices;

   /// <summary>
   /// Auszuführende Aktion mit einem Dienst.
   /// </summary>
   public enum SCActionType
   {
      /// <summary>
      /// Keine Aktion.
      /// </summary>
      SCActionNone = 0x00000000,

      /// <summary>
      /// Dienst neu starten.
      /// </summary>
      SCActionRestart = 0x00000001,

      /// <summary>
      /// System neu starten.
      /// </summary>
      SCActionReboot = 0x00000002,

      /// <summary>
      /// Eine Anweisung ausführen.
      /// </summary>
      SCActionRunCommand = 0x00000003,
   }

   /// <summary>
   /// Der SID-Typ des Dienstes. Eine Änderung wird erst nach einem Neustart des Systems übernommen.
   /// </summary>
   public enum ServiceSidType
   {
      /// <summary>
      /// Kein spezifischer Typ. Die Benutzung dieses Typs reduziert Probleme mit der Kompatibilität. 
      /// </summary>
      None = 0x00000000,

      /// <summary>
      /// Wenn der Dienst-Prozess erstellt wurde, wird die SID mit folgenden Eigenschaften hinzugefügt: SE_GROUP_ENABLED_BY_DEFAULT | SE_GROUP_OWNER.
      /// </summary>
      Unrestricted = 0x00000001,

      /// <summary>
      /// Der Typ enthält SERVICE_SID_TYPE_UNRESTRICTED. Drei zusätzliche SIDs werden zu dem Prozess des Dienstes hinzugefügt:
      /// <para>*World SID S-1-1-0
      ///   <para>*Service logon SID
      ///    <para>*Write-restricted SID S-1-5-33
      ///  </para></para></para>
      /// Wenn mehrere Dienste in einem Prozess untergebracht sind und ein Dienst den Typ SERVICE_SID_TYPE_RESTRICTED hat, müssen alle anderen Dienste auch diesen Typ haben.
      /// </summary>
      Restricted = 0x00000003
   }

   /// <summary>
   /// Information über einen Dienst, die geändert wird.
   /// </summary>
   internal enum InfoLevel
   {
      /// <summary>
      /// Der 'lpInfo'-Parameter ist eine <see cref="ServiceDescription"/>-Struktur. Es wird die Beschreibung des Dienstes angegeben.
      /// </summary>
      ServiceConfigDescription = 1,

      /// <summary>
      /// Der 'lpInfo'-Parameter ist eine <see cref="ServiceFailureActions"/>-Struktur. Es werden Aktionen im Fehlerfall angegeben. wenn die 'SCActionReboot'-Aktion behandelt wird, dann muss die 'SEShutdownName'-Berechtigung angegeben sein.
      /// </summary>
      ServiceConfigFailureActions = 2,

      /// <summary>
      /// Der 'lpinfo'-Parameter ist ein Zeiger zur <see cref="ServiceDelayedAutoStartInfo"/>-Struktur. Nicht unterstützt von Windows Server 2003 und Windows XP/2000.
      /// </summary>
      ServiceConfigDelayedAutoStartInfo = 3,

      /// <summary>
      /// Der 'lpinfo'-Parameter ist ein Zeiger zur <see cref="ServiceConfigFailureActionsFlag"/>-Struktur. Nicht unterstützt von Windows Server 2003 und Windows XP/2000.
      /// </summary>
      ServiceConfigFailureActionsFlag = 4,

      /// <summary>
      /// Der 'lpinfo'-Parameter ist ein Zeiger zur <see cref="ServiceConfigServiceSidInfo"/>-Struktur. Nicht unterstützt von Windows Server 2003 und Windows XP/2000.
      /// </summary>
      ServiceConfigServiceSidInfo = 5,

      /// <summary>
      /// Der 'lpinfo'-Parameter ist ein Zeiger zur <see cref="ServiceRequiredPrivilegesInfo"/>-Struktur. Nicht unterstützt von Windows Server 2003 und Windows XP/2000.
      /// </summary>
      ServiceConfigRequiredPrivilegesInfo = 6,

      /// <summary>
      /// Der 'lpinfo'-Parameter ist ein Zeiger zur 'service_Config_preshutdown_info'-Struktur. Nicht unterstützt von Windows Server 2003 und Windows XP/2000.
      /// </summary>
      ServiceConfigPreshutdownInfo = 7,

      /// <summary>
      /// Nicht implementiert, da erst ab Windows 7 unterstützt!
      /// Der 'lpinfo'-Parameter ist ein Zeiger zur 'service_Config_trigger_info'-Struktur. Nicht unterstützt von Windows Server 2003/2008 und Windows Vista/XP/2000.
      /// </summary>
      ServiceConfigTriggerInfo = 8,

      /// <summary>
      /// Nicht implementiert, da erst ab Windows 7 unterstützt!
      /// Der 'lpinfo'-Parameter ist ein Zeiger zur 'service_Config_preferred_node'-Struktur.
      /// </summary>
      ServiceConfigPreferredNode = 9
   }

   /// <summary>
   /// Beschreibung eines Dienstes.
   /// </summary>
   public struct ServiceDescription
   {
      /// <summary>
      /// Die Beschreibung des Dienstes.
      /// </summary>
      public string Description { get; set; }

      /// <summary>
      /// Prüft zwei <see cref="ServiceSidInfo"/>-Instanzen auf Gleichheit.
      /// </summary>
      /// <param name="obj1">Die erste <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <param name="obj2">Die zweite <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <returns>True, wenn die Instanzen gleich sind, sonst false.</returns>
      public static bool operator ==(ServiceDescription obj1, ServiceDescription obj2)
      {
         return obj1.Equals(obj2);
      }

      /// <summary>
      /// Prüft zwei <see cref="ServiceSidInfo"/>-Instanzen auf Ungleichheit.
      /// </summary>
      /// <param name="obj1">Die erste <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <param name="obj2">Die zweite <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <returns>True, wenn die Instanzen ungleich sind, sonst false.</returns>
      public static bool operator !=(ServiceDescription obj1, ServiceDescription obj2)
      {
         return !obj1.Equals(obj2);
      }

      /// <summary>
      /// Prüft diese Instanz und ein Objekt auf Gleichheit.
      /// </summary>
      /// <param name="obj">Das Objekt, mit dem diese Instanz verglichen wird.</param>
      /// <returns>True, wenn das Objekt dieser Instanz gleicht, sonst false.</returns>
      public override bool Equals(object obj)
      {
         if (obj == null || this.GetType() != obj.GetType())
         {
            return false;
         }

         ServiceDescription other = (ServiceDescription)obj;
         return this.Description == other.Description;
      }

      /// <summary>
      /// Berechnet den Hashcode dieser Instanz.
      /// </summary>
      /// <returns>Der Hashcode dieser Instanz wird zurückgegeben.</returns>
      public override int GetHashCode()
      {
         return this.Description.GetHashCode();
      }
   }

   /// <summary>
   /// Im Fehlerfall ausgeführte Aktionen.
   /// </summary>
   public struct ServiceFailureActions
   {
      /// <summary>
      /// Zeit, nach der die Fehleranzahl auf null zurückgesetzt wird in Sekunden. Es kann auch INFINITE angegeben werden.
      /// </summary>
      public int ResetPeriod { get; set; }

      /// <summary>
      /// Zu übertragene Nachricht beim Neustart des Dienstes.
      /// </summary>
      public string RebootMsg { get; set; }

      /// <summary>
      /// Argumente für das ausgeführte Programm im Fehlerfall. Null verändert die Argumente nicht, string.Empty löscht sie.
      /// </summary>
      public string Command { get; set; }

      /// <summary>
      /// Anzahl der Elemente im lpsaActions-Array.
      /// </summary>
      public int CountActions { get; set; }

      /// <summary>
      /// Zeiger zu einem <see cref="SCAction"/>-Array.
      /// </summary>
      public IntPtr Actions { get; set; }

      /// <summary>
      /// Prüft zwei <see cref="ServiceSidInfo"/>-Instanzen auf Gleichheit.
      /// </summary>
      /// <param name="obj1">Die erste <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <param name="obj2">Die zweite <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <returns>True, wenn die Instanzen gleich sind, sonst false.</returns>
      public static bool operator ==(ServiceFailureActions obj1, ServiceFailureActions obj2)
      {
         return obj1.Equals(obj2);
      }

      /// <summary>
      /// Prüft zwei <see cref="ServiceSidInfo"/>-Instanzen auf Ungleichheit.
      /// </summary>
      /// <param name="obj1">Die erste <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <param name="obj2">Die zweite <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <returns>True, wenn die Instanzen ungleich sind, sonst false.</returns>
      public static bool operator !=(ServiceFailureActions obj1, ServiceFailureActions obj2)
      {
         return !obj1.Equals(obj2);
      }

      /// <summary>
      /// Prüft diese Instanz und ein Objekt auf Gleichheit.
      /// </summary>
      /// <param name="obj">Das Objekt, mit dem diese Instanz verglichen wird.</param>
      /// <returns>True, wenn das Objekt dieser Instanz gleicht, sonst false.</returns>
      public override bool Equals(object obj)
      {
         if (obj == null || this.GetType() != obj.GetType())
         {
            return false;
         }

         ServiceFailureActions other = (ServiceFailureActions)obj;
         return this.Actions == other.Actions && this.Command == other.Command && this.CountActions == other.CountActions && this.RebootMsg == other.RebootMsg && this.ResetPeriod == other.ResetPeriod;
      }

      /// <summary>
      /// Berechnet den Hashcode dieser Instanz.
      /// </summary>
      /// <returns>Der Hashcode dieser Instanz wird zurückgegeben.</returns>
      public override int GetHashCode()
      {
         return this.Actions.ToInt32() ^ this.Command.GetHashCode() ^ this.CountActions ^ this.RebootMsg.GetHashCode() ^ this.ResetPeriod;
      }
   }

   /// <summary>
   /// Enthält die Einstellungen für veezögert, automatisch startetende Dienste.
   /// </summary>
   public struct ServiceDelayedAutoStartInfo
   {
      /// <summary>
      /// Gibt an oder legt fest, ob der Dienst verzögert (true) oder während des Boot-Vorgangs startet.
      /// </summary>
      public bool DelayedAutoStart { get; set; }

      /// <summary>
      /// Prüft zwei <see cref="ServiceSidInfo"/>-Instanzen auf Gleichheit.
      /// </summary>
      /// <param name="obj1">Die erste <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <param name="obj2">Die zweite <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <returns>True, wenn die Instanzen gleich sind, sonst false.</returns>
      public static bool operator ==(ServiceDelayedAutoStartInfo obj1, ServiceDelayedAutoStartInfo obj2)
      {
         return obj1.Equals(obj2);
      }

      /// <summary>
      /// Prüft zwei <see cref="ServiceSidInfo"/>-Instanzen auf Ungleichheit.
      /// </summary>
      /// <param name="obj1">Die erste <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <param name="obj2">Die zweite <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <returns>True, wenn die Instanzen ungleich sind, sonst false.</returns>
      public static bool operator !=(ServiceDelayedAutoStartInfo obj1, ServiceDelayedAutoStartInfo obj2)
      {
         return !obj1.Equals(obj2);
      }

      /// <summary>
      /// Prüft diese Instanz und ein Objekt auf Gleichheit.
      /// </summary>
      /// <param name="obj">Das Objekt, mit dem diese Instanz verglichen wird.</param>
      /// <returns>True, wenn das Objekt dieser Instanz gleicht, sonst false.</returns>
      public override bool Equals(object obj)
      {
         if (obj == null || this.GetType() != obj.GetType())
         {
            return false;
         }

         ServiceDelayedAutoStartInfo other = (ServiceDelayedAutoStartInfo)obj;
         return this.DelayedAutoStart == other.DelayedAutoStart;
      }

      /// <summary>
      /// Berechnet den Hashcode dieser Instanz.
      /// </summary>
      /// <returns>Der Hashcode dieser Instanz wird zurückgegeben.</returns>
      public override int GetHashCode()
      {
         return this.DelayedAutoStart.GetHashCode();
      }
   }

   /// <summary>
   /// Legt fest, wann Aktionen im Fehlerfall ausgeführt werden. Die Änderungen werden erst nach einem Neustart des Systems wirksam.
   /// </summary>
   public struct ServiceFailureActionsFlag
   {
      /// <summary>
      /// Falls true, dann werden die Fehler-Aktionen ausgeführt, wenn der Dienst beendet ist ohne den SERVICE_STOPPED-Status mitzuteilen oder der Exitcode nicht 0 ist.
      /// Falls false, dann werden die Fehler-Aktionen ausgeführt, wenn der Dienst beendet ist ohne den SERVICE_STOPPED-Status mitzuteilen.
      /// </summary>
      public bool FailureActionsOnNonCrashFailures { get; set; }

      /// <summary>
      /// Prüft zwei <see cref="ServiceSidInfo"/>-Instanzen auf Gleichheit.
      /// </summary>
      /// <param name="obj1">Die erste <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <param name="obj2">Die zweite <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <returns>True, wenn die Instanzen gleich sind, sonst false.</returns>
      public static bool operator ==(ServiceFailureActionsFlag obj1, ServiceFailureActionsFlag obj2)
      {
         return obj1.Equals(obj2);
      }

      /// <summary>
      /// Prüft zwei <see cref="ServiceSidInfo"/>-Instanzen auf Ungleichheit.
      /// </summary>
      /// <param name="obj1">Die erste <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <param name="obj2">Die zweite <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <returns>True, wenn die Instanzen ungleich sind, sonst false.</returns>
      public static bool operator !=(ServiceFailureActionsFlag obj1, ServiceFailureActionsFlag obj2)
      {
         return !obj1.Equals(obj2);
      }

      /// <summary>
      /// Prüft diese Instanz und ein Objekt auf Gleichheit.
      /// </summary>
      /// <param name="obj">Das Objekt, mit dem diese Instanz verglichen wird.</param>
      /// <returns>True, wenn das Objekt dieser Instanz gleicht, sonst false.</returns>
      public override bool Equals(object obj)
      {
         if (obj == null || this.GetType() != obj.GetType())
         {
            return false;
         }

         ServiceFailureActionsFlag other = (ServiceFailureActionsFlag)obj;
         return this.FailureActionsOnNonCrashFailures == other.FailureActionsOnNonCrashFailures;
      }

      /// <summary>
      /// Berechnet den Hashcode dieser Instanz.
      /// </summary>
      /// <returns>Der Hashcode dieser Instanz wird zurückgegeben.</returns>
      public override int GetHashCode()
      {
         return this.FailureActionsOnNonCrashFailures.GetHashCode();
      }
   }

   /// <summary>
   /// Informationen über den SID-typ des Dienstes. Ein Änderung wird erst nach dem Neustart des Systems übernommen.
   /// </summary>
   public struct ServiceSidInfo
   {
      /// <summary>
      /// Der SID-Typ des Dienstes.
      /// </summary>
      public int ServiceSidType { get; set; }

      /// <summary>
      /// Prüft zwei <see cref="ServiceSidInfo"/>-Instanzen auf Gleichheit.
      /// </summary>
      /// <param name="obj1">Die erste <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <param name="obj2">Die zweite <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <returns>True, wenn die Instanzen gleich sind, sonst false.</returns>
      public static bool operator ==(ServiceSidInfo obj1, ServiceSidInfo obj2)
      {
         return obj1.Equals(obj2);
      }

      /// <summary>
      /// Prüft zwei <see cref="ServiceSidInfo"/>-Instanzen auf Ungleichheit.
      /// </summary>
      /// <param name="obj1">Die erste <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <param name="obj2">Die zweite <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <returns>True, wenn die Instanzen ungleich sind, sonst false.</returns>
      public static bool operator !=(ServiceSidInfo obj1, ServiceSidInfo obj2)
      {
         return !obj1.Equals(obj2);
      }

      /// <summary>
      /// Prüft diese Instanz und ein Objekt auf Gleichheit.
      /// </summary>
      /// <param name="obj">Das Objekt, mit dem diese Instanz verglichen wird.</param>
      /// <returns>True, wenn das Objekt dieser Instanz gleicht, sonst false.</returns>
      public override bool Equals(object obj)
      {
         if (obj == null || this.GetType() != obj.GetType())
         {
            return false;
         }

         ServiceSidInfo other = (ServiceSidInfo)obj;
         return this.ServiceSidType == other.ServiceSidType;
      }

      /// <summary>
      /// Berechnet den Hashcode dieser Instanz.
      /// </summary>
      /// <returns>Der Hashcode dieser Instanz wird zurückgegeben.</returns>
      public override int GetHashCode()
      {
         return (int)this.ServiceSidType;
      }
   }

   /// <summary>
   /// Erforderliche Rechte eines Dienstes. Eine Änderung wird erst nach dem Neustart des Systems übernommen.
   /// </summary>
   public struct ServiceRequiredPrivilegesInfo
   {
      /// <summary>
      /// Multi-String, der die Rechte angibt. Elemente von Multi-Strings sind durch '\0' abgetrennt.
      /// </summary>
      public string RequiredPrivileges { get; set; }

      /// <summary>
      /// Prüft zwei <see cref="ServiceSidInfo"/>-Instanzen auf Gleichheit.
      /// </summary>
      /// <param name="obj1">Die erste <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <param name="obj2">Die zweite <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <returns>True, wenn die Instanzen gleich sind, sonst false.</returns>
      public static bool operator ==(ServiceRequiredPrivilegesInfo obj1, ServiceRequiredPrivilegesInfo obj2)
      {
         return obj1.Equals(obj2);
      }

      /// <summary>
      /// Prüft zwei <see cref="ServiceSidInfo"/>-Instanzen auf Ungleichheit.
      /// </summary>
      /// <param name="obj1">Die erste <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <param name="obj2">Die zweite <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <returns>True, wenn die Instanzen ungleich sind, sonst false.</returns>
      public static bool operator !=(ServiceRequiredPrivilegesInfo obj1, ServiceRequiredPrivilegesInfo obj2)
      {
         return !obj1.Equals(obj2);
      }

      /// <summary>
      /// Prüft diese Instanz und ein Objekt auf Gleichheit.
      /// </summary>
      /// <param name="obj">Das Objekt, mit dem diese Instanz verglichen wird.</param>
      /// <returns>True, wenn das Objekt dieser Instanz gleicht, sonst false.</returns>
      public override bool Equals(object obj)
      {
         if (obj == null || this.GetType() != obj.GetType())
         {
            return false;
         }

         ServiceRequiredPrivilegesInfo other = (ServiceRequiredPrivilegesInfo)obj;
         return this.RequiredPrivileges == other.RequiredPrivileges;
      }

      /// <summary>
      /// Berechnet den Hashcode dieser Instanz.
      /// </summary>
      /// <returns>Der Hashcode dieser Instanz wird zurückgegeben.</returns>
      public override int GetHashCode()
      {
         return this.RequiredPrivileges.GetHashCode();
      }
   }

   /// <summary>
   /// Enthält Einstellungen für das Herunterfahren.
   /// </summary>
   public struct ServicePreshutdownInfo
   {
      /// <summary>
      /// Timeout in Sekunden. Der Standardwert ist 3 Minuten.
      /// </summary>
      public int PreshutdownTimeout { get; set; }

      /// <summary>
      /// Prüft zwei <see cref="ServiceSidInfo"/>-Instanzen auf Gleichheit.
      /// </summary>
      /// <param name="obj1">Die erste <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <param name="obj2">Die zweite <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <returns>True, wenn die Instanzen gleich sind, sonst false.</returns>
      public static bool operator ==(ServicePreshutdownInfo obj1, ServicePreshutdownInfo obj2)
      {
         return obj1.Equals(obj2);
      }

      /// <summary>
      /// Prüft zwei <see cref="ServiceSidInfo"/>-Instanzen auf Ungleichheit.
      /// </summary>
      /// <param name="obj1">Die erste <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <param name="obj2">Die zweite <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <returns>True, wenn die Instanzen ungleich sind, sonst false.</returns>
      public static bool operator !=(ServicePreshutdownInfo obj1, ServicePreshutdownInfo obj2)
      {
         return !obj1.Equals(obj2);
      }

      /// <summary>
      /// Prüft diese Instanz und ein Objekt auf Gleichheit.
      /// </summary>
      /// <param name="obj">Das Objekt, mit dem diese Instanz verglichen wird.</param>
      /// <returns>True, wenn das Objekt dieser Instanz gleicht, sonst false.</returns>
      public override bool Equals(object obj)
      {
         if (obj == null || this.GetType() != obj.GetType())
         {
            return false;
         }

         ServicePreshutdownInfo other = (ServicePreshutdownInfo)obj;
         return this.PreshutdownTimeout == other.PreshutdownTimeout;
      }

      /// <summary>
      /// Berechnet den Hashcode dieser Instanz.
      /// </summary>
      /// <returns>Der Hashcode dieser Instanz wird zurückgegeben.</returns>
      public override int GetHashCode()
      {
         return (int)this.PreshutdownTimeout;
      }
   }

   /// <summary>
   /// Aktion, die der ServiceControlManager ausführt.
   /// </summary>
   public struct SCAction
   {
      /// <summary>
      /// Angabe der auszuführenden Aktion.
      /// </summary>
      public SCActionType ActionType { get; set; }

      /// <summary>
      /// Zeitverzögerung in Millisekunden, nach der die Aktion ausgeführt wird.
      /// </summary>
      public int Delay { get; set; }

      /// <summary>
      /// Prüft zwei <see cref="ServiceSidInfo"/>-Instanzen auf Gleichheit.
      /// </summary>
      /// <param name="obj1">Die erste <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <param name="obj2">Die zweite <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <returns>True, wenn die Instanzen gleich sind, sonst false.</returns>
      public static bool operator ==(SCAction obj1, SCAction obj2)
      {
         return obj1.Equals(obj2);
      }

      /// <summary>
      /// Prüft zwei <see cref="ServiceSidInfo"/>-Instanzen auf Ungleichheit.
      /// </summary>
      /// <param name="obj1">Die erste <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <param name="obj2">Die zweite <see cref="ServiceSidInfo"/>-Instanz.</param>
      /// <returns>True, wenn die Instanzen ungleich sind, sonst false.</returns>
      public static bool operator !=(SCAction obj1, SCAction obj2)
      {
         return !obj1.Equals(obj2);
      }

      /// <summary>
      /// Ermittelt einen Zeiger, der auf diese Instanz zeigt.
      /// </summary>
      /// <returns>Der Zeiger dieser Instanz wird zurückgegeben.</returns>
      public IntPtr ToPointer()
      {
         IntPtr alloc = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SCAction)));
         Marshal.StructureToPtr(this, alloc, true);
         return alloc;
      }

      /// <summary>
      /// Prüft diese Instanz und ein Objekt auf Gleichheit.
      /// </summary>
      /// <param name="obj">Das Objekt, mit dem diese Instanz verglichen wird.</param>
      /// <returns>True, wenn das Objekt dieser Instanz gleicht, sonst false.</returns>
      public override bool Equals(object obj)
      {
         if (obj == null || this.GetType() != obj.GetType())
         {
            return false;
         }

         SCAction other = (SCAction)obj;
         return this.Delay == other.Delay && this.ActionType == other.ActionType;
      }

      /// <summary>
      /// Berechnet den Hashcode dieser Instanz.
      /// </summary>
      /// <returns>Der Hashcode dieser Instanz wird zurückgegeben.</returns>
      public override int GetHashCode()
      {
         return this.Delay ^ (int)this.ActionType;
      }
   }
}
