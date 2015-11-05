@echo off
setlocal enabledelayedexpansion

echo 当前目录：%CD%
echo 计算机名：%COMPUTERNAME%
echo 随机数（0~32767）：%RANDOM%
echo Windows目录：%WINDIR%
echo 当前用户：%USERNAME%
echo 域：%USERDOMAIN%
echo 时间：%TIME%
echo 临时目录：%TEMP%
echo 系统根位置：%SYSTEMROOT%
echo ______________________________________
echo.
echo 当前实际执行的.bat的位置：%0
echo %1
echo %2
call .\parameter.bat
echo 多层.bat调用会报错↑
echo 全部参数：%*
echo ______________________________________
echo.
REM set /p userInput=请输入姓名：
echo 你输入的名字是：%userInput%

set var1=1+1
echo %var1%
set /A var2=1+1
echo %var2%

set aa=0812
set /a aa=1%aa%-10000
echo %aa%

echo set /a命名可以执行很多种数值运算，包括位运算
echo ______________________________________
echo.
set bbb=我   在  这 里
set bbbb=%bbb: =%
echo 去除空格后：
echo %bbbb%
echo %bbb:这=那%

REM 从第1个（计数从0开始）开始截取2个长度；长度可以省略
set bbbbb=012345678
echo %bbbbb:~1,2%

echo ______________________________________
echo.

for /l %%i in (1,1,5) do (
set a=%%i
echo !a!
)
pause