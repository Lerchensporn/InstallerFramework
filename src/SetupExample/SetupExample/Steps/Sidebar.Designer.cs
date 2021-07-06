// <copyright file="Sidebar.Designer.cs" company="Default">
// Copyright (c) 2009 All Rights Reserved
// </copyright>
// <author>Andreas Becker</author>
// <email>abecker@web.de</email>
// <date>26.04.2009</date>
// <summary></summary>

namespace SetupExample.Steps
{
   /// <summary>
   /// Sidebar des Setups-Dialogs.
   /// </summary>
   public partial class Sidebar
   {
      /// <summary> 
      /// Erforderliche Designervariable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Link zur Webseite.
      /// </summary>
      private System.Windows.Forms.LinkLabel linkLabel1;

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
         this.linkLabel1 = new System.Windows.Forms.LinkLabel();
         this.SuspendLayout();
         // 
         // linkLabel1
         // 
         this.linkLabel1.AutoSize = true;
         this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.linkLabel1.Font = new System.Drawing.Font("Iskoola Pota", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
         this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
         this.linkLabel1.Location = new System.Drawing.Point(0, 0);
         this.linkLabel1.Name = "linkLabel1";
         this.linkLabel1.Size = new System.Drawing.Size(386, 32);
         this.linkLabel1.TabIndex = 0;
         this.linkLabel1.TabStop = true;
         this.linkLabel1.Text = "InstallerFramework.codeplex.com";
         this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
         // 
         // Sidebar
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.BackColor = System.Drawing.Color.Lime;
         this.Controls.Add(this.linkLabel1);
         this.Name = "Sidebar";
         this.Size = new System.Drawing.Size(1037, 602);
         this.Paint += new System.Windows.Forms.PaintEventHandler(this.SidebarPaint);
         this.ResumeLayout(false);
         this.PerformLayout();
      }

      #endregion
   }
}
