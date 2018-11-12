#pragma once

#include <fstream>
#include <vector>
#include <ctime>
#include <sstream>
#include <windows.h>

#include "PCarsSharedMemory.h"

typedef struct 
{
	float time;

	float pX;
	float pY;
	float pZ;

	float rX;
	float rY;
	float rZ;

	float vX;
	float vY;
	float vZ;

	float aX;
	float aY;
	float aZ;

	float avX;
	float avY;
	float avZ;

	int gear;

	float brake;
	float throttle;
	float clutch;

	float rpm;
	float speed;
	float steer;

	float tyreGrip_FL;
	float tyreGrip_FR;
	float tyreGrip_BL;
	float tyreGrip_BR;

	float tyreTemp_FL;
	float tyreTemp_FR;
	float tyreTemp_BL;
	float tyreTemp_BR;

	float tyreRPS_FL;
	float tyreRPS_FR;
	float tyreRPS_BL;
	float tyreRPS_BR;

	float windX;
	float windY;
	float windSpeed;

	float waterTemp;
	float brakeTemp;
	bool antiLockActive;	

	std::ofstream& write(std::ofstream& in)
	{
		in << \
			time << " " <<
			//---
			pX << " " << 
			pY << " " <<
			pZ << " " <<
			//---
			rX << " " <<
			rY << " " <<
			rZ << " " <<
			//---
			vX << " " <<
			vY << " " <<
			vZ << " " <<
			//---
			aX << " " <<
			aY << " " <<
			aZ << " " <<
			//---
			avX << " " <<
			avY << " " <<
			avZ << " " <<
			//---
			gear << " " <<
			//---
			brake << " " <<
			throttle << " " <<
			clutch << " " <<
			//---
			rpm << " " <<
			speed << " " <<
			steer << " " <<
			//---
			tyreGrip_FL << " " <<
			tyreGrip_FR << " " <<
			tyreGrip_BL << " " <<
			tyreGrip_BR << " " <<
			//---
			tyreTemp_FL << " " <<
			tyreTemp_FR << " " <<
			tyreTemp_BL << " " <<
			tyreTemp_BR << " " <<
			//---
			tyreRPS_FL << " " <<
			tyreRPS_FR << " " <<
			tyreRPS_BL << " " <<
			tyreRPS_BR << " " <<
			//---
			windX << " " <<
			windY << " " <<
			windSpeed << " " <<
			//---
			waterTemp << " " <<
			brakeTemp << " " <<
			antiLockActive;

		return in;
	}

} PCarsTelemetry;

class PCarsTelemetryLogger
{

public:

	std::vector<PCarsTelemetry>* buffer;

	int ticks;
	int delta;

	int bufSize;
	int bufIndex;

	const char* basePath;
	std::string fileName;
	bool started = false;

	std::ofstream file;

	PCarsTelemetryLogger(const char* basePath, int updateInterval, int bufferSize);

	bool update(PCarsSharedMemory* pcars);
	void updateLogger(PCarsSharedMemory* pcars);

	void restart(void);
	void start(PCarsSharedMemory* pcars);
	void finish(PCarsSharedMemory* pcars);
};