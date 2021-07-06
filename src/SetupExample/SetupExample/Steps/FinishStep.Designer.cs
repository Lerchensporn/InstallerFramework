// <copyright file="FinishStep.Designer.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>07.06.2009</date>
// <summary></summary>

namespace SetupExample.Steps
{
   /// <summary>
   /// Die letzte Stufe des Installers.
   /// </summary>
   public partial class FinishStep
   {
      /// <summary>
      /// Label, das einen Text anzeigt.
      /// </summary>
      private System.Windows.Forms.Label label1;

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
         this.label1 = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(57, 63);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(277, 13);
         this.label1.TabIndex = 0;
         this.label1.Text = "Die Software wurde erfolgreich installiert oder deinstalliert.";
         // 
         // Finish
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.label1);
         this.Name = "Finish";
         this.Size = new System.Drawing.Size(1037, 602);
         this.FocusChanged += new System.EventHandler<InstallerFramework.Dialog.FocusChangedEventArgs>(this.Finish_FocusChanged);
         this.ResumeLayout(false);
         this.PerformLayout();
      }

      #endregion
   }
}
