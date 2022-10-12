using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Security.Cryptography;

namespace CardStorageService.Utils
{
    public class CacheProviderException : Exception 
    {
    }

    public class ConnectionString {
        public string DataSourse { get; set; }

        public string DatabaseName { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

        public override string ToString()
        {
            return $"data source={DataSourse};initial catalog={DatabaseName};User Id={UserId};Password={Password}";
        }
    }
    public class CacheConnectionString
    {
        static byte[] additionalEntropy = { 1, 2, 3, 4, 5 };

        public void CacheConnections(List<ConnectionString> connections)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ConnectionString>));

                using MemoryStream memoryStream = new MemoryStream();
                using XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

                xmlSerializer.Serialize(xmlTextWriter, connections);

                byte[] protectedData = Protect(memoryStream.ToArray());

                File.WriteAllBytes("MyData.protected", protectedData);
            }
            catch (Exception e)
            {
                Console.WriteLine("Serialize data error.");
                throw new CacheProviderException();
            }
        }

        private byte[] Protect(byte[] data)
        {
            try
            {
                return ProtectedData.Protect(data, additionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (Exception e)
            {
                Console.WriteLine("Protected error");
                throw new CacheProviderException();
            }
        }

        public List<ConnectionString> GetConnectionsFromCache()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ConnectionString>));

                byte[] protectedData = File.ReadAllBytes("data.protected");
                byte[] data = Unprotect(protectedData);

                return (List<ConnectionString>)xmlSerializer.Deserialize(new MemoryStream(data));
            }
            catch (Exception e)
            {
                Console.WriteLine("Deserialize data error.");
                throw new CacheProviderException();
            }
        }

        private byte[] Unprotect(byte[] data)
        {
            try
            {
                return ProtectedData.Unprotect(data, additionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unprotect error.");
                throw new CacheProviderException();
            }
        }
    }
}
