#include <iostream>
#include <Windows.h>
#include "PCarsSharedMemory.h"
#include <random>

// Name of the pCars memory mapped file
#define MAP_OBJECT_NAME "$pcars$"

PCarsSharedMemory::PCarsSharedMemory(void)
{
	std::srand(GetTickCount());

	shuffle();
}

void PCarsSharedMemory::shuffle(void)
{
	std::random_shuffle(this->shuffled.begin(), this->shuffled.end());
}

SharedMemory* PCarsSharedMemory::getSharedMemory(void) const
{
	// Open the memory-mapped file
	HANDLE fileHandle = OpenFileMappingA(PAGE_READONLY, FALSE, MAP_OBJECT_NAME);

	if (fileHandle == NULL)
		return NULL;

	// Get the data structure
	SharedMemory* sharedData = new SharedMemory;

	//Get map
	sharedData = (SharedMemory*)MapViewOfFile(fileHandle, PAGE_READONLY, 0, 0, sizeof(SharedMemory));

	if (sharedData == NULL)
	{
		CloseHandle(fileHandle);
		return NULL;
	}

	// Ensure we're sync'd to the correct data version
	if (sharedData->mVersion != SHARED_MEMORY_VERSION)
		return NULL;

	return sharedData;
}

bool PCarsSharedMemory::isPlayerInGame(void) const
{
	return (game->mGameState == GAME_INGAME_PAUSED || game->mGameState == GAME_INGAME_PLAYING);
}

bool PCarsSharedMemory::isPlayerInRaceSession(void) const
{
	return isPlayerInGame() && (game->mSessionState == SESSION_RACE);
}

bool PCarsSharedMemory::isPlayerRacing(void) const
{
	return isPlayerInGame() && isPlayerInRaceSession() && (game->mRaceState == RACESTATE_RACING);
}

bool PCarsSharedMemory::isRaceFinished(void) const
{
	return isPlayerInGame() && isPlayerInRaceSession() && (game->mRaceState == RACESTATE_FINISHED);
}

bool PCarsSharedMemory::isWaitingForRaceStart(void) const
{
	return isPlayerInGame() && (game->mRaceState == RACESTATE_NOT_STARTED);
}

std::string PCarsSharedMemory::getLapTime(bool fileFriendly) const
{
	float laptime = game->mCurrentTime;

	char time[32];

	int minutes = floorf(laptime / 60);
	int seconds = floorf(fmod(laptime, 60.0f));
	int milliseconds = floorf(fmod(laptime, 1.0f) * 1000000);

	if(!fileFriendly)
		sprintf_s(time, "%02d:%02d.%d", minutes, seconds, milliseconds);
	else
		sprintf_s(time, "%02d;%02d.%d", minutes, seconds, milliseconds);

	return std::string(time);
}

void PCarsSharedMemory::copyVector3(const float * src, float * dest)
{
	memcpy(dest, src, sizeof(float) * 3);
}

void PCarsSharedMemory::blockUntilDetected(void)
{
	while ((this->game = getSharedMemory()) == NULL);

	printf("[detected] Project CARS 2 -- opening shared memory!\n");
}

void PCarsSharedMemory::copyVector3Random(const float* src, float* dest, bool normalise)
{
	copyVector3Swizzled(src, dest, normalise);

	#define surge dest[0]
	#define roll dest[1]

	float surge_vaxis_p = max(surge, 0);
	float surge_vaxis_n = min(surge, 0);
	float roll_vaxis_p  = max(roll, 0);
	float roll_vaxis_n  = min(roll, 0);

	float vaxis[] = 
	{
		surge_vaxis_p, surge_vaxis_n,
		roll_vaxis_p,  roll_vaxis_n
	};

	//
	//shuffle vaxis!
	//

	surge = vaxis[shuffled[0]] + vaxis[shuffled[1]];
	roll  = vaxis[shuffled[2]] + vaxis[shuffled[3]];
}

void PCarsSharedMemory::copyVector3Swizzled(const float* src, float* dest, bool normalise)
{
	#define SCALE 1.0f

	#define mph_to_ms 0.44704f
	#define ms_to_g   0.101972f

	float norm_scale = 1.0f;

	if (normalise)
		norm_scale = 200.0f * mph_to_ms;

	//0: x, 1: y, 2: z
	dest[0] = (src[2] * SCALE * surge_mult) / norm_scale;
	dest[1] = (src[0] * SCALE * roll_mult)  / norm_scale;
	dest[2] = (src[1] * SCALE) / norm_scale;

	if (!normalise)
		dest[2] += ms_to_g;

	dest[0] = (this->invertSurge) ? (-dest[0]) : (dest[0]);
	dest[1] = (this->invertRoll)  ? (-dest[1]) : (dest[1]);
}