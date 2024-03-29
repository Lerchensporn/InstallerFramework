﻿// <copyright file="LicenseStep.Designer.cs" company="Default">
// Copyright (c) 2009 All Rights Reserved
// </copyright>
// <author>Andreas Becker</author>
// <email>abecker@web.de</email>
// <date>26.04.2009</date>
// <summary></summary>

namespace SetupExample.Steps
{
   /// <summary>
   /// UserControl, in dem der Lizenzvertrag angezeigt wird.
   /// </summary>
   public partial class LicenseStep
   {
      /// <summary> 
      /// Erforderliche Designervariable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// RichTextBox, in der die Lizenz angezeigt wird.
      /// </summary>
      private System.Windows.Forms.RichTextBox richTextBox1;

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
         this.richTextBox1 = new System.Windows.Forms.RichTextBox();
         this.SuspendLayout();
         //// 
         // richTextBox1
         //// 
         this.richTextBox1.BackColor = System.Drawing.Color.White;
         this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.richTextBox1.Location = new System.Drawing.Point(0, 0);
         this.richTextBox1.Name = "richTextBox1";
         this.richTextBox1.ReadOnly = true;
         this.richTextBox1.Size = new System.Drawing.Size(895, 602);
         this.richTextBox1.TabIndex = 0;
         this.richTextBox1.Text = global::SetupExample.StringResource.Dialog_Title;
         //// 
         // LicenseStep
         //// 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.richTextBox1);
         this.Name = "LicenseStep";
         this.Size = new System.Drawing.Size(895, 602);
         this.Load += new System.EventHandler(this.LicenseStep_Load);
         this.FocusChanged += new System.EventHandler<InstallerFramework.Dialog.FocusChangedEventArgs>(this.LicenseStep_FocusChanged);
         this.ResumeLayout(false);
      }

      #endregion
   }
}
