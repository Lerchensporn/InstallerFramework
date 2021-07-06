// <copyright file="WelcomeStep.Designer.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>07.06.2009</date>
// <summary></summary>

namespace SetupExample.Steps
{
   /// <summary>
   /// Startseite für den Setup-Assistenten.
   /// </summary>
   public partial class WelcomeStep
   {
      /// <summary> 
      /// Erforderliche Designervariable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Label mit der Überschrift.
      /// </summary>
      private System.Windows.Forms.Label labelHeader;

      /// <summary>
      /// Label mit dem Begrüßungstext.
      /// </summary>
      private System.Windows.Forms.Label labelWelcome;

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
         this.labelHeader = new System.Windows.Forms.Label();
         this.labelWelcome = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // labelHeader
         // 
         this.labelHeader.AutoSize = true;
         this.labelHeader.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
         this.labelHeader.Location = new System.Drawing.Point(3, 10);
         this.labelHeader.Name = "labelHeader";
         this.labelHeader.Size = new System.Drawing.Size(45, 23);
         this.labelHeader.TabIndex = 0;
         // 
         // labelWelcome
         // 
         this.labelWelcome.AutoSize = true;
         this.labelWelcome.Font = new System.Drawing.Font("Verdana", 8F);
         this.labelWelcome.Location = new System.Drawing.Point(4, 43);
         this.labelWelcome.Name = "labelWelcome";
         this.labelWelcome.Size = new System.Drawing.Size(28, 13);
         this.labelWelcome.TabIndex = 1;
         // 
         // WelcomeStep
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.labelWelcome);
         this.Controls.Add(this.labelHeader);
         this.Name = "WelcomeStep";
         this.Size = new System.Drawing.Size(1003, 602);
         this.ResumeLayout(false);
         this.PerformLayout();
      }

      #endregion
   }
}
