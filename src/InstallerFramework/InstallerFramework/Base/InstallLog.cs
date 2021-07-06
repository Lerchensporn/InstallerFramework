// <copyright file="InstallLog.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>15.04.2009</date>
// <summary></summary>

namespace InstallerFramework.Base
{
   using System;
   using System.Globalization;
   using System.IO;

   /// <summary>
   /// Verarbeitet Log-Nachrichten, indem sie auf der Konsole ausgegeben werden und in eine Log-Datei geschrieben werden.
   /// </summary>
   public class InstallLog
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="InstallLog"/>-Klasse.
      /// </summary>
      public InstallLog()
      {
      }

      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="InstallLog"/>-Klasse.
      /// </summary>
      /// <param name="logfile">Der Pfad zur Log-Datei.</param>
      public InstallLog(string logfile)
      {
         this.LogFilePath = logfile;
         this.LogWriter = new StreamWriter(logfile);
      }

      /// <summary>
      /// Gibt an oder legt fest, wo sich die Log-Datei befindet.
      /// </summary>
      public string LogFilePath { get; set; }
   
      /// <summary>
      /// Gibt an oder legt fest, welcher <see cref="StreamWriter"/> zum Schreiben der Log-Datei verwendet wird.
      /// </summary>
      public StreamWriter LogWriter { get; private set; }

      /// <summary>
      /// Gibt eine Nachricht auf der Konsole aus und schreibt sie mit einer Zeitangabe in die Log-Datei.
      /// </summary>
      /// <param name="msg">Die Nachricht.</param>
      public void LogMessage(string msg)
      {
         Console.WriteLine(msg);
         if (this.LogWriter != null)
         {
            this.LogWriter.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture) + ": " + msg);
            this.LogWriter.Flush();
         }
      }

      /// <summary>
      /// Schließt die Log-Datei.
      /// </summary>
      public void CloseLogFile()
      {
         if (this.LogWriter != null)
         {
            this.LogWriter.Close();
         }
      }

      /// <summary>
      /// Löscht die Log-Datei und schließt den StreamWriter.
      /// </summary>
      public void DeleteLogFile()
      {
         this.LogWriter.Close();
         File.Delete(this.LogFilePath);
      }
   }
}
