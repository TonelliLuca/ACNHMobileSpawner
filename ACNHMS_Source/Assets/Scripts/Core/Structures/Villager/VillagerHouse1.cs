﻿using System;

namespace NHSE.Core
{
    public class VillagerHouse1 : IVillagerHouse
    {
        public const int SIZE = 0x1D4;
        public const int ItemCount = 36;
        public virtual string Extension => "nhvh";

        public readonly byte[] Data;
        public VillagerHouse1(byte[] data) => Data = data;

        public byte[] Write() => Data;

        public uint HouseLevel { get => BitConverter.ToUInt32(Data, 0x00); set => BitConverter.GetBytes(value).CopyTo(Data, 0x00); }
        public uint HouseStatus { get => BitConverter.ToUInt32(Data, 0x04); set => BitConverter.GetBytes(value).CopyTo(Data, 0x04); }
        public WallType WallUniqueID { get => (WallType)BitConverter.ToUInt16(Data, 0x08); set => BitConverter.GetBytes((ushort)value).CopyTo(Data, 0x08); }
        public RoofType RoofUniqueID { get => (RoofType)BitConverter.ToUInt16(Data, 0x0A); set => BitConverter.GetBytes((ushort)value).CopyTo(Data, 0x0A); }
        public DoorKind DoorUniqueID { get => (DoorKind)BitConverter.ToUInt16(Data, 0x0C); set => BitConverter.GetBytes((ushort)value).CopyTo(Data, 0x0C); }
        public WallType OrderWallUniqueID { get => (WallType)BitConverter.ToUInt16(Data, 0x0E); set => BitConverter.GetBytes((ushort)value).CopyTo(Data, 0x0E); }
        public RoofType OrderRoofUniqueID { get => (RoofType)BitConverter.ToUInt16(Data, 0x10); set => BitConverter.GetBytes((ushort)value).CopyTo(Data, 0x10); }
        public DoorKind OrderDoorUniqueID { get => (DoorKind)BitConverter.ToUInt16(Data, 0x12); set => BitConverter.GetBytes((ushort)value).CopyTo(Data, 0x12); }

        public Item DoorDecoItemName
        {
            get => Data.Slice(0x1C8, 8).ToClass<Item>();
            set => value.ToBytesClass().CopyTo(Data, 0x1C8);
        }

        public sbyte NPC1 { get => (sbyte)Data[0x1C4]; set => Data[0x1C4] = (byte)value; }
        public sbyte NPC2 { get => (sbyte)Data[0x1C5]; set => Data[0x1C5] = (byte)value; }

        public sbyte BuildPlayer { get => (sbyte)Data[0x1D0]; set => Data[0x1D0] = (byte)value; }

        public VillagerHouse2 Upgrade()
        {
            var data = new byte[VillagerHouse2.SIZE];
            var empty = Item.NONE.ToBytes();
            Data.CopyTo(data, 0);
            for (int i = 0; i < 236; i++)
                empty.CopyTo(data, 0x1D8 + (i * 0xC));
            VillagerHouse2.Footer.CopyTo(data, 0x1270);
            return new VillagerHouse2(data);
        }
    }
}