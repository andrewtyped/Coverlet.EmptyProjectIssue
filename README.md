Coverlet Empty Project Issue
============================

This repo demonstrates a problem with Coverlet 2.1.1. When running `dotnet test` against a test assembly which references a project with no public methods, Coverlet fails with the error "C:\Users\.nuget\coverlet.msbuild\2.1.1\build\netstandard2.0\coverlet.msbuild.targets(23,5): error : Index was out of range. Must be non-negative and less than the size of the collection."

To reproduce the issue, open a Powershell window and run the `build.ps1` script. This will install Cake and execute the build steps defined in `build.cake`. The error occurs while running the `Task_Test_My_Test_Project_net461` method. 


Why Run Coverlet Against an Empty Project?
==========================================

This is useful when establishing a build pipeline for a new repository.