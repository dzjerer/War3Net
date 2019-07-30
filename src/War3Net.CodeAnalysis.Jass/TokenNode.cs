﻿// ------------------------------------------------------------------------------
// <copyright file="TokenNode.cs" company="Drake53">
// Copyright (c) 2019 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

using System.IO;

namespace War3Net.CodeAnalysis.Jass
{
    public sealed class TokenNode : SyntaxNode
    {
        private readonly SyntaxToken _token;

        public TokenNode(SyntaxToken token, int position)
            : base(position, false)
        {
            _token = token;
        }

        public SyntaxTokenType TokenType => _token.TokenType;

        public string ValueText => _token.TokenValue;

        // TODO: cast SyntaxTokenType to SyntaxKind
        // public override SyntaxKind Kind => throw new NotImplementedException();

        public override void WriteTo(StreamWriter streamWriter)
        {
            streamWriter.Write(ValueText);
            if (!ValueText.EndsWith("\n"))
                streamWriter.Write(" ");
        }
    }
}