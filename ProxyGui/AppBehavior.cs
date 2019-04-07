using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using PobreLibrary.Registry;
using PobreLibrary.Registry.Certificate;
using PobreLibrary.Registry.Proxy;

namespace ProxyGui
{
    static class AppBehavior
    {
		private static RegistryUtils Reg = new RegistryUtils();
		private static CertificateUtils Cert = new CertificateUtils();
		private static ProxyUtils Proxy = new ProxyUtils();

        public static bool ProxyEnabled()
        {
            return Proxy.GetProxyStatus();
        }

        public static void DisableProxy()
        {
            Proxy.DisableProxy();
        }

        public static void SetProxy(string ProxyAddress)
        {
            Proxy.EnableProxy(ProxyAddress);
        }

        public static bool CheckCertificate()
        {
            return Cert.Equals("Zscaler Root CA");
        }

        public static void InstallCertificate()
        {
            Cert.InstallCertificate("zscaler-root-2048.crt");
        }
        
        public static List<string> GetIpList()
        {
        	Configuration config = new Configuration(@"Config.xml");
        	XmlNodeList xnList = config.GetList();
        	
        	List<string> IpList = new List<string>();
        	foreach(XmlNode xn in xnList)
        	{
        		IpList.Add(xn.InnerText);
        	}
        	
        	return IpList;
        }
    }
}
