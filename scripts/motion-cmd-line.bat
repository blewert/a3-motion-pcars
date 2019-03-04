@echo off

echo killing writer..
@taskkill /f /im:project-cars-telemetry-writer.exe
echo killing inverter..
@taskkill /f /im:chair-inverter-udp.exe

pushd C:\git\chair-inverter-pcars\project-cars-telemetry-writer\x64\Debug

if "%3"=="yes" GOTO skip
echo running writer..
start project-cars-telemetry-writer.exe
:skip


echo running inverter..
pushd C:\Users\Ben\source\repos\chair-inverter-udp\chair-inverter\x64\Debug
start chair-inverter-udp.exe %1 -x%2 -no-persist

popd
popd

echo start chair-inverter-udp.exe %1 -x%2
