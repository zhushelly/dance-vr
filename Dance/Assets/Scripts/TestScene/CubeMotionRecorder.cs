using System.Collections.Generic;
using UnityEngine;

public class CubeMotionRecorder : MonoBehaviour
{
    public GameObject cube;
    private List<Vector3> positions = new List<Vector3>();
    private List<Quaternion> rotations = new List<Quaternion>();

    private bool isRecording = false;

    void Update()
    {
        if (isRecording)
        {
            // Record position and rotation each frame
            positions.Add(cube.transform.position);
            rotations.Add(cube.transform.rotation);
        }
    }

    public void StartRecording()
    {
        isRecording = true;
        positions.Clear();
        rotations.Clear();
    }

    public void StopRecording()
    {
        isRecording = false;
    }

    public List<Vector3> GetRecordedPositions()
    {
        return positions;
    }

    public List<Quaternion> GetRecordedRotations()
    {
        return rotations;
    }
}