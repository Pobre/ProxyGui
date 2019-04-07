using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace PobreLibrary.Registry.Proxy
{
    internal class WinINetUtils
    {
        // É cara, o BAGULHO é LOCO nesse código. Tudo macumba do Windows.
        // Essa classe é responsável para fazer uma chamada externa no Windows, notificando a mudança de Proxy no Internet Explorer.
        #region WININET Options
        private const uint INTERNET_PER_CONN_PROXY_SERVER = 2;
        private const uint INTERNET_PER_CONN_PROXY_BYPASS = 3;
        private const uint INTERNET_PER_CONN_FLAGS = 1;

        private const uint INTERNET_OPTION_REFRESH = 37;
        private const uint INTERNET_OPTION_PROXY = 38;
        private const uint INTERNET_OPTION_SETTINGS_CHANGED = 39;
        private const uint INTERNET_OPTION_END_BROWSER_SESSION = 42;
        private const uint INTERNET_OPTION_PER_CONNECTION_OPTION = 75;

        private const uint PROXY_TYPE_DIRECT = 0x1;
        private const uint PROXY_TYPE_PROXY = 0x2;
        
        private const uint INTERNET_OPEN_TYPE_PROXY = 3;
        #endregion

        #region STRUCT
        struct Value1
        {
            uint dwValue;
            string pszValue;
            FILETIME ftValue;
        };

        [StructLayout(LayoutKind.Sequential)]
        struct INTERNET_PER_CONN_OPTION
        {
            uint dwOption;
            Value1 Value;
        };

        [StructLayout(LayoutKind.Sequential)]
        struct INTERNET_PER_CONN_OPTION_LIST
        {
            uint dwSize;
            [MarshalAs(UnmanagedType.LPStr, SizeConst = 256)]
            string pszConnection;
            uint dwOptionCount;
            uint dwOptionError;
            IntPtr pOptions;

        };

        [StructLayout(LayoutKind.Sequential)]
        struct INTERNET_CONNECTED_INFO
        {
            int dwConnectedState;
            int dwFlags;
        };
        #endregion

        #region Interop
        [DllImport("wininet.dll", EntryPoint = "InternetSetOptionA", CharSet = CharSet.Ansi, SetLastError = true, PreserveSig = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, uint dwOption, IntPtr pBuffer, int dwReserved);
        #endregion

        internal WinINetUtils(){ }


        internal static void NotifyOptionSettingChanges()
        {
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }

    }
}
