using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PobreLibrary.Registry.Certificate
{
    public class CertificateUtils
    {
        public CertificateUtils() { }

        private X509Store Store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);

        public bool Equals(string CertName)
        {
            Store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection CertCollection =
                Store.Certificates.Find(X509FindType.FindBySubjectName, CertName, true);

            return (CertCollection.Count > 0) ? true : false;
        }

        public bool Equals(X509Certificate2 Certificate) { return false; } // TODO

        public void InstallCertificate(string CertFileName)
        {
            X509Certificate2 Certificate = new X509Certificate2(CertFileName);

            Store.Open(OpenFlags.ReadWrite);
            Store.Add(Certificate);
            Store.Close();
        }

        public void RemoveCertificate(string CertFileName)
        {
            X509Certificate2 Certificate = new X509Certificate2(CertFileName);

            Store.Open(OpenFlags.ReadWrite);
            Store.Remove(Certificate);
            Store.Close();
        }
    }
}
