using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeheaderTavern.Scripts.Core;
using BeheaderTavern.Scripts.Mobiles.Player;
using PlayFab;
using PlayFab.ClientModels;

public class PhotonDebugTest : Photon.MonoBehaviour
{
    string playfabId;
    // Use this for initialization
    void Start()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = "henrique.carrasco.1@gmail.com",
            Password = "882288",

               
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnFail);
     
    }
    // Update is called once per frame
    void Update()
    {
		
    }

    void OnLoginSuccess(LoginResult result)
    {
        playfabId = result.PlayFabId;
        var request = new GetPhotonAuthenticationTokenRequest
        {
            PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppID.Trim(),
        };

        PlayFabClientAPI.GetPhotonAuthenticationToken(request, OnAuthTokenResult, OnFail);
        
    }

    void OnFail(PlayFabError error)
    {
        
        Debug.LogError(string.Format("Error {0}: {1}",error.Error.ToString(),error.ErrorMessage));
    }

    void OnAuthTokenResult(GetPhotonAuthenticationTokenResult result)
    {
        AuthenticationValues customAuth = new AuthenticationValues();
        customAuth.AuthType = CustomAuthenticationType.Custom;
        customAuth.AddAuthParameter("username", playfabId);
        customAuth.AddAuthParameter("token", result.PhotonCustomAuthenticationToken);

        PhotonNetwork.AuthValues = customAuth;

        PhotonNetwork.ConnectUsingSettings("v0.1d");
    }


    #region Photon Callbacks
    void OnCustomAuthenticationFailed(string debugMessage)
    {
        Debug.LogError(debugMessage);
    }

    void OnJoinedLobby()
    {
        RoomOptions room = new RoomOptions();
        room.EmptyRoomTtl = 3600000;
        room.MaxPlayers = 0;
        PhotonNetwork.JoinOrCreateRoom("teste", room, TypedLobby.Default);
    }

    void OnJoinedRoom()
    {
        var player = PhotonNetwork.Instantiate("Player", new Vector3(0, 1, -5), Quaternion.identity, 0);
        GameManager.instance.PlayerActor = player.GetComponent<PlayerActor>();

        if(player.GetPhotonView().owner.IsMasterClient)
        {
            PhotonNetwork.InstantiateSceneObject("Dummy02", new Vector3(5, 1, -5), Quaternion.identity, 0,null);
            PhotonNetwork.InstantiateSceneObject("Dummy02", new Vector3(5, 1, 10), Quaternion.identity,0,null);
        }
    }
    #endregion
}
