// <copyright file="RegistryProperties.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>20.04.2009</date>
// <summary></summary>

namespace InstallerFramework.Installers
{
   /// <summary>
   /// Auflistung von Software-Eigenschaften, die im String-Format angegeben werden.
   /// </summary>
   public enum StringProperty
   {
      /// <summary>
      /// Der in der Systemsteuerung angezeigte Name der Software.
      /// </summary>
      DisplayName,

      /// <summary>
      /// Der Veröffentlicher der Software.
      /// </summary>
      Publisher,

      /// <summary>
      /// Ein Hilfe-Link, zum Beipiel eine Webseite.
      /// </summary>
      HelpLink,

      /// <summary>
      /// In der Systemsteuerung angezeigte Kommentare.
      /// </summary>
      Comments,

      /// <summary>
      /// Kontakt zum Herausgeber der Software.
      /// </summary>
      Contact,

      /// <summary>
      /// Das Verzeichnis, in dem die Software installiert ist.
      /// </summary>
      InstallLocation,

      /// <summary>
      /// Die Id der Software.
      /// </summary>
      ProductID,

      /// <summary>
      /// Das Verzeichnis, aus dem die Installation gestartet wurde.
      /// </summary>
      InstallSource,

      /// <summary>
      /// Der Pfad zu einer Readme-Datei.
      /// </summary>
      Readme,

      /// <summary>
      /// Die registrierte Firma.
      /// </summary>
      RegCompany,

      /// <summary>
      /// Der registrierte Benutzer.
      /// </summary>
      RegOwner,

      /// <summary>
      /// Eine URL, bei der es Informationen über die Software gibt.
      /// </summary>
      UrlInfoAbout,

      /// <summary>
      /// Eine Telefonnummer für Support zur Software. 
      /// </summary>
      HelpTelephone,

      /// <summary>
      /// Eine URL, bei der es Informationen über Updates gibt.
      /// </summary>
      UrlUpdateInfo,

      /// <summary>
      /// Die angezeigte Version der Software.
      /// </summary>
      DisplayVersion,

      // Andere Eigenschaften

      /// <summary>
      /// Der Pfad zu einem Deinstallationsprogramm.
      /// </summary>
      UninstallString,

      /// <summary>
      /// Der Pfad zum Icon der Software.
      /// </summary>
      DisplayIcon,
   }

   /// <summary>
   /// Auflistung von Software-Eigenschaften, die im DWORD-Format angegeben werden.
   /// </summary>
   public enum DwordProperty
   {
      /// <summary>
      /// Die Hauptversion der Software.
      /// </summary>
      VersionMajor,

      /// <summary>
      /// Die Nebenversion der Software.
      /// </summary>
      VersionMinor,

      /// <summary>
      /// Gibt an, ob die Software eine Systemkomponente ist. Erlaubt sind die Werte 1 und 0. 
      /// Systemkomponenten werden nicht in der Systemsteuerung angezeigt.
      /// </summary>
      SystemComponent,

      /// <summary>
      /// Die Version der Software.
      /// </summary>
      Version,

      /// <summary>
      /// Der Speicherplatz, den die Software benötigt.
      /// </summary>
      EstimatedSize,

      /// <summary>
      /// Gibt an, ob die Software nicht geändert werden kann. Erlaubt sind die Werte 1 und 0.
      /// </summary>
      NoModify,

      /// <summary>
      /// Gibt an, ob die Software nicht repariert werden kann. Erlaubt sind die Werte 1 und 0.
      /// </summary>
      NoRepair,

      /// <summary>
      /// Gibt an, ob die Software nicht entfernt werden kann. Erlaubt sind die Werte 1 und 0.
      /// </summary>
      NoRemove,

      /// <summary>
      /// Die Sprache der Software.
      /// </summary>
      Language
   }
}