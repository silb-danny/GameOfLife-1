using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speedScroll = 2f;
    public float speedMov = 1f;
    public float[] value= {2,100};
    public Camera cam;
    void Start()
    {
        
    }
    void Update()
    {
        cam.orthographicSize=Mathf.Clamp(Mathf.Lerp(cam.orthographicSize ,cam.orthographicSize-Input.mouseScrollDelta[1]*speedScroll*Time.fixedDeltaTime*cam.orthographicSize, Time.time), value[0], value[1]);
    }
    void FixedUpdate()
    {
        transform.position=new Vector3(Mathf.Lerp(transform.position.x, transform.position.x+speedMov*Input.GetAxisRaw("Horizontal")*Time.fixedDeltaTime*cam.orthographicSize ,Time.time),Mathf.Lerp(transform.position.y, transform.position.y+speedMov*Input.GetAxisRaw("Vertical")*Time.fixedDeltaTime*cam.orthographicSize ,Time.time),this.transform.position.z);
    }
}
