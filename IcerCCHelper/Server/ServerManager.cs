namespace IcerDesign.CCHelper.Server
{
    using System.IO;
    using System.Xml.Serialization;

    internal class ServerManager
    {
        private const string ServerListFilename = "server.xml";

        private ServerList LoadServerList(string file)
        {
            if (File.Exists(file))
            {
                using (var stream = File.Open(file, FileMode.Open))
                {
                    var xs = new XmlSerializer(typeof(ServerList));
                    var obj = xs.Deserialize(stream) as ServerList;
                    return obj;
                }
            }

            return null;
        }

        private ServerListServer[] loadedServerList;

        public ServerListServer[] ServerList
        {
            get
            {
                if (this.loadedServerList == null)
                {
                    this.loadedServerList = this.LoadServerList(ServerListFilename).Server;
                }

                return this.loadedServerList;
            }
        }
    }
}