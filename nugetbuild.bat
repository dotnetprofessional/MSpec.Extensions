"%ProgramFiles%\MSBuild\12.0\bin\amd64\msbuild.exe" MSpec.Extensions.sln /p:Configuration=Release /t:Rebuild

cd MSpec.Extensions
..\.nuget\nuget.exe pack
xcopy *.nupkg "C:\Users\Garry\SkyDrive\Public\nuget" /F /Y
cd ..
