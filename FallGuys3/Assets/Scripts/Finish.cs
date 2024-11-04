using UnityEngine;
using Photon.Pun;
using System.Security.Cryptography;
using System.Collections;

public class Finish : MonoBehaviour
{
    RoomMananger room;
    public int passed = 0;
    public int timeLeft = 40;
    public int timeLeftG = 0;
    int scores = 70;
    bool isRace = true;
    private void Start()
    {
        room = FindAnyObjectByType<RoomMananger>();
    }
    public void StartCount()
    {
        timeLeftG = timeLeft;
        StartCoroutine(Delaying());
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponentInParent<PlayerSetup>().passed == false && isRace == true)
        {
            if(passed == 0) other.GetComponentInParent<PhotonView>().RPC("AddScore", RpcTarget.All, 30);
            else if (passed == 1) other.GetComponentInParent<PhotonView>().RPC("AddScore", RpcTarget.All, 20);
            else if (passed == 2) other.GetComponentInParent<PhotonView>().RPC("AddScore", RpcTarget.All, 10);
            else if (passed == 3) other.GetComponentInParent<PhotonView>().RPC("AddScore", RpcTarget.All, 5);
            passed++;
            other.GetComponentInParent<PlayerSetup>().passed = false;
            if(PhotonNetwork.PlayerList.Length == passed)
            {
                End();
            }
        }
    }
    private void End()
    {
        PlayerSetup[] player = FindObjectsByType<PlayerSetup>(FindObjectsSortMode.None);
        for (int i = 0; i < player.Length; i++)
        {
            if (player[i].passed == false)
            {
                player[i].GetComponentInChildren<PlayerHealth>().Finish(transform.position);
            }
        }
        room.cycles -= 1;
    }
    IEnumerator Delaying()
    {
       
        for (int i = 0; i < timeLeftG; i++)
        {
            yield return new WaitForSeconds(1);
            timeLeft -= 1;
        }
        End();
    }
}
