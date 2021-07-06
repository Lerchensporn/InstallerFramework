// <copyright file="UninstallStep.Designer.cs" company="Default">
// Copyright (c) 2009 All Rights Reserved
// </copyright>
// <author>Andreas Becker</author>
// <email>abecker@web.de</email>
// <date>26.04.2009</date>
// <summary></summary>

namespace SetupExample.Steps
{
   /// <summary>
   /// Startseite für die Deinstallation.
   /// </summary>
   public partial class UninstallStep
   {
      /// <summary> 
      /// Erforderliche Designervariable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Das Willkommen-Label.
      /// </summary>
      private System.Windows.Forms.Label labelWelcome;

      /// <summary>
      /// Label mit der Überschrift.
      /// </summary>
      private System.Windows.Forms.Label labelHeader;

      /// <summary>
      /// Option, bei der die Software deinstalliert wird.
      /// </summary>
      private System.Windows.Forms.RadioButton radioButtonUninstall;

      /// <summary>
      /// Option, bei der die Software über die vorhandene Installation installiert wird (reparieren).
      /// </summary>
      private System.Windows.Forms.RadioButton radioButtonReinstall;

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
         this.labelWelcome = new System.Windows.Forms.Label();
         this.labelHeader = new System.Windows.Forms.Label();
         this.radioButtonUninstall = new System.Windows.Forms.RadioButton();
         this.radioButtonReinstall = new System.Windows.Forms.RadioButton();
         this.SuspendLayout();
         // 
         // labelWelcome
         // 
         this.labelWelcome.AutoSize = true;
         this.labelWelcome.Font = new System.Drawing.Font("Verdana", 8F);
         this.labelWelcome.Location = new System.Drawing.Point(26, 51);
         this.labelWelcome.Name = "labelWelcome";
         this.labelWelcome.Size = new System.Drawing.Size(370, 13);
         this.labelWelcome.TabIndex = 3;
         // 
         // labelHeader
         // 
         this.labelHeader.AutoSize = true;
         this.labelHeader.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
         this.labelHeader.Location = new System.Drawing.Point(25, 19);
         this.labelHeader.Name = "labelHeader";
         this.labelHeader.Size = new System.Drawing.Size(241, 23);
         this.labelHeader.TabIndex = 2;
         // 
         // radioButtonUninstall
         // 
         this.radioButtonUninstall.AutoSize = true;
         this.radioButtonUninstall.Checked = true;
         this.radioButtonUninstall.Location = new System.Drawing.Point(59, 91);
         this.radioButtonUninstall.Name = "radioButtonUninstall";
         this.radioButtonUninstall.Size = new System.Drawing.Size(88, 17);
         this.radioButtonUninstall.TabIndex = 4;
         this.radioButtonUninstall.TabStop = true;
         this.radioButtonUninstall.UseVisualStyleBackColor = true;
         // 
         // radioButtonReinstall
         // 
         this.radioButtonReinstall.AutoSize = true;
         this.radioButtonReinstall.Location = new System.Drawing.Point(60, 114);
         this.radioButtonReinstall.Name = "radioButtonReinstall";
         this.radioButtonReinstall.Size = new System.Drawing.Size(206, 17);
         this.radioButtonReinstall.TabIndex = 6;
         this.radioButtonReinstall.UseVisualStyleBackColor = true;
         // 
         // UninstallStep
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.radioButtonReinstall);
         this.Controls.Add(this.radioButtonUninstall);
         this.Controls.Add(this.labelWelcome);
         this.Controls.Add(this.labelHeader);
         this.Name = "UninstallStep";
         this.Size = new System.Drawing.Size(960, 628);
         this.FocusChanged += new System.EventHandler<InstallerFramework.Dialog.FocusChangedEventArgs>(this.UninstallStep_FocusChanged);
         this.ResumeLayout(false);
         this.PerformLayout();
      }

      #endregion
   }
}
