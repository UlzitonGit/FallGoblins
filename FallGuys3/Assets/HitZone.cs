using Photon.Pun;
using StarterAssets;
using UnityEngine;

public class HitZone : MonoBehaviour
{
    [SerializeField] GameObject player;
    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Player") && other.gameObject != player)
       {
           other.GetComponent<PhotonView>().RPC("GetPunch", RpcTarget.Others, player.transform.forward);
       }
    }
}
