using System;
using System.IO;

namespace GameFramework.Networking.Framework
{
    public abstract class NetworkMessage
    {
        public int recieverId
        {
            get;
            protected set;
        }

        public int senderId
        {
            get;
            protected set;
        }

        public long sentTime
        {
            get;
            private set;
        }

        /// not written into stream
        public long recieveTime
        {
            get;
            private set;
        }

        /// not written into stream
        public long ping
        {
            get;
            private set;
        }

        public void Write(BinaryWriter writer)
        {
            sentTime = DateTime.Now.Ticks;

            writer.Write(recieverId);
            writer.Write(senderId);
            writer.Write(sentTime);

            HandleWrite(writer);
        }

        public void Read(BinaryReader reader)
        {
            recieverId = reader.ReadInt32();
            senderId = reader.ReadInt32();
            sentTime = reader.ReadInt64();

            recieveTime = DateTime.Now.Ticks;
            ping = (sentTime - recieveTime) / TimeSpan.TicksPerMillisecond;

            HandleRead(reader);
        }

        protected abstract void HandleWrite(BinaryWriter writer);
        protected abstract void HandleRead(BinaryReader reader);
    }
}
