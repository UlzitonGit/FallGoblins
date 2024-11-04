using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool down = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition.y < 1) down = false;
        if(transform.localPosition.y > 6) down = true;
        if (down == false) transform.localPosition = Vector3.Slerp(transform.localPosition, new Vector3(0, 7, 0), Time.deltaTime);
        if (down == true) transform.localPosition = Vector3.Slerp(transform.localPosition, new Vector3(0, 0, 0), Time.deltaTime);
    }
}
