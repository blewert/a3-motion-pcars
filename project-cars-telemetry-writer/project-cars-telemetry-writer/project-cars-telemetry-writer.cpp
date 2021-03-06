// project-cars-telemetry-writer.cpp : Defines the entry point for the console application.
//

#include <vector>
#include "PCarsSharedMemory.h"
#include "PCarsTelemetryLogger.h"
//--
#include <cstdio>
#include <windows.h>
#include <tlhelp32.h>

/*!
\brief Check if a process is running
\param [in] processName Name of process to check if is running
\returns \c True if the process is running, or \c False if the process is not running
*/
bool IsProcessRunning(const wchar_t *processName)
{
	bool exists = false;
	PROCESSENTRY32 entry;
	entry.dwSize = sizeof(PROCESSENTRY32);

	HANDLE snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, NULL);

	if (Process32First(snapshot, &entry))
		while (Process32Next(snapshot, &entry))
			if (!_wcsicmp(entry.szExeFile, processName))
				exists = true;

	CloseHandle(snapshot);
	return exists;
}


int main()
{
	//Hide the console first
	ShowWindow(GetConsoleWindow(), SW_HIDE);

	while (true)
	{
		PCarsSharedMemory pcars;
		PCarsTelemetryLogger* logger = new PCarsTelemetryLogger("C:\\pcars-logs", 50, 5);

		printf("waiting for project cars to start\n");
		pcars.blockUntilDetected();

		unsigned int prevState = RACESTATE_INVALID;

		while (true)
		{
			if (pcars.game->mGameState == GAME_INGAME_PAUSED)
				continue;

			bool loggerUpdated = logger->update(&pcars);

			if (!IsProcessRunning(L"pcars2avx.exe"))
			{
				printf("process closed.. breaking\n");
				logger->finish(&pcars);
				break;
			}

			unsigned int state = pcars.game->mRaceState;

			if (prevState == RACESTATE_RACING && state <= 1)
				logger->restart();

			if (prevState == RACESTATE_NOT_STARTED && state == RACESTATE_RACING)
				logger->start(&pcars);

			if (prevState == RACESTATE_RACING && state == RACESTATE_FINISHED)
				logger->finish(&pcars);

			prevState = state;
		}

		printf("restarting outer loop\n");

		delete logger;
		logger = NULL;

		CloseHandle(pcars.fileHandle);
	}
    return 0;
}

