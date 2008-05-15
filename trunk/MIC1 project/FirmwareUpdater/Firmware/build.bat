@echo off

rem copy %1 config.inc

rem "C:\Program Files\Atmel\AVR Tools\AvrAssembler2\avrasm2.exe" -S "labels.tmp" -fI -W+ie -o "FirmwareUpdater.hex" -d "f:\Projects\FirmwareUpdater\Firmware\FirmwareUpdater.obj" -e "FirmwareUpdater.eep" -m "FirmwareUpdater.map" "FirmwareUpdater.asm"

rem -fI => Intel hex file
rem -W+ie => Generate for unsupported instructions
rem -o => Name of output file
"C:\Program Files\Atmel\AVR Tools\AvrAssembler2\avrasm2.exe" -fI -W+ie -o "FirmwareUpdater.hex" "FirmwareUpdater.asm"
pause
