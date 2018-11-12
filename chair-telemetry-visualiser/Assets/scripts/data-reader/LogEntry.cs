using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LogHeader
{
    public string car;
    public string carClass;
    public string track;
    public string trackVariant;
}

public struct LogLapInfo
{
    public float lapTime;
    public string formattedLapTime;
    public int totalLaps;
}

public class Log
{
    public LogHeader header;
    public List<LogEntry> entries;
    public LogLapInfo lapInfo;

    public Log(LogHeader header, List<LogEntry> data, LogLapInfo lapInfo)
    {
        this.header = header;
        this.entries = data;
        this.lapInfo = lapInfo;
    }
}

public class LogEntry
{
    public class TelemetryVar
    {
        private object value;
        public System.Type type;

        public TelemetryVar(System.Type type, object value)
        {
            this.type = type;
            this.value = value;
        }

        public static implicit operator Vector3(TelemetryVar v)
        {
            return (Vector3)System.Convert.ChangeType(v.value, typeof(Vector3));
        }

        public static implicit operator bool(TelemetryVar v)
        {
            return !!(bool)System.Convert.ChangeType(v.value, typeof(bool));
        }

        public static implicit operator float(TelemetryVar v)
        {
            return (float)System.Convert.ChangeType(v.value, typeof(float));
        }

        public static implicit operator int(TelemetryVar v)
        {
            return (int)System.Convert.ChangeType(v.value, typeof(int));
        }

        public static implicit operator string(TelemetryVar v)
        {
            return (string)System.Convert.ChangeType(v.value, typeof(string));
        }
    }

    private Dictionary<string, float> data             = new Dictionary<string, float>();
    public Dictionary<string, TelemetryVar> parsedData = new Dictionary<string, TelemetryVar>();

    private static string[] lineSpec =
    {
        "time", "pX", "pY", "pZ",
        "rX", "rY", "rZ",
        "vX", "vY", "vZ",
        "aX", "aY", "aZ",
        "avX", "avY", "avZ",
        "gear", "brake", "throttle", "clutch",
        "rpm", "speed", "steer", "tyreGrip_FL",
        "tyreGrip_FR", "tyreGrip_BL", "tyreGrip_BR",
        "tyreTemp_FL", "tyreTemp_FR", "tyreTemp_BL",
        "tyreTemp_BR", "tyreRPS_FL", "tyreRPS_FR",
        "tyreRPS_BL", "tyreRPS_BR", "windX", "windY",
        "windSpeed", "waterTemp", "brakeTemp", "antiLockActive"
    };

    public LogEntry(string line)
    {
        var chunks = line.Split(' ');

        if (line.Length <= 1)
            return;

        for (var i = 0; i < chunks.Length; i++)
            data[lineSpec[i]] = float.Parse(chunks[i]);

        parsedData["position"]     = new TelemetryVar(typeof(Vector3), new Vector3(data["pX"], data["pY"], data["pZ"]));
        parsedData["rotation"]     = new TelemetryVar(typeof(Vector3), new Vector3(data["rX"], data["rY"], data["rZ"]));
        parsedData["velocity"]     = new TelemetryVar(typeof(Vector3), new Vector3(data["vX"], data["vY"], data["vZ"]));
        parsedData["acceleration"] = new TelemetryVar(typeof(Vector3), new Vector3(data["aX"], data["aY"], data["aZ"]));
        parsedData["angularVel"]   = new TelemetryVar(typeof(Vector3), new Vector3(data["avX"], data["avY"], data["avZ"]));

        parsedData["gear"] = new TelemetryVar(typeof(int), data["gear"]);
        parsedData["time"] = new TelemetryVar(typeof(float), data["time"]);

        foreach(var key in data.Keys)
        {
            if (parsedData.ContainsKey(key))
                continue;

            parsedData[key] = new TelemetryVar(typeof(float), data[key]);
        }


    }

    public TelemetryVar this[string key]
    {
        get
        {
            return this.parsedData[key];
        }
    }

}
