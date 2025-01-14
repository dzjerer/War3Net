﻿// ------------------------------------------------------------------------------
// <copyright file="UnitObjectData.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;

using War3Net.Build.Extensions;
using War3Net.Common.Extensions;

namespace War3Net.Build.Object
{
    public abstract class UnitObjectData
    {
        internal UnitObjectData(ObjectDataFormatVersion formatVersion)
        {
            FormatVersion = formatVersion;
        }

        internal UnitObjectData(BinaryReader reader)
        {
            ReadFrom(reader);
        }

        public ObjectDataFormatVersion FormatVersion { get; set; }

        public List<SimpleObjectModification> BaseUnits { get; init; } = new();

        public List<SimpleObjectModification> NewUnits { get; init; } = new();

        internal void ReadFrom(BinaryReader reader)
        {
            FormatVersion = reader.ReadInt32<ObjectDataFormatVersion>();

            nint baseUnitsCount = reader.ReadInt32();
            for (nint i = 0; i < baseUnitsCount; i++)
            {
                BaseUnits.Add(reader.ReadSimpleObjectModification(FormatVersion));
            }

            nint newUnitsCount = reader.ReadInt32();
            for (nint i = 0; i < newUnitsCount; i++)
            {
                NewUnits.Add(reader.ReadSimpleObjectModification(FormatVersion));
            }
        }

        internal void WriteTo(BinaryWriter writer)
        {
            writer.Write((int)FormatVersion);

            writer.Write(BaseUnits.Count);
            foreach (var unit in BaseUnits)
            {
                writer.Write(unit, FormatVersion);
            }

            writer.Write(NewUnits.Count);
            foreach (var unit in NewUnits)
            {
                writer.Write(unit, FormatVersion);
            }
        }
    }
}