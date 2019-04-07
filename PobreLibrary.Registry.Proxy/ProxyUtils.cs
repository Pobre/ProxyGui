using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace PobreLibrary.Registry.Proxy
{
    public class ProxyUtils
    {
        public ProxyUtils() {}

        private string RegKey = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings\";
        private string RegNameServer = "ProxyServer";
        private string RegNameEnable = "ProxyEnable";

        private RegistryUtils RegUtils = new RegistryUtils();

        private void SetProxyName(string ProxyAddress)
        {
            RegUtils.SetKeyValue(RegKeyType.CurrentUser, RegKey, RegNameServer, ProxyAddress);
        }

        public void EnableProxy(string ProxyAddress)
        {
            string Value = string.Empty;

            RegUtils.GetKeyValue(RegKeyType.CurrentUser, RegKey, RegNameServer, out Value);

            if (Value != ProxyAddress)
            {
                SetProxyName(ProxyAddress);
            }

            RegUtils.SetKeyValue(RegKeyType.CurrentUser, RegKey, RegNameEnable, 1);

            // Notifica a mudança no sistema e no Internet Eplorer, respectivamente.
            User32Utils.NotifySettingChange();
            WinINetUtils.NotifyOptionSettingChanges();

            /*
             * TODO:
             * + Precisa verificar os bytes que também devem ser modificados dentro da Chave Connections
             * | DefaultConnectionSettings, quase certeza que esta chave é responsável para entregar a config
             * | já pronta para os programas que peçam ao sistema o Proxy
             * | Ela é um valor binário e com endereço char.
             */
            byte[] ValueByte;
            char[] ValueChar;

            RegUtils.GetKeyValue(RegKeyType.CurrentUser, RegKey + "Connections", "DefaultConnectionSettings", out ValueByte);
            // Antes que perguntem, aqui eu redimensiono o tamanho de ValueChar para o mesmo do ValueByte, após receber o valor do registro.
            ValueChar = new char[ValueByte.Length];

            // Aqui é a parte mais divertida, mexer nos bytes, por motivos sombrios dos cantos mais obscuros do Windows, os registros,
            // você precisa deixar o valor de byte da posição 8, 03, por que? Eu não sei. Suspeito que seja para ativar no Windows e
            // não só no Internet Explorer.
            for(int pos = 0; pos < ValueByte.Length; pos++)
            {
                if (pos == 8)
                    ValueByte[pos] = 3;
                ValueChar[pos] = (char)ValueByte[pos];
            }

            // Não pergunte, funciona.
            RegUtils.SetKeyValue(RegKeyType.CurrentUser, RegKey + "Connections", "DefaultConnectionSettings", 
                Encoding.ASCII.GetBytes((new string(ValueChar)).Replace(Value, ProxyAddress)));
        }

        public void DisableProxy()
        {
            RegUtils.SetKeyValue(RegKeyType.CurrentUser, RegKey, RegNameEnable, 0);

            // Notifica a mudança no sistema e no Internet Eplorer, respectivamente.
            User32Utils.NotifySettingChange();
            WinINetUtils.NotifyOptionSettingChanges();

            byte[] ValueByte;

            RegUtils.GetKeyValue(RegKeyType.CurrentUser, RegKey + "Connections", "DefaultConnectionSettings", out ValueByte);

            // Desfazer tudo que na outra função eu fiz.
            for (int pos = 0; pos < ValueByte.Length; pos++)
            {
                if (pos == 8)
                {
                    ValueByte[pos] = 0;
                    break; // Não precisamos deletar o resto do proxy, afinal, já desativamos (como eu disse que suspeito lá em cima correto?)
                }
            }

            // Não pergunte, funciona.
            RegUtils.SetKeyValue(RegKeyType.CurrentUser, RegKey + "Connections", "DefaultConnectionSettings", ValueByte);
        }

        public string GetProxyName()
        {
            string ProxyAddress = string.Empty;

            RegUtils.GetKeyValue(RegKeyType.CurrentUser, RegKey, RegNameServer, out ProxyAddress);

            return ProxyAddress;
        }

        public bool GetProxyStatus()
        {
            int ProxyStatus = 0;

            RegUtils.GetKeyValue(RegKeyType.CurrentUser, RegKey, RegNameEnable, out ProxyStatus);

            return (ProxyStatus == 1) ? true : false;
        }
    }
}
