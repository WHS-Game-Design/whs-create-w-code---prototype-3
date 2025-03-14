using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPosition;
    private float repeatWidth;

    void Start()
    {
        startPosition = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    void Update()
    {
        if(transform.position.x < startPosition.x - repeatWidth)
        {
            transform.position = startPosition;
        }
        else if(transform.position.x > startPosition.x)
        {
            float x = startPosition.x - repeatWidth;
            float y = startPosition.y;
            float z = startPosition.z;

            Vector3 endPosition = new(x, y, z);

            transform.position = endPosition;
        }
    }
}
