using UnityEngine;
using System.Collections;

public class MoveChar : MonoBehaviour {

    bool targetReached = true;
    Vector2 target;

    [SerializeField]
    float speed = 0.5f;

    public bool TargetReached
    {
        get
        {
            return targetReached;
        }

        set
        {
            targetReached = value;
        }
    }

    public Vector2 Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (!TargetReached)
        {
            float dist = Vector2.Distance(transform.position, target);

            if (dist < 0.1f)
            {
                targetReached = true;
                transform.position = target;
            }

            transform.position = Vector2.Lerp(transform.position, target, Time.deltaTime * speed / dist);
        }
	}
}
