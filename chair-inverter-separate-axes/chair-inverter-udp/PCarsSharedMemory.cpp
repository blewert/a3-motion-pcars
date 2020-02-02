/**
 * @file    PCarsSharedMemory.cpp
 * @author  Benjamin Williams
 */

//Includes
#include <iostream>
#include <Windows.h>
#include "PCarsSharedMemory.h"
#include "Simphynity.h"
#include <random>

// Name of the pCars memory mapped file
#define MAP_OBJECT_NAME "$pcars$"

/**
 * Returns the number of CPU cycles via rdtsc
 */
DWORD64 __inline __volatile __fastcall PCarsSharedMemory::getCPUCycles(void) const
{
	//The low and high bits of rdtsc
	uint32_t low = 0, high = 0;

	__asm __volatile
	{
		//Save eax/edx, rdtsc into eax:edx
		push eax
		push edx
		rdtsc

		//move edx -> hi, 
		//move eax -> lo
		mov [ebp - 4], edx
		mov [ebp - 8], eax

		//Restore state of original stack
		pop eax
		pop edx
	}

	//Glue them together
	return ((uint64_t)low) | ((uint64_t)high << 32);
}



/**
 * Constructs an instance of PCarsSharedMemory
 */
PCarsSharedMemory::PCarsSharedMemory(void)
{
	//Get the cpu cycles
	uint64_t cycles = this->getCPUCycles();
		 
	//Seed the RNG generator
	std::srand(this->getCPUCycles());

	//Call shuffle
	shuffle();
}


/**
 * Shuffles the vector randomly
 */
void PCarsSharedMemory::shuffle(void)
{
	//Just shuffle using std::random_shuffle
	//std::random_shuffle(this->shuffled.begin(), this->shuffled.end());
}


/**
 * Opens shared memory for Project CARS 2
 */
SharedMemory* PCarsSharedMemory::getSharedMemory(void) const
{
	// Open the memory-mapped file
	HANDLE fileHandle = OpenFileMappingA(PAGE_READONLY, FALSE, MAP_OBJECT_NAME);

	//If it can't open, return null
	if (fileHandle == NULL)
		return NULL;

	// Get the data structure
	SharedMemory* sharedData = new SharedMemory;

	//Get map
	sharedData = (SharedMemory*)MapViewOfFile(fileHandle, PAGE_READONLY, 0, 0, sizeof(SharedMemory));

	if (sharedData == NULL)
	{
		//Is shared data null? If so, close and
		//return null
		CloseHandle(fileHandle);
		return NULL;
	}

	// Ensure we're sync'd to the correct data version
	if (sharedData->mVersion != SHARED_MEMORY_VERSION)
		return NULL;

	//Otherwise everything is good -- return the data
	return sharedData;
}


/**
 * Returns if the player is in the game
 */
bool PCarsSharedMemory::isPlayerInGame(void) const
{
	return (game->mGameState == GAME_INGAME_PAUSED || game->mGameState == GAME_INGAME_PLAYING);
}

/**
 * Returns if the player is in a race session
 */
bool PCarsSharedMemory::isPlayerInRaceSession(void) const
{
	return isPlayerInGame() && (game->mSessionState == SESSION_RACE);
}

/**
 * Returns if the player is racing
 */
bool PCarsSharedMemory::isPlayerRacing(void) const
{
	return isPlayerInGame() && isPlayerInRaceSession() && (game->mRaceState == RACESTATE_RACING);
}

/**
 * Returns if the race is finished
 */
bool PCarsSharedMemory::isRaceFinished(void) const
{
	return isPlayerInGame() && isPlayerInRaceSession() && (game->mRaceState == RACESTATE_FINISHED);
}

/**
 * Returns if the player is waiting for the next race start
 */
bool PCarsSharedMemory::isWaitingForRaceStart(void) const
{
	return isPlayerInGame() && (game->mRaceState == RACESTATE_NOT_STARTED);
}

/**
 * Like sprintf_s but for std::string
 */
//std::string PCarsSharedMemory::formatString(const std::string& format, ...) const
//{
//	//List of args, start according to format
//	va_list vl;
//	va_start(vl, format);
//
//	//Pointer to array which will be allocated
//	char* buffer;
//
//	//Find the length of things to format
//	unsigned int len = _vscprintf(format.c_str(), vl);
//
//	//Allocate that number of things
//	buffer = (char*)malloc(sizeof(char) * len);
//
//	//Save into this string
//	vsprintf_s(buffer, 256, format.c_str(), vl);
//
//	//Call va_end
//	va_end(vl);
//
//	//Make the string
//	std::string str = std::string(buffer);
//
//	//Free the buffer
//	free(buffer);
//
//	//Return the string!
//	return str;
//}

/**
 * Gets the lap time as a string
 */
std::string PCarsSharedMemory::getLapTime(void) const
{
	//Find the lap time
	float laptime = game->mLastLapTime;

	//Allocate space 
	char time[32];

	//Minutes, seconds and milliseconds
	int minutes      = floorf(laptime / 60);
	int seconds      = floorf(fmod(laptime, 60.0f));
	int milliseconds = floorf(fmod(laptime, 1.0f) * 1000000);

	sprintf_s(time, "%02d:%02d.%d", minutes, seconds, milliseconds);

	//Format the string
	//std::string time = this->formatString("%02d:%02d.%d", minutes, seconds, milliseconds);

	//And return the time
	return time;
}

/**
 * Copies a vector3 (array) from src to desc
 */
void PCarsSharedMemory::copyVector3(const float* src, float* dest)
{
	//Just use memcpy
	memcpy(dest, src, sizeof(float) * 3);
}

/**
 * Blocks the process until Project CARS 2 is detected
 */
void __volatile PCarsSharedMemory::blockUntilDetected(void)
{
	//Just run while shared memory is null (call noop)
	while ((this->game = getSharedMemory()) == NULL) 
		__noop();

	//Call printf after detection
	printf("[detected] Project CARS 2 -- opening shared memory!\n");
}

/**
 * Copies a vector3 with shuffling
 */
void PCarsSharedMemory::copyVector3Random(const float* src, float* dest, bool normalise)
{
	//Swizzle the vec3f
	copyVector3Swizzled(src, dest, normalise);

	//I'm so lazy
	#define surge dest[0]
	#define roll  dest[1]

	//Clamp all from negative to >0
	float surge_vaxis_p = max(surge, 0);
	float surge_vaxis_n = min(surge, 0);
	float roll_vaxis_p  = max(roll, 0);
	float roll_vaxis_n  = min(roll, 0);

	//Set vaxes
	float vaxis[] = 
	{
		surge_vaxis_p, surge_vaxis_n,
		roll_vaxis_p,  roll_vaxis_n
	};

	//Calculate surge and roll
	surge = vaxis[shuffled[0]] + vaxis[shuffled[1]];
	roll  = vaxis[shuffled[2]] + vaxis[shuffled[3]];
}

/**
 * Copies a vector3 with swizzling
 */
void PCarsSharedMemory::copyVector3Swizzled(const float* src, float* dest, bool normalise)
{
	//The scale
	const float SCALE = 1.0f;

	//SI unit conversion constants
	const float mph_to_ms = 0.44704f;
	const float ms_to_g = 0.101972f;

	//Normalisation scale
	float norm_scale = 1.0f;

	//If normalised, set normal scale to 200mph max
	if (normalise)
		norm_scale = 200.0f * mph_to_ms;

	//0: x, 1: y, 2: z
	dest[0] = (src[2] * SCALE * surge_mult) / norm_scale;
	dest[1] = (src[0] * SCALE * roll_mult)  / norm_scale;
	dest[2] = (src[1] * SCALE) / norm_scale;

	//Not normalising? Add gravity
	if (!normalise)
		dest[2] += ms_to_g;

	//Do we need to flip it?
	dest[0] = (this->invertSurge) ? (-dest[0]) : (dest[0]);
	dest[1] = (this->invertRoll)  ? (-dest[1]) : (dest[1]);

	//Do we need to cancel it?
	dest[0] = (int)(this->cancelSurge) * dest[0];
	dest[1] = (int)(this->cancelRoll)  * dest[1];
}