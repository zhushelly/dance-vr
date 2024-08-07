using UnityEngine;
using System.Collections.Generic;
using System.IO;
public class MotionPlayback : MonoBehaviour
{
    public Transform leftController;
    public Transform rightController;
    public Transform hmd;
    private List<string> motionData = new List<string>();
    private int currentIndex = 0;
    private float timer = 0f;
    private bool isPlaying = false;
    void Start()
    {
        LoadDataFromFile();
    }
    void Update()
    {
        if (isPlaying)
        {
            timer += Time.deltaTime;
            if (currentIndex < motionData.Count)
            {
                string[] data = motionData[currentIndex].Split(',');
                float recordTime = float.Parse(data[0]);
                if (timer >= recordTime)
                {
                    leftController.position = new Vector3(float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3]));
                    leftController.rotation = new Quaternion(float.Parse(data[4]), float.Parse(data[5]), float.Parse(data[6]), float.Parse(data[7]));
                    rightController.position = new Vector3(float.Parse(data[8]), float.Parse(data[9]), float.Parse(data[10]));
                    rightController.rotation = new Quaternion(float.Parse(data[11]), float.Parse(data[12]), float.Parse(data[13]), float.Parse(data[14]));
                    hmd.position = new Vector3(float.Parse(data[15]), float.Parse(data[16]), float.Parse(data[17]));
                    hmd.rotation = new Quaternion(float.Parse(data[18]), float.Parse(data[19]), float.Parse(data[20]), float.Parse(data[21]));
                    currentIndex++;
                }
            }
            else
            {
                isPlaying = false;
                Debug.Log("Playback finished.");
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartPlayback();
        }
    }
    public void StartPlayback()
    {
        isPlaying = true;
        timer = 0f;
        currentIndex = 0;
    }
    private void LoadDataFromFile()
    {
        string path = Path.Combine(Application.persistentDataPath, "motionData.txt");
        motionData = new List<string>(File.ReadAllLines(path));
        Debug.Log("Data loaded from " + path);
    }
}











