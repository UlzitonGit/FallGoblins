using UnityEngine;
using Photon.Pun;
using StarterAssets;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
    Vector3 checkPoint;
    bool ressed = false;
    public bool isLocal = false;
    [SerializeField] GameObject playerparent;
    [SerializeField] ThirdPersonController controller;
    Finish finish;
    [SerializeField] TextMeshProUGUI timetext;
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("CheckPoint"))
        {
            checkPoint = col.gameObject.transform.position;
        }
        if (col.CompareTag("Hammer") && isLocal == true)
        {
            print("yes");
            controller.isPunching = false;
            controller.canPunch = true;
            playerparent.transform.position = checkPoint;
            playerparent.GetComponent<PlayerSetup>().RessetPlayer();

        }
        if (col.CompareTag("Finish") && isLocal == true)
        {
            print("yes");
            Finish(col.transform.position);
            

        }
    }
    private void Start()
    {
        finish = FindAnyObjectByType<Finish>();
    }
    public void Finish(Vector3 col)
    {
        playerparent.transform.position = col;
        playerparent.GetComponent<PlayerSetup>().RessetPlayer();
    }
    private void Update()
    {
        timetext.text = "time left " + finish.timeLeft + "/" + finish.timeLeftG;
    }
}
