﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Bluetooth.Advertisement;


namespace Plugin.BluetoothLE
{
    public class AdvertisementData : IAdvertisementData
    {
        readonly BluetoothLEAdvertisementReceivedEventArgs adData;
        readonly Lazy<byte[]> manufacturerData;
        readonly Lazy<int> txPower;


        public AdvertisementData(BluetoothLEAdvertisementReceivedEventArgs args)
        {
            this.adData = args;

            this.manufacturerData = new Lazy<byte[]>(() => args.Advertisement.GetManufacturerSpecificData());
            this.ServiceUuids = args.Advertisement.ServiceUuids.ToArray();
            this.txPower = new Lazy<int>(() => args.Advertisement.GetTxPower());
        }


        public BluetoothLEAdvertisement Native => this.adData.Advertisement;
        public ulong BluetoothAddress => this.adData.BluetoothAddress;
        public string LocalName => this.adData.Advertisement.LocalName;
        public bool IsConnectable => this.adData.AdvertisementType == BluetoothLEAdvertisementType.ConnectableDirected ||
                                     this.adData.AdvertisementType == BluetoothLEAdvertisementType.ConnectableUndirected;
        public IReadOnlyList<byte[]> ServiceData { get; }
        public byte[] ManufacturerData => this.manufacturerData.Value;
        public Guid[] ServiceUuids { get; }
        public int TxPower => this.txPower.Value;
    }
}
/*
// Manufacturer data - currently unused
if (btAdv.Advertisement.ManufacturerData.Any())
{
foreach (var manufacturerData in btAdv.Advertisement.ManufacturerData)
{
// Print the company ID + the raw data in hex format
//var manufacturerDataString = $"0x{manufacturerData.CompanyId.ToString("X")}: {BitConverter.ToString(manufacturerData.Data.ToArray())}";
//Debug.WriteLine("Manufacturer data: " + manufacturerDataString);
var manufacturerDataArry = manufacturerData.Data.ToArray();
if (manufacturerData.CompanyId == 0x4C && manufacturerData.Data.Length >= 23 &&
manufacturerDataArry[0] == 0x02)
{
}
}*/