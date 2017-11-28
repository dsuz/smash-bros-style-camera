using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Sub camera controller.
/// Based on https://www.gamasutra.com/blogs/NicholasDiMucci/20150217/236492/Single_Camera_System_for_Four_Players.php
/// and https://unity3d.com/learn/tutorials/projects/tanks-tutorial/camera-control?playlist=20081
/// </summary>
[RequireComponent(typeof(Camera))]
public class SubCameraController : MonoBehaviour
{
    [SerializeField] List<GameObject> m_targetList;
    [SerializeField] float m_zoomBuffer = 0f;
    Camera m_cam;

    void Start()
    {
        m_cam = GetComponent<Camera>();
    }
    
    void Update()
    {
        AdjustCamera();
    }

    void AdjustCamera()
    {
        transform.position = FindRequiredPosition();
        m_cam.orthographicSize = FindRequiredSize();
    }

    Vector3 FindRequiredPosition()
    {
        float maxX = m_targetList.Max(go => go.transform.position.x);
        float minX = m_targetList.Min(go => go.transform.position.x);
        float maxZ = m_targetList.Max(go => go.transform.position.z);
        float minZ = m_targetList.Min(go => go.transform.position.z);

        return new Vector3((maxX + minX) / 2, transform.position.y, (maxZ + minZ) / 2);
    }

    private float FindRequiredSize()
    {
        float size = 0f;

        foreach(var target in m_targetList)
        {
            Vector3 desiredPosToTarget = transform.InverseTransformPoint(target.transform.position);
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x), Mathf.Abs(desiredPosToTarget.y));
        }

        return size + m_zoomBuffer;
    }
}
