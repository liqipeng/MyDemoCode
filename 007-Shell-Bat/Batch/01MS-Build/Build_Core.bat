REM ToDo: 如果路径含有空格等符号会有bug

@echo off
setlocal enabledelayedexpansion

REM 中文版错误标记
SET ErrorFlag= 个错误
REM 英文版错误标记
REM SET ErrorFlag= Error(s)

IF "%1"=="" (
  echo 没有指定项目列表文件
  echo 按任意键退出
  pause
  exit
) ELSE (
  echo 项目列表：%1
  echo ____________
  echo 正在开始编译
  pause
)

SET LogDir=logs_default

IF NOT "%2"=="" (
  IF EXIST %2 (
    rd /s /q %2
  )
  SET LogDir=%2
)

SET ErrorProjects=error_projects_default.txt

IF NOT "%3"=="" (
  SET ErrorProjects=%3
)
SET TEMP_Error_Projects=_Temp_%ErrorProjects%
REM IF EXIST %ErrorProjects% (
REM  del %ErrorProjects%
REM )
REM (echo #) > %ErrorProjects%

mkdir %LogDir%

REM 备份path
set originalPath=%path%
path %SYSTEMROOT%\Microsoft.NET\Framework64\v4.0.30319\
SET MSBuild_Params=/t:Rebuild /p:Configuration=Release /p:VisualStudioVersion=12.0 /l:FileLogger,Microsoft.Build.Engine;logfile=.\%LogDir%\

FOR /F "eol=#" %%i in (%1) do ( 
  for /F "usebackq delims=[]" %%I in (`echo %cd%\%%i`) do echo.正在生成%%~nxI
  for /F "usebackq delims=[]" %%I in (`echo %cd%\%%i`) do ( 
    msbuild.exe %%i %MSBuild_Params%%%~nxI.log.txt
	cd %LogDir%
    %SYSTEMROOT%\System32\find.exe /N "0%ErrorFlag%" %%~nxI.log.txt
    IF NOT "!errorlevel!"=="0" (
      echo %%i >> ..\%TEMP_Error_Projects%
    ) else (
      echo Build successfully.
    )
    echo current dir %cd%
	cd ..
  )
)

echo 所有项目完毕，编译日志见%LogDir%

REM 还原path
path %originalPath%

pause
REM ----------日志分析----------
echo 开始分析日志

cls
cd %LogDir%
%SYSTEMROOT%\System32\find.exe /n "%ErrorFlag%" *.log.txt
cd ..

IF EXIST %TEMP_Error_Projects% (
  del %ErrorProjects%
  move %TEMP_Error_Projects% %ErrorProjects%
)

REM 生成Retry
SET RetryBatName=%~n1.Retry.bat
IF EXIST %RetryBatName% (
  del %RetryBatName%
)
echo @echo off >> %RetryBatName%
echo set name=%~n3 >> %RetryBatName%
echo set logDir=logs_%%name%% >> %RetryBatName%
echo call ..\Build_Core.bat %ErrorProjects% %%logDir%% >> %RetryBatName%
echo pause >> %RetryBatName%