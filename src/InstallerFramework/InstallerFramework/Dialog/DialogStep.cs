// <copyright file="DialogStep.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>15.05.2009</date>
// <summary></summary>

namespace InstallerFramework.Dialog
{
   using System;
   using System.ComponentModel;
   using System.Windows.Forms;

   /// <summary>
   /// Die Stufe eines mehrstufigen Dialogs.
   /// </summary>
   public partial class DialogStep : UserControl
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="DialogStep"/>-Klasse.
      /// </summary>
      public DialogStep()
      {
         this.InitializeComponent();
         this.Dock = DockStyle.Fill;
      }

      /// <summary>
      /// Tritt ein, wenn sich der Fokus dieser Sufe im Dialog ändert.
      /// </summary>
      [Description("Tritt ein, wenn sich der Fokus dieser Sufe im Dialog ändert.")]
      public event EventHandler<FocusChangedEventArgs> FocusChanged;

      /// <summary>
      /// Ruft das <see cref="FocusChanged"/>-Event auf.
      /// </summary>
      /// <param name="e">Ereignisdaten für das <see cref="FocusChanged"/>-Event.</param>
      internal void OnFocusChanged(FocusChangedEventArgs e)
      {
         EventHandler<FocusChangedEventArgs> ev = this.FocusChanged;
         if (ev != null)
         {
            ev(this, e);
         }
      }
   }
}
