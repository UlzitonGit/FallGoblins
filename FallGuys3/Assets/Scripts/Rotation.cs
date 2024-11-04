using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] Vector3 rot;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(rot * Time.deltaTime);
    }
}
