/**
 * @file    UDPClient.cpp
 * @author  Benjamin Williams
 */

#include "UDPClient.h"

 /**
  * Constructs a new UDP client with an ip, port and tick rate
  */
UDPClient::UDPClient(std::string ip, int port, int tick_rate)
{
	//Set the ticks initially, and the tick rate
	this->ticks = GetTickCount();
	this->tick_rate = tick_rate;

	//Initialise winscok
	this->initWinsock();
}

/**
 * Initialises winsock
 */
void UDPClient::initWinsock(void)
{
	//Set up socket and data variables
	SOCKET ConnectSocket = INVALID_SOCKET;
	WSADATA wsaData;

	//Try to start winsock
	int iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);

	//Couldn't start? oh no
	if (iResult != 0)
		printf("WSAStartup failed with error: %d\n", iResult);

	//Fill hints with nothing
	ZeroMemory(&hints, sizeof(hints));

	//Set up the client for UDP traffic
	hints.ai_family = AF_INET;
	hints.ai_socktype = SOCK_DGRAM;
	hints.ai_protocol = IPPROTO_UDP;

	// Resolve the server address and port
	iResult = getaddrinfo(SimphynitySettings.IP.c_str(), SimphynitySettings.PORTSTR, &hints, &result);

	if (iResult != 0)
	{
		//Can't resolve? clean up and die
		printf("getaddrinfo failed with error: %d\n", iResult);
		WSACleanup();
	}

	// Attempt to connect to an address until one succeeds
	for (ptr = result; ptr != NULL; ptr = ptr->ai_next) 
	{
		// Create a SOCKET for connecting to server
		ConnectSocket = socket(ptr->ai_family, ptr->ai_socktype, ptr->ai_protocol);

		if (ConnectSocket == INVALID_SOCKET) 
		{
			printf("socket failed with error: %ld\n", WSAGetLastError());
			WSACleanup();
		}

		// Connect to server.
		iResult = connect(ConnectSocket, ptr->ai_addr, (int)ptr->ai_addrlen);

		if (iResult == SOCKET_ERROR) 
		{
			closesocket(ConnectSocket);
			ConnectSocket = INVALID_SOCKET;
			continue;
		}

		//Break if its not null
		break;
	}

	//Free address information struct
	freeaddrinfo(result);

	if (ConnectSocket == INVALID_SOCKET)
	{
		//Can't connect? clean up
		printf("Unable to connect to server!\n");
		WSACleanup();
	}

	//Set the connection socket
	this->sock = ConnectSocket;

	//Test string to send and test the send capabitilities
	const char *sendbuf = "this is a test";

	//Send it
	iResult = send(this->sock, sendbuf, (int)strlen(sendbuf), 0);

	if (iResult == SOCKET_ERROR) 
	{
		//Error? close itttt
		printf("send failed with error: %d\n", WSAGetLastError());
		closesocket(ConnectSocket);
		WSACleanup();
	}
}

/**
 * Sends a UDP packet
 */
void UDPClient::sendData(SimphynityUDPPacket* packet)
{
	//Send packet through this socket
	send(this->sock, (char*)packet, sizeof(SimphynityUDPPacket), 0);
}

/**
 * Update iteration for UDP client
 */
bool UDPClient::update(void)
{
	//Get current tick count
	int currentTicks = GetTickCount();

	//Return value is false initially
	bool returnValue = false;

	//Check difference
	if ((currentTicks - this->ticks) >= this->tick_rate)
	{
		//Reset current ticks
		this->ticks = GetTickCount();

		//Set return value to true
		returnValue = true;
	}

	//Return if there was a reset needed
	return returnValue;
}
