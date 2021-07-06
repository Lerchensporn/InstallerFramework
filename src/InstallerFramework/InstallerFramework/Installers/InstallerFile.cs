// <copyright file="InstallerFile.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>19.05.2009</date>
// <summary></summary>

namespace InstallerFramework.Installers
{
   using System;
   using System.IO;
   using System.Security.AccessControl;

   /// <summary>
   /// Enthält Informationen für die Installation einer Datei.
   /// </summary>
   [Serializable]
   public class InstallerFile
   {
      /// <summary>
      /// Private Variable für die <see cref="AccessControl"/>-Eigenschaft.
      /// </summary>
      [NonSerialized]
      private FileSecurity accessControl;

      /// <summary>
      /// Private Variable für die <see cref="ValueStream"/>-Eigenschaft.
      /// </summary>
      [NonSerialized]
      private Stream valueStream;

      /// <summary>
      /// Gibt an oder legt fest, aus welchem Stream der Inhalt der Datei gelesen wird.
      /// </summary>
      public Stream ValueStream
      {
         get { return this.valueStream; }
         set { this.valueStream = value; }
      }

      /// <summary>
      /// Gibt an oder legt fest, welche Attribute das Verzeichnis bekommt.
      /// </summary>
      public FileAttributes Attributes { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welches das übergeordnete Verzeichnis ist.
      /// </summary>
      public string ParentDirectory { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welche Zugriffsberechtigungen für die Datei festgelegt werden.
      /// </summary>
      public FileSecurity AccessControl
      {
         get { return this.accessControl; }
         set { this.accessControl = value; }
      }

      /// <summary>
      /// Gibt an oder legt fest, wie der Name der Datei ohne Pfadangabe  ist.
      /// </summary>
      public string FileName { get; set; }

      /// <summary>
      /// Gibt an, in welchem Pfad die Datei erstellt wird.
      /// </summary>
      public string FullPath
      {
         get
         {
            return Path.Combine(this.ParentDirectory, this.FileName);
         }
      }
   }
}