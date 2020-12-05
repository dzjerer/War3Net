﻿// ------------------------------------------------------------------------------
// <copyright file="StringTranspiler.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

using System;
using System.Text;

using War3Net.CodeAnalysis.Jass.Syntax;

namespace War3Net.CodeAnalysis.Transpilers
{
    public static partial class JassToLuaTranspiler
    {
        public static void Transpile(this StringSyntax stringNode, ref StringBuilder sb)
        {
            _ = stringNode ?? throw new ArgumentNullException(nameof(stringNode));

            if (stringNode.StringNode is null)
            {
                sb.Append("\"\"");
            }
            else
            {
                stringNode.StringNode.TranspileExpression(ref sb);
            }
        }
    }
}