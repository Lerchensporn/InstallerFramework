// <copyright file="StepCounter.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>11.04.2009</date>
// <summary></summary>

namespace InstallerFramework.Base
{
   using System;
   using System.Collections.Generic;

   /// <summary>
   /// Zählt die Fortschrittsstufen eines Installationsvorgangs.
   /// </summary>
   public class StepCounter
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="StepCounter"/>-Klasse.
      /// </summary>
      public StepCounter()
      {
         this.Installers = new List<AdvancedInstaller>();
         this.MaximumSteps = StepInfo.Nothing;
      }

      /// <summary>
      /// Tritt ein, wenn sich die aktuelle Stufe verändert.
      /// </summary>
      public event EventHandler<StepChangedEventArgs> StateChanged;

      /// <summary>
      /// Gibt an oder legt fest, von welchen Installern die Stufen gezählt werden.
      /// </summary>
      public IList<AdvancedInstaller> Installers { get; private set; }

      /// <summary>
      /// Gibt an oder legt fest, welche die maximale Stufenanzahl ist.
      /// </summary>
      public StepInfo MaximumSteps { get; set; }

      /// <summary>
      /// Gibt an, bei welcher Stufe der Installations-Vorgang ist.
      /// </summary>
      public int CurrentStep { get; private set; }

      /// <summary>
      /// Startet das Zählen der Stufen und ermittelt die maximale Anzahl der Stufen (StepCounter.MaximumSteps).
      /// </summary>
      public void Start()
      {
         StepInfo maxstep = StepInfo.Nothing;
         foreach (AdvancedInstaller item in this.GetAllInstallers(this.Installers))
         {
            item.StepChanged += new System.EventHandler<StepChangedEventArgs>(this.Changed);
            maxstep += item.MaxSteps;
         }

         this.MaximumSteps = maxstep;
      }

      /// <summary>
      /// Ermittelt rekursiv alle Installer.
      /// </summary>
      /// <param name="inst">Ein <see cref="AdvancedInstaller"/>-Array, in dem alle Installer gesucht werden.</param>
      /// <returns>Die gefundenen Installer werden zurückgegeben.</returns>
      public IList<AdvancedInstaller> GetAllInstallers(IList<AdvancedInstaller> inst)
      {
         List<AdvancedInstaller> list = new List<AdvancedInstaller>();
         foreach (AdvancedInstaller item in inst)
         {
            list.Add(item);
            if (item.Installers.Count != 0)
            {
               list.AddRange(this.GetAllInstallers(item.Installers));
            }
         }

         return list;
      }

      /// <summary>
      /// Ruft das <see cref="StateChanged"/>-Event auf.
      /// </summary>
      protected void OnStateChanged()
      {
         EventHandler<StepChangedEventArgs> handli = this.StateChanged;
         if (handli != null)
         {
            handli(this, new StepChangedEventArgs(this.CurrentStep));
         }
      }

      /// <summary>
      /// Wird ausgeführt, wenn sich die Stufe eines Installers ändert.
      /// </summary>
      /// <param name="sender">Der Installer, dessen Stufe sich änderte (die Ereignisquelle).</param>
      /// <param name="e">Informationen über das Ereignis.</param>
      private void Changed(object sender, StepChangedEventArgs e)
      {
         this.CurrentStep += ((AdvancedInstaller)sender).CurrentSteps - e.OldStep;
         this.OnStateChanged();
      }
   }
}
