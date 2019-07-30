﻿// ------------------------------------------------------------------------------
// <copyright file="AlternativeParser.cs" company="Drake53">
// Copyright (c) 2019 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

using System.Collections.Generic;

namespace War3Net.CodeAnalysis.Jass
{
    /// <summary>
    /// A parser combinator that implements the | operator.
    /// </summary>
    internal abstract class AlternativeParser : IParser
    {
        private readonly List<IParser> _baseParsers;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlternativeParser"/> class.
        /// </summary>
        public AlternativeParser(params IParser[] baseParsers)
        {
            _baseParsers = new List<IParser>(baseParsers);
        }

        public int Alternatives => _baseParsers.Count;

        public virtual void AddParser(IParser baseParser)
        {
            _baseParsers.Add(baseParser);
        }

        public IEnumerable<ParseResult> Parse(ParseState state)
        {
            foreach (var baseParser in _baseParsers)
            {
                foreach (var result in baseParser.Parse(state))
                {
                    yield return new ParseResult(CreateNode(result.Node), result.ConsumedTokens);
                }
            }
        }

        protected abstract SyntaxNode CreateNode(SyntaxNode node);
    }
}