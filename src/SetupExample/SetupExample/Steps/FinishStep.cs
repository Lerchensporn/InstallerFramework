// <copyright file="FinishStep.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>07.06.2009</date>
// <summary></summary>

namespace SetupExample.Steps
{
   using InstallerFramework.Dialog;

   /// <summary>
   /// Fertigstellen der Installation.
   /// </summary>
   public partial class FinishStep : DialogStep
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="FinishStep"/>-Klasse.
      /// </summary>
      public FinishStep()
      {
         this.InitializeComponent();
      }

      /// <summary>
      /// Tritt ein, wenn das FocusChanged-Event ausgelöst wurde.
      /// </summary>
      /// <param name="sender">Ereignisquelle des Ereignisses.</param>
      /// <param name="e">Ereignisdaten für das FocusChanged-Event.</param>
      private void Finish_FocusChanged(object sender, FocusChangedEventArgs e)
      {
         e.ParentDialogForm.AllowGotoPrevious = false;
      }
   }
}
