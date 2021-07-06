// <copyright file="ServiceInstaller.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>18.04.2009</date>
// <summary></summary>

namespace InstallerFramework.Installers.Service
{
   using System;
   using System.Collections.Generic;
   using System.Runtime.InteropServices;
   using System.Security.Permissions;
   using System.Text;
   using InstallerFramework.Base;

   /// <summary>
   /// Installiert Dienste oder Treiber auf dem System.
   /// </summary>
   public class ServiceInstaller : AdvancedInstaller
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="ServiceInstaller"/>-Klasse.
      /// </summary>
      public ServiceInstaller()
      {
         this.ServiceType = ServiceType.ServiceWin32OwnProcess;
         this.ServiceError = ServiceError.Normal;
         this.Account = new ServiceAccount(StandardAccount.LocalSystem);
         this.ServicesDependedOn = new List<string>();
         this.AdditionalProperties = new ServiceProperties();
         this.MaxSteps = new StepInfo(5, 4);
      }

      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="ServiceInstaller"/>-Klasse und legt alle erforderlichen Eigenschaften des Dienstes fest.
      /// </summary>
      /// <param name="displayName">Angezeigter Name des Dienstes.</param>
      /// <param name="serviceName">Name des Dienstes.</param>
      /// <param name="targetPathName">Name zur ausführbaren Datei des Dienstes.</param>
      /// <param name="startType">Start-Modus des Dienstes.</param>
      /// <param name="account">Account, unter dem der Dienst läuft.</param>
      public ServiceInstaller(string displayName, string serviceName, string targetPathName, ServiceStart startType, ServiceAccount account)
         : this()
      {
         this.DisplayName = displayName;
         this.Account = account;
         this.ServiceName = serviceName;
         this.StartType = startType;
         this.TargetPathName = targetPathName;
      }

      /// <summary>
      /// Erweiterte Diensteigenschaften.
      /// </summary>
      public ServiceProperties AdditionalProperties { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welcher der angezeigte Name des Dienstes ist.
      /// </summary>
      public string DisplayName { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, wie der Name des Dienstes lautet.
      /// </summary>
      public string ServiceName { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, von welchen anderen Diensten der Dienst abhängt.
      /// </summary>
      public IList<string> ServicesDependedOn { get; private set; }

      /// <summary>
      /// Gibt an oder legt fest, bei welchen Host der Dienst ausgeführt wird. Diese Eigenschaft kann ignoriert werden.
      /// </summary>
      public string MachineName { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, wie der Name der Dienst-Datenbank lautet. Diese Eigenschaft ist nicht erforderlich.
      /// </summary>
      public string DataBaseName { get; set; }

      /// <summary>
      /// Gibt oder oder legt fest, welchen Typ der Dienst hat. Der Standardwert ist 'ServiceType.ServiceWin32OwnProcess'.
      /// </summary>
      public ServiceType ServiceType { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welche Aktionen durchgeführt werden, wenn ein Fehler im Dienst auftritt. Der Standardwert ist 'ServiceError.Normal'.
      /// </summary>
      public ServiceError ServiceError { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welches der Pfad der ausführbaren Datei des Dienstes ist.
      /// </summary>
      public string TargetPathName { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, wie der Dienst beim Start des Systems gestartet wird.
      /// </summary>
      public ServiceStart StartType { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welches der Anmeldeaccount des Dienstes ist. Der Standardwert ist 'LocalSystem'.
      /// </summary>
      public ServiceAccount Account { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welche die Gruppe des Dienstes ist. Diese Eigenschaft ist nicht erforderlich.
      /// </summary>
      public string LoadOrderGroup { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welche Tag-Id der Dienst hat. Diese Eigenschaft ist nicht erforderlich.
      /// </summary>
      public string TagId { get; set; }

      /// <summary>
      /// Installiert den Dienst auf dem System.
      /// </summary>
      /// <param name="stateSaver">Ein <see cref="System.Collections.IDictionary"/>, in dem die zum Ausführen eines Commit-, Rollback- oder Deinstallationsvorgangs erforderlichen Daten gespeichert werden.</param>
      [SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
      public override void Install(System.Collections.IDictionary stateSaver)
      {
         this.Log.LogMessage("ServiceInstaller hat die Installation gestartet.");
         base.Install(stateSaver);
         if (this.TargetPathName == null)
         {
            this.Log.LogMessage(" Es ist ein Fehler im Installer aufgetreten: Die TargetPathName-Eigenschaft ist null. Der Installer wurde vom Hersteller falsch konfiguriert.");
            throw new InstallException("AdvancedInstaller.TargetPathName ist null.");
         }

         if (this.ServiceName == null)
         {
            this.Log.LogMessage(" Es ist ein Fehler im Installer aufgetreten: Die ServiceName-Eigenschaft ist null. Der Installer wurde vom Hersteller falsch konfiguriert.");
            throw new InstallException("AdvancedInstaller.ServiceName ist null.");
         }

         if (this.DisplayName == null)
         {
            this.Log.LogMessage(" Es ist ein Fehler im Installer aufgetreten: Die DisplayName-Eigenschaft ist null. Der Installer wurde vom Hersteller falsch konfiguriert.");
            throw new InstallException("AdvancedInstaller.DisplayName ist null");
         }

         this.Log.LogMessage(" Der Dienst '" + this.ServiceName + "' wird installiert...");
         StringBuilder sb;
         if (this.ServicesDependedOn != null && this.ServicesDependedOn.Count > 0)
         {
            sb = new StringBuilder();
            foreach (string item in this.ServicesDependedOn)
            {
               sb.Append(item);
               sb.Append('\0');
            }
         }
         else
         {
            sb = null;
         }

         IntPtr handle = new IntPtr();
         IntPtr hservice = new IntPtr();
         IntPtr alloc = new IntPtr();

         this.CurrentSteps = 1;

         try
         {
            this.Log.LogMessage(" Der ServiceControlManager wird geöffnet...");
            handle = NativeMethods.OpenSCManager(this.MachineName, this.DataBaseName, (uint)InstallerFramework.Installers.Service.NativeMethods.SERVICE_ACCESS.GENERIC_WRITE);
            if (handle.ToInt32() < 1)
            {
               this.Log.LogMessage(" FEHLER: Der ServiceControlManager kann nicht geöffnet werden.");
               throw new InstallerFramework.Base.InstallException("Kann den ServiceControlManager nicht öffnen.");
            }

            this.CurrentSteps = 2;

            this.Log.LogMessage(" Der Dienst wird erstellt...");
            hservice = NativeMethods.CreateService(
                handle,
                this.ServiceName,
                this.DisplayName,
                (uint)InstallerFramework.Installers.Service.NativeMethods.SERVICE_ACCESS.SERVICE_ALL_ACCESS,
                (uint)this.ServiceType,
                (uint)this.StartType,
                (uint)this.ServiceError,
                this.TargetPathName,
                this.LoadOrderGroup,
                this.TagId,
                sb == null ? null : sb.ToString(),
                this.Account.AccountToken,
                this.Account.Password);

            if (handle.ToInt32() < 1)
            {
               this.Log.LogMessage(" FEHLER: Der Dienst kann nicht erstellt werden.");
               throw new InstallException("Kann keinen Dienst erstellen.");
            }

            stateSaver.Add("name", this.ServiceName);
            stateSaver.Add("machine", this.MachineName);
            stateSaver.Add("database", this.DataBaseName);

            this.CurrentSteps = 3;
            this.Log.LogMessage(" Die Diensteigenschaften werden festgelegt...");
            this.SetServiceProperties(hservice);
            this.CurrentSteps = 4;
         }
         catch
         {
            throw;
         }
         finally
         {
            this.Log.LogMessage(" Die Handles werden geschlossen...");
            if (alloc.ToInt32() > 0)
            {
               Marshal.FreeHGlobal(alloc);
            }

            if (handle.ToInt32() > 0)
            {
               NativeMethods.CloseServiceHandle(handle);
            }

            if (hservice.ToInt32() > 0)
            {
               NativeMethods.CloseServiceHandle(hservice);
            }
         }

         this.Log.LogMessage("ServiceInstaller hat die Installation abgeschlossen.");
         this.CurrentSteps = 5;
      }

      /// <summary>
      /// Deinstalliert den Service.
      /// </summary>
      /// <param name="savedState">Ein <see cref="System.Collections.IDictionary"/> mit Informationen über den Zustand, in dem sich der Computer nach Abschluss der Installation befindet.</param>
      public override void Uninstall(System.Collections.IDictionary savedState)
      {
         this.Log.LogMessage("ServiceInstaller hat die Deinstallation gestartet.");
         base.Uninstall(savedState);
         this.Log.LogMessage(" Der Dienst '" + (string)savedState["name"] + "' wird deinstalliert...");
         IntPtr scmHandle = NativeMethods.OpenSCManager((string)savedState["machine"], (string)savedState["database"], (uint)NativeMethods.SCM_ACCESS.SC_MANAGER_ALL_ACCESS);

         this.CurrentSteps = 1;
         IntPtr serviceHandle = NativeMethods.OpenService(scmHandle, (string)savedState["name"], (uint)NativeMethods.SERVICE_ACCESS.SERVICE_ALL_ACCESS);
         this.CurrentSteps = 2;

         NativeMethods.SERVICE_STATUS status = new NativeMethods.SERVICE_STATUS();
         if (NativeMethods.QueryServiceStatus(serviceHandle, ref status))
         {
            if (status.dwCurrentState != NativeMethods.ServiceState.SERVICE_STOPPED)
            {
               this.Log.LogMessage(" Der Dienst wird beendet...");
               NativeMethods.ControlService(serviceHandle, NativeMethods.ServiceControl.STOP, ref status);
            }
         }

         this.CurrentSteps = 3;

         this.Log.LogMessage(" Der Dienst wird entfernt...");
         NativeMethods.DeleteService(serviceHandle);
         NativeMethods.CloseServiceHandle(scmHandle);
         this.Log.LogMessage("ServiceInstaller hat die Deinstallation abgeschlossen.");
         this.CurrentSteps = 4;
      }

      /// <summary>
      /// Wandelt ein Objekt in einen Zeiger um.
      /// </summary>
      /// <param name="obj">Das Objekt, dessen Zeiger bestimmt wird.</param>
      /// <returns>Der Zeiger zum Objekt wird zurückgegeben.</returns>
      private static IntPtr GetPointer(object obj)
      {
         IntPtr alloc = Marshal.AllocHGlobal(obj.GetType().StructLayoutAttribute.Size);
         Marshal.StructureToPtr(obj, alloc, false);
         return alloc;
      }

      /// <summary>
      /// Setzt von einem Zeiger genutztem Speicher zurück.
      /// </summary>
      /// <param name="pointer">Zeiger zu einer Struktur.</param>
      private static void Free(IntPtr pointer)
      {
         if (pointer.ToInt32() > 0)
         {
            Marshal.FreeHGlobal(pointer);
         }
      }

      /// <summary>
      /// Setzt alle erweitereten Diensteigenschaften.
      /// </summary>
      /// <param name="serviceHandle">Handle zum Dienst, dessen Eigenschaften eingestellt werden.</param>
      private void SetServiceProperties(IntPtr serviceHandle)
      {
         this.SetProperty(serviceHandle, InfoLevel.ServiceConfigDelayedAutoStartInfo, this.AdditionalProperties.DelayedAutoStartInfo);
         this.SetProperty(serviceHandle, InfoLevel.ServiceConfigDescription, this.AdditionalProperties.Description);
         this.SetProperty(serviceHandle, InfoLevel.ServiceConfigFailureActions, this.AdditionalProperties.FailureActions);
         this.SetProperty(serviceHandle, InfoLevel.ServiceConfigFailureActionsFlag, this.AdditionalProperties.FailureActionsFlag);
         this.SetProperty(serviceHandle, InfoLevel.ServiceConfigPreshutdownInfo, this.AdditionalProperties.PreshutdownInfo);
         this.SetProperty(serviceHandle, InfoLevel.ServiceConfigRequiredPrivilegesInfo, this.AdditionalProperties.RequiredPrivilegesInfo);
         this.SetProperty(serviceHandle, InfoLevel.ServiceConfigServiceSidInfo, this.AdditionalProperties.SidInfo);
      }

      /// <summary>
      /// Setzt eine Diensteigenschaft.
      /// </summary>
      /// <param name="serviceHandle">Handle des Dienstes, dessen Eigenschaften festgelegt werden.</param>
      /// <param name="level">Eigenschaft, die festgelegt wird.</param>
      /// <param name="obj">Wert der festzulegenden Eigenschaft.</param>
      private void SetProperty(IntPtr serviceHandle, InfoLevel level, object obj)
      {
         if (this.AdditionalProperties.RequiredPrivilegesInfo != null && serviceHandle.ToInt32() > 0)
         {
            IntPtr propertyHandle = ServiceInstaller.GetPointer(obj);
            NativeMethods.ChangeServiceConfig2(serviceHandle, (uint)level, propertyHandle);
            ServiceInstaller.Free(propertyHandle);
         }
      }
   }
}