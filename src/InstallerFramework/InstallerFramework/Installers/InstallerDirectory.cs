// <copyright file="InstallerDirectory.cs" company="InstallerFramework Project">
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
   /// Bedingung für das Löschen der Verzeichnisse bei der Deinstallation.
   /// </summary>
   public enum DirectoryDeleteMode
   {
      /// <summary>
      /// Das Verzeichnis wird bei der Deinstallation oder beim Rollback nur gelöscht, wenn es keine Dateien enthält.
      /// Leere Unterordner werden ignoriert.
      /// </summary>
      DeleteIfNotContainsFiles = 0,

      /// <summary>
      /// Das Verzeichnis wird bei der Deinstallation oder beim Rollback nur gelöscht, wenn es keine Elemente enthält.
      /// </summary>
      DeleteIfEmpty = 1,

      /// <summary>
      /// Das Verzeichnis wird bei der Deinstallation gelöscht.
      /// </summary>
      DeleteAlways = 2,

      /// <summary>
      /// Das Verzeichnis wird bei der Deinstallation nicht gelöscht.
      /// </summary>
      DeleteNever = 3
   }

   /// <summary>
   /// Enthält Informationen für die Installation eines Verzeichnisses.
   /// </summary>
   [Serializable]
   public class InstallerDirectory
   {
      /// <summary>
      /// Private Variable für die <see cref="AccessControl"/>-Eigenschaft.
      /// </summary>
      [NonSerialized]
      private DirectorySecurity accessControl;

      /// <summary>
      /// Gibt an oder legt fest, welche Attribute das Verzeichnis bekommt.
      /// </summary>
      public FileAttributes Attributes { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welches das übergeordnete Verzeichnis ist.
      /// </summary>
      public string ParentDirectory { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welche Zugriffsberechtigungen für das Verzeichnis festgelegt werden.
      /// </summary>
      public DirectorySecurity AccessControl
      {
         get { return this.accessControl; }
         set { this.accessControl = value; }
      }

      /// <summary>
      /// Gibt an oder legt fest, wie der Name des Verzeichnisses ohne Pfadangabe  ist.
      /// </summary>
      public string DirectoryName { get; set; }

      /// <summary>
      /// Gibt an, in welchem Pfad das Verzeichnis erstellt wird.
      /// </summary>
      public string FullPath
      {
         get
         {
            return Path.Combine(this.ParentDirectory, this.DirectoryName);
         }
      }

      /// <summary>
      /// Gibt an oder legt fest, unter welchen Bedingungen das Verzeichnis bei der Deinstallation oder beim Rollback gelöscht wird.
      /// </summary>
      public DirectoryDeleteMode DeleteMode { get; set; }
   }
}