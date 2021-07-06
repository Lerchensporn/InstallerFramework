// <copyright file="SetupForm.Designer.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>07.06.2009</date>
// <summary></summary>

namespace SetupExample
{
   using SetupExample.Steps;

   /// <summary>
   /// Dialog für die Installation von Software auf dem System.
   /// </summary>
   public partial class SetupForm
   {
      /// <summary>
      /// Die Sidebar des Dialogs.
      /// </summary>
      private Sidebar sidebar1;

      /// <summary>
      /// Erforderliche Designervariable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Bereinigt alle verwendeten Ressourcen.
      /// </summary>
      /// <param name="disposing">True, wenn verwaltete Ressourcen bereinigt werden sollen; sonst false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (this.components != null))
         {
            this.components.Dispose();
         }

         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.sidebar1 = new SetupExample.Steps.Sidebar();
         this.SuspendLayout();
         // 
         // sidebar1
         // 
         this.sidebar1.BackColor = System.Drawing.Color.Lime;
         this.sidebar1.Dock = System.Windows.Forms.DockStyle.Top;
         this.sidebar1.Location = new System.Drawing.Point(0, 0);
         this.sidebar1.Name = "sidebar1";
         this.sidebar1.Size = new System.Drawing.Size(434, 48);
         this.sidebar1.TabIndex = 2;
         // 
         // SetupForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(434, 359);
         this.Name = "SetupForm";
         this.ShowIcon = true;
         this.ShowSideBar = true;
         this.SideBar = this.sidebar1;
         this.Text = "Setup";
         this.ResumeLayout(false);

      }

      #endregion
   }
}