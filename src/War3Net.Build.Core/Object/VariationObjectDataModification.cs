﻿// ------------------------------------------------------------------------------
// <copyright file="VariationObjectDataModification.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

using System.IO;

using War3Net.Common.Extensions;

namespace War3Net.Build.Object
{
    public sealed class VariationObjectDataModification : ObjectDataModification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VariationObjectDataModification"/> class.
        /// </summary>
        public VariationObjectDataModification()
        {
        }

        internal VariationObjectDataModification(BinaryReader reader, ObjectDataFormatVersion formatVersion)
        {
            ReadFrom(reader, formatVersion);
        }

        public int Variation { get; set; }

        public int Pointer { get; set; }

        internal void ReadFrom(BinaryReader reader, ObjectDataFormatVersion formatVersion)
        {
            Id = reader.ReadInt32();
            Type = reader.ReadInt32<ObjectDataType>();
            Variation = reader.ReadInt32();
            Pointer = reader.ReadInt32();
            Value = ReadValue(reader, formatVersion);
            SanityCheck = reader.ReadInt32();
        }

        internal void WriteTo(BinaryWriter writer, ObjectDataFormatVersion formatVersion)
        {
            writer.Write(Id);
            writer.Write((int)Type);
            writer.Write(Variation);
            writer.Write(Pointer);
            WriteValue(writer, formatVersion);
            writer.Write(SanityCheck);
        }
    }
}