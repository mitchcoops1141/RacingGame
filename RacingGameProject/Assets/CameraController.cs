using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float cameraSpeed;
    [SerializeField] Transform player;

    // Update is called once per frame
    void Update()
    {
        //lerp POS
        transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * cameraSpeed);

        //rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation, Time.deltaTime * cameraSpeed);
    }
}
