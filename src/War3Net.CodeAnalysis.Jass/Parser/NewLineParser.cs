// ------------------------------------------------------------------------------
// <copyright file="NewLineParser.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

using Pidgin;

using static Pidgin.Parser;

namespace War3Net.CodeAnalysis.Jass
{
    internal partial class JassParser
    {
        internal static Parser<char, Unit> GetNewLineParser(Parser<char, Unit> whitespaceParser)
        {
            return OneOf(
                Try(String($"{JassSymbol.CarriageReturn}{JassSymbol.LineFeed}")).IgnoreResult(),
                Symbol.CarriageReturn.IgnoreResult(),
                Symbol.LineFeed.IgnoreResult()).Before(whitespaceParser).Labelled("newline");
        }
    }
}