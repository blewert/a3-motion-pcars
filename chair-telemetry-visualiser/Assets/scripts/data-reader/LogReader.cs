using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LogReader
{
    /// <summary>
    /// The stage of the file that is being read/parsed
    /// </summary>
    internal enum ReadStage
    {
        STAGE_HEADER   = (1 << 1),
        STAGE_DATA     = (1 << 2),
        STAGE_LAP_INFO = (1 << 3)
    };

    /// <summary>
    /// The read stage for the log reader
    /// </summary>
    internal ReadStage readStage = ReadStage.STAGE_HEADER;

    private void parseLapInfoLine(string line, ref LogLapInfo lapInfo)
    {
        var chunks = line.Split('=');

        if (chunks.Length <= 1)
            return;

        chunks[0] = chunks[0].TrimEnd();
        chunks[1] = chunks[1].TrimStart();

        if (chunks[0] == "lap_time")
            lapInfo.lapTime = float.Parse(chunks[1]);

        else if (chunks[0] == "flap_time")
            lapInfo.formattedLapTime = chunks[1];

        else if (chunks[0] == "total_laps")
            lapInfo.totalLaps = int.Parse(chunks[1]);
    }

    private void parseHeaderLine(string line, ref LogHeader header)
    {
        var chunks = line.Split('=');

        if (chunks.Length <= 1)
            return;

        chunks[0] = chunks[0].TrimEnd();
        chunks[1] = chunks[1].TrimStart();

        if (chunks[0] == "car")
            header.car = chunks[1];

        else if (chunks[0] == "class")
            header.carClass = chunks[1];

        else if (chunks[0] == "track")
            header.track = chunks[1];

        else if (chunks[0] == "track_variant")
            header.trackVariant = chunks[1];
    }

    /// <summary>
    /// Sets the read stage depending on the line that 
    /// has been passed 
    /// </summary>
    /// <param name="line">The line to check from</param>
    private void setStage(string line)
    {
        //Header data
        if (line.Contains("session"))
            readStage = ReadStage.STAGE_HEADER;

        //Actual telemetry data
        else if (line.Contains("data"))
            readStage = ReadStage.STAGE_DATA;

        //Lap information data (at end)
        else if (line.Contains("lap_info"))
            readStage = ReadStage.STAGE_LAP_INFO;
    }

    /// <summary>
    /// Reads a log file and returns a list of log entries
    /// </summary>
    /// <param name="path">The path to the log file</param>
    /// <returns>A list of parsed log entries</returns>
    public Log readLog(string path)
    {
        //Create a new list of log entries
        var logEntries = new List<LogEntry>();

        //Create a log header
        LogHeader logHeader = default(LogHeader);

        //Create a log lap info
        LogLapInfo logLapInfo = default(LogLapInfo);

        //---

        //Does it exist? If not, error..
        if (!File.Exists(path))
            throw new UnityException("Log file doesn't exist: " + path);

        var f = new StreamReader(path);
        //Open the file, read line by line
        //..
        string line;


        while((line = f.ReadLine()) != null)
        {
            if (line.Length <= 1)
                continue;

            //The line contains a square bracket, therefore its
            //a designated section of the log file
            //..
            if(line.Contains("["))
            {
                setStage(line);
                continue;
            }

            if (readStage == ReadStage.STAGE_HEADER)
                parseHeaderLine(line, ref logHeader);

            else if (readStage == ReadStage.STAGE_LAP_INFO)
                parseLapInfoLine(line, ref logLapInfo);

            else
                logEntries.Add(new LogEntry(line));

        }
        
        //Return the list of log entries
        return new Log(logHeader, logEntries, logLapInfo);
    }
}
