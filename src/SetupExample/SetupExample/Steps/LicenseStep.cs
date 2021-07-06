// <copyright file="LicenseStep.cs" company="Default">
// Copyright (c) 2009 All Rights Reserved
// </copyright>
// <author>Andreas Becker</author>
// <email>abecker@web.de</email>
// <date>26.04.2009</date>
// <summary></summary>

namespace SetupExample.Steps
{
   using System.IO;
   using System.Windows.Forms;
   using InstallerFramework.Dialog;

   /// <summary>
   /// Zeigt den Lizenzvertrag an.
   /// </summary>
   public partial class LicenseStep : DialogStep
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="LicenseStep"/>-Klasse.
      /// </summary>
      public LicenseStep()
      {
         this.InitializeComponent();
      }

      /// <summary>
      /// Wird ausgeführt wenn das UserControl geladen wird.
      /// </summary>
      /// <param name="sender">Die Ereignisquelle.</param>
      /// <param name="e">Die <see cref="System.EventArgs"/>-Instanz, die die Daten für das Ereignis enthält.</param>
      private void LicenseStep_Load(object sender, System.EventArgs e)
      {
         this.richTextBox1.Text = Properties.Resources.License;
      }

      /// <summary>
      /// Wird ausgeführt, wenn sich der Fokus dieser Stufe im Dialog ändern.
      /// </summary>
      /// <param name="sender">Die Ereignisquelle des <see cref="DialogStep.FocusChanged"/>-Events.</param>
      /// <param name="e">Ereignisdaten für die Änderung des Fokus dieser Stufe im übergeorneten Dialog.</param>
      private void LicenseStep_FocusChanged(object sender, FocusChangedEventArgs e)
      {
         if (e.GotFocus)
         {
            e.ParentDialogForm.NextButtonText = StringResource.LicenseStep_AcceptButtonText;
         }
         else
         {
            e.ParentDialogForm.NextButtonText = StringResource.Dialog_Next;
         }
      }
   }
}
