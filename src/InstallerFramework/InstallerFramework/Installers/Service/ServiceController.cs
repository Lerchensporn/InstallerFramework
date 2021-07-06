// <copyright file="ServiceController.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>18.04.2009</date>
// <summary></summary>

namespace InstallerFramework.Installers.Service
{
   using System;
   using System.Runtime.InteropServices;

   /// <summary>
   /// Verschiedene Dienst-Typen.
   /// </summary>
   [Flags]
   public enum ServiceType
   {
      /// <summary>
      /// Ein Kernel-Treiber.
      /// </summary>
      ServiceKernelDriver = 0x00000001,

      /// <summary>
      /// Dateisystem-Treiber, der auch ein Kernel-Treiber ist.
      /// </summary>
      ServiceFileSystemDriver = 0x00000002,

      /// <summary>
      /// Dienst, der als eigener Prozess ausgeführt wird.
      /// </summary>
      ServiceWin32OwnProcess = 0x00000010,

      /// <summary>
      /// Dienst, der einen Prozess mit anderen Diensten gemeinsam nutzt.
      /// </summary>
      ServiceWin32ShareProcess = 0x00000020,

      /// <summary>
      /// Dienst, der auf dem Desktop zugreifen kann.
      /// </summary>
      ServiceInteractiveProcess = 0x00000100,
   }

   /// <summary>
   /// Optionen für den Start des Dienstes.
   /// </summary>
   public enum ServiceStart
   {
      /// <summary>
      /// Ein Gerätetreiber, der vom Boot-Loader gestartet wird, bevor das Betriebssystem geladen wird. 
      /// Dieser Wert kann nur dür Treiber-Dienste verwendet werden.
      /// </summary>
      ServiceBootStart = 0x00000000,

      /// <summary>
      /// Ein Geräte-Treiber, der von der 'IoInitSystem'-Funktion während der Initialisierung des Kernels gestartet wird. 
      /// Dieser Wert kann nur für Treiber-Dienste verwendet werden.
      /// </summary>
      ServiceSystemStart = 0x00000001,

      /// <summary>
      /// Ein Dienst, der bei jedem System-Start automatisch von 'Service Control Manager' gestartet wird.
      /// </summary>        
      ServiceAutoStart = 0x00000002,

      /// <summary>
      /// Ein Dienst, der vom 'Service Control Manager' gestartet wird, wenn ein Prozess die 'StartService'-Funktion aufruft.
      /// </summary>
      ServiceDemandStart = 0x00000003,

      /// <summary>
      /// Ein deaktivierter Dienst, der nicht gestartet werden kann. Startversuche verursachen der Fehler 'ERROR_SERVICE_DISABLED'.
      /// </summary>
      ServiceDisabled = 0x00000004,
   }

   /// <summary>
   /// Schweregrad der Fehler und ausgeführte Aktion, wenn der Start des Dienstes fehlschlägt.
   /// </summary>
   public enum ServiceError
   {
      /// <summary>
      /// Das System ignoriert den Fehler und fährt mit dem Start fort. 
      /// </summary>
      Ignore = 0x00000000,

      /// <summary>
      /// Das System verzeichnet den Fehler in der Ereignisanzeige und fährt mit dem Systemstart fort.
      /// </summary>
      Normal = 0x00000001,

      /// <summary>
      /// Das System verzeichnet den Fehler in der Ereignisanzeige. 
      /// Wenn mit der letzten als funktionierend bekannten Konfiguration gestartet wird, wird weiterhin gestartet.
      /// Anderenfalls wird das System mit der letztem als funktionierend bekannten Konfiguration neu gestartet.
      /// </summary>
      Severe = 0x00000002,

      /// <summary>
      /// Das System verzeichnet den Fehler in der Ereignisanzeige, wenn es möglich ist. 
      /// Wenn mit der letzten als funktionierend bekannten Konfiguration gestartet wird, schlägt der Start fehl.
      /// Anderenfalls wird das System mit der letztem als funktionierend bekannten Konfiguration neu gestartet.
      /// </summary>
      Critical = 0x00000003,
   }

   /// <summary>
   /// Importierte Methoden zum Steuern von Diensten.
   /// </summary>
   internal static class NativeMethods
   {
      /// <summary>
      /// Unvollständiges Enum mit Befehlen an einen Dienst.
      /// </summary>
      [Flags]
      public enum ServiceControl
      {
         /// <summary>
         /// Beenden den Dienst.
         /// </summary>
         STOP = 0x00000001,

         /// <summary>
         /// Hält den Dienst an.
         /// </summary>
         PAUSE = 0x00000002,

         /// <summary>
         /// Setzt den Dienst fort.
         /// </summary>
         CONTINUE = 0x00000003,

         /// <summary>
         /// Fragt den Dienst ab.
         /// </summary>
         INTERROGATE = 0x00000004,

         /// <summary>
         /// Fährt den Dienst herunter.
         /// </summary>
         SHUTDOWN = 0x00000005,
      }

      /// <summary>
      /// Status eines Dienstes.
      /// </summary>
      public enum ServiceState
      {
         /// <summary>
         /// Dienst ist beendet.
         /// </summary>
         SERVICE_STOPPED = 0x00000001,

         /// <summary>
         /// Dienst ist im Startvorgang.
         /// </summary>
         SERVICE_START_PENDING = 0x00000002,

         /// <summary>
         /// Dienst ist im Stopvorgang.
         /// </summary>
         SERVICE_STOP_PENDING = 0x00000003,

         /// <summary>
         /// Dienst wird ausgeführt.
         /// </summary>
         SERVICE_RUNNING = 0x00000004,

         /// <summary>
         /// Dienst ist dabei, fortgesetzt zu werden.
         /// </summary>
         SERVICE_CONTINUE_PENDING = 0x00000005,

         /// <summary>
         /// Dienst ist dabei, angehalten zu werden.
         /// </summary>
         SERVICE_PAUSE_PENDING = 0x00000006,

         /// <summary>
         /// Dienst ist angehalten.
         /// </summary>
         SERVICE_PAUSED = 0x00000007
      }

      /// <summary>
      /// Zugriff auf den Dienst. Bevor der angeforderte Zugriff gewährleistet wird, prüft das System die Berechtigung des anfragenden Prozesses.
      /// </summary>
      [Flags]
      public enum SERVICE_ACCESS : uint
      {
         /// <summary>
         /// Erforderlich, um die 'QueryServiceConfig' und 'QueryServiceConfig2'-Funktionen aufzurufen 
         /// und um so die Dienstkonfiguration abzufragen.
         /// </summary>
         SERVICE_QUERY_CONFIG = 0x00001,

         /// <summary>
         /// Erforderlich, um die 'ChangeServiceConfig' und 'ChangeServiceConfig2'-Funktionen aufrufen zu können 
         /// und um so die Dienstkonfiguration zu ändern.
         /// Weil das dem Aufrufer das Recht gibt, die ausführbare Datei des Dienstes zu verändern, sollte 
         /// es nur Administratorn vorbehalten sein.
         /// </summary>
         SERVICE_CHANGE_CONFIG = 0x00002,

         /// <summary>
         /// Erforderlich, um die 'QueryServiceStatusEx'-Funktion auszurufen und um 
         /// so mit dem Service Control Manager den Status des Dienstes abzufragen.
         /// </summary>
         SERVICE_QUERY_STATUS = 0x00004,

         /// <summary>
         /// Erforderlich, um die 'EnumDependentServices'-Funktion aufzurufen und um so
         /// alle von einem bestimmten Dienst abhängige Dienste aufzuzählen.
         /// </summary>
         SERVICE_ENUMERATE_DEPENDENTS = 0x00008,

         /// <summary>
         /// Erforderlich, um die 'StartService'-Funktion aufzurufen und damit den Dienst zu starten.
         /// </summary>
         SERVICE_START = 0x00010,

         /// <summary>
         /// Erforderlich, um die 'ControlService'-Funktion aufzurufen und damit den Dienst zu beenden.
         /// </summary>
         SERVICE_STOP = 0x00020,

         /// <summary>
         /// Erforderlich, um die 'ControlService'-Funktion aufzurufen und so den Dienst anzuhalten oder fortzusetzen.
         /// </summary>
         SERVICE_PAUSE_CONTINUE = 0x00040,

         /// <summary>
         /// Erforderlich, um die 'ControlService'-Funktion aufzurufen und damit den Dienst anfragt, seinen Status sofort anzugeben.
         /// </summary>
         SERVICE_INTERROGATE = 0x00080,

         /// <summary>
         /// Erforderlich, um die 'ControlService'-Funktion aufzurufen und um so eine benutzerdefinierte Anweisung anzugeben. 
         /// </summary>
         SERVICE_USER_DEFINED_CONTROL = 0x00100,

         /// <summary>
         /// Includes STANDARD_RIGHTS_REQUIRED in addition to all access rights in this table.
         /// </summary>
         SERVICE_ALL_ACCESS = (ACCESS_MASK.STANDARD_RIGHTS_REQUIRED |
             SERVICE_QUERY_CONFIG |
             SERVICE_CHANGE_CONFIG |
             SERVICE_QUERY_STATUS |
             SERVICE_ENUMERATE_DEPENDENTS |
             SERVICE_START |
             SERVICE_STOP |
             SERVICE_PAUSE_CONTINUE |
             SERVICE_INTERROGATE |
             SERVICE_USER_DEFINED_CONTROL),

         /// <summary>
         /// Allgemeine Leseberechtigung.
         /// </summary>
         GENERIC_READ = ACCESS_MASK.STANDARD_RIGHTS_READ |
             SERVICE_QUERY_CONFIG |
             SERVICE_QUERY_STATUS |
             SERVICE_INTERROGATE |
             SERVICE_ENUMERATE_DEPENDENTS,

         /// <summary>
         /// Allgemeine Schreibberechtigung.
         /// </summary>
         GENERIC_WRITE = ACCESS_MASK.STANDARD_RIGHTS_WRITE |
             SERVICE_CHANGE_CONFIG,

         /// <summary>
         /// Allgemeine Berechtigung zum Ausfgühren.
         /// </summary>
         GENERIC_EXECUTE = ACCESS_MASK.STANDARD_RIGHTS_EXECUTE |
             SERVICE_START |
             SERVICE_STOP |
             SERVICE_PAUSE_CONTINUE |
             SERVICE_USER_DEFINED_CONTROL,

         /// <summary>
         /// Required to call the QueryServiceObjectSecurity or
         /// SetServiceObjectSecurity function to access the SACL. The proper
         /// way to obtain this access is to enable the SE_SECURITY_NAME
         /// privilege in the caller's current access token, open the handle
         /// for ACCESS_SYSTEM_SECURITY access, and then disable the privilege.
         /// </summary>
         ACCESS_SYSTEM_SECURITY = ACCESS_MASK.ACCESS_SYSTEM_SECURITY,

         /// <summary>
         /// Required to call the DeleteService function to delete the service.
         /// </summary>
         DELETE = ACCESS_MASK.DELETE,

         /// <summary>
         /// Required to call the QueryServiceObjectSecurity function to query
         /// the security descriptor of the service object.
         /// </summary>
         READ_CONTROL = ACCESS_MASK.READ_CONTROL,

         /// <summary>
         /// Required to call the SetServiceObjectSecurity function to modify
         /// the Dacl member of the service object's security descriptor.
         /// </summary>
         WRITE_DAC = ACCESS_MASK.WRITE_DAC,

         /// <summary>
         /// Required to call the SetServiceObjectSecurity function to modify
         /// the Owner and Group members of the service object's security
         /// descriptor.
         /// </summary>
         WRITE_OWNER = ACCESS_MASK.WRITE_OWNER,
      }

      /// <summary>
      /// Werte für Berechtigungen.
      /// </summary>
      [Flags]
      public enum ACCESS_MASK : uint
      {
         /// <summary>
         /// Berchtigung zum Löschen.
         /// </summary>
         DELETE = 0x00010000,

         /// <summary>
         /// Berechtigung zum Lesen für den Besitzer, Gruppen und DACL (Verwaltung der Berechtigungen).
         /// </summary>
         READ_CONTROL = 0x00020000,

         /// <summary>
         /// Berechtigung zum Schreiben der DACL.
         /// </summary>
         WRITE_DAC = 0x00040000,

         /// <summary>
         /// Berechtigung zum Schreiben für den Besitzer.
         /// </summary>
         WRITE_OWNER = 0x00080000,

         /// <summary>
         /// Berechtigung zum Synchronisieren.
         /// </summary>
         SYNCHRONIZE = 0x00100000,

         /// <summary>
         /// Erforderliche Standardrechte.
         /// </summary>
         STANDARD_RIGHTS_REQUIRED = 0x000f0000,

         /// <summary>
         /// Bestimmte Standardrechte zum Lesen, die sich auf GENERIC_READ beziehen.
         /// </summary>
         STANDARD_RIGHTS_READ = 0x00020000,

         /// <summary>
         /// Bestimmte Standardrechte zum Schreiben, die sich auf GENERIC_WRITE beziehen.
         /// </summary>
         STANDARD_RIGHTS_WRITE = 0x00020000,

         /// <summary>
         /// Bestimmte Standardrechte zum Ausführen, die sich auf GENERIC_EXECUTE beziehen.
         /// </summary>
         STANDARD_RIGHTS_EXECUTE = 0x00020000,

         /// <summary>
         /// Alle Standardrechte.
         /// </summary>
         STANDARD_RIGHTS_ALL = 0x001f0000,

         /// <summary>
         /// Maximale Rechte.
         /// </summary>
         MAXIMUM_ALLOWED = 0x02000000,

         /// <summary>
         /// Allgemeine Rechte zum Lesen.
         /// </summary>
         GENERIC_READ = 0x80000000,

         /// <summary>
         /// Allgemeine Rechte zum Lesen.
         /// </summary>
         GENERIC_WRITE = 0x40000000,

         /// <summary>
         /// Allgemeine Rechte Ausführen.
         /// </summary>
         GENERIC_EXECUTE = 0x20000000,

         /// <summary>
         /// Allgemeine Rechte für Operationen mit einem Objekt (Lesen, Schreiben, Ausführen).
         /// </summary>      
         GENERIC_ALL = 0x10000000,

         /// <summary>
         /// Rechte zum Zugreifen auf die System-Sicherheit.
         /// </summary>
         ACCESS_SYSTEM_SECURITY
      }

      /// <summary>
      /// Berechtigungen für den ServiceControlManager.
      /// </summary>
      [Flags]
      public enum SCM_ACCESS : uint
      {
         /// <summary>
         /// Erforderlich, um sich zum ServiceControlManager zu verbinden.
         /// </summary>
         SC_MANAGER_CONNECT = 0x00001,

         /// <summary>
         /// Erforderlich, um die CreateService()-Funktion aufzurufen und so einen Dienst zu erstellen und ihn zur Datenbank hinzuzufügen.
         /// </summary>
         SC_MANAGER_CREATE_SERVICE = 0x00002,

         /// <summary>
         /// Erforderlich, um die EnumServicesStatusEx-Funktion aufzurufen und damit die Dienste in der Datenbank aufzulisten.
         /// </summary>
         SC_MANAGER_ENUMERATE_SERVICE = 0x00004,

         /// <summary>
         /// Erforderlich, um die LockServiceDatabase-Funktion aufzurufen und so eine Sperrung der Datenbank zu verursachen.
         /// </summary>
         SC_MANAGER_LOCK = 0x00008,

         /// <summary>
         /// Erforderlich, um die QueryServiceLockStatus-Funktion aufzurufen und so die Informationen über den Status der Sperrung für die Datenbank abzurufen.
         /// </summary>
         SC_MANAGER_QUERY_LOCK_STATUS = 0x00010,

         /// <summary>
         /// Erforderlich, um die NotifyBootConfigStatus-Funktion aufzurufen.
         /// </summary>
         SC_MANAGER_MODIFY_BOOT_CONFIG = 0x00020,

         /// <summary>
         /// Enthält STANDARD_RIGHTS_REQUIRED zusätzlich zu allen anderen Rechten.
         /// </summary>
         SC_MANAGER_ALL_ACCESS = ACCESS_MASK.STANDARD_RIGHTS_REQUIRED |
             SC_MANAGER_CONNECT |
             SC_MANAGER_CREATE_SERVICE |
             SC_MANAGER_ENUMERATE_SERVICE |
             SC_MANAGER_LOCK |
             SC_MANAGER_QUERY_LOCK_STATUS |
             SC_MANAGER_MODIFY_BOOT_CONFIG,

         /// <summary>
         /// Allgemeine Leserechte.
         /// </summary>
         GENERIC_READ = ACCESS_MASK.STANDARD_RIGHTS_READ |
             SC_MANAGER_ENUMERATE_SERVICE |
             SC_MANAGER_QUERY_LOCK_STATUS,

         /// <summary>
         /// Allgemeine Rechte zum Ausführen.
         /// </summary>
         GENERIC_WRITE = ACCESS_MASK.STANDARD_RIGHTS_WRITE |
             SC_MANAGER_CREATE_SERVICE |
             SC_MANAGER_MODIFY_BOOT_CONFIG,

         /// <summary>
         /// Allgemeine Rechte zum Ausführen.
         /// </summary>
         GENERIC_EXECUTE = ACCESS_MASK.STANDARD_RIGHTS_EXECUTE |
             SC_MANAGER_CONNECT | SC_MANAGER_LOCK,

         /// <summary>
         /// Allgemeine Rechte, um ein Operation am Objekt durchzuführen.
         /// </summary>
         GENERIC_ALL = SC_MANAGER_ALL_ACCESS,
      }

      /// <summary>
      /// Erstellt einen neuen Dienst.
      /// </summary>
      /// <param name="hSCManager">Handle des zur ServiceControlManager-Datenbank. Das Handle muss die Berechtigung 'SC_MANAGER_CREATE_SERVICE' haben.</param>
      /// <param name="lpServiceName">Name des Dienstes, maximal 256 Zeichen.</param>
      /// <param name="lpDisplayName">Angezeigter Name des Dienstes, maximal 256 Zeichen.</param>
      /// <param name="dwDesiredAccess">Zugriffsrechte, mit denen der Dienst erstellt wird.</param>
      /// <param name="dwServiceType">Typ des Dienstes.</param>
      /// <param name="dwStartType">Starttyp des Dienstes.</param>
      /// <param name="dwErrorControl">Ausgeführte Aktionen, wenn das Laden des Dienstes fehlschlägt.</param>
      /// <param name="lpBinaryPathName">Vollständiger Pfad zur ausführbaren Datei des Dienstes. Wenn der Pfad Leerzeichen enthält, muss die Pfadangabe in Anführungszeichen eingeschlossen werden.</param>
      /// <param name="lpLoadOrderGroup">Start-Gruppe des Dienstes oder null.</param>
      /// <param name="lpdwTagId">Nummer des Dienststarts oder null.</param>
      /// <param name="lpDependencies">Dienste, von denen der zu erstellende Dienst abhängig ist.</param>
      /// <param name="lpServiceStartName">Das Konto, unter dem der Dienst läuft. Mögliche Werte sind NULL (LocalSystem), 'NT AUTHORITY\LocalService' (LocalService), 'NT AUTHORITY\NetworkService' (NetworkService) oder ein Benutzeraccount. Für Dienste mit der Eigenschaft 'SERVICE_INTERACTIVE_PROCESS' muss dieser Parameter null sein.</param>
      /// <param name="lpPassword">Passwort, wenn der Dienst unter einem passwortgeschütztem Konto läuft, sonst null.</param>
      /// <returns>Das Handle des Dienstes, wenn der Dienst erfolgreich erstellt wurde, sonst NULL. Mit GetLastError() kann im Fehlerfall der FehlerCode geholt werden.</returns>
      [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
      public static extern IntPtr CreateService(IntPtr hSCManager, string lpServiceName, string lpDisplayName, uint dwDesiredAccess, uint dwServiceType, uint dwStartType, uint dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, string lpdwTagId, string lpDependencies, string lpServiceStartName, string lpPassword);

      /// <summary>
      /// Lässt den Dienst eine Aktion ausführen.
      /// </summary>
      /// <param name="hService">Handle des Dienstes. Die Rechte dieses Handles müssen ausreichen, um die Aktion 'dwControl' auszuführen.</param>
      /// <param name="dwControl">Die auszuführende Aktion. Die Werte 128 bis 255 sind benutzerdefinierte Aktionen, die mit der Berechtigung 'SERVICE_USER_DEFINED_CONTROL' ausgeführt werden können.</param>
      /// <param name="lpServiceStatus">Der Status des Dienstes wird ausgegeben.</param>
      /// <returns>True, wenn die Methode erfolgreich ausgeführt wurde, sonst false. Im Fehlerfall kann der Fehler mit GetLastError() geholt werden.</returns>
      [DllImport("advapi32.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool ControlService(IntPtr hService, ServiceControl dwControl, ref SERVICE_STATUS lpServiceStatus);

      /// <summary>
      /// Stellt eine Verbindung zum ServiceControlManager her un öffnet die anzugebende Dienst-Datenbank.
      /// </summary>
      /// <param name="machineName">Name des Computers oder NULL als Ersatz für 'localhost'.</param>
      /// <param name="databaseName">Name der ServiceControlManager-Datenban. Der Standardwert ist 'SERVICES_ACTIVE_DATABASE', er wird verwendet, wenn der Parameter NULL ist.</param>
      /// <param name="dwAccess">Zugriff zum ServiceControlManager. Er sollte mindestens 'SC_MANAGER_CONNECT' sein.</param>
      /// <returns>Handle zur ServiceControlManager-Datenbank.</returns>
      [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
      public static extern IntPtr OpenSCManager(string machineName, string databaseName, uint dwAccess);

      /// <summary>
      /// Öffnet einen existierenden Dienst. 
      /// </summary>
      /// <param name="hSCManager">Handle zur ServiceControlManager-Datenbank, das mit OpenSCManager() geholt werden kann.</param>
      /// <param name="lpServiceName">Name des Dienstes.</param>
      /// <param name="dwDesiredAccess">Zugriff zum Dienst.</param>
      /// <returns>Handle zum Dienst, im Fehlerfall NULL.</returns>
      [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
      public static extern IntPtr OpenService(IntPtr hSCManager, [MarshalAs(UnmanagedType.LPWStr)]string lpServiceName, uint dwDesiredAccess);

      /// <summary>
      /// Ändert optionale Eigenschaften eines Dienstes.
      /// </summary>
      /// <param name="hService">Handle des Dienstes.</param>
      /// <param name="dwInfoLevel">Angabe der zu ändernden Eigenschaft.</param>
      /// <param name="lpInfo">Informationen über die Eigenschaft (optional).</param>
      /// <returns>True, wenn die Methode erfolgreich ausgeführt wurde, sonst false.</returns>
      [DllImport("advapi32.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool ChangeServiceConfig2(IntPtr hService, uint dwInfoLevel, IntPtr lpInfo);

      /// <summary>
      /// Schließt ein Handle zum ServiceControlManager oder zu einem Dienst.
      /// </summary>
      /// <param name="hSCObject">Das Handle des ServiceControlManagers, das mit OpenSCManager geholt wird, oder das Handle des Dienstes, das mit OpenService() oder CreateService() geholt werden kann.</param>
      /// <returns>True, wenn das Handle erfolgreich geschlossen wurde, sonst false.</returns>
      [DllImport("advapi32.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool CloseServiceHandle(IntPtr hSCObject);

      /// <summary>
      /// Ruft den Status des Dientes ab.
      /// </summary>
      /// <param name="hService">Handle zum Dienst.</param>
      /// <param name="dwServiceStatus">Der Status des Dienstes wird ausgegeben.</param>
      /// <returns>True, wenn die Methode erfolgreich ausgeführt wurde, sonst false.</returns>
      [DllImport("advapi32.dll", EntryPoint = "QueryServiceStatus", CharSet = CharSet.Auto)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool QueryServiceStatus(IntPtr hService, ref SERVICE_STATUS dwServiceStatus);

      /// <summary>
      /// Löscht einen Dienst, wenn er gerade nicht ausgeführt wird.
      /// </summary>
      /// <param name="hService">Das Handle des Dienstes, es muss die Berechtigung 'DELETE' haben.</param>
      /// <returns>True, wenn das Ausführen der Methode erfolgreich war, sonst false. Im Fehlerfall kann der Fehlercode mit GetLastError() geholt werden.</returns>
      [DllImport("advapi32.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool DeleteService(IntPtr hService);

      /// <summary>
      /// Status eines Dienstes.
      /// </summary>
      [StructLayout(LayoutKind.Sequential)]
      public struct SERVICE_STATUS
      {
         /// <summary>
         /// Typ des Dienstes.
         /// </summary>
         public ServiceType dwServiceType;

         /// <summary>
         /// Status des Dienstes.
         /// </summary>
         public ServiceState dwCurrentState;

         /// <summary>
         /// Akzeptierte Controls.
         /// </summary>
         public int dwControlsAccepted;

         /// <summary>
         /// Exit-Code des Dienstes für das System.
         /// </summary>
         public int dwWin32ExitCode;

         /// <summary>
         /// Exit-Code des Dienstes.
         /// </summary>
         public int dwServiceSpecificExitCode;

         /// <summary>
         /// Die CheckPoint-Eigenschaft.
         /// </summary>
         public int dwCheckPoint;

         /// <summary>
         /// Die WaitHint-Eigenschaft.
         /// </summary>
         public int dwWaitHint;
      }
   }
}
