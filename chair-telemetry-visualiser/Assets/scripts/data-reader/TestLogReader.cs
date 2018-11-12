using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLogReader : MonoBehaviour
{
    public string path;
    public Log log;

    public float scale = 100f;
    public float playbackSpeed = 1f;

    public Transform infoText;
    public Transform marker;

    public string trackedVariable = "gear";
    public int index;

    // Use this for initialization
    void Start ()
    {
        var reader = new LogReader();
        log = reader.readLog(path);

        GetComponent<LineRenderer>().positionCount = log.entries.Count;

        for (int i = 0; i < log.entries.Count; i++)
        {
            Vector3 pos = log.entries[i]["position"];
            GetComponent<LineRenderer>().SetPosition(i, pos / scale);
        }
	}

    private void Update()
    {
        for(int i = 1; i < log.entries.Count; i++)
        {
            var prev = log.entries[i - 1];
            var next = log.entries[i];

            var t = Time.time * playbackSpeed;

            if(prev["time"] <= t && next["time"] >= t)
            {
                index = i;
                break;
            }
        }

        index = Mathf.Clamp(index, 0, log.entries.Count);
        
        Vector3 pos = (Vector3)log.entries[index]["position"] / 100f;
        marker.position = Vector3.Lerp(marker.position, pos, Time.deltaTime * 10f);

        Vector3 rot = (Vector3)log.entries[index]["rotation"];
        marker.eulerAngles = rot * Mathf.Rad2Deg;

        infoText.gameObject.SetActive(false);

        if (!log.entries[index].parsedData.ContainsKey(trackedVariable))
            return;

        var type = log.entries[index][trackedVariable].type;

        if (type == typeof(int))
        {            
            infoText.position = marker.position + Vector3.up * 0.3f;
            infoText.GetComponent<TextMesh>().text = trackedVariable.ToUpper() + "\n" + (int)log.entries[index][trackedVariable];
            infoText.gameObject.SetActive(true);
        }

        if(type == typeof(float))
        {
            infoText.position = marker.position + Vector3.up * 0.3f;
            infoText.GetComponent<TextMesh>().text = trackedVariable.ToUpper() + "\n" + (float)log.entries[index][trackedVariable];
            infoText.gameObject.SetActive(true);
        }
    }

    public void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;
    }
}
