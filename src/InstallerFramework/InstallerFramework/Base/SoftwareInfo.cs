// <copyright file="SoftwareInfo.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>16.04.2009</date>
// <summary></summary>

namespace InstallerFramework.Base
{
   using System.Collections.Generic;

   /// <summary>
   /// Informationen über eine Software.
   /// </summary>
   public class SoftwareInfo
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="SoftwareInfo"/>-Klasse.
      /// </summary>
      public SoftwareInfo()
      {
         this.OtherProperties = new Dictionary<string, object>();
      }

      /// <summary>
      /// Gibt an oder legt, welche optionalen Werte in der Registry gespeichert werden.
      /// </summary>
      public Dictionary<string, object> OtherProperties { get; private set; }

      /// <summary>
      /// Gibt an oder legt fest, wer der Herausgeber der Software ist. Diese Eigenschaft muss zugewiesen werden.
      /// </summary>
      public string Publisher { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, ob die Software für alle Benutzer installiert ist (true) oder nicht (false).
      /// </summary>
      public bool InstalledForAllUsers { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welche Zeichenkette die Deinstallations-Routine bestimmt. Diese Eigenschaft muss zugewiesen werden.
      /// </summary>
      public string UninstallString { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welches der angezeigte Name des Produnktes ist.
      /// </summary>
      public string DisplayName { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welche Version des Produktes installiert ist. Diese Eigenschaft sollte angegeben werden.
      /// </summary>
      public string DisplayVersion { get; set; }

      /// <summary>
      /// Die Guid des Produkts. Wenn diese Eigenschaft nicht zugewiesen ist, wird eine zufällige Guid erzeugt.
      /// </summary>
      public string ProductGuid { get; set; }
   }
}
