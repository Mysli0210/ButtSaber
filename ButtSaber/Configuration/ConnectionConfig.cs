﻿using BeatSaberMarkupLanguage.Attributes;
using System;

namespace ButtSaber.Configuration
{
    public class ConnectionConfig
    {
        [UIValue(nameof(Prefix))]
        public string Prefix { get; set; } = "ws://";

        [UIValue(nameof(IpAdress))]
        public string IpAdress { get; set; } = "";

        [UIValue(nameof(Port))]
        public string Port { get; set; } = "12345";

        [UIValue(nameof(Name))]
        public string Name { get; set; } = "";

        [UIValue(nameof(Active))]
        public bool Active { get; set; } = false;
        
        public static ConnectionConfig CreatLocalHostConnection()
        {
            return CreateCustomConnection("Localhost", "127.0.0.1");
        }

        public static ConnectionConfig CreateCustomConnection(String Name, String Ip, String Port = "12345")
        {
            ConnectionConfig connection = new ConnectionConfig
            {
                IpAdress = Ip,
                Port = Port,
                Name = Name
            };
            return connection;
        }

        public String CreateBaseUrl()
        {
            return Prefix + IpAdress + ":" + Port;
        }

        public void SetStatus(bool active)
        {
            Active = active;
        }

    }
}
