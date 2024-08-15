using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.IO;

public class AnimationPlayback : MonoBehaviour {

    // public string loadPath;
    public string fileName = "motiondata";

    // Input Action asset reference
    public InputActionAsset inputActionAsset;
    private InputAction togglePlaybackAction;

    Transform[] playbackObjs;
    Dictionary<string, Transform> objDict;

    List<FrameData> frameDataList;
    int currentFrame = 0;
    float playbackTime = 0.0f;
    bool isPlaying = false;

    void Start () {
        SetupPlayback();
        LoadData();

        // Find action in the input action asset
        togglePlaybackAction = inputActionAsset.FindActionMap("GamePlay").FindAction("StartPlayback");

        // Enable action
        togglePlaybackAction.Enable();

        // Bind action to method
        togglePlaybackAction.performed += _ => TogglePlayback();
    }

    void SetupPlayback() {
        playbackObjs = gameObject.GetComponentsInChildren<Transform>();
        objDict = new Dictionary<string, Transform>();

        foreach (var obj in playbackObjs) {
            objDict[obj.name] = obj;
        }

        frameDataList = new List<FrameData>();
    }

    void LoadData() {
        string filePath = Path.Combine(Application.persistentDataPath, fileName + ".txt");

        if (File.Exists(filePath)) {
            using (StreamReader reader = new StreamReader(filePath)) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    FrameData frameData = ParseFrameData(line);
                    frameDataList.Add(frameData);
                }
            }

            Debug.Log("Data Loaded");
        } else {
            Debug.LogError("File not found: " + filePath);
        }
    }

    FrameData ParseFrameData(string line) {
        string[] parts = line.Split(',');
        string name = parts[0];
        Vector3 position = new Vector3(float.Parse(parts[1]), float.Parse(parts[2]), float.Parse(parts[3]));
        Quaternion rotation = new Quaternion(float.Parse(parts[4]), float.Parse(parts[5]), float.Parse(parts[6]), float.Parse(parts[7]));

        return new FrameData(name, position, rotation);
    }

    void Update () {
        if (isPlaying && currentFrame < frameDataList.Count) {
            playbackTime += Time.deltaTime;

            ApplyFrameData(frameDataList[currentFrame]);

            currentFrame++;
        }
    }

    public void TogglePlayback() {
        if (isPlaying) {
            StopPlayback();
        } else {
            StartPlayback();
        }
    }

    public void StartPlayback() {
        currentFrame = 0;
        playbackTime = 0.0f;
        isPlaying = true;
        Debug.Log("Playback Started");
    }

    public void StopPlayback() {
        isPlaying = false;
        Debug.Log("Playback Stopped");
    }

    void ApplyFrameData(FrameData frameData) {
        if (objDict.ContainsKey(frameData.name)) {
            Transform obj = objDict[frameData.name];
            obj.position = frameData.position;
            obj.rotation = frameData.rotation;
        }
    }

    class FrameData {
        public string name;
        public Vector3 position;
        public Quaternion rotation;

        public FrameData(string name, Vector3 position, Quaternion rotation) {
            this.name = name;
            this.position = position;
            this.rotation = rotation;
        }
    }
}
