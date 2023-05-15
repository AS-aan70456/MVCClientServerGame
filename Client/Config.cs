﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client{
    class Config{

        public static Config config;

        public bool isTransparantTextures { get; set; }
        public bool isDisplayFPS { get; set; }

        public int numRey { get; set; }
        public float fov { get; set; }

        public static void LoadConfig() {
            config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("appsettings.json"));
        }

        public static void Save(Config config) {
            File.WriteAllText("appsettings.json", JsonConvert.SerializeObject(config));
        }

    }
}
