using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class Network : MonoBehaviourPunCallbacks
{
    public static int[] numbers = new int[10];
    public Transform[] spawns = new Transform[10];
    private void Awake()
    {
        bool change = true;
		for (int i = 0; i < numbers.Length; i++) 
        {
			if (change) 
            {
                numbers[i] = 1;
            }
            else
			{
                numbers[i] = 0;
            }
            change = !change;
        }
        print("Iniciado Network...");
        PhotonNetwork.LocalPlayer.NickName = "Player" + Random.Range(0,1000);
        PhotonNetwork.ConnectUsingSettings();
        print(PhotonNetwork.LocalPlayer.NickName);
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
        PhotonNetwork.JoinRoom("Game");
        print("Entrando na sala");
    }
	public override void OnJoinRoomFailed(short returnCode, string message)
	{
        print("Erro: " + message + " Codigo: " + returnCode);
        if (returnCode == ErrorCode.GameDoesNotExist) 
        {
            RoomOptions room = new RoomOptions { MaxPlayers = 10 };
            Hashtable options = new Hashtable();
            options.Add("Time", 180);
            options.Add("StartMatch", false);
            options.Add("TimesUp",false);
            room.CustomRoomProperties = options;
            PhotonNetwork.CreateRoom("Game", room,null);
            print("Criando sala");
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print(newPlayer.NickName + " entrou");

        print(PhotonNetwork.PlayerList.Length);
    }
	public override void OnJoinedRoom()
	{
        print("Entrou na Sala: " + PhotonNetwork.NickName);
        if (numbers[PhotonNetwork.PlayerList.Length-1] == 1) 
        {
            PhotonNetwork.Instantiate("PlayerSurvivalObject", spawns[PhotonNetwork.PlayerList.Length - 1].position, Quaternion.identity);
        }
        else if (numbers[PhotonNetwork.PlayerList.Length - 1] == 0)
        {
            PhotonNetwork.Instantiate("PlayerGhostObject", spawns[PhotonNetwork.PlayerList.Length - 1].position, Quaternion.identity);
        }
    }
	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
        print(otherPlayer.NickName + " Saiu");

        print(PhotonNetwork.PlayerList.Length);
    }
	public override void OnLeftRoom()
	{
        print("Você saiu da Sala");

        print(PhotonNetwork.PlayerList.Length);
    }
	public override void OnErrorInfo(ErrorInfo errorInfo)
	{
        print("Error: " + errorInfo.Info);

        print(PhotonNetwork.PlayerList.Length);
    }
}
