#load "./vars.cake"

MSBuildSettings MSBuild_BuildStandardSettings()
{
    return new MSBuildSettings {
					Verbosity = Verbosity.Normal,
					ToolVersion = MSBuildToolVersion.VS2017,
					PlatformTarget = PlatformTarget.MSIL
				};
}

// Restore NuGet packages on the primary My.Test.Project solution.
CakeTaskBuilder Task_Restore_My_Test_Project = Task(nameof(Task_Restore_My_Test_Project))
	.Does(() => {
		MSBuild(vars_My_Test_Project.solution, 
            MSBuild_BuildStandardSettings().WithTarget("Restore"));
	});

// Build the primary My.Test.Project solution.
CakeTaskBuilder Task_Build_My_Test_Project = Task(nameof(Task_Build_My_Test_Project))
	.IsDependentOn(Task_Restore_My_Test_Project)
	.Does(() => {
		var settings = MSBuild_BuildStandardSettings();
		settings.Configuration = vars_My_Test_Project.configuration;

		MSBuild(vars_My_Test_Project.solution, 
				settings);
	});

// Run all tests in My.Test.Project.Test
CakeTaskBuilder Task_Test_My_Test_Project_net461 = Task(nameof(Task_Test_My_Test_Project_net461))
	.IsDependentOn(Task_Build_My_Test_Project)
	.Does(() => {
		var testResultsSubdirectory = "My.Test.Project.net461"; 
		var resultsDirectory = vars_My_Test_Project.testResults.Combine(testResultsSubdirectory);

		CleanDirectory(resultsDirectory);

		DotNetCoreTest(vars_My_Test_Project.testProject.FullPath, new DotNetCoreTestSettings(){
			Logger = "trx;LogFileName=My.Test.Project.trx",
			ResultsDirectory = resultsDirectory,
			Configuration=vars_My_Test_Project.configuration,
			Framework = "net461",
			NoBuild = true,
			NoRestore = true,
			ArgumentCustomization = (args) => args.Append($"/p:CollectCoverage=true /p:CoverletOutputFormat=\"cobertura%2cjson\" /p:CoverletOutput=\"./TestResults/{testResultsSubdirectory}/\""),	
		});
	});