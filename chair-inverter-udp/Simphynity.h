#pragma once

#include <Windows.h>
#include <string>

/*
* E.g. If the vehicle's +ve X axis points West (relative to the vehicle itself), its Y axis points North and its Z axis points UP,
*    the <b>axis</b> value would be <i>AXIS_X_WEST | AXIS_Y_NORTH | AXIS_Z_UP</i>
*
* <b>float accel[3]</b>
*
*  The vehicle linear acceleration vector, in M/S/S (meters per second per second).
*
* <b>float vel[3]</b>
*
*  The vehicle velocity vector, in M/S (meters per second).
*
* <b>float rotationMatrix[3][3]</b>
*
*  The rotation matrix representing the rotation of the vehicle relative to the world (global) co-ordinate system.
*
* <b>DWORD packetTimeMillis</b>
*
*  The world (game) time this packet pertains to, in milliseconds.  While this value is changing, Simphyinty assumes
*  motion output is active (i.e. it is used as a 'stay alive' time and must be changing for motion output to operate
*  correctly - this value would stop changing when the game is paused or in menus etc).
*/

/*
bool useLocalVals;

Instructs Simphynity to use the local values specified in 'localAccel' and 'localVel'.
Any values specified in 'globalVel' or 'rotationMatrix' will be ignored.
Similarly, if set to 'false', any values in 'localAccel' or 'localVel' will be ignored.
While the ability to simply specify local longitudinal, lateral and vertical acceleration (3-dof)
is offered for simplicity, the advantage of using global values is it will allow Simphynity to
derive up to 6-dof of motion data should future hardware devices support it.
Please note: This is a 4 byte value (first byte 0 = false, first byte > 0 = true).

float localAccel[3];

The longitudinal[0] (-ve = accelerating forward/leaning backwards), lateral[1] (-ve = accelerating right/leaning left) and
vertical[2] (+ve = down) acceleration values local to the vehicle's co-ordinate system, in ms2 (1G = 9.81ms2), -1G > +1G.  World gravity
(1g) must be included - this will center the 'vert accel' dial when the vehicle is sat on a flat surface.  E.g. a vector of
[0.0 0.0 9.81] will result in all three 'accel' guages showing as centered, a vector of [9.81 9.81 (9.81 * 2)] will show all guages
at 'max' and a vector of [-9.81, -9.81, 0.0] will show all gauges at 'min'.

float localVel[3];

The longitudinal[0] (+ve = forward), lateral[1] (+ve = right) and vertical[2] (+ve = down)
velocity values local to the vehicle's co-ordinate system.  Should be a vector of magnitude 0.0 > 1.0
(scaled according to the maximum possible speed of the game vehicle, 0.0 = min, 1.0 = max).
Only required if 'useLocalVals' is set to 'true'.

float globalVel[3];

The X[0], Y[1] and Z[2] velocity values of the vehicle relative to the world (global)
co-ordinate system.  Should be a vector of magnitude 0.0 > 1.0 (scaled according to the maximum
possible speed of the game vehicle, 0.0 = min, 1.0 = max).
Only required if 'useLocalVals' is set to 'false'.

float rotationMatrix[3][3];

The rotation matrix representing the rotation of the vehicle relative to the world (global) co-ordinate system.
Only required if 'useLocalVals' is set to 'false'.

DWORD packetTimeMillis;

The world (game) time this packet pertains to, in milliseconds.  Must be the exact time of the frame as represented
by the physics engine of the game (as it is used to calculate acceleration values from changes in velocity).
PLEASE NOTE: This value is also used as a 'keep alive' by SIMPHYNITY - while this value is changing SIMPHYNITY will
assume the physics engine to be active (not idle) and continue reading velocity / acceleration values from your game
and sending them to output devices - when inactive (not changing), SIMPHYINTY will revert to 'Idle' values.
Due to this, this value should always be changing when your physics engine is outputting data, and should stop changing
during game menus, loading, paused states etc.
*/
struct SimphynityUDPPacket
{
	bool useLocalVals; 			// 4 byte bool value.  First byte = val, followed by 3 padding bytes. 
	float localAccel[3];			// 12 byte (3 x 4 byte) float vector.
	float localVel[3];			// 12 byte (3 x 4 byte) float vector.
	float globalVel[3];			// 12 byte (3 x 4 byte) float vector.
	float rotationMatrix[3][3];		// 36 byte (9 x 4 byte) float matrix.
	DWORD packetTimeMillis;		// 4 byte integer value.
};


const struct
{
	//IP is localhost by default
	std::string IP = "127.0.0.1";

	//Default port is 8051 for AMS
	int PORT = 20777;
	PCSTR PORTSTR = "20777";

	//The default tick rate is 40 update per sec
	int TICK_SEND_RATE   = 1000 / 20;
	int TICK_SAMPLE_RATE = 1000 / 5;

} SimphynitySettings;