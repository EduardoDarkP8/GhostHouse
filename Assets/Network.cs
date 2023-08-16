using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class Network : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        print("Iniciado Network...");
        PhotonNetwork.LocalPlayer.NickName = "Player" + Random.Range(0,1000);
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        print("Conectando ao Servidor.");
        if (PhotonNetwork.InLobby == false) 
        {
           print("Entrando no Lobby.");
           PhotonNetwork.JoinLobby(); 
        }
    }
    public override void OnJoinedLobby()
    {
        print("Entrou no Lobby");
        PhotonNetwork.JoinRoom("GameLoot");
        print("Entrando na sala");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("Erro: " + message + " Codigo: " + returnCode);
        if (returnCode == ErrorCode.GameDoesNotExist) 
        {
            RoomOptions room = new RoomOptions { MaxPlayers = 10 };
            PhotonNetwork.CreateRoom("GameLoot", room,null);
            print("Criando sala");
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print(newPlayer.NickName);
    }

}
