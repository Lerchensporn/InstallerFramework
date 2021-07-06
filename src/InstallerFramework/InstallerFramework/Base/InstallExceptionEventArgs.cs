// <copyright file="InstallExceptionEventArgs.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>26.04.2009</date>
// <summary></summary>

namespace InstallerFramework.Base
{
   using System;

   /// <summary>
   /// Enthält Informationen für das <see cref="InstallException"/>-Event.
   /// </summary>
   public class InstallExceptionEventArgs : EventArgs
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="InstallExceptionEventArgs"/>-Klasse.
      /// </summary>
      /// <param name="catchedException">Die abgefangene Ausnahme.</param>
      public InstallExceptionEventArgs(Exception catchedException)
      {
         this.CatchedException = catchedException;
      }

      /// <summary>
      /// Gibt an, welche Ausnahme gefangen wurde.
      /// </summary>
      public Exception CatchedException { get; private set; }
   }
}
