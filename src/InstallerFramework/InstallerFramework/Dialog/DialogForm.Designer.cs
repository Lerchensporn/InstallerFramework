// <copyright file="DialogForm.Designer.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>19.04.2009</date>
// <summary></summary>

namespace InstallerFramework.Dialog
{
   /// <summary>
   /// Stellt einen mehrstufigen Dialog als Benutzeroberfläche für Installationen dar.
   /// </summary>
   public partial class DialogForm
   {
      /// <summary>
      /// Panel, auf dem Steuerelemente zum Navigieren platziert sind.
      /// </summary>
      private System.Windows.Forms.Panel navigationPanel;

      /// <summary>
      /// Der 'Weiter'-Button des Dialogs.
      /// </summary>
      private System.Windows.Forms.Button nextButton;

      /// <summary>
      /// Der 'Zurück'-Button des Dialogs.
      /// </summary>
      private System.Windows.Forms.Button previousButton;

      /// <summary>
      /// Die 'Abbrechen'-Button des Dialogs.
      /// </summary>
      private System.Windows.Forms.Button cancelButton;

      /// <summary>
      /// Panel, das die Sidebar des Dialogs enthält.
      /// </summary>
      private System.Windows.Forms.Panel panelSidebar;

      /// <summary>
      /// Panel, das den Inhalt des Dialogs enthält.
      /// </summary>
      private System.Windows.Forms.Panel panelContent;

      /// <summary>
      /// Erforderliche Designervariable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Verwendete Ressourcen bereinigen.
      /// </summary>
      /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (this.components != null))
         {
            this.components.Dispose();
         }

         base.Dispose(disposing);
      }

      #region Vom Windows Form-Designer generierter Code

      /// <summary>
      /// Erforderliche Methode für die Designerunterstützung.
      /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
      /// </summary>
      private void InitializeComponent()
      {
         this.navigationPanel = new System.Windows.Forms.Panel();
         this.nextButton = new System.Windows.Forms.Button();
         this.previousButton = new System.Windows.Forms.Button();
         this.cancelButton = new System.Windows.Forms.Button();
         this.panelSidebar = new System.Windows.Forms.Panel();
         this.panelContent = new System.Windows.Forms.Panel();
         this.navigationPanel.SuspendLayout();
         this.SuspendLayout();

         this.navigationPanel.Controls.Add(this.nextButton);
         this.navigationPanel.Controls.Add(this.previousButton);
         this.navigationPanel.Controls.Add(this.cancelButton);
         this.navigationPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.navigationPanel.Location = new System.Drawing.Point(0, 375);
         this.navigationPanel.Name = "navigationPanel";
         this.navigationPanel.Size = new System.Drawing.Size(489, 37);
         this.navigationPanel.TabIndex = 0;
         this.navigationPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.NavigationPanel_Paint);

         this.nextButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
         this.nextButton.Cursor = System.Windows.Forms.Cursors.Arrow;
         this.nextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.nextButton.Location = new System.Drawing.Point(288, 8);
         this.nextButton.Name = "nextButton";
         this.nextButton.Size = new System.Drawing.Size(82, 23);
         this.nextButton.TabIndex = 3;
         this.nextButton.UseVisualStyleBackColor = true;
         this.nextButton.Click += new System.EventHandler(this.NextButton_Click);

         this.previousButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
         this.previousButton.Cursor = System.Windows.Forms.Cursors.Arrow;
         this.previousButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.previousButton.Location = new System.Drawing.Point(200, 8);
         this.previousButton.Name = "previousButton";
         this.previousButton.Size = new System.Drawing.Size(82, 23);
         this.previousButton.TabIndex = 2;
         this.previousButton.UseVisualStyleBackColor = true;
         this.previousButton.Click += new System.EventHandler(this.PreviousButton_Click);

         this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
         this.cancelButton.Cursor = System.Windows.Forms.Cursors.Arrow;
         this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.cancelButton.Location = new System.Drawing.Point(400, 8);
         this.cancelButton.Name = "cancelButton";
         this.cancelButton.Size = new System.Drawing.Size(82, 23);
         this.cancelButton.TabIndex = 1;
         this.cancelButton.UseVisualStyleBackColor = true;
         this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);

         this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Top;
         this.panelSidebar.Location = new System.Drawing.Point(0, 0);
         this.panelSidebar.Name = "panelSidebar";
         this.panelSidebar.Size = new System.Drawing.Size(489, 17);
         this.panelSidebar.TabIndex = 1;

         this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panelContent.Location = new System.Drawing.Point(0, 17);
         this.panelContent.Name = "panelContent";
         this.panelContent.Size = new System.Drawing.Size(489, 358);
         this.panelContent.TabIndex = 0;

         this.ClientSize = new System.Drawing.Size(489, 412);
         this.Controls.Add(this.panelContent);
         this.Controls.Add(this.panelSidebar);
         this.Controls.Add(this.navigationPanel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "DialogForm";
         this.ShowIcon = false;
         this.navigationPanel.ResumeLayout(false);
         this.ResumeLayout(false);
      }

      #endregion
   }
}