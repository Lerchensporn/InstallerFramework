// <copyright file="TransactedInstallMode.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>18.04.2009</date>
// <summary></summary>

namespace InstallerFramework.Base
{
   /// <summary>
   /// Möglichkeiten der Fehlerbehandlung während der Installation.
   /// </summary>
   public enum TransactedInstallMode
   {     
      /// <summary>
      /// Ignoriert den Fehler.
      /// </summary>
      Ignore = 0,

      /// <summary>
      /// Setzt die gesamte Installation im Fehlerfall zurück.
      /// </summary>
      RollbackAllOnError = 1,

      /// <summary>
      /// Setzt nur die Installation zurück, die den Fehler verursachte.
      /// </summary>
      RollbackOneOnError = 2
   }
}
