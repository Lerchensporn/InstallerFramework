// <copyright file="Program.cs" company="None">
// Copyright (c) 2009 All Rights Reserved
// </copyright>
// <author>Andreas Becker</author>
// <email>abecker5@web.de</email>
// <date>15.05.2009</date>
// <summary></summary>

namespace SetupExample
{
   using System;
   using System.Windows.Forms;

   /// <summary>
   /// Enthält den Haupteinstiegspunkt des Programms.
   /// </summary>
   public static class Program
   {
      /// <summary>
      /// Haupteinstiegspunkt des Programms.
      /// </summary>
      [STAThread]
      private static void Main()
      {
 Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new SetupForm());
      }
   }
}
