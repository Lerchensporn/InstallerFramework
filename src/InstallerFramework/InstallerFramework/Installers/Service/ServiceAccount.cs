// <copyright file="ServiceAccount.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>20.04.2009</date>
// <summary></summary>

namespace InstallerFramework.Installers.Service
{
   /// <summary>
   /// Standardmäßige Accounts für Dienste.
   /// </summary>
   public enum StandardAccount
   {
      /// <summary>
      /// Wird als lokaler System-Dienst ausgeführt.
      /// </summary>
      LocalSystem = 0,

      /// <summary>
      /// Ein Netzwerk-Dienst.
      /// </summary>
      NetworkService = 1,

      /// <summary>
      /// Ein lokaler Dienst.
      /// </summary>
      LocalService = 2
   }

   /// <summary>
   /// Anmeldeinformationen für einen Dienst.
   /// </summary>
   public class ServiceAccount
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="ServiceAccount"/>-Klasse. Es wird ein Standardaccount für Dienste verwendet.
      /// </summary>
      /// <param name="standardAccount">Standardmäßiger Account für den Dienst.</param>
      public ServiceAccount(StandardAccount standardAccount)
      {
         switch (standardAccount)
         {
            case StandardAccount.LocalService:
               this.AccountToken = "NT AUTHORITY\\LocalService";
               return;
            case StandardAccount.LocalSystem:
               this.AccountToken = null;
               return;
            case StandardAccount.NetworkService:
               this.AccountToken = "NT AUTHORITY\\NetworkService";
               return;
         }
      }

      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="ServiceAccount"/>-Klasse. Es wird ein spezieller Account angegeben.
      /// </summary>
      /// <param name="accountName">Name des Accounts.</param>
      /// <param name="password">Passwort des Accounts oder NULL, wenn keines vorhanden ist.</param>
      public ServiceAccount(string accountName, string password)
      {
         this.AccountToken = accountName;
         this.Password = password;
      }

      /// <summary>
      /// Gibt an, welcher String dem Account zugeordnet wird.
      /// </summary>
      internal string AccountToken { get; private set; }

      /// <summary>
      /// Gibt an, welches Passwort der Account hat.
      /// </summary>
      internal string Password { get; private set; }
   }
}
