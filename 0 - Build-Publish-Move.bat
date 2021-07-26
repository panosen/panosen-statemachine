@echo off

dotnet restore

dotnet build --no-restore -c Release

move /Y Panosen.StateMachine\bin\Release\Panosen.StateMachine.*.nupkg D:\LocalSavoryNuget\

pause