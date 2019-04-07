using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace PobreLibrary.Registry.Proxy
{
    internal class User32Utils
    {
        // É cara, o BAGULHO é LOCO nesse código. Tudo macumba do Windows.
        // Essa classe é responsável para fazer uma chamada externa no Windows, notificando a mudança de Proxy.
        #region USER32 Options
        static IntPtr HWND_BROADCAST = new IntPtr(0xffff);
        static IntPtr WM_SETTINGCHANGE = new IntPtr(0x001A);
        #endregion

        #region STRUCT
        enum SendMessageTimeoutFlags : uint
        {
            SMTO_NORMAL = 0x0000,
            SMTO_BLOCK = 0x0001,
            SMTO_ABORTIFHUNG = 0x0002,
            SMTO_NOTIMEOUTIFNOTHUNG = 0x0008
        }
        #endregion

        #region Interop
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int SendMessage(int hWnd, int msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, UIntPtr wParam, UIntPtr lParam, SendMessageTimeoutFlags fuFlags, uint uTimeout, out UIntPtr lpdwResult);
        #endregion


        internal User32Utils() { }

        internal static void NotifySettingChange()
        {
            UIntPtr result;
            SendMessageTimeout(HWND_BROADCAST, (uint)WM_SETTINGCHANGE, UIntPtr.Zero, UIntPtr.Zero, SendMessageTimeoutFlags.SMTO_NORMAL, 1000, out result);
        }

    }
}
