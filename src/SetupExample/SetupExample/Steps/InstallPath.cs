// <copyright file="InstallPath.cs" company="None">
// Copyright (c) 2009 All Rights Reserved
// </copyright>
// <author>Andreas Becker</author>
// <email>abecker5@web.de</email>
// <date>15.05.2009</date>
// <summary></summary>

namespace SetupExample.Steps
{
   using System;
   using System.IO;
   using System.Windows.Forms;
   using InstallerFramework.Dialog;

   /// <summary>
   /// Legt den Pfad für die Installation fest.
   /// </summary>
   public partial class InstallPath : DialogStep
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="InstallPath"/>-Klasse.
      /// </summary>
      public InstallPath()
      {
         this.InitializeComponent();
         this.textBoxPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
      }

      /// <summary>
      /// Wird ausgeführt, wenn auf den 'Browse'-Button geklickt wird.
      /// </summary>
      /// <param name="sender">Objekt, das das Ereignis ausgelöst hat.</param>
      /// <param name="e">Ereignisdaten für das Klicken auf den Button.</param>
      private void ButtonBrowse_Click(object sender, System.EventArgs e)
      {
          FolderBrowserDialog folder = new FolderBrowserDialog();
          folder.Description = "Installationspfad";
          folder.ShowNewFolderButton = true;
          if (folder.ShowDialog() == DialogResult.OK)
          {
              this.textBoxPath.Text = folder.SelectedPath;
          }
      }

      /// <summary>
      /// Wird ausgeführt, wenn mit dem Dialog navigiert wird und diese Stufe fokussiert ist.
      /// </summary>
      /// <param name="sender">Ereignisquelle für das <see cref="DialogStep.FocusChanged"/>-Event.</param>
      /// <param name="e">Ereignisdaten für die Navigation mit dem Dialog.</param>
      private void InstallPath_Navigate(object sender, InstallerFramework.Dialog.FocusChangedEventArgs e)
      {
         if (e.NaviType == NavigationType.Next && !e.GotFocus)
         {
            if (!Directory.Exists(this.textBoxPath.Text))
            {
               MessageBox.Show("Der angegebene Pfad ist ungültig.", "Ungültiger Pfad",  MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
               e.ParentDialogForm.AllowGotoNext = false;
            }
            else
            {
               StaticProperties.InstallPath = this.textBoxPath.Text;
               e.ParentDialogForm.AllowGotoNext = true;
            }
         }
      }
   }
}