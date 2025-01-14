﻿// ------------------------------------------------------------------------------
// <copyright file="MapBuilderTest.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

using System.Diagnostics;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using War3Net.Build.Extensions;
using War3Net.Build.Info;

namespace War3Net.Build.Tests
{
    [TestClass]
    public class MapBuilderTest
    {
        private const string Warcraft3ExecutableFilePath = null;

        [TestMethod]
        public void TestGenerateJassScriptWithUnitData()
        {
            const string OutputMapName = "TestOutput.w3x";
            const string InputPath = @".\TestData\MapFiles\TestGenerateUnitData";

            var scriptCompilerOptions = new ScriptCompilerOptions();
            scriptCompilerOptions.ForceCompile = true;
            scriptCompilerOptions.SourceDirectory = null;
            scriptCompilerOptions.OutputDirectory = @".\TestOutput\TestGenerateUnitData";

            var mapBuilder = new LegacyMapBuilder(OutputMapName);
            if (mapBuilder.Build(scriptCompilerOptions, InputPath))
            {
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestGenerateLuaScriptWithUnitData()
        {
            const string OutputMapName = "TestOutput.w3x";
            const string InputPath = @".\TestData\MapFiles\TestGenerateUnitData";

            using var mapInfoStream = File.OpenRead(Path.Combine(InputPath, MapInfo.FileName));
            using var mapInfoReader = new BinaryReader(mapInfoStream);
            var mapInfo = mapInfoReader.ReadMapInfo();
            mapInfo.ScriptLanguage = ScriptLanguage.Lua;

            var scriptCompilerOptions = new ScriptCompilerOptions();
            scriptCompilerOptions.MapInfo = mapInfo;
            scriptCompilerOptions.ForceCompile = true;
            scriptCompilerOptions.SourceDirectory = null;
            scriptCompilerOptions.OutputDirectory = @".\TestOutput\TestGenerateUnitData";

            var mapBuilder = new LegacyMapBuilder(OutputMapName);
            if (mapBuilder.Build(scriptCompilerOptions, InputPath))
            {
                var mapPath = Path.Combine(scriptCompilerOptions.OutputDirectory, OutputMapName);
                var absoluteMapPath = new FileInfo(mapPath).FullName;

                Assert.IsNotNull(Warcraft3ExecutableFilePath, "Path to Warcraft III.exe is not set.");
                Process.Start(Warcraft3ExecutableFilePath, $"-loadfile \"{absoluteMapPath}\"");
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestCreateNewTemplateMap()
        {
            const string OutputMapName = "Template.w3x";

            var scriptCompilerOptions = new ScriptCompilerOptions(CSharpLua.CoreSystem.CoreSystemProvider.GetCoreSystemFiles());

            scriptCompilerOptions.MapInfo = MapFactory.Info();
            scriptCompilerOptions.MapEnvironment = MapFactory.Environment(scriptCompilerOptions.MapInfo);

            scriptCompilerOptions.SourceDirectory = @".\TestData\Script\Template";
            scriptCompilerOptions.OutputDirectory = @".\TestOutput\Template";

#if DEBUG
            scriptCompilerOptions.Debug = true;
#endif

            // Build and launch
            var mapBuilder = new LegacyMapBuilder(OutputMapName);
            if (mapBuilder.Build(scriptCompilerOptions))
            {
                var mapPath = Path.Combine(scriptCompilerOptions.OutputDirectory, OutputMapName);
                var absoluteMapPath = new FileInfo(mapPath).FullName;

                Assert.IsNotNull(Warcraft3ExecutableFilePath, "Path to Warcraft III.exe is not set.");
                Process.Start(Warcraft3ExecutableFilePath, $"-loadfile \"{absoluteMapPath}\"");
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}