using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;
    public bool CameraFollowEnabled;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
        CameraFollowEnabled = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (CameraFollowEnabled)
        {
            transform.position = player.transform.position + offset;
        }
    }

    public RunStatus EnableCameraFollow()
    {
        CameraFollowEnabled = true;
        return RunStatus.Success;
    }
}
