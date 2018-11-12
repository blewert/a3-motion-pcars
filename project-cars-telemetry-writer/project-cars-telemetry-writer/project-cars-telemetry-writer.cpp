// project-cars-telemetry-writer.cpp : Defines the entry point for the console application.
//

#include <vector>
#include "PCarsSharedMemory.h"
#include "PCarsTelemetryLogger.h"
#include <windows.h>

int main()
{
	//Hide the console first
	ShowWindow(GetConsoleWindow(), SW_HIDE);

	PCarsSharedMemory pcars;
	PCarsTelemetryLogger* logger = new PCarsTelemetryLogger("C:\\pcars-logs", 50, 5);

	printf("waiting for project cars to start\n");
	pcars.blockUntilDetected();

	unsigned int prevState = RACESTATE_INVALID;

	while (true)
	{
		if (pcars.game->mGameState == GAME_INGAME_PAUSED)
			continue;

		logger->update(&pcars);

		unsigned int state = pcars.game->mRaceState;

		if (prevState == RACESTATE_RACING && state <= 1)
			logger->restart();

		if (prevState == RACESTATE_NOT_STARTED && state == RACESTATE_RACING)
			logger->start(&pcars);

		if (prevState == RACESTATE_RACING && state == RACESTATE_FINISHED)
			logger->finish(&pcars);

		prevState = state;
	}

    return 0;
}

