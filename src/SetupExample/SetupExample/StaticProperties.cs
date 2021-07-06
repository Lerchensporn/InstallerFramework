// <copyright file="StaticProperties.cs" company="Default">
// Copyright (c) 2009 All Rights Reserved
// </copyright>
// <author>Andreas Becker</author>
// <email>abecker@web.de</email>
// <date>26.04.2009</date>
// <summary></summary>

namespace SetupExample
{
   /// <summary>
   /// Auszuführende Aktion.
   /// </summary>
   internal enum InstallAction
   {      
      /// <summary>
      /// Software installieren.
      /// </summary>
      Install = 0,

      /// <summary>
      /// Software deinstallaieren.
      /// </summary>
      Uninstall = 1 
   }

   /// <summary>
   /// Statische Eigenschaften.
   /// </summary>
   internal static class StaticProperties
   {
      /// <summary>
      /// Gibt an oder legt fest, welcher Installationspfad für die Software verwendet wird.
      /// </summary>
      public static string InstallPath { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welche Aktion ausgeführt wird.
      /// </summary>
      public static InstallAction InstallMode { get; set; }
   }
}
