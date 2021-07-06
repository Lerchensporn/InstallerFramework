// <copyright file="InstallException.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>25.04.2009</date>
// <summary></summary>

namespace InstallerFramework.Base
{
   using System;

   /// <summary>
   /// Stellt eine Ausnahme dar, die während der Installation auftritt.
   /// </summary>
   [global::System.Serializable]
   public class InstallException : Exception
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="InstallException"/>-Klasse.
      /// </summary>
      public InstallException()
      {
      }

      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="InstallException"/>-Klasse.
      /// </summary>
      /// <param name="message">Nachricht, die den Fehler beschreibt.</param>
      public InstallException(string message)
         : base(message)
      {
      }

      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="InstallException"/>-Klasse.
      /// </summary>
      /// <param name="message">Begründung für den Waurf der Ausnahme.</param>
      /// <param name="inner">Ausnahme, die diese Ausnahme verursachte oder null.</param>
      public InstallException(string message, Exception inner)
         : base(message, inner)
      {
      }

      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="InstallException"/>-Klasse.
      /// </summary>
      /// <param name="info">Die SerialisationInfo, die die serialisierten Daten über die geworfene Ausnahme enthält.</param>
      /// <param name="context">Ein StreamingContext mit Informationen über Quelle oder Ziel.</param>
      protected InstallException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
         : base(info, context)
      {
      }
   }
}
