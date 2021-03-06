#include "main.h"

int main()
{
	//Make a new UDP client with given settings
	UDPClient* client = new UDPClient(SimphynitySettings.IP, SimphynitySettings.PORT, SimphynitySettings.TICK_SEND_RATE);

	//Create new shared memory stuff
	PCarsSharedMemory* pcars = new PCarsSharedMemory();

	//Make a new packet, allocate on the heap as we're gonna modify this throughout
	SimphynityUDPPacket* packet = new SimphynityUDPPacket();

	//Shortcut keys
	ShortcutKeys keys;

	//Print out program prologue
	std::cout << "\t" << std::endl;
	std::cout << "\t CHAIR INVERTER" << std::endl << std::endl;
	std::cout << "\t(PCARS over UDP)" << std::endl;
	std::cout << "\t" << std::endl << std::endl;

	//Prompt them
	std::cout << ">> Invert surge? (forward/back) [1/0]: ";
	std::cin >> pcars->invertSurge;

	//Prompt them
	std::cout << ">> Invert roll? (side-to-side) [1/0]: ";
	std::cin >> pcars->invertRoll;

	//Prompt them
	std::cout << ">> Use noise? (all axes) [1/0]: ";
	std::cin >> pcars->useNoise;

	//Prompt them
	std::cout << std::endl << ">> Do you want to modify roll/surge multipliers? [y/n]: ";
	std::cin.ignore();

	//Have they said yes?
	if (getchar() == 'y')
	{
		//Prompt user
		std::cout << ">> Enter surge multiplier (forwards/backwards): ";
		std::cin >> pcars->surge_mult;

		//Prompt user
		std::cout << ">> Enter roll multiplier (side-to-side): ";
		std::cin >> pcars->roll_mult;
	}

	//Print out a bunch of info
	printf("\n\n[Starting inverter for Atomic A3, Project CARS]");
	printf("\n- Surge inverted: %d", pcars->invertSurge);
	printf("\n- Roll inverted: %d", pcars->invertRoll);
	printf("\n- Surge multiplier: %.3f", pcars->surge_mult);
	printf("\n- Roll multiplier: %.3f\n\n", pcars->roll_mult);

	//Block until shared memory is detected
	pcars->blockUntilDetected();

	while (true)
	{
		//Update shortcut key states
		keys.update();

		//If they want to invert the roll, then invert the roll
		if (keys.invertRollKey.justPressed)
		{
			pcars->invertRoll = !pcars->invertRoll;
			printf("[key press] Roll inverted: %d\n", pcars->invertRoll);
		}
		
		//If they want to invert the surge, then invert the surge
		if (keys.invertSurgeKey.justPressed)
		{
			pcars->invertSurge = !pcars->invertSurge;
			printf("[key press] Surge inverted: %d\n", pcars->invertSurge);
		}

		//Counter for packets sent
		static int t = 0;

		if (client->update())
		{
			//They're on the main menu.. skip
			if (pcars->game->mGameState == GAME_FRONT_END)
				continue;

			//They're in replay mode.. skip
			if (pcars->game->mGameState == GAME_REPLAY)
				continue;
			
			if (pcars->game->mSessionState != SESSION_TIME_ATTACK)
			{
				//Is the race finished? If yes, skip.. we don't care about motion
				if (pcars->game->mRaceState == RACESTATE_FINISHED)
					continue;
			}

			//The client needs to update. So, build a relevant simphynity packet
			//...

			//We're gonna use local values, not global values
			packet->useLocalVals = true;

			//Set global/local velocity
			if (!pcars->useNoise)
			{
				pcars->copyVector3Swizzled(pcars->game->mLocalVelocity, packet->localVel,       true);
				pcars->copyVector3Swizzled(pcars->game->mLocalAcceleration, packet->localAccel, false);
				pcars->copyVector3Swizzled(pcars->game->mWorldVelocity, packet->globalVel,      true);
			}
			else
			{
				pcars->copyVector3Random(pcars->game->mLocalVelocity, packet->localVel,       true);
				pcars->copyVector3Random(pcars->game->mLocalAcceleration, packet->localAccel, false);
				pcars->copyVector3Random(pcars->game->mWorldVelocity, packet->globalVel,      true);
			}
			
			//Set packet millisecond frame time
			packet->packetTimeMillis = GetTickCount();

			//Counter mod value per 250ms
			int c = (int) (1.0f / SimphynitySettings.TICK_SEND_RATE * 1000) * 0.25f;
			int second = (int)(1.0f / SimphynitySettings.TICK_SEND_RATE * 1000);

			//Show log
			if (t++ % c == 0)
				printf("[sent packet %04d]: accel %.2f %.2f %.2f\n", t, packet->localAccel[0], packet->localAccel[1], packet->localAccel[2]);

			if (t % (second * 4) == 0)
			{
				pcars->shuffle();
				printf(">> shuffled: %d %d %d %d\n", pcars->shuffled[0], pcars->shuffled[1], pcars->shuffled[2], pcars->shuffled[3]);
			}

			//And send data
			client->sendData(packet);
		}
	}


    return 0;
}

