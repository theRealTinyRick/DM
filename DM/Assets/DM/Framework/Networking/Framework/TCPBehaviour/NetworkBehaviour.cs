using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Net;

namespace GameFramework.Networking.Framework
{
    public abstract partial class NetworkBehaviour
    {
        public string DEFAULT_SERVER_IP = "127.0.0.1";
        public string DEFAULT_SERVER_PORT = "8080";

        public IPAddress serverIPAdress
        {
            get;
            protected set;
        }

        public int serverPort
        {
            get;
            protected set;
        }

        public abstract void Start();
        public abstract void Stop();
        public abstract void Tick();
    }
}
