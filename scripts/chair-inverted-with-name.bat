@echo off

echo killing writer..
@taskkill /f /im:project-cars-telemetry-writer.exe
echo killing inverter..
@taskkill /f /im:chair-inverter-udp.exe

echo running writer..
pushd C:\git\chair-inverter-pcars\project-cars-telemetry-writer\x64\Debug
start project-cars-telemetry-writer.exe

cls

echo running inverter..
pushd C:\Users\Ben\source\repos\chair-inverter-udp\chair-inverter\x64\Debug
start chair-inverter-udp.exe -n -prompt-name -no-persist

popd
popd