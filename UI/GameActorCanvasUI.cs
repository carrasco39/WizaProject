using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BeheaderTavern.Scripts.Mobiles;

public class GameActorCanvasUI : MonoBehaviour
{
    public Image healthBarImage;
    private GameActor _actor;
    // Use this for initialization
    void Start()
    {
        _actor = GetComponentInParent<GameActor>();
    }
	
    // Update is called once per frame
    void Update()
    {
        if (_actor != null)
            AnimateHealthBar();

        FaceBillboard();
    }


    private void AnimateHealthBar()
    {
        healthBarImage.fillAmount = _actor.actorProps.Health / _actor.actorProps.MaxHealth;
    }

    private void FaceBillboard()
    {

        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up);
    }
}
