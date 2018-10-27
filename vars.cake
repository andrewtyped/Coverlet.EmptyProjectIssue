T Variables<T>(Func<T> buildVariablesMethod)
{
    return buildVariablesMethod();
}

var vars_My_Test_Project = Variables(() => {
	var configuration = Argument_Configuration();
	var nugetFeed = Argument_NuGetFeed("bastiansoftware_core");
	var projectName = "My.Test.Project";
	var testProjectName = $"{projectName}.Test";
	var rootDirectory = new DirectoryPath("./");
	var sourceDirectory = rootDirectory.Combine("src");
	var projectDirectory = sourceDirectory.Combine($"{projectName}");
	var testDirectory = rootDirectory.Combine("test");
	var testProjectDirectory = testDirectory.Combine($"{testProjectName}");
	var solution = rootDirectory.CombineWithFilePath($"{projectName}.sln");
	var project = projectDirectory.CombineWithFilePath($"{projectName}.csproj");
	var testProject = testProjectDirectory.CombineWithFilePath($"{testProjectName}.csproj");
	var testResults = testProjectDirectory.Combine("TestResults");
	var reportGeneratorToolFilePath = rootDirectory.Combine("tools").Combine("ReportGenerator").Combine("tools").Combine("net47").CombineWithFilePath("ReportGenerator.exe");

	return (configuration,
            nugetFeed,
            projectName,
            testProjectName,
            rootDirectory,
            sourceDirectory,
            projectDirectory,
            testDirectory,
            testProjectDirectory,
            solution,
            project,
            testProject,
			testResults,
			reportGeneratorToolFilePath);
});