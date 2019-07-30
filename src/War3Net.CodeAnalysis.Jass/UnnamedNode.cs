﻿// ------------------------------------------------------------------------------
// <copyright file="UnnamedNode.cs" company="Drake53">
// Copyright (c) 2019 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

namespace War3Net.CodeAnalysis.Jass
{
    /// <summary>
    /// A node for which no proper subclass of <see cref="SyntaxNode"/> has been defined.
    /// Using this subclass should be avoided.
    /// </summary>
    public sealed class UnnamedNode : SyntaxNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnnamedNode"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is used by the default implementation of the <see cref="Many1Parser"/>.
        /// Instances of <see cref="UnnamedNode"/> should only be created when using a <see cref="ManyParser"/>.
        /// </remarks>
        public UnnamedNode(params SyntaxNode[] nodes)
            : base(nodes)
        {
        }
    }
}