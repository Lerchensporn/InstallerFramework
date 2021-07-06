// <copyright file="WelcomeStep.cs" company="Default">
// Copyright (c) 2009 All Rights Reserved
// </copyright>
// <author>Andreas Becker</author>
// <email>abecker@web.de</email>
// <date>26.04.2009</date>
// <summary></summary>

namespace SetupExample.Steps
{
   using InstallerFramework.Dialog;

   /// <summary>
   /// Erste Stufe des Installationsdialogs.
   /// </summary>
   public partial class WelcomeStep : DialogStep
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="WelcomeStep"/>-Klasse.
      /// </summary>
      public WelcomeStep()
      {
         this.InitializeComponent();
         this.labelHeader.Text = StringResource.WelcomeStep_Headline;
         this.labelWelcome.Text = StringResource.WelcomeStep_WelcomeText;
      }
   }
}