using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using TMPro;
using System.Collections;
using UnityEngine.UI;
public class RoomMananger : MonoBehaviourPunCallbacks
{
    public static RoomMananger _instance;
    [SerializeField] GameObject player;
    [SerializeField] GameObject namepicker;
    [SerializeField] Transform[] spp;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject[] loadouts;
    [SerializeField] GameObject joinButton;
    [SerializeField] GameObject load;
    [SerializeField] GameObject startWall;
    [SerializeField] TextMeshPro textPl;
    [SerializeField] TextMeshPro playerNick;
    [SerializeField] GameObject finalPanel;
    [SerializeField] TextMeshPro firstPlace;
    [SerializeField] GameObject masterWarning;
    public PlayerSetup[] players ; 
    public int playersCount;
    public int cycles = 1;
    [SerializeField] int playersToStart = 2;
    private string nickname = "Player";
    bool isStarted = false;
    private void Awake()
    {
        _instance = this;
        load.SetActive(false);
    }

    public void ChangeNickname(TMP_InputField _name)
    {
        nickname = _name.text;
    }

    public void JoinRoomButtonPressed()
    {
        Debug.Log("Connecting....");
        Debug.Log(nickname);
        PhotonNetwork.ConnectUsingSettings();
        namepicker.SetActive(false);
        joinButton.SetActive(false);
        load.SetActive(true);
        
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connect to master...");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        PhotonNetwork.JoinOrCreateRoom("test", null, null);
        Debug.Log("Connected");

    }
    public override void OnJoinedRoom()
    {
        
        base.OnJoinedRoom();
        RespawnPlayer(spp[Random.Range(0, spp.Length)].position);
        if(PhotonNetwork.IsMasterClient) masterWarning.SetActive(true);
        loadingScreen.SetActive(false);
       
    }
    

    
    public void RespawnPlayer(Vector3 pos)
    {

        GameObject _player = PhotonNetwork.Instantiate(player.name, pos, Quaternion.identity);
        _player.GetComponentInParent<PlayerSetup>().IsLocalPlayer();
        _player.GetComponentInParent<PhotonView>().RPC("SetNickname", RpcTarget.AllBuffered, nickname);


    }
    [PunRPC]
    private void StartCount()
    {
        StartCoroutine(Starts());
    }
    IEnumerator Starts()
    {
        textPl.text = "start in 3";
        yield return new WaitForSeconds(1);
        textPl.text = "start in 2";
        yield return new WaitForSeconds(1);
        textPl.text = "start in 1";
        yield return new WaitForSeconds(1);
        textPl.text = "start in Start!";
        yield return new WaitForSeconds(1);
        startWall.SetActive(false);
        FindAnyObjectByType<Finish>().StartCount();
    }
    private void Update()
    {
       
        if (PhotonNetwork.IsMasterClient == true && Input.GetKey(KeyCode.E) && isStarted == false)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            isStarted = true;
            GetComponent<PhotonView>().RPC("StartCount", RpcTarget.AllBuffered);
        }
        if (cycles <= 0) GetComponent<PhotonView>().RPC("End", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void End()
    {
        PlayerSetup[] players = FindObjectsByType<PlayerSetup>(FindObjectsSortMode.None);
        PlayerSetup plf = null;
        finalPanel.SetActive(true);
        for (int i = 0; i < players.Length; i++)
        {
            
            if(plf == null) plf = players[i];
            else if (players[i].score > plf.score)
            {
                plf = players[i];
            }
        }
      
        firstPlace.text = plf.nickname;
    }
    
}
