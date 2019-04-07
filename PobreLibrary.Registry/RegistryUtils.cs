using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace PobreLibrary.Registry
{
    public enum RegKeyType : int
    {
        CurrentUser = 1,
        LocalMachine = 2
    }

    public class RegistryUtils
    {
        public RegistryUtils() { }

        // byte[]
        public void GetKeyValue(RegKeyType KeyType, string RegKey, string Name, out byte[] Value)
        {
            RegistryKey pRegKey = null;

            switch ((int)KeyType)
            {
                case 1:
                    pRegKey = Microsoft.Win32.Registry.CurrentUser;
                    break;
                case 2:
                    pRegKey = Microsoft.Win32.Registry.LocalMachine;
                    break;
            }
            pRegKey = pRegKey.OpenSubKey(RegKey);

            Value = (byte[])pRegKey.GetValue(Name);

            pRegKey.Close();
        }

        // int
        public void GetKeyValue(RegKeyType KeyType, string RegKey, string Name, out int Value)
        {
            RegistryKey pRegKey = null;

            switch ((int)KeyType)
            {
                case 1:
                    pRegKey = Microsoft.Win32.Registry.CurrentUser;
                    break;
                case 2:
                    pRegKey = Microsoft.Win32.Registry.LocalMachine;
                    break;
            }
            pRegKey = pRegKey.OpenSubKey(RegKey);

            Value = (int)pRegKey.GetValue(Name, null);

            pRegKey.Close();

        }

        // string
        public void GetKeyValue(RegKeyType KeyType, string RegKey, string Name, out string Value)
        {
            RegistryKey pRegKey = null;

            switch ((int)KeyType)
            {
                case 1:
                    pRegKey = Microsoft.Win32.Registry.CurrentUser;
                    break;
                case 2:
                    pRegKey = Microsoft.Win32.Registry.LocalMachine;
                    break;
            }
            pRegKey = pRegKey.OpenSubKey(RegKey);

            if(pRegKey.GetValue(Name) != null)
            {
                Value = pRegKey.GetValue(Name).ToString();
            }
            else
            {
                Value = "0"; // aparentemente Value não pode de jeito nenhum retornar null.
            }

            pRegKey.Close();

        }

        // byte[]
        public void SetKeyValue(RegKeyType KeyType, string RegKey, string Name, byte[] Value)
        {
            RegistryKey pRegKey = null;

            switch ((int)KeyType)
            {
                case 1:
                    pRegKey = Microsoft.Win32.Registry.CurrentUser;
                    break;
                case 2:
                    pRegKey = Microsoft.Win32.Registry.LocalMachine;
                    break;
            }

            pRegKey = pRegKey.OpenSubKey(RegKey, true);
            System.Threading.Thread.Sleep(100);
            pRegKey.SetValue(Name, Value);
            System.Threading.Thread.Sleep(100);
            pRegKey.Close();
        }

        // string
        public void SetKeyValue(RegKeyType KeyType, string RegKey, string Name, string Value)
        {
            RegistryKey pRegKey = null;

            switch ((int)KeyType)
            {
                case 1:
                    pRegKey = Microsoft.Win32.Registry.CurrentUser;
                    break;
                case 2:
                    pRegKey = Microsoft.Win32.Registry.LocalMachine;
                    break;
            }

            pRegKey = pRegKey.OpenSubKey(RegKey, true);
            System.Threading.Thread.Sleep(100);
            pRegKey.SetValue(Name, Value);
            System.Threading.Thread.Sleep(100);
            pRegKey.Close();
        }

        // int
        public void SetKeyValue(RegKeyType KeyType, string RegKey, string Name, int Value)
        {
            RegistryKey pRegKey = null;

            switch ((int)KeyType)
            {
                case 1:
                    pRegKey = Microsoft.Win32.Registry.CurrentUser;
                    break;
                case 2:
                    pRegKey = Microsoft.Win32.Registry.LocalMachine;
                    break;
            }

            pRegKey = pRegKey.OpenSubKey(RegKey, true);
            System.Threading.Thread.Sleep(100);
            pRegKey.SetValue(Name, Value);
            System.Threading.Thread.Sleep(100);
            pRegKey.Close();
        }
    }
}
