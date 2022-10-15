/*
 * Author: Roger Weiss
 * Initial Version Date: 16/04/2019
 * Requires: http://cakebuild.net to run.
 *
 * Install Cake Build bootstrap once-off:
 *
 * OS X:
 * Open a new shell and run the following command.
 *   $> curl -Lsfo build.sh http://cakebuild.net/download/bootstrapper/osx
 *
 * Enable execute permissions once-off
 *   $> chmod +x build.sh
 *
 *
 * Example usage
 *  ./build.sh -target="CleanAllBinObj"
 *  
 * Documenation on MSBuild
 * See https://developer.xamarin.com/guides/cross-platform/xamarin-studio/build-system/
 */

#addin "nuget:?package=Cake.Incubator&version=5.0.1"

var target = Argument("target", "Default");

var solutionFolder = MakeAbsolute(Directory(".")).ToString() + "/";
var solutionPath = solutionFolder + "Nacelle.KMA.sln"; 
var solutionTestPath = solutionFolder + "Nacelle.KMA.Tests.sln"; 

Task("CleanAllBinObj")
  .Does(() =>
  {
    CleanDirectories("./**/bin/**");
    CleanDirectories("./**/obj/**");
  });

Task("NugetPackageRestore")
    .Does(() =>
{
    NuGetRestore(solutionPath);
});

Task("SolutionBuild")
    .Does(() =>
{
    MSBuild(solutionPath);
});

Task("SolutionTestBuild")
    .IsDependentOn("NugetPackageRestore")
    .Does(() =>
{
    MSBuild(solutionTestPath);
});

Task("RunTests")
    .IsDependentOn("SolutionTestBuild")
    .Does(() =>
{
    var settings = new DotNetCoreTestSettings { NoBuild = true, NoRestore = true, ResultsDirectory = Directory("."),
     ArgumentCustomization = args => args.Append("--logger \"trx;LogFileName=Nacelle.KMA.UI.Tests.xml\"")
      };
    DotNetCoreTest("./src/Nacelle.KMA.UI.Tests/Nacelle.KMA.UI.Tests.csproj", settings);
});

Task("Default")
  .Does(() =>
{
   Information("Choose a target to build");
});

RunTarget(target);