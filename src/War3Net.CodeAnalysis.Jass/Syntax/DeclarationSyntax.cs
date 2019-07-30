﻿// ------------------------------------------------------------------------------
// <copyright file="DeclarationSyntax.cs" company="Drake53">
// Copyright (c) 2019 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

using System;

namespace War3Net.CodeAnalysis.Jass.Syntax
{
    public sealed class DeclarationSyntax : SyntaxNode
    {
        private readonly TypeDefinitionSyntax _type;
        private readonly GlobalsBlockSyntax _globals;
        private readonly NativeFunctionDeclarationSyntax _native;

        public DeclarationSyntax(TypeDefinitionSyntax typeDefinitionNode)
            : base(typeDefinitionNode)
        {
            _type = typeDefinitionNode ?? throw new ArgumentNullException(nameof(typeDefinitionNode));
        }

        public DeclarationSyntax(GlobalsBlockSyntax globalsBlockNode)
            : base(globalsBlockNode)
        {
            _globals = globalsBlockNode ?? throw new ArgumentNullException(nameof(globalsBlockNode));
        }

        public DeclarationSyntax(NativeFunctionDeclarationSyntax nativeFunctionNode)
            : base(nativeFunctionNode)
        {
            _native = nativeFunctionNode ?? throw new ArgumentNullException(nameof(nativeFunctionNode));
        }

        internal sealed class Parser : AlternativeParser
        {
            private static Parser _parser;

            internal static Parser Get => _parser ?? (_parser = new Parser()).Init();

            protected override SyntaxNode CreateNode(SyntaxNode node)
            {
                if (node is TypeDefinitionSyntax typeDefinitionNode)
                {
                    return new DeclarationSyntax(typeDefinitionNode);
                }

                if (node is GlobalsBlockSyntax globalsBlockNode)
                {
                    return new DeclarationSyntax(globalsBlockNode);
                }

                return new DeclarationSyntax(node as NativeFunctionDeclarationSyntax);
            }

            private Parser Init()
            {
                AddParser(TypeDefinitionSyntax.Parser.Get);
                AddParser(GlobalsBlockSyntax.Parser.Get);
                AddParser(NativeFunctionDeclarationSyntax.Parser.Get);

                return this;
            }
        }
    }
}