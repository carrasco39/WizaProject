using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabLoginTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Username = "Carrasco",
            DisplayName = "Carrasco",
            Email = "henrique.carrasco.1@gmail.com",
            Password = "882288",
        };   
        PlayFabClientAPI.RegisterPlayFabUser(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log(result.PlayFabId + " Logged In");
    }


    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("Register Failed: " + error.ErrorMessage);
    }
}
