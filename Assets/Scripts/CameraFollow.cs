using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollow : MonoBehaviour
{
    /*
    private Func<Vector3> GetCameraFollowPositionFunc;

    public void Setup(Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }

    void Update()
    {
        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;

        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);
        float cameraMoveSpeed = 1f;

        transform.position = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;

    }
    */

    
    private Transform target;
    public Vector3 cameraPosition = Vector3.zero;

    private float xMax, xMin, yMin, yMax;

    [SerializeField]
    private Tilemap tilemap;

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        player = target.GetComponent<Player>();

        Vector3 minTile = tilemap.CellToWorld(tilemap.cellBounds.min);
        Vector3 maxTile = tilemap.CellToWorld(tilemap.cellBounds.max);

        setLimits(minTile, maxTile);
        player.SetLimits(minTile, maxTile);
    }
    /*
    void FixedUpdate()
    {

        cameraPosition = new Vector3(
                                   //     Mathf.SmoothStep(transform.position.x, target.transform.position.x, 0.1f),
                                   //      Mathf.SmoothStep(transform.position.y, target.transform.position.y, 0.1f));
                                   Mathf.SmoothStep(transform.position.x, target.transform.position.x, 0.1f),
                                   Mathf.SmoothStep(transform.position.y, target.transform.position.y, 0.1f));

    }
    */

    private void LateUpdate()
    { ///clamp
        transform.position = new Vector3(Mathf.Clamp(target.position.x,xMin,xMax), Mathf.Clamp(target.position.y, yMin, yMax), -10);
         //   transform.position = cameraPosition + Vector3.forward * -10;

    }

    private void setLimits(Vector3 minTile, Vector3 maxTile)
    {
        Camera cam = Camera.main;

        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        xMin = minTile.x + width / 2f;
        xMax = maxTile.x - width / 2f;

        yMin = minTile.y + height / 2f;
        yMax = maxTile.y - height / 2f;
    }
}
