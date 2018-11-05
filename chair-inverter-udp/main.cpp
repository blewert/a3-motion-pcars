#include "main.h"

int main()
{
	//Make a new UDP client with given settings
	UDPClient* client = new UDPClient(SimphynitySettings.IP, SimphynitySettings.PORT, SimphynitySettings.TICK_SEND_RATE);

	bool invertRoll = false;
	bool invertSurge = false;

	//Create new shared memory stuff
	PCarsSharedMemory* pcars = new PCarsSharedMemory();

	//Make a new packet, allocate on the heap as we're gonna modify this throughout
	SimphynityUDPPacket* packet = new SimphynityUDPPacket();
	
	std::cout << "\t" << std::endl;
	std::cout << "\t CHAIR INVERTER" << std::endl << std::endl;
	std::cout << "\t(PCARS over UDP)" << std::endl;
	std::cout << "\t" << std::endl << std::endl;

	//Prompt them
	std::cout << ">> Invert surge? (forward/back) [1/0]: ";
	std::cin >> invertSurge;

	//Prompt them
	std::cout << ">> Invert roll? (side-to-side) [1/0]: ";
	std::cin >> invertRoll;

	std::cout << std::endl << ">> Do you want to modify roll/surge multipliers? [y/n]: ";
	std::cin.ignore();

	if (getchar() == 'y')
	{
		std::cout << ">> Enter surge multiplier (forwards/backwards): ";
		std::cin >> pcars->surge_mult;

		std::cout << ">> Enter roll multiplier (side-to-side): ";
		std::cin >> pcars->roll_mult;
	}

	printf("\n\n[Starting inverter for Atomic A3, Project CARS]");
	printf("\n- Surge inverted: %d", invertSurge);
	printf("\n- Roll inverted: %d", invertRoll);
	printf("\n- Surge multiplier: %.3f", pcars->surge_mult);
	printf("\n- Roll multiplier: %.3f\n\n", pcars->roll_mult);

	//Set up axis inversion
	pcars->invertRoll = invertRoll;
	pcars->invertSurge = invertSurge;

	while (true)
	{
		static int t = 0;

		if (client->update())
		{
			//Is the race finished? If yes, skip.. we don't care about motion
			if (pcars->game->mRaceState == RACESTATE_FINISHED)
				continue;

			//Is the race not started? If no, skip.. we don't care about motion
			if (pcars->game->mRaceState == RACESTATE_NOT_STARTED)
				continue;

			//The client needs to update. So, build a relevant simphynity packet
			//...

			//We're gonna use local values, not global values
			packet->useLocalVals = true;

			//Set global/local velocity
			pcars->copyVector3Swizzled(pcars->game->mLocalVelocity,     packet->localVel, true);
			pcars->copyVector3Swizzled(pcars->game->mLocalAcceleration, packet->localAccel, false);
			pcars->copyVector3Swizzled(pcars->game->mWorldVelocity,     packet->globalVel, true);
			
			//Set packet millisecond frame time
			packet->packetTimeMillis = GetTickCount();

			//Counter mod value per 250ms
			int c = (int) (1.0f / SimphynitySettings.TICK_SEND_RATE * 1000) * 0.25f;

			//Show log
			if (t++ % c == 0)
				printf("[sent packet %04d]: accel %.2f %.2f %.2f\n", t, packet->localAccel[0], packet->localAccel[1], packet->localAccel[2]);

			//And send data
			client->sendData(packet);
		}
	}

    return 0;
}

