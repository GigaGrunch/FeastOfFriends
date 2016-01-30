using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float speed = 2.0f;

    public static bool ClickBlockedByUI()
    {
        return (Input.mousePosition.y < 200);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Camera.main.orthographicSize += 1;

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Camera.main.orthographicSize -= 1;
        }

        Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 200;

        if (Camera.main.orthographicSize >= 20)
        {
            Camera.main.orthographicSize = 20;
        }
        else if (Camera.main.orthographicSize <= 1)
        {
            Camera.main.orthographicSize = 1;
        }
    }
}
