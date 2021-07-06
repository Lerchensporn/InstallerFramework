// <copyright file="Sidebar.cs" company="Default">
// Copyright (c) 2009 All Rights Reserved
// </copyright>
// <author>Andreas Becker</author>
// <email>abecker@web.de</email>
// <date>26.04.2009</date>
// <summary></summary>

namespace SetupExample.Steps
{
   using System.Diagnostics;
   using System.Windows.Forms;
   using InstallerFramework.Dialog;

   /// <summary>
   /// Sidebar für das Setup.
   /// </summary>
   public partial class Sidebar : DialogStep
   {
      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="Sidebar"/>-Klasse.
      /// </summary>
      public Sidebar()
      {
         this.InitializeComponent();
         this.Dock = DockStyle.Top;
      }

      /// <summary>
      /// Tritt ein, wenn auf ein LinkLabel geklickt wird.
      /// </summary>
      /// <param name="sender">Die Ereignisquelle.</param>
      /// <param name="e">Die <see cref="LinkLabelLinkClickedEventArgs"/>-Instanz mit Daten für das Event.</param>
      private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         Process.Start("http://installerframework.codeplex.com");
      }

      /// <summary>
      /// Wird ausgeführt, wenn auf dem Control gezeichnet wird.
      /// </summary>
      /// <param name="sender">Ereignisquelle des Paint-Events.</param>
      /// <param name="e">Ereignisdaten für das Zeichnen auf dem Control.</param>
      private void SidebarPaint(object sender, PaintEventArgs e)
      {
         ControlPaint.DrawBorder3D(e.Graphics, 0, 0, this.Width, this.Height, Border3DStyle.Etched, Border3DSide.Bottom);
      }
   }
}
