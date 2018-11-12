using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLogReader : MonoBehaviour
{
    public string path;

    // Use this for initialization
    void Start ()
    {
        var reader = new LogReader();
        reader.readLog(path);
	}
}
