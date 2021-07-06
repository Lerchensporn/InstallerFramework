// <copyright file="SoftwareDetector.cs" company="InstallerFramework Project">
// Copyright (c) 2009 Andreas Becker. License: The MIT License (MIT).
// </copyright>
// <author>Andreas Becker</author>
// <website>http://installerframework.codeplex.com</website>
// <date>16.04.2009</date>
// <summary></summary>

namespace InstallerFramework.Base
{
   using System;
   using System.Collections.Generic;
   using Microsoft.Win32;

   /// <summary>
   /// Methode zum Filtern von auf dem System installierter Software.
   /// </summary>
   /// <param name="info">Das zu überprüfende Programm.</param>
   /// <returns>Gibt an, ob das Programm die Filter-Kriterien erfüllt.</returns>
   public delegate bool SoftwareInfoFilter(SoftwareInfo info);

   /// <summary>
   /// Findet Software, die auf dem System installiert ist.
   /// </summary>
   public static class SoftwareDetector
   {
      /// <summary>
      /// Findet alle installierten Programme auf dem System.
      /// </summary>
      /// <returns>Ein <see cref="SoftwareInfo"/>-Array mit den installierten Programmen.</returns>
      public static SoftwareInfo[] GetSoftware()
      {
         List<SoftwareInfo> result = new List<SoftwareInfo>();
         Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall", RegistryKeyPermissionCheck.ReadSubTree, System.Security.AccessControl.RegistryRights.ReadKey);
         foreach (string item in key.GetSubKeyNames())
         {
            RegistryKey sub = key.OpenSubKey(item);
            result.Add(SoftwareDetector.GetSoftware(sub));
            sub.Close();
         }

         key.Close();

         key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall", RegistryKeyPermissionCheck.ReadSubTree, System.Security.AccessControl.RegistryRights.ReadKey);
         foreach (string item in key.GetSubKeyNames())
         {
            RegistryKey sub = key.OpenSubKey(item);
            result.Add(SoftwareDetector.GetSoftware(sub));
            sub.Close();
         }

         key.Close();
         return result.ToArray();
      }

      /// <summary>
      /// Durchsucht alle installierte Programme.
      /// </summary>
      /// <param name="filter">Der Filter, mit dem das gewünschte Programm ermittelt wird. </param>
      /// <returns>Das gefundene Program oder null, wenn keines gefunden wurde.</returns>
      public static SoftwareInfo FilterSoftware(SoftwareInfoFilter filter)
      {
         Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall", RegistryKeyPermissionCheck.ReadSubTree, System.Security.AccessControl.RegistryRights.ReadKey);
         foreach (string item in key.GetSubKeyNames())
         {
            RegistryKey sub = key.OpenSubKey(item);
            SoftwareInfo tmp = SoftwareDetector.GetSoftware(sub);
            sub.Close();

            if (filter(tmp))
            {
               key.Close();
               return tmp;
            }
         }

         key.Close();

         key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall", RegistryKeyPermissionCheck.ReadSubTree, System.Security.AccessControl.RegistryRights.ReadKey);
         foreach (string item in key.GetSubKeyNames())
         {
            RegistryKey sub = key.OpenSubKey(item);
            SoftwareInfo tmp = SoftwareDetector.GetSoftware(sub);
            sub.Close();

            if (filter(tmp))
            {
               key.Close();
               return tmp;
            }
         }

         key.Close();
         return null;
      }

      /// <summary>
      /// Prüft, ob ein Programm mit einer bestimmten ProductGuid existiert.
      /// </summary>
      /// <param name="productGuid">Die ProductGuid des installierten Programms.</param>
      /// <returns>Es wird zurückgegeben, ob das Programm existiert.</returns>
      /// <param name="allUsers">Gibt an, ob nach Software gesucht wird, die für alle Benutzer installiert ist.</param>
      public static bool SoftwareExists(string productGuid, bool allUsers)
      {
         Microsoft.Win32.RegistryKey key;
         if (allUsers)
         {
            key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall");
         }
         else
         {
            key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall");
         }

         foreach (string item in key.GetSubKeyNames())
         {
            if (item == productGuid)
            {
               key.Close();
               return true;
            }
         }

         key.Close();
         return false;
      }
      
      /// <summary>
      /// Prüft, ob ein Programm mit einer bestimmten ProductGuid existiert.
      /// </summary>
      /// <param name="productGuid">Die ProductGuid des installierten Programms.</param>
      /// <returns>Es wird zurückgegeben, ob das Programm existiert.</returns>
      public static bool SoftwareExists(string productGuid)
      {
         Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall");
         foreach (string item in key.GetSubKeyNames())
         {
            if (item == productGuid)
            {
               key.Close();
               return true;
            }
         }
         
         key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall");
         foreach (string item in key.GetSubKeyNames())
         {
            if (item == productGuid)
            {
               key.Close();
               return true;
            }
         }

         key.Close();
         return false;
      }

      /// <summary>
      /// Sammelt Informationen über ein Programm, das in der Registry verzeichnet ist.
      /// </summary>
      /// <param name="sub">Der <see cref="RegistryKey"/> in 'HKLM_LocalMachine\Software\Microsoft\Windows\CurrentVersion\Uninstall'.</param>
      /// <returns>Das gefundene Programm wird zurückgegeben.</returns>
      private static SoftwareInfo GetSoftware(RegistryKey sub)
      {
         SoftwareInfo result = new SoftwareInfo();
         if (sub.Name.Contains("HKEY_LOCAL_MACHINE"))
         {
            result.InstalledForAllUsers = true;
         }

         foreach (string val in sub.GetValueNames())
         {
            switch (val)
            {
               case "DisplayName":
                  result.DisplayName = (string)sub.GetValue(val);
                  continue;
               case "Publisher":
                  result.Publisher = (string)sub.GetValue(val);
                  continue;
               case "UninstallString":
                  result.UninstallString = (string)sub.GetValue(val);
                  continue;
               case "DisplayVersion":
                  result.DisplayVersion = (string)sub.GetValue(val);
                  continue;
               default:
                  result.OtherProperties.Add(val, sub.GetValue(val));
                  break;
            }
         }

         return result;
      }
   }
}

