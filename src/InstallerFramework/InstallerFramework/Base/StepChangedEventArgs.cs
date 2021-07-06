// <copyright file="StepChangedEventArgs.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>25.04.2009</date>
// <summary></summary>

namespace InstallerFramework.Base
{
   /// <summary>
   /// Ereignisdaten für das StepChanged-Event.
   /// </summary>
   public class StepChangedEventArgs : System.EventArgs
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="StepChangedEventArgs"/>-Klasse.
      /// </summary>
      /// <param name="oldStep">Ursprüngliche Anzahl der Stufen.</param>
      public StepChangedEventArgs(int oldStep)
      {
         this.OldStep = oldStep;
      }

      /// <summary>
      /// Gibt an, welche die ursprüngliche Stufenanzahl ist.
      /// </summary>
      public int OldStep { get; private set; }
   }
}