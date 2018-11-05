#include "UDPClient.h"

UDPClient::UDPClient(std::string ip, int port, int tick_rate)
{
	this->ticks = GetTickCount();
	this->tick_rate = tick_rate;

	this->initWinsock();
}

void UDPClient::initWinsock(void)
{
	SOCKET ConnectSocket = INVALID_SOCKET;
	WSADATA wsaData;
	int iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);

	if (iResult != 0) {
		printf("WSAStartup failed with error: %d\n", iResult);
	}

	ZeroMemory(&hints, sizeof(hints));

	//Set up the client for UDP traffic
	hints.ai_family = AF_INET;
	hints.ai_socktype = SOCK_DGRAM;
	hints.ai_protocol = IPPROTO_UDP;

	// Resolve the server address and port
	//iResult = getaddrinfo(argv[1], DEFAULT_PORT, &hints, &result);
	iResult = getaddrinfo(SimphynitySettings.IP.c_str(), SimphynitySettings.PORTSTR, &hints, &result);

	if (iResult != 0) {
		printf("getaddrinfo failed with error: %d\n", iResult);
		WSACleanup();
	}

	// Attempt to connect to an address until one succeeds
	for (ptr = result; ptr != NULL; ptr = ptr->ai_next) {

		// Create a SOCKET for connecting to server
		ConnectSocket = socket(ptr->ai_family, ptr->ai_socktype,
			ptr->ai_protocol);
		if (ConnectSocket == INVALID_SOCKET) {
			printf("socket failed with error: %ld\n", WSAGetLastError());
			WSACleanup();
		}

		// Connect to server.
		iResult = connect(ConnectSocket, ptr->ai_addr, (int)ptr->ai_addrlen);
		if (iResult == SOCKET_ERROR) {
			closesocket(ConnectSocket);
			ConnectSocket = INVALID_SOCKET;
			continue;
		}
		break;
	}

	freeaddrinfo(result);

	if (ConnectSocket == INVALID_SOCKET) {
		printf("Unable to connect to server!\n");
		WSACleanup();
	}

	this->sock = ConnectSocket;

	const char *sendbuf = "this is a test";

	// Send an initial buffer
	iResult = send(this->sock, sendbuf, (int)strlen(sendbuf), 0);
	if (iResult == SOCKET_ERROR) {
		printf("send failed with error: %d\n", WSAGetLastError());
		closesocket(ConnectSocket);
		WSACleanup();
	}
}


void UDPClient::sendData(SimphynityUDPPacket* packet)
{
	send(this->sock, (char*)packet, sizeof(SimphynityUDPPacket), 0);
}


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
