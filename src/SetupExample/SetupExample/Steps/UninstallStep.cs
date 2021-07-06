// <copyright file="UninstallStep.cs" company="Default">
// Copyright (c) 2009 All Rights Reserved
// </copyright>
// <author>Andreas Becker</author>
// <email>abecker@web.de</email>
// <date>19.04.2009</date>
// <summary></summary>

namespace SetupExample.Steps
{
   using System.Windows.Forms;
   using InstallerFramework.Dialog;

   /// <summary>
   /// Startseite zur Deinstallation.
   /// </summary>
   public partial class UninstallStep : DialogStep
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="UninstallStep"/>-Klasse.
      /// </summary>
      public UninstallStep()
      {
         this.InitializeComponent();
         this.labelHeader.Text = StringResource.UninstallStep_Headline;
         this.labelWelcome.Text = StringResource.UninstallStep_Text;
         this.radioButtonReinstall.Text = StringResource.UninstallStep_ReinstallText;
         this.radioButtonUninstall.Text = StringResource.UninstallStep_UninstallText;
      }

      /// <summary>
      /// Tritt ein, wenn sich der Fokus dieser Stufe im Dialog ändert.
      /// </summary>
      /// <param name="sender">Die Ereignisquelle.</param>
      /// <param name="e">Die Ereignisdaten.</param>
      private void UninstallStep_FocusChanged(object sender, FocusChangedEventArgs e)
      {
         if (!e.GotFocus && e.NaviType == NavigationType.Next)
         {
            if (this.radioButtonUninstall.Checked)
            {
               StaticProperties.InstallMode = InstallAction.Uninstall;
            }
            else if (this.radioButtonReinstall.Checked)
            {
               StaticProperties.InstallMode = InstallAction.Install;
               e.ParentDialogForm.HideCurrentStep();
               e.ParentDialogForm.StepControls.Clear();
               e.ParentDialogForm.StepControls.Add(new WelcomeStep());
               e.ParentDialogForm.StepControls.Add(new LicenseStep());
               e.ParentDialogForm.StepControls.Add(new InstallPath());
               e.ParentDialogForm.StepControls.Add(new InstallStep());
               e.ParentDialogForm.StepControls.Add(new FinishStep());
               e.ParentDialogForm.GotoStep(0);
               e.CancelCurrentAction = true;
            }
         }
      }
   }
}
