@echo Off

REM No glob support on Windows
dotnet restore 
dotnet test ./tests/SixLabors.Shapes.Tests/

REM run only if gitversion has ran i.e. from appveyor    
 if not "%GitVersion_NuGetVersion%" == "" (
     cd src/SixLabors.Shapes
     ECHO Setting version number to "%GitVersion_NuGetVersion%"
     dotnet version "%GitVersion_NuGetVersion%"
     cd ../../
     if not "%errorlevel%"=="0" goto failure
 )

ECHO Building nuget packages
if not "%GitVersion_NuGetVersion%" == "" (
	dotnet pack -c Release --output ./artifacts ./src/SixLabors.Shapes/project.json 
)ELSE ( 
	dotnet pack -c Release --version-suffix "local-build"  --output ./artifacts ./src/SixLabors.Shapes/project.json
)
if not "%errorlevel%"=="0" goto failure

:success
ECHO successfully built project
REM exit 0
goto end

:failure
ECHO failed to build.
REM exit -1
goto end

:end