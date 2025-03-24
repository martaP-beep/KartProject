using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Vector3[] positions;
    public CinemachineVirtualCamera camera;
    int activePosition = 0;


    // Start is called before the first frame update
    void Start()
    {
        if (positions.Length == 0) return;

        camera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
            positions[activePosition];
    }

    // Update is called once per frame
    void Update()
    {
        if (positions.Length == 0) return;

        if (Input.GetKeyDown(KeyCode.C))
        {
            activePosition++;
            activePosition = activePosition % positions.Length;
            camera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
            positions[activePosition];
        }
    }
}
