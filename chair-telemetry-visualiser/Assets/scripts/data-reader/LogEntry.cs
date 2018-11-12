using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal struct LogHeader
{
    public string car;
    public string carClass;
    public string track;
    public string trackVariant;
}

internal struct LogLapInfo
{
    public float lapTime;
    public string formattedLapTime;
    public int totalLaps;
}

public class Log
{
    internal LogHeader header;
    public List<LogEntry> entries;
    internal LogLapInfo lapInfo;
}

public class LogEntry
{
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

}
