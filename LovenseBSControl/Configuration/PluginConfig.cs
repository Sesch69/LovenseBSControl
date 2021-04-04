using IPA.Config.Stores.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace LovenseBSControl.Configuration
{
    public class PluginConfig
    {
        public static PluginConfig Instance { get; set; }

        public bool Enabled { get; set; } = true;

        public bool VibrateMiss { get; set; } = true;
        public bool VibrateHit { get; set; } = false;

        public int IntenseHit { get; set; } = 10;
        public int IntenseMiss { get; set; } = 10;

        public bool RandomIntenseHit { get; set; } = false;
        public bool RandomIntenseMiss { get; set; } = false;

        public int DurationHit { get; set; } = 500;
        public int DurationMiss { get; set; } = 500;

        public int PresetBomb { get; set; } = 2;

        public string Modus { get; set; } = "Default";

        public int Rotate { get; set; } = 1;

        public int Air { get; set; } = 1;

        public bool VibeBombs { get; set; } = true;

        public int LovenseConnectAPI = 1;

        public bool Fireworks { get; set; } = false;

        public bool BattteryShow { get; set; } = true;

        [UseConverter]
        public virtual Dictionary<string, ToysConfig> ToyConfigurations { get; set; } = new Dictionary<string, ToysConfig>();

        [UseConverter]
        public virtual Dictionary<string, ConnectionConfig> ConnectionConfigurations { get; set; } = new Dictionary<string, ConnectionConfig>();

        public void AddToyConfiguration(string Id,  ToysConfig ToyConfiguration)
        {
            if (!IsAdded(Id))
            {
                ToyConfigurations.Add(Id,ToyConfiguration);
            }
        }

        public ToysConfig getToyConfig(string Id) 
        {
            return ToyConfigurations[Id];
        }

        public bool IsAdded(string Id)
        {
            return ToyConfigurations.ContainsKey(Id);
        }

       
        public ConnectionConfig AddConnectionConfiguration(ConnectionConfig ConnectionConfiguration)
        {
            if (!ConnectionExist(ConnectionConfiguration.Name))
            {
                ConnectionConfigurations.Add(ConnectionConfiguration.Name, ConnectionConfiguration);
            }
            return ConnectionConfiguration;
        }

        public ConnectionConfig GetConnectionConfig(string Name)
        {
            return ConnectionConfigurations[Name];
        }

        public List<ConnectionConfig>  GetConnections()
        {
            return ConnectionConfigurations.Values.ToList();
        }

        public Dictionary<string, ConnectionConfig> GetActiveConnections()
        {
            Dictionary<string, ConnectionConfig> activeConnections = new Dictionary<string, ConnectionConfig>();

            foreach(var entry in ConnectionConfigurations)
            {
                if (entry.Value.Active)
                {
                    activeConnections.Add(entry.Key, entry.Value);
                }
            }
            if(activeConnections.Count == 0)
            {
                activeConnections.Add("Default", ConnectionConfig.CreateDefaultConnection());
            }

            return activeConnections;
        }


        public bool ConnectionExist(string Name)
        {
            return ConnectionConfigurations.ContainsKey(Name);
        }

        public void DeleteConnection(string Name)
        {
            ConnectionConfigurations.Remove(Name);
        }

        public ConnectionConfig CreateConnection(string Name, string IP, string Port)
        {
            ConnectionConfig newConnection = ConnectionConfig.CreateCustomConnection(Name, IP, Port);
            return AddConnectionConfiguration(newConnection);
        }


    }
}
