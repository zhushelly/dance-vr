using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMotionPlayback : MonoBehaviour
{
    public GameObject cube;
    public CubeMotionRecorder recorder;
    private List<Vector3> positions;
    private List<Quaternion> rotations;

    private bool isPlaying = false;
    private int currentFrame = 0;

    void Start()
    {
        positions = recorder.GetRecordedPositions();
        rotations = recorder.GetRecordedRotations();
    }

    void Update()
    {
        if (isPlaying && currentFrame < positions.Count)
        {
            // Play back the recorded motion frame by frame
            cube.transform.position = positions[currentFrame];
            cube.transform.rotation = rotations[currentFrame];
            currentFrame++;
        }
    }

    public void StartPlayback()
    {
        currentFrame = 0;
        isPlaying = true;
    }

    public void StopPlayback()
    {
        isPlaying = false;
    }
}
