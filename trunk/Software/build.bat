call p.bat
echo.
echo.
echo Building img/flashn.vxx
echo.
vxa src/flashn.vxa -l -m -o img/flashn.vxx
echo.
echo.
echo Building img/qf.vxx
echo.
vxa src/qf.vxa -o img/qf.vxx
echo.
echo.
echo Building img/readpins.vxx
echo.
vxa src/readpins.vxa -o img/readpins.vxx
echo.
echo.
echo Building img/flash.vxx
echo.
vxa src/flash.vxa -o img/flash.vxx
echo.
echo.
echo Building disc image from img/
echo.
vxic img
