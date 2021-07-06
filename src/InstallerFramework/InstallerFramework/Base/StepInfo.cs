// <copyright file="StepInfo.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>11.04.2009</date>
// <summary></summary>

namespace InstallerFramework.Base
{
   /// <summary>
   /// Verwaltet die Stufenanzahl des Fortschritts von Installationsvorgängen.
   /// </summary>
   public class StepInfo
   {      
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="StepInfo"/>Klasse.
      /// </summary>
      /// <param name="install">Die Anzahl der Stufen für die Installation.</param>
      /// <param name="uninstall">Die Anzahl der Stufen für Deinstallation.</param>
      public StepInfo(int install, int uninstall)
      {
         this.InstallSteps = install;
         this.UninstallSteps = uninstall;
      }
      
      /// <summary>
      /// Gibt eine <see cref="StepInfo"/>-Instanz an, deren Anzahl an Stufen null ist.
      /// </summary>
      public static StepInfo Nothing
      {
         get { return new StepInfo(0, 0); }
      }

      /// <summary>
      /// Gibt an oder legt fest, wie groß die Anzahl der Stufen für die Installation ist.
      /// </summary>
      public int InstallSteps { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, wie groß die Anzahl der Stufen für die Deinstallation ist.
      /// </summary>
      public int UninstallSteps { get; set; }

      /// <summary>
      /// Addiert die Stufenanzahl zweier <see cref="StepInfo"/>-Instanzen.
      /// </summary>
      /// <param name="s1">Der erste Summand.</param>
      /// <param name="s2">Der zweite Summand.</param>
      /// <returns>Eine <see cref="StepInfo"/>-Instanz, deren Stufenanzahl der Summe der Stufenanzahlen der beiden Summanden entspricht.</returns>
      public static StepInfo operator +(StepInfo s1, StepInfo s2)
      {
         return s1.Add(s2);
      }

      /// <summary>
      /// Subtrahiert die Stufenanzahl zweier <see cref="StepInfo"/>-Instanzen.
      /// </summary>
      /// <param name="s1">Der Minuend.</param>
      /// <param name="s2">Der Subtrahend.</param>
      /// <returns>Eine <see cref="StepInfo"/>-Instanz, deren Stufenanzahl der Differenz der Stufenanzahlen von Minuend und Subtrahend entspricht.</returns>
      public static StepInfo operator -(StepInfo s1, StepInfo s2)
      {
         return s1.Subtract(s2);
      }

      /// <summary>
      /// Prüft zwei StepInfo-Instanzen auf Gleichheit.
      /// </summary>
      /// <param name="s1">Erste StepInfo-Instanz.</param>
      /// <param name="s2">Zweite StepInfo-Instanz.</param>
      /// <returns>True, wenn die Instanzen gleich sind, sonst false.</returns>
      public static bool operator ==(StepInfo s1, StepInfo s2)
      {
         return s1.Equals(s2);
      }

      /// <summary>
      /// Prüft zwei SztepInfo-Instanzen auf Ungleichheit.
      /// </summary>
      /// <param name="s1">Erste StepInfo-Instanz.</param>
      /// <param name="s2">Zweite StepInfo-Instanz.</param>
      /// <returns>True, wenn die Instanzen verschieden sind, sonst false.</returns>
      public static bool operator !=(StepInfo s1, StepInfo s2)
      {
         return !s1.Equals(s2);
      }

      /// <summary>
      /// Berechnet eine Hashfunktion dieser Instanz.
      /// </summary>
      /// <returns>Der Hashcode dieser Instanz.</returns>
      public override int GetHashCode() 
      {
         return this.InstallSteps ^ this.UninstallSteps;
      }

      /// <summary>
      /// Vergleicht eine Instanz der StepInfo-Klasse mit einem Objekt.
      /// </summary>
      /// <param name="obj">Das zu vergleichende Objekt.</param>
      /// <returns>True, wenn das Objekt mit dieser Instanz übereinstimmt.</returns>
      public override bool Equals(object obj)
      {
         StepInfo info = obj as StepInfo;

         return
             info != null &&
             info.InstallSteps == this.InstallSteps &&
             info.UninstallSteps == this.UninstallSteps;
      }

      /// <summary>
      /// Addiert zwei StepInfo-Instanzen.
      /// </summary>
      /// <param name="si">Die zu dieser Instanz zu addierende Instanz.</param>
      /// <returns>Die Summe der StepInfo-Instanzen.</returns>
      public StepInfo Add(StepInfo si)
      {
         int install = this.InstallSteps + si.InstallSteps;
         int uninstall = this.UninstallSteps + si.UninstallSteps;

         return new StepInfo(install, uninstall);
      }

      /// <summary>
      /// Subtrahiert zwei StepInfo-Instanzen.
      /// </summary>
      /// <param name="si">Die von dieser Instanz zu subtrahierende Instanz.</param>
      /// <returns>Die Differenz der StepInfo-Instanzen.</returns>    
      public StepInfo Subtract(StepInfo si)
      {
         int install = this.InstallSteps - si.InstallSteps;
         int uninstall = this.UninstallSteps - si.UninstallSteps;

         return new StepInfo(install, uninstall);
      }
   }
}
