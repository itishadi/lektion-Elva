﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulerad_Iot.Models.Devices
{
    public class DeviceConfiguration
    {
        private readonly string _connectionString;
        public DeviceConfiguration (string connectionString)
        {
            _connectionString = connectionString;
        }


        public string DeviviceId => _connectionString.Split(";")[1].Split("=")[1];
        public string ConnectionString => _connectionString;
        public bool AllowSending { get; set; } = false;
        public int TelemetryInterval { get; set; } = 10000;
    }
}
