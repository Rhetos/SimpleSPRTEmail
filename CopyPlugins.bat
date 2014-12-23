ECHO Target folder = [%1]
ECHO $(ConfigurationName) = [%2]

REM "%~dp0" is this script's folder.

XCOPY /Y/D/R "%~dp0"Plugins\Rhetos.AspNetFormsAuth.SimpleSPRTEmail\bin\%2\Rhetos.AspNetFormsAuth.SimpleSPRTEmail.dll %1 || EXIT /B 1
XCOPY /Y/D/R "%~dp0"Plugins\Rhetos.AspNetFormsAuth.SimpleSPRTEmail\bin\%2\Rhetos.AspNetFormsAuth.SimpleSPRTEmail.pdb %1 || EXIT /B 1

EXIT /B 0
