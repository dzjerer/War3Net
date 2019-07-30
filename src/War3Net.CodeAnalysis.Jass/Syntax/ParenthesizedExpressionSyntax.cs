﻿// ------------------------------------------------------------------------------
// <copyright file="ParenthesizedExpressionSyntax.cs" company="Drake53">
// Copyright (c) 2019 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace War3Net.CodeAnalysis.Jass.Syntax
{
    public sealed class ParenthesizedExpressionSyntax : SyntaxNode
    {
        private readonly TokenNode _open;
        private readonly NewExpressionSyntax _expression;
        private readonly TokenNode _close;

        public ParenthesizedExpressionSyntax(TokenNode openNode, NewExpressionSyntax expressionNode, TokenNode closeNode)
            : base(openNode, expressionNode, closeNode)
        {
            _open = openNode ?? throw new ArgumentNullException(nameof(openNode));
            _expression = expressionNode ?? throw new ArgumentNullException(nameof(expressionNode));
            _close = closeNode ?? throw new ArgumentNullException(nameof(closeNode));
        }

        internal sealed class Parser : SequenceParser
        {
            private static Parser _parser;

            internal static Parser Get => _parser ?? (_parser = new Parser()).Init();

            protected override SyntaxNode CreateNode(List<SyntaxNode> nodes)
            {
                return new ParenthesizedExpressionSyntax(nodes[0] as TokenNode, nodes[1] as NewExpressionSyntax, nodes[2] as TokenNode);
            }

            private Parser Init()
            {
                AddParser(TokenParser.Get(SyntaxTokenType.ParenthesisOpenSymbol));
                AddParser(NewExpressionSyntax.Parser.Get);
                AddParser(TokenParser.Get(SyntaxTokenType.ParenthesisCloseSymbol));

                return this;
            }
        }
    }
}