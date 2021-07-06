// <copyright file="FocusChangedEventArgs.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>15.05.2009</date>
// <summary></summary>

namespace InstallerFramework.Dialog
{
   /// <summary>
   /// Art der Navigation in einem <see cref="DialogForm"/>.
   /// </summary>
   public enum NavigationType
   {
      /// <summary>
      /// Die Stufe wurde mit dem Programm geändert.
      /// </summary>
      ProgramStepChanged = 0,

      /// <summary>
      /// Es wird weiter navigiert.
      /// </summary>
      Next = 1,

      /// <summary>
      /// Es wird zurück navigiert.
      /// </summary>
      Previous = 2,

      /// <summary>
      /// Es wird abgebrochen.
      /// </summary>
      Cancel = 3,

      /// <summary>
      /// Es wird fertiggestellt, wenn die aktuelle Stufe die letzte ist.
      /// </summary>
      Finish = 4
   }

   /// <summary>
   /// Enthält Ereignisdaten für das das Wechseln der Stufe in einem mehrstufigen Dialog.
   /// </summary>
   public class FocusChangedEventArgs : System.EventArgs
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="FocusChangedEventArgs"/>-Klasse.
      /// </summary>
      /// <param name="parent">Der Dialog, der das Ereignis ausgelöst hat.</param>
      /// <param name="gotFocus">Gibt an, ob die aktuelle Stufe den Fokus bekam oder verlor.</param>
      /// <param name="type">Gibt die Art der Navigation an.</param>
      public FocusChangedEventArgs(DialogForm parent, bool gotFocus, NavigationType type)
      {
         this.NaviType = type;
         this.ParentDialogForm = parent;
         this.GotFocus = gotFocus;
      }

      /// <summary>
      /// Gibt an, ob die Stufe den Fokus bekommen hat.
      /// </summary>
      public NavigationType NaviType { get; private set; }

      /// <summary>
      /// Gibt an, welcher Dialog die Änderung der Stufe verursachte.
      /// </summary>
      public DialogForm ParentDialogForm { get; private set; }

      /// <summary>
      /// Gibt an, ob die aktuelle Stufe beim letzten Navigieren den Fokus bekam.
      /// </summary>
      public bool GotFocus { get; private set; }

      /// <summary>
      /// Gibt an oder legt fest, ob die momentan ausgeführte Aktion des übergeordneten Dialogs abgebrochen wird.
      /// </summary>
      public bool CancelCurrentAction { get; set; }
   }
}