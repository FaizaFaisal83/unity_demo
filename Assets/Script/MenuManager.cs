using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro; // Add this line to use TextMeshPro

public class MenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject UserNameScreen, ConnectScreen;

    [SerializeField]
    private GameObject CreateUserNameButton;

    [SerializeField]
    private TMP_InputField UserNameInput, CreateRoomInput, JoinRoomInput; // Change InputField to TMP_InputField

    void Awake() //Connect to the Photon Master Server using the settings defined in the PhotonServerSettings file
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server.");
        // After connecting to the Master Server, join the lobby
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby.");
        // Players can now browse available rooms or create/join a specific room
        UserNameScreen.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        // play game scene 
        PhotonNetwork.LoadLevel(1);
    }

    public void OnClick_CreateNameBtn()
    {
        PhotonNetwork.NickName = UserNameInput.text;
        UserNameScreen.SetActive(false);
        ConnectScreen.SetActive(true);
    }
    public void OnNameField_Changed()
    {
        if (UserNameInput.text.Length >= 2)
        {
            CreateUserNameButton.SetActive(true);
        }
        else
        {
            CreateUserNameButton.SetActive(false);
        }
    }

    public void Onclick_JoinRoom()
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(JoinRoomInput.text, ro, TypedLobby.Default);

    }
    public void Onclick_CreateRoom()
    {
         PhotonNetwork.CreateRoom(CreateRoomInput.text, new RoomOptions { MaxPlayers=4}, null);
    }
}
