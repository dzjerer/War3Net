﻿// ------------------------------------------------------------------------------
// <copyright file="PlayerData.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

using System.IO;
using System.Numerics;

using War3Net.Build.Common;
using War3Net.Build.Extensions;
using War3Net.Common.Extensions;

namespace War3Net.Build.Info
{
    public class PlayerData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerData"/> class.
        /// </summary>
        public PlayerData()
        {
            AllyLowPriorityFlags = new Bitmask32(0);
            AllyHighPriorityFlags = new Bitmask32(0);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerData"/> class.
        /// </summary>
        public PlayerData(int id)
            : this()
        {
            Id = id;
            Name = $"Player {id + 1}";
        }

        internal PlayerData(BinaryReader reader, MapInfoFormatVersion formatVersion)
        {
            ReadFrom(reader, formatVersion);
        }

        public int Id { get; set; }

        public PlayerController Controller { get; set; }

        public PlayerRace Race { get; set; }

        public PlayerFlags Flags { get; set; }

        public string Name { get; set; }

        public Vector2 StartPosition { get; set; }

        public Bitmask32 AllyLowPriorityFlags { get; set; }

        public Bitmask32 AllyHighPriorityFlags { get; set; }

        public Bitmask32 EnemyLowPriorityFlags { get; set; }

        public Bitmask32 EnemyHighPriorityFlags { get; set; }

        public override string ToString() => Name;

        internal void ReadFrom(BinaryReader reader, MapInfoFormatVersion formatVersion)
        {
            Id = reader.ReadInt32();
            Controller = reader.ReadInt32<PlayerController>();
            Race = reader.ReadInt32<PlayerRace>();
            Flags = reader.ReadInt32<PlayerFlags>();
            Name = reader.ReadChars();
            StartPosition = new Vector2(reader.ReadSingle(), reader.ReadSingle());
            AllyLowPriorityFlags = reader.ReadBitmask32();
            AllyHighPriorityFlags = reader.ReadBitmask32();

            if (formatVersion >= MapInfoFormatVersion.Reforged)
            {
                EnemyLowPriorityFlags = reader.ReadBitmask32();
                EnemyHighPriorityFlags = reader.ReadBitmask32();
            }
            else
            {
                EnemyLowPriorityFlags = new Bitmask32(0);
                EnemyHighPriorityFlags = new Bitmask32(0);
            }
        }

        internal void WriteTo(BinaryWriter writer, MapInfoFormatVersion formatVersion)
        {
            writer.Write(Id);
            writer.Write((int)Controller);
            writer.Write((int)Race);
            writer.Write((int)Flags);
            writer.WriteString(Name);
            writer.Write(StartPosition.X);
            writer.Write(StartPosition.Y);
            writer.Write(AllyLowPriorityFlags);
            writer.Write(AllyHighPriorityFlags);

            if (formatVersion >= MapInfoFormatVersion.Reforged)
            {
                writer.Write(EnemyLowPriorityFlags);
                writer.Write(EnemyHighPriorityFlags);
            }
        }
    }
}