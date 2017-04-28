using UnityEngine;
using UnityEngine.UI;
using BeheaderTavern.Scripts.Spells;
using System.Collections.Generic;

public class SpellBookmarkUI : MonoBehaviour 
{
    public Button uiButton;
    private Button[] _buttons;

    void Update()
    {
        if(this.transform.childCount > 1)
            Animate();
    }

    public void InitializeComponents(Sprite[] spellsIcons, int currIndex)
    {
        _buttons = new Button[spellsIcons.Length];
        for(int i =0; i < spellsIcons.Length; i++)
        {
            var button = GameObject.Instantiate<Button>(uiButton, this.transform);
            button.transform.GetChild(0).GetComponentInChildren<Image>().sprite = spellsIcons[i];
            _buttons[i] = button;
        }

        do
        {
            Next();
        } while(_buttons[currIndex] != transform.GetChild(1).GetComponent<Button>());
    }

    public int GetCurrentSpellIndex()
    {
        for(int i =0; i < _buttons.Length; i++)
        {
            if(_buttons[i] == this.transform.GetChild(1).GetComponent<Button>())
            {
                return i;
            }
        }

        return 0;
    }
    public void Prior()
    {
        var child = transform.GetChild(0);
        child.SetAsLastSibling();
    }

    public void Next()
    {
        var child = transform.GetChild(transform.childCount-1);
        child.SetAsFirstSibling();
    }

    void Animate()
    {
        for(int i =0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if(child == transform.GetChild(1))
            {
                var images = child.GetComponentsInChildren<Image>();
                foreach(var image in images)
                {
                    if(image.gameObject.name != "cooldownSlide")
                        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
                }
                child.transform.localScale = Vector3.Lerp(child.transform.localScale,Vector3.one,Time.deltaTime * 2f);
            }
            else
            {
                var images = child.GetComponentsInChildren<Image>();
                foreach(var image in images)
                {
                    if(image.gameObject.name != "cooldownSlide")
                        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.6f);
                }
                child.transform.localScale = Vector3.one * 0.7f;//Vector3.Lerp(child.transform.localScale,Vector3.one * 0.7f,Time.deltaTime * 2f);
            }
        }
    }

    public void UpdateCooldownIcon(int spellIndex, float duration, float maxDuration)
    {
        var image = _buttons[spellIndex].transform.GetChild(1).GetComponent<Image>();
        image.fillAmount = duration / maxDuration;
    }
}
