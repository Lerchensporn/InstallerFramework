// <copyright file="RegistryValue.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>29.04.2009</date>
// <summary></summary>

namespace InstallerFramework.Installers
{
   using Microsoft.Win32;

   /// <summary>
   /// Informationen über zu erstellende Registrierungseinträge.
   /// </summary>
   public class RegistryValue
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="RegistryValue"/>-Struktur.
      /// </summary>
      /// <param name="parentKey">Übergeordneter, existierender Schlüssel in der Registry.</param>
      /// <param name="subKeyPath">Erweitereter, relativer Name des Schlüssels. Er muss nicht existieren.</param>
      /// <param name="valueName">Name des Inhalts.</param>
      /// <param name="value">Inhalt des Wertes.</param>
      public RegistryValue(RegistryKey parentKey, string subKeyPath, string valueName, object value)
      {
         this.ParentKey = parentKey;
         this.Value = value;
         this.ValueName = valueName;
         this.Kind = RegistryValueKind.Unknown;
         this.SubKeyPath = subKeyPath;
      }

      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="RegistryValue"/>-Struktur.
      /// </summary>
      /// <param name="parentKey">Übergeordneter, existierender Schlüssel in der Registry.</param>
      /// <param name="subKeyPath">Erweitereter, relativer Name des Schlüssels. Er muss nicht existieren.</param>
      /// <param name="valueName">Name des Inhalts.</param>
      /// <param name="value">Inhalt des Wertes.</param>
      /// <param name="kind">Datentyp des Wertes.</param>
      public RegistryValue(RegistryKey parentKey, string subKeyPath, string valueName, object value, RegistryValueKind kind)
      {
         this.ParentKey = parentKey;
         this.Value = value;
         this.ValueName = valueName;
         this.Kind = kind;
         this.SubKeyPath = subKeyPath;
      }

      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="RegistryValue"/>-Klasse.
      /// </summary>
      /// <param name="parentKey">Übergeordneter, existierender Schlüssel in der Registry.</param>
      /// <param name="subKeyPath">Erweitereter, relativer Name des Schlüssels. Er muss nicht existieren.</param>
      public RegistryValue(RegistryKey parentKey, string subKeyPath)
      {
         this.ParentKey = parentKey;
         this.SubKeyPath = subKeyPath;
      }

      /// <summary>
      /// Gibt an oder legt fest, welchen Namen der Schlüssel hat, in dem ein Wert angelegt wird.
      /// </summary>
      public string SubKeyPath { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, wo der Schlüssel gespeichert ist.
      /// </summary>
      public RegistryKey ParentKey { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welchen Namen der Wert hat.
      /// </summary>
      public string ValueName { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welchen Inhalt der Wert hat.
      /// </summary>
      public object Value { get; set; }

      /// <summary>
      /// Gibt an oder legt fest, welchen Datentyp der Wert besitzt.
      /// </summary>
      public RegistryValueKind Kind { get; set; }
   }
}