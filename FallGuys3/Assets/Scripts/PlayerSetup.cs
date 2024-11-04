using Cinemachine;
using Photon.Pun;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson.PunDemos;

public class PlayerSetup : MonoBehaviour
{
   
    [SerializeField] GameObject cameraPlayer;
    [SerializeField] GameObject cameraBrain;
    [SerializeField] ThirdPersonController player;
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] PlayerHealth _PlayerHealth;
    [SerializeField] TextMeshPro nickText;
    public string nickname;
    public int score;
    public bool passed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void IsLocalPlayer()
    {
        cameraPlayer.SetActive(true);
        cameraBrain.SetActive(true);
        _PlayerHealth.enabled = true;
        player.enabled = true;
        _PlayerHealth.isLocal = true;
        cam.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void RessetPlayer()
    {
        player.gameObject.SetActive(false);
        player.transform.localPosition = new Vector3(0,0,0);
        player.gameObject.SetActive(true);
    }

    [PunRPC]
    public void SetNickname(string _name)
    {
        nickname = _name;
        nickText.text = nickname;
    }
    [PunRPC]
    public void AddScore(int scores)
    {
        score += scores;
    }
}
