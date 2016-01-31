using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float speed = 2.0f;
    int basicOrthoSize = 7;

    Vector3 anker, camAnker;
    Vector3 difference, camDiff;

    bool isJournalOpen;

    public bool IsJournalOpen
    {
        get
        {
            return isJournalOpen;
        }

        set
        {
            isJournalOpen = value;
        }
    }

    public bool ClickBlockedByUI()
    {
        return ((Input.mousePosition.y < 200) || IsJournalOpen);
    }

    void Update()
    {
        if (!ClickBlockedByUI())
        {
            if (Input.GetMouseButtonDown(0))
            {
                anker = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                camAnker = transform.position;
            }

            if (Input.GetMouseButton(0))
            {
                difference = anker - Camera.main.ScreenToViewportPoint(Input.mousePosition);
                transform.position = camAnker + new Vector3(difference.x * 24, difference.y * 13.5f, 0) * (Camera.main.orthographicSize / basicOrthoSize);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.LoadLevel("Mainmenu");
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                Application.CaptureScreenshot("screenshot.png");
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += Vector3.up * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow))
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

            if (Camera.main.orthographicSize >= 13)
            {
                Camera.main.orthographicSize = 13;
            }
            else if (Camera.main.orthographicSize <= 1)
            {
                Camera.main.orthographicSize = 1;
            }

            if (transform.position.x >= 100)
            {
                transform.position = new Vector2(100, transform.position.y);
            }
            if (transform.position.x <= -100)
            {
                transform.position = new Vector2(-100, transform.position.y);
            }
            if (transform.position.y >= 100)
            {
                transform.position = new Vector2(transform.position.x, 100);
            }
            if (transform.position.y <= -100)
            {
                transform.position = new Vector2(transform.position.x, -100);
            }
        }
    }
}