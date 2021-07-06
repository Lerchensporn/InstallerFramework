// <copyright file="AdvancedInstaller.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>13.04.2009</date>
// <summary></summary>

namespace InstallerFramework.Base
{
   using System;
   using System.Collections;
   using System.Collections.Generic;

   /// <summary>
   /// Basisklasse für die Installation von Software.
   /// </summary>
   public abstract class AdvancedInstaller
   {
      /// <summary>
      /// Die aktuelle Stufe.
      /// </summary>
      private int currentSteps;

      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="AdvancedInstaller"/>-Klasse.
      /// </summary>
      protected AdvancedInstaller()
      {
         this.Initialize();
      }

      /// <summary>
      /// Tritt ein, wenn sich die aktuelle Stufe des Installers ändert.
      /// </summary>
      public event EventHandler<StepChangedEventArgs> StepChanged;

      /// <summary>
      /// Tritt ein, wenn ein untergeordneter Installer einen Fehler verursacht.
      /// </summary>
      public event EventHandler<InstallExceptionEventArgs> InstallException;

      /// <summary>
      /// Gibt an oder legt fest, wie viele Stufen der Installer insgesamt ausführt.
      /// </summary>
      public StepInfo MaxSteps { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welcher <see cref="InstallLog"/> für das Aufzeichnen des Installationsvorgang verwendet wird.
      /// </summary>
      public InstallLog Log { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, ob Ausnahmen geworfen werden. Der Standardwert ist true.
      /// </summary>
      public bool ThrowExceptions { get; set; }

      /// <summary>
      /// Gibt an, bei welcher Stufe die Ausführung des Installers ist. 
      /// </summary>
      public int CurrentSteps
      {
         get
         {
            return this.currentSteps;
         }

         protected set
         {
            if (this.currentSteps != value)
            {
               int oldStep = this.currentSteps;
               this.currentSteps = value;
               this.OnStepChanged(oldStep);
            }
         }
      }

      /// <summary>
      /// Gibt an oder legt fest, welche Installer ausgeführt werden.
      /// </summary>
      public IList<AdvancedInstaller> Installers { get; private set; }

      /// <summary>
      /// Gibt an oder legt fest, was passiert, wenn die Installation fehlschlägt. Der Standard-Wert ist 'TransactedInstallMode.RollbackAllOnError'.
      /// </summary>
      public TransactedInstallMode TransactedInstall { get; set; }

      /// <summary>
      /// Führt die Installation aus. 
      /// </summary>
      /// <param name="stateSaver">Ein <see cref="System.Collections.IDictionary"/>, in dem die zum Ausführen eines Commit-, Rollback- oder Deinstallationsvorgangs erforderlichen Daten gespeichert werden.</param>
      public virtual void Install(System.Collections.IDictionary stateSaver)
      {
         this.Log.LogMessage(" Untergeordnete Installationsvorgänge werden ausgeführt...");
         this.CurrentSteps = 0;
         IDictionary[] dictio = new Hashtable[this.Installers.Count];
         for (int i = 0; i < dictio.Length; i++)
         {
            dictio[i] = new Hashtable();
         }

         stateSaver.Add("_reserved_NestedSavedState", dictio);

         for (int i = 0; i < this.Installers.Count; i++)
         {
            this.Installers[i].Log = this.Log;

            try
            {
               this.Installers[i].Install(dictio[i]);
            }
            catch (Exception ex)
            {
               this.Log.LogMessage("  FEHLER bei der Ausführung der untergeordneten Installationsvorgänge: " + ex.Message + "StackTrace: " + ex.StackTrace);
               this.OnInstallException(ex);
               if (this.TransactedInstall == TransactedInstallMode.RollbackAllOnError)
               {
                  this.Log.LogMessage("  Das Rollback wird wegen eines Fehlers für die gesamte Installation gestartet...");
                  this.Uninstall(stateSaver);
               }
               else if (this.TransactedInstall == TransactedInstallMode.RollbackOneOnError)
               {
                  this.Log.LogMessage(" Das Rollback wird wegen eines Fehlers für die fehlerhafte Installation gestartet...");
                  IDictionary[] nestedSavedState = (IDictionary[])stateSaver["_reserved_NestedSavedState"];
                  this.Installers[i].Uninstall(nestedSavedState[i]);
               }

               if (this.ThrowExceptions)
               {
                  throw new InstallException("Eine Ausnahme im untergeordneten Installer ist aufgetreten.", ex);
               }
            }
         }

         this.Log.LogMessage(" Untergeordnete Installationsvorgänge werden abgeschlossen...");
      }

      /// <summary>
      /// Führt die Deinstallation aus.
      /// </summary>
      /// <param name="savedState">Ein <see cref="System.Collections.IDictionary"/> mit Informationen über den Zustand, in dem sich der Computer nach Abschluss der Installation befindet.</param>
      public virtual void Uninstall(IDictionary savedState)
      {
         this.Log.LogMessage(" Untergeordnete Deinstallationsvorgänge werden ausgeführt...");
         this.CurrentSteps = 0;
         IDictionary[] nestedSavedState = (IDictionary[])savedState["_reserved_NestedSavedState"];

         for (int i = 0; i < this.Installers.Count; i++)
         {
            this.Installers[i].Log = this.Log;
            try
            {
               this.Installers[i].Uninstall(nestedSavedState[i]);
            }
            catch (Exception ex)
            {
               this.Log.LogMessage("  FEHLER bei der Ausführung der untergeordneten Deinstallationsvorgänge: " + ex.Message + "StackTrace: " + ex.StackTrace);
               this.OnInstallException(ex);
            }
         }

         this.Log.LogMessage(" Untergeordnete Deinstallationsvorgänge werden abgeschlossen.");
      }

      /// <summary>
      /// Ruft das <see cref="StepChanged"/>-Event auf.
      /// </summary>
      /// <param name="oldStep">Die vorherige Stufe.</param>
      protected virtual void OnStepChanged(int oldStep)
      {
         EventHandler<StepChangedEventArgs> handli = this.StepChanged;
         if (handli != null)
         {
            handli(this, new StepChangedEventArgs(oldStep));
         }
      }

      /// <summary>
      /// Ruft das <see cref="InstallException"/>-Event auf.
      /// </summary>
      /// <param name="ex">Die gefangene Ausnahme.</param>
      protected virtual void OnInstallException(Exception ex)
      {
         EventHandler<InstallExceptionEventArgs> ev = this.InstallException;
         if (ev != null)
         {
            ev(this, new InstallExceptionEventArgs(ex));
         }
      }

      /// <summary>
      /// Initialisiert die Felder und Eigenschaften.
      /// </summary>
      protected void Initialize()
      {
         this.TransactedInstall = TransactedInstallMode.RollbackAllOnError;
         this.MaxSteps = StepInfo.Nothing;
         this.Installers = new List<AdvancedInstaller>();
         this.Log = new InstallLog();
         this.ThrowExceptions = true;
      }
   }
}
