call p.bat
echo.
echo.
echo Compiling src/program.vxc
echo.
vxc src/program.vxc
echo.
echo.
echo Assembling src/program.vxa
echo.
vxa src/program.vxa -o img/program.vxx
echo.
echo.
echo Building disc image from img/
echo.
vxic img
