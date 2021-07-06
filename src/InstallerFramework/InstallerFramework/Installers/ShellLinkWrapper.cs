// <copyright file="ShellLinkWrapper.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>30.05.2009</date>
// <summary></summary>

namespace InstallerFramework.Installers
{
   using System;
   using System.Runtime.InteropServices;

   /// <summary>
   /// Anzeigestatus eines Fensters.
   /// </summary>
   public enum ShowWindow
   {
      /// <summary>
      /// Das Fenster wird normal angezeigt.
      /// </summary>
      ShowNormal = 1,

      /// <summary>
      /// Das Fenster wird maximiert angezeigt.
      /// </summary>
      ShowMaximized = 3,

      /// <summary>
      /// Das Fenster wird minimiert angezeigt und ist nicht aktiv.
      /// </summary>
      ShowMinNoActive = 7
   }

   /// <summary>
   /// Wrapper für COM-Interfaces mit ShellLink-Objekten.
   /// </summary>
   internal class NativeMethods
   {
      /// <summary>
      /// COM-Konstante für die erfolgreiche Ausführung einer Funktion.
      /// </summary>
      internal const int S_OK = 0x0;

      /// <summary>
      /// COM-Konstante, die einen Fehler anzeigt.
      /// </summary>
      internal const int S_FALSE = 1;

      /// <summary>
      /// Art der Suche nach dem Ziel eines ShellLink-Objekts.
      /// </summary>
      internal enum SLR_FLAGS
      {
         /// <summary>
         /// Do not display a dialog box if the link cannot be resolved. When SLR_NO_UI is set, the high-order word of fFlags can be set to a time-out value that specifies the maximum amount of time to be spent resolving the link. 
         /// The function returns if the link cannot be resolved within the time-out duration. If the high-order word is set to zero, the time-out duration will be set to the default value of 3,000 milliseconds (3 seconds). 
         /// To specify a value, set the high word of fFlags to the desired time-out duration, in milliseconds.
         /// </summary>
         SLR_NO_UI = 0x1,

         /// <summary>
         /// Undocumented member.
         /// </summary>
         SLR_ANY_MATCH = 0x2,

         /// <summary>
         /// If the link object has changed, update its path and list of identifiers. If SLR_UPDATE is set, you do not need to call IPersistFile::IsDirty to determine whether or not the link object has changed.
         /// </summary>
         SLR_UPDATE = 0x4,

         /// <summary>
         /// Link-Informationen werden nicht aktualisiert.
         /// </summary>
         SLR_NOUPDATE = 0x8,

         /// <summary>
         /// Es werden keine Suchaktionen ausgeführt.
         /// </summary>
         SLR_NOSEARCH = 0x10,

         /// <summary>
         /// Do not use distributed link tracking.
         /// </summary>
         SLR_NOTRACK = 0x20,

         /// <summary>
         /// Disable distributed link tracking. By default, distributed link tracking tracks removable media across multiple devices based on the volume name. It also uses the Universal Naming Convention (UNC) path to track remote file systems whose drive letter has changed. Setting SLR_NOLINKINFO disables both types of tracking.
         /// </summary>
         SLR_NOLINKINFO = 0x40,

         /// <summary>
         /// Der Windows Installer wird aufgerufen.
         /// </summary>
         SLR_INVOKE_MSI = 0x80
      }

      /// <summary>
      /// Art der Pfadinformation, die gegeben wird.
      /// </summary>
      internal enum SLGP_FLAGS
      {
         /// <summary>
         /// Emittelt den "standard short"- Dateinamen.
         /// </summary>
         SLGP_SHORTPATH = 0x1,

         /// <summary>
         /// Ermittelt den Pfadnamen gemäß der "Universal Naming Convention" (UNC).
         /// </summary>
         SLGP_UNCPRIORITY = 0x2,

         /// <summary>
         /// Ermittelt den rohen Pfad. Ein roher Pfad darf Umgebungsvariablen enthalten. 
         /// </summary>
         SLGP_RAWPATH = 0x4
      }

      /// <summary>
      /// Enthält Methoden zu, Erstellen und Ändern von ShellLinks.
      /// </summary>
      [ComImport]
      [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
      [Guid("000214F9-0000-0000-C000-000000000046")]
      internal interface IShellLink
      {
         /// <summary>
         /// Ermittelt den Pfad mit dem Dateinamen eines ShellLink-Objekts.
         /// </summary>
         /// <param name="pszFile">Die Adresse des Puffers, in den der Pfad mit dem Dateinamen geschrieben wird.</param>
         /// <param name="cchMaxPath">Die maximale Anzahl an Zeichen, die in den Puffer geschreiben werden, auf den 'pszFile' zeigt.</param>
         /// <param name="pfd">Die Adresse der WIN32_FIND_DATA, in die Informatonen über das ShellLink-Objekt geschrieben werden.
         /// Wenn der Parameter null ist, werden keine zusätzlichen Informationen gegeben.</param>
         /// <param name="fFlags">Flags, die die Art der zu gebenden Pfadinformationen bestimmen.</param>
         /// <returns>Gibt S_OK zurück, wenn die Ausführung erfolgreich war und korrekter Pfad ermittelt wurde. 
         /// Wenn die Ausführung erfolgreich war, aber kein Pfad ermittelt wurde, wird S_FALSE zurückgegeben. Anderenfalls wird ein Fehlercode zurückgegeben.</returns>
         int GetPath([MarshalAs(UnmanagedType.LPWStr)] out string pszFile, int cchMaxPath, out object pfd, SLGP_FLAGS fFlags);

         /// <summary>
         /// Ermittelt den Zeiger zu einer "Item Identifier List" (PIDL) für ein ShellLink-Objekt.
         /// </summary>
         /// <param name="pidl">Zeiger zur PIDL des ShellLink-Objekts.</param>
         /// <returns>Gibt S_OK bei erfolgreicher Ausführung zurück, sonst wird ein Fehlercode zurückgegeben. S_FALSE wird bei erfolgreicher Ausführung zurückgegeben, wenn keine PIDl gefunden wurde.</returns>
         int GetIDList(out IntPtr pidl);

         /// <summary>
         /// Ermittelt den Speicherort des Icon für ein ShellLink-Objekt.
         /// </summary>
         /// <param name="pszIconPath">Der Pfad zur Datei, in der das Icon gespeichert ist.</param>
         /// <param name="cchIconPath">Die maximale Zeichenanzahl, die in den pszIconPath-Parameter geschrieben wird.</param>
         /// <param name="piIcon">Der Index des Icons.</param>
         /// <returns>Gibt S_OK bei erfolgreicher Ausführung zurück, sonst wird ein Fehlercode zurückgegeben.</returns>
         int GetIconLocation([MarshalAs(UnmanagedType.LPWStr)]out string pszIconPath, int cchIconPath, out int piIcon);

         /// <summary>
         /// Ermittelt das Arbeitsverzeichnis eines ShellLink-Objekts.
         /// </summary>
         /// <param name="pszDir">Der Name das Arbeitsverzeichnisses.</param>
         /// <param name="cchMaxPath">Die maximale Zeichenanzahl, die in den pszDir-Parameter geschrieben wird.</param>
         /// <returns>Gibt S_OK bei erfolgreicher Ausführung zurück, sonst wird ein Fehlercode zurückgegeben.</returns>
         int GetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] out string pszDir, int cchMaxPath);

         /// <summary>
         /// Ermittelt den Status des Fensters, das durch das Shortcut geöffnet wird.  --beachte: Es geht auch SW_MINIMIZED.
         /// </summary>
         /// <param name="piShowCmd">Ein Zeiger zum Kommando für den Anzeigestatus des Fensters.</param>
         /// <returns>Gibt S_OK bei erfolgreicher Ausführung zurück, sonst wird ein Fehlercode zurückgegeben.</returns>
         int GetShowCmd(out ShowWindow piShowCmd);

         /// <summary>
         /// Ermittelt die Tastenkombination für ein ShellLink-Objekt.
         /// </summary>
         /// <param name="pwHotkey">Die Adresse der Tastenkombination. Der virtuelle Tastencode wird im niederwerigen Byte angegeben, die Modifer-Flags werden im höherwertigen Byte angegeben.</param>
         /// <returns>Gibt S_OK bei erfolgreicher Ausführung zurück, sonst wird ein Fehlercode zurückgegeben.</returns>
         int GetHotkey(out short pwHotkey);

         /// <summary>
         /// Ermittelt die Beschreibung eines ShellLink-Objekts.
         /// </summary>
         /// <param name="pszName">Die Adresse eines Puffer, in den die Zeichenkette mit der Beschreibung geschrieben wird.</param>
         /// <param name="cchMaxName">Die maximale Anzahl an Zeichen, die in den Puffer geschreiben werden, auf den 'pszName' zeigt.</param>
         /// <returns>Gibt S_OK bei erfolgreicher Ausführung zurück, sonst wird ein Fehlercode zurückgegeben.</returns>
         int GetDescription([MarshalAs(UnmanagedType.LPWStr)] out string pszName, int cchMaxName);

         /// <summary>
         /// Ermittelt die Kommandizeilenargumente eines ShellLink-Objekts.
         /// </summary>
         /// <param name="pszArgs">Die Kommandozeilenargumente.</param>
         /// <param name="cchMaxPath">Die maximale Zeichenanzahl, die in den pszArgs-Parameter geschrieben wird.</param>
         /// <returns>Gibt S_OK bei erfolgreicher Ausführung zurück, sonst wird ein Fehlercode zurückgegeben.</returns>
         int GetArguments([MarshalAs(UnmanagedType.LPWStr)] out string pszArgs, int cchMaxPath);

         /// <summary>
         /// Sucht das Ziel eines ShellLink-Objekts.
         /// </summary>
         /// <param name="hwnd">Das Handle zum Fenster, das das ShellLink-Objekt als übergeordnetes Fenster für Nachrichtenfenster nutzt. Das Shell zeigt ein Nachrichtenfenster an, wenn der Benutzer erweiterte Informationen während der Suche des Ziels angeben muss.</param>
         /// <param name="fFlags">Flag, das die Art des Suchvorgangs angibt.</param>
         /// <returns>Gibt S_OK bei erfolgreicher Ausführung zurück, sonst wird ein Fehlercode zurückgegeben.</returns>
         int Resolve(IntPtr hwnd, SLR_FLAGS fFlags);

         /// <summary>
         /// Legt die Beschreibung eines ShellLink-Objekts fest.
         /// </summary>
         /// <param name="pszName">Die neue Beschreibung des ShellLink-Objekts.</param>
         /// <returns>Gibt S_OK bei erfolgreicher Ausführung zurück, sonst wird ein Fehlercode zurückgegeben.</returns>
         int SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);

         /// <summary>
         /// Setzt einen Zeiger zu einer "Item Identifier List" (PIDL) für ein ShellLink-Objekt.
         /// </summary>
         /// <param name="pidl">Die PIDL des Objekts.</param>
         /// <returns>Gibt S_OK bei erfolgreicher Ausführung zurück, sonst wird ein Fehlercode zurückgegeben.</returns>
         int SetIDList(IntPtr pidl);

         /// <summary>
         /// Legt das Arbeitsverzeichnis für das Ziel eines ShellLink-Objekt fest.
         /// </summary>
         /// <param name="pszDir">Pfad des Arbeitsverzeichnisses.</param>
         /// <returns>Gibt S_OK bei erfolgreicher Ausführung zurück, sonst wird ein Fehlercode zurückgegeben.</returns>
         int SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);

         /// <summary>
         /// Legt die Kommandozeilenargumente für ein ShellLink-Objekt fest.
         /// </summary>
         /// <param name="pszArgs">Die Kommandozeilenargumente.</param>
         /// <returns>Gibt S_OK bei erfolgreicher Ausführung zurück, sonst wird ein Fehlercode zurückgegeben.</returns>
         int SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

         /// <summary>
         /// Legt eine Tastenkombination für ein ShellLink-Objekt fest.
         /// </summary>
         /// <param name="wHotkey">Die neue Tastenkombination. Der KeyCode befindet sich im niederwertigen Byte, das Modifier-Flag befindet sich im höherwertigen Byte.</param>
         /// <returns>Gibt S_OK bei erfolgreicher Ausführung zurück, sonst wird ein Fehlercode zurückgegeben.</returns>
         int SetHotkey(short wHotkey);

         /// <summary>
         /// Legt den Status des Fensters fest, das durch das Shortcut geöffnet wird.
         /// </summary>
         /// <param name="piShowCmd">Der Status des Fensters.</param> 
         /// <returns>Gibt S_OK bei erfolgreicher Ausführung zurück, sonst wird ein Fehlercode zurückgegeben.</returns>
         int SetShowCmd(ShowWindow piShowCmd);

         /// <summary>
         /// Legt das Icon eines ShellLink-Objekts fest.
         /// </summary>
         /// <param name="pszIconPath">Pfad der Datei, die das Icon enthält.</param>
         /// <param name="iIcon">Index des Icons.</param>
         /// <returns>Gibt S_OK bei erfolgreicher Ausführung zurück, sonst wird ein Fehlercode zurückgegeben.</returns>
         int SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);

         /// <summary>
         /// Legt den relativen Pfad zu einem ShellLink-Objekt fest.
         /// </summary>
         /// <param name="pszPathRel">Der Name des neuen relativen Pfads.</param>
         /// <param name="dwReserved">Reserviert, dieser Parameter muss mit 0 angegeben werden.</param>
         /// <returns>Gibt S_OK bei erfolgreicher Ausführung zurück, sonst wird ein Fehlercode zurückgegeben.</returns>
         int SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);

         /// <summary>
         /// Legt den Pfad ein ShellLink-Objekts fest.
         /// </summary>
         /// <param name="pszFile">Die Adresse eines Puffers, der den neuen Pfadnamen enthält.</param>
         /// <returns>Gibt S_OK bei erfolgreicher Ausführung zurück, sonst wird ein Fehlercode zurückgegeben.</returns>
         int SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
      }

      /// <summary>
      /// ShellLink-Objekt zum Erstellen einer Verknüpfung. Eine Instanz dieser Klasse kann <see cref="System.Runtime.InteropServices.ComTypes.IPersistFile"/> und IShellLink gecastet werden.
      /// </summary>
      [ComImport]
      [Guid("00021401-0000-0000-C000-000000000046")]
      internal class ShellLink
      { 
      }
   }
}