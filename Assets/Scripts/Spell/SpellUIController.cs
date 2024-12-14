using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellUIController : MonoBehaviour
{

    public SpellController m_controller;
    List<RawImage> childs;
    List<TextMeshProUGUI> texts;
    bool inited = false;
    // Start is called before the first frame update
    void Start()
    {
        childs = new (GetComponentsInChildren<RawImage>());
        texts = new (GetComponentsInChildren<TextMeshProUGUI>());
    }

    // Update is called once per frame
    void Update()
    {   
        if(!inited){
            for(int i=0; i<4; i++) {
                texts[i].text = m_controller.m_spells[i].Name;
            }
            inited = true;
        }

        if(m_controller.m_player.spellingMode){
            foreach(RawImage i in childs)
                i.color = Color.blue;
        } else if(m_controller.m_player.invokeSpell) {
            for(int i=0; i<4; i++){
                if(i == m_controller.m_currentSpell)
                    childs[i].color = Color.yellow;
                else
                    childs[i].color = Color.red;
            }
        } 
        else {
            foreach(RawImage i in childs)
                i.color = Color.white;
        }
        for(int i=0; i<4; i++){
            if(m_controller.m_spells[i].isOnCooldown)
                childs[i].color = Color.black;
        }
    }
}
