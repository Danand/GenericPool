@title Create .unitypackage
@echo off
set unity_path=c:\Program Files\Unity\Hub\Editor\2019.2.3f1\Editor\Unity.exe
set solution_path=%~dp0
echo Creating package...
"%unity_path%" -projectPath "%solution_path%\GenericPool.Unity" -quit -batchmode -exportPackage "Assets/GenericPool.Unity" "%solution_path%\GenericPool.Unity.unitypackage"
echo Success.