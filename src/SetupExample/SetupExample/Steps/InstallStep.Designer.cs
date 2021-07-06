// <copyright file="InstallStep.Designer.cs" company="Default">
// Copyright (c) 2009 All Rights Reserved
// </copyright>
// <author>Andreas Becker</author>
// <email>abecker@web.de</email>
// <date>26.04.2009</date>
// <summary></summary>

namespace SetupExample.Steps
{
   /// <summary>
   /// UserControl, das Software installiert.
   /// </summary>
   public partial class InstallStep
   {
      /// <summary> 
      /// Erforderliche Designervariable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Zeigt den Status an.
      /// </summary>
      private System.Windows.Forms.Label label1;

      /// <summary>
      /// Zeigt den Fortschritt der Installation an.
      /// </summary>
      private System.Windows.Forms.ProgressBar progressBar1;

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
         this.label1 = new System.Windows.Forms.Label();
         this.progressBar1 = new System.Windows.Forms.ProgressBar();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(13, 26);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(0, 13);
         this.label1.TabIndex = 0;
         // 
         // progressBar1
         // 
         this.progressBar1.Location = new System.Drawing.Point(16, 42);
         this.progressBar1.Maximum = 1;
         this.progressBar1.Name = "progressBar1";
         this.progressBar1.Size = new System.Drawing.Size(402, 11);
         this.progressBar1.Step = 1;
         this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
         this.progressBar1.TabIndex = 1;
         // 
         // InstallStep
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.progressBar1);
         this.Controls.Add(this.label1);
         this.Name = "InstallStep";
         this.Size = new System.Drawing.Size(1004, 602);
         this.Load += new System.EventHandler(this.InstallStep_Load);
         this.FocusChanged += new System.EventHandler<InstallerFramework.Dialog.FocusChangedEventArgs>(this.InstallStep_FocusChanged);
         this.ResumeLayout(false);
         this.PerformLayout();
      }

      #endregion
   }
}
