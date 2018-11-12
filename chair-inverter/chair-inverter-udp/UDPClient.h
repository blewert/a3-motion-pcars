#pragma once


#define WIN32_LEAN_AND_MEAN
#include <windows.h>
#include <winsock2.h>
#include <ws2tcpip.h>

// Need to link with Ws2_32.lib, Mswsock.lib, and Advapi32.lib
#pragma comment (lib, "Ws2_32.lib")
#pragma comment (lib, "Mswsock.lib")
#pragma comment (lib, "AdvApi32.lib")

#define DEFAULT_BUFLEN 512

#include <string>
#include "Simphynity.h"

class UDPClient
{
public:
	UDPClient(std::string ip, int port, int tick_rate);
	bool update(void);
	void sendData(SimphynityUDPPacket* packet);
	void initWinsock(void);

	SOCKET sock;

	struct addrinfo *result = NULL,
		*ptr = NULL,
		hints;

	int ticks;
	int tick_rate;
};