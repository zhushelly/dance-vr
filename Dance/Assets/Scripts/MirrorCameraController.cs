using UnityEngine;

public class MirrorCameraController : MonoBehaviour
{
    public Transform vrHead;
    public Camera mirrorCamera;

    void Update()
    {
        Vector3 mirrorPosition = vrHead.position;
        mirrorPosition.y = -vrHead.position.y; // Adjust based on the mirror's position

        Vector3 mirrorRotation = vrHead.eulerAngles;
        mirrorRotation.x = -vrHead.eulerAngles.x; // Adjust based on the mirror's rotation

        mirrorCamera.transform.position = mirrorPosition;
        mirrorCamera.transform.eulerAngles = mirrorRotation;
    }
}
