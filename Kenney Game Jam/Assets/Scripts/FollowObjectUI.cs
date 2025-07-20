using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjectUI : MonoBehaviour
{
    public Transform target;        
    RectTransform rt;
    [SerializeField] Vector3 Offset;
    Camera cam;
    void Awake()
    {
        rt = GetComponent<RectTransform>();
        cam = Camera.main;
    }
    void Update()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(target.position + Offset);
        rt.position = screenPos;
    }
}

