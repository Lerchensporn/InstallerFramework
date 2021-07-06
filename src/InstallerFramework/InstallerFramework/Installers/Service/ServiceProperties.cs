// <copyright file="ServiceProperties.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>26.04.2009</date>
// <summary></summary>

namespace InstallerFramework.Installers.Service
{
   /// <summary>
   /// Erweiterte Diensteigenschaften.
   /// </summary>
   public class ServiceProperties
   {
      /// <summary>
      /// Gibt an oder legt fest, welche Beschrebung der Dienst hat.
      /// </summary>
      public ServiceDescription Description { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welche Aktionen im Fehlerfall ausgeführt werden.
      /// </summary>
      public ServiceFailureActions FailureActions { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, wann die Aktionen im Fehlerfall ausgeführt werden.
      /// </summary>
      public ServiceFailureActionsFlag FailureActionsFlag { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, wann der Dienst herunterfährt.
      /// </summary>
      public ServicePreshutdownInfo PreshutdownInfo { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welchen SID-Typ der Dienst hat.
      /// </summary>
      public ServiceSidInfo SidInfo { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, mit welcher Verzögerung der Dienst startet.
      /// </summary>
      public ServiceDelayedAutoStartInfo DelayedAutoStartInfo { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welche Rechte für den Dienst erforderlich sind.
      /// </summary>
      public ServiceRequiredPrivilegesInfo RequiredPrivilegesInfo { get; set; }
   }
}
