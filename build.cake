var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");
var environment = Argument("Environment","GE1");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .WithCriteria(c => HasArgument("rebuild"))
    .Does(() =>
{
	Information("Clean");
    CleanDirectory($"./src/Example/bin/{configuration}");
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(() =>
{
	Information("Build");
    DotNetCoreBuild("./RetryApi.csproj", new DotNetCoreBuildSettings
    {
        Configuration = configuration,
    });
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{	
	Information("Test");
    DotNetCoreTest("./RetryApi.csproj", new DotNetCoreTestSettings
    {
        Configuration = configuration,
        NoBuild = true,
    });
});

Task("Publish")
    //.IsDependentOn("Build")
    .Does(()=>{
        Information($"Let`s deploy on {environment}!");
    });

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);