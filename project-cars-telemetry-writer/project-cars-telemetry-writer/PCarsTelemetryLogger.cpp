#include "PCarsTelemetryLogger.h"

PCarsTelemetryLogger::PCarsTelemetryLogger(const char* basePath, int interval, int bufSize)
{
	//Set base path
	this->basePath = basePath;

	//Set the time
	this->delta = interval;

	//Set buffer stuff
	this->bufSize = bufSize;
}

void PCarsTelemetryLogger::updateLogger(PCarsSharedMemory* pcars)
{
	//printf("%d\n", ticks);

	if (this->bufIndex == (this->bufSize - 1))
	{
		for (int i = 0; i < buffer->size(); i++)
			buffer->at(i).write(file) << std::endl;

		buffer->clear();
	}

	PCarsTelemetry data;

	data.antiLockActive = pcars->game->mAntiLockActive;
	//--
	data.avX = pcars->game->mAngularVelocity[0];
	data.avY = pcars->game->mAngularVelocity[1];
	data.avZ = pcars->game->mAngularVelocity[2];
	//--
	data.aX = pcars->game->mLocalAcceleration[0];
	data.aY = pcars->game->mLocalAcceleration[1];
	data.aZ = pcars->game->mLocalAcceleration[2];
	//--
	data.vX = pcars->game->mLocalVelocity[0];
	data.vY = pcars->game->mLocalVelocity[1];
	data.vZ = pcars->game->mLocalVelocity[2];
	//--
	data.pX = pcars->game->mParticipantInfo->mWorldPosition[0];
	data.pY = pcars->game->mParticipantInfo->mWorldPosition[1];
	data.pZ = pcars->game->mParticipantInfo->mWorldPosition[2];
	//--
	data.rX = pcars->game->mOrientation[0];
	data.rY = pcars->game->mOrientation[1];
	data.rZ = pcars->game->mOrientation[2];
	//--
	data.brakeTemp = pcars->game->mBrakeTempCelsius[0] + pcars->game->mBrakeTempCelsius[1] + pcars->game->mBrakeTempCelsius[2] + pcars->game->mBrakeTempCelsius[3];
	data.waterTemp = pcars->game->mWaterTempCelsius;
	data.windX = pcars->game->mWindDirectionX;
	data.windY = pcars->game->mWindDirectionY;
	data.windSpeed = pcars->game->mWindSpeed;
	//--
	data.tyreGrip_FL = pcars->game->mTyreGrip[0];
	data.tyreGrip_FR = pcars->game->mTyreGrip[1];
	data.tyreGrip_BL = pcars->game->mTyreGrip[2];
	data.tyreGrip_BR = pcars->game->mTyreGrip[3];
	//--
	data.tyreTemp_FL = pcars->game->mTyreTreadTemp[0];
	data.tyreTemp_FR = pcars->game->mTyreTreadTemp[1];
	data.tyreTemp_BL = pcars->game->mTyreTreadTemp[2];
	data.tyreTemp_BR = pcars->game->mTyreTreadTemp[3];
	//--
	data.tyreRPS_FL = pcars->game->mTyreRPS[0];
	data.tyreRPS_FR = pcars->game->mTyreRPS[1];
	data.tyreRPS_BL = pcars->game->mTyreRPS[2];
	data.tyreRPS_BR = pcars->game->mTyreRPS[3];
	//--
	data.brake = pcars->game->mBrake;
	data.throttle = pcars->game->mThrottle;
	data.rpm = pcars->game->mRpm;
	data.clutch = pcars->game->mClutch;
	data.gear = pcars->game->mGear;
	data.speed = pcars->game->mSpeed;
	//--
	data.time = pcars->game->mCurrentTime;

	buffer->push_back(data);
}

void PCarsTelemetryLogger::restart(void)
{
	//the player has restarted the game before finishing the race..
	//so we want to delete the log currently being written to
	//..

	file.close();
	printf("restarted. deleting file %s\n", this->fileName.c_str());
	std::remove(this->fileName.c_str());
}

bool PCarsTelemetryLogger::update(PCarsSharedMemory* pcars)
{
	if (!started)
		return false;

	int newTicks = GetTickCount();

	if ((newTicks - this->ticks) > this->delta)
	{
		this->bufIndex = (this->bufIndex + 1) % (this->bufSize);
		this->updateLogger(pcars);
		this->ticks = newTicks;
		return true;
	}

	return false;
}

void PCarsTelemetryLogger::start(PCarsSharedMemory* pcars)
{
	printf("started -- open file\n");
	 
	//Create buffer or clear if needed
	if (!buffer)
		buffer = new std::vector<PCarsTelemetry>();
	else
		buffer->clear();

	//Build string
	std::stringstream stream;

	//Build string more
	stream << basePath << "\\";
	stream << "pcars-play-" << std::time(NULL) << ".log";

	//Open the file
	this->fileName = stream.str();
	this->file.open(this->fileName);

	//Started is true now
	this->started = true;
	this->bufIndex = 0;
	this->ticks = GetTickCount();

	//Write session header
	file << "[session]" << std::endl;
	file << "car = " << pcars->game->mCarName << std::endl;
	file << "class = " << pcars->game->mCarClassName << std::endl;
	file << "track = " << pcars->game->mTrackLocation << std::endl;
	file << "track_variant = " << pcars->game->mTrackVariation << std::endl;
	file << std::endl;

	//Write out data header
	file << "[data]" << std::endl;
}

void PCarsTelemetryLogger::finish(PCarsSharedMemory* pcars)
{
	printf("finished -- write to file \n");

	//Write out finished header
	file << std::endl;
	file << "[lap_info]" << std::endl;
	file << "lap_time = " << pcars->game->mCurrentTime << std::endl;
	file << "flap_time = " << pcars->getLapTime() << std::endl;
	file << "total_laps = " << pcars->game->mParticipantInfo->mLapsCompleted << std::endl << std::endl;

	//Open up the name file
	std::ifstream t("C:\\pcars-logs\\name");
	std::stringstream buffer;
	buffer << t.rdbuf();

	//And write the data into there
	file << "[participant]" << std::endl;
	file << "name = " << buffer.str() << std::endl;
	t.close();

	//Close file
	this->file.close();

	//Rename it to something nicer
	std::stringstream sstream;
	sstream << this->basePath << "\\" << pcars->getLapTime(true) << " - " << pcars->game->mTrackLocation << " - " << pcars->game->mCarName << " - " << std::time(NULL) << ".log";
	std::string str = sstream.str();
	std::rename(this->fileName.c_str(), str.c_str());

	//Set started to false
	this->started = false;
}