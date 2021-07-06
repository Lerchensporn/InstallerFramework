// <copyright file="InstallPath.Designer.cs" company="None">
// Copyright (c) 2009 All Rights Reserved
// </copyright>
// <author>Andreas Becker</author>
// <email>abecker5@web.de</email>
// <date>15.05.2009</date>
// <summary></summary>

namespace SetupExample.Steps
{
   /// <summary>
   /// Legt den Pfad der Installation fest.
   /// </summary>
   public partial class InstallPath
   {
      /// <summary>
      /// Label mit deskriptivem Text für den Benutzer.
      /// </summary>
      private System.Windows.Forms.Label label1;

      /// <summary>
      /// Button zum Aussuchen eines Installationspfades.
      /// </summary>
      private System.Windows.Forms.Button buttonBrowse;

      /// <summary>
      /// TextBox, die den Installationspfad enthält.
      /// </summary>
      private System.Windows.Forms.TextBox textBoxPath;

      /// <summary>
      /// Label mit einem deskriptiven Text, der das Eingabefeld für den Installationspfad verweist.
      /// </summary>
      private System.Windows.Forms.Label labelPath;

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

      #region Vom Komponenten-Designer generierter Code

      /// <summary> 
      /// Erforderliche Methode für die Designerunterstützung. 
      /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
      /// </summary>
      private void InitializeComponent()
      {
         this.buttonBrowse = new System.Windows.Forms.Button();
         this.textBoxPath = new System.Windows.Forms.TextBox();
         this.labelPath = new System.Windows.Forms.Label();
         this.label1 = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // buttonBrowse
         // 
         this.buttonBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
         this.buttonBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.75F);
         this.buttonBrowse.Location = new System.Drawing.Point(294, 60);
         this.buttonBrowse.Name = "buttonBrowse";
         this.buttonBrowse.Size = new System.Drawing.Size(63, 20);
         this.buttonBrowse.TabIndex = 0;
         this.buttonBrowse.Text = "Browse...";
         this.buttonBrowse.UseVisualStyleBackColor = true;
         this.buttonBrowse.Click += new System.EventHandler(this.ButtonBrowse_Click);
         // 
         // textBoxPath
         // 
         this.textBoxPath.Location = new System.Drawing.Point(109, 60);
         this.textBoxPath.Name = "textBoxPath";
         this.textBoxPath.Size = new System.Drawing.Size(179, 20);
         this.textBoxPath.TabIndex = 1;
         // 
         // labelPath
         // 
         this.labelPath.AutoSize = true;
         this.labelPath.Location = new System.Drawing.Point(20, 63);
         this.labelPath.Name = "labelPath";
         this.labelPath.Size = new System.Drawing.Size(83, 13);
         this.labelPath.TabIndex = 2;
         this.labelPath.Text = "Installationspfad";
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(20, 33);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(192, 13);
         this.label1.TabIndex = 3;
         this.label1.Text = "Geben Sie den Pfad der Installation an.";
         // 
         // InstallPath
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.label1);
         this.Controls.Add(this.labelPath);
         this.Controls.Add(this.textBoxPath);
         this.Controls.Add(this.buttonBrowse);
         this.Name = "InstallPath";
         this.Size = new System.Drawing.Size(1004, 602);
         this.FocusChanged += new System.EventHandler<InstallerFramework.Dialog.FocusChangedEventArgs>(this.InstallPath_Navigate);
         this.ResumeLayout(false);
         this.PerformLayout();
      }

      #endregion
   }
}
