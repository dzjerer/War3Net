﻿// ------------------------------------------------------------------------------
// <copyright file="NewExpressionSyntax.cs" company="Drake53">
// Copyright (c) 2019 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace War3Net.CodeAnalysis.Jass.Syntax
{
    public sealed class NewExpressionSyntax : SyntaxNode
    {
        private readonly ExpressionSyntax _expression;
        private readonly BinaryExpressionTailSyntax _tail;
        private readonly EmptyNode _empty;

        public NewExpressionSyntax(ExpressionSyntax expressionNode, BinaryExpressionTailSyntax expressionTailNode)
            : base(expressionNode, expressionTailNode)
        {
            _expression = expressionNode ?? throw new ArgumentNullException(nameof(expressionNode));
            _tail = expressionTailNode ?? throw new ArgumentNullException(nameof(expressionTailNode));
        }

        public NewExpressionSyntax(ExpressionSyntax expressionNode, EmptyNode emptyExpressionTailNode)
            : base(expressionNode, emptyExpressionTailNode)
        {
            _expression = expressionNode ?? throw new ArgumentNullException(nameof(expressionNode));
            _empty = emptyExpressionTailNode ?? throw new ArgumentNullException(nameof(emptyExpressionTailNode));
        }

        internal sealed class Parser : SequenceParser
        {
            private static Parser _parser;

            internal static Parser Get => _parser ?? (_parser = new Parser()).Init();

            protected override SyntaxNode CreateNode(List<SyntaxNode> nodes)
            {
                if (nodes[1] is EmptyNode emptyNode)
                {
                    return new NewExpressionSyntax(nodes[0] as ExpressionSyntax, emptyNode);
                }
                else
                {
                    return new NewExpressionSyntax(nodes[0] as ExpressionSyntax, nodes[1] as BinaryExpressionTailSyntax);
                }
            }

            private Parser Init()
            {
                AddParser(ExpressionSyntax.Parser.Get);
                AddParser(new OptionalParser(BinaryExpressionTailSyntax.Parser.Get));

                return this;
            }
        }
    }
}