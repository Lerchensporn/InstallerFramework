// <copyright file="SetupForm.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>07.06.2009</date>
// <summary></summary>

namespace SetupExample
{
   using InstallerFramework.Dialog;

   /// <summary>
   /// Oberfläche des Setup-Assistenten.
   /// </summary>
   public partial class SetupForm : DialogForm
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="SetupForm"/>-Klasse.
      /// </summary>
      public SetupForm()
         : base()
      {
         this.InitializeComponent();

         this.Text = StringResource.Dialog_Title;
         this.PreviousButtonText = StringResource.Dialog_Previous;
         this.NextButtonText = StringResource.Dialog_Next;
         this.CancelButtonText = StringResource.Dialog_Cancel;
         this.FinishButtonText = StringResource.Dialog_Finish;

         bool exists = InstallerFramework.Base.SoftwareDetector.SoftwareExists("SetupExample");
         if (exists)
         {
            this.StepControls.Add(new Steps.UninstallStep());
            this.StepControls.Add(new Steps.InstallStep());
            this.StepControls.Add(new Steps.FinishStep());
         }
         else
         {
            StaticProperties.InstallMode = InstallAction.Install;
            this.StepControls.Add(new Steps.WelcomeStep());
            this.StepControls.Add(new Steps.LicenseStep());
            this.StepControls.Add(new Steps.InstallPath());
            this.StepControls.Add(new Steps.InstallStep());
            this.StepControls.Add(new Steps.FinishStep());
         }

         this.GotoStep(0);
      }
   }
}
