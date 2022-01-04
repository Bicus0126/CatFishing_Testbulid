using UnityEngine;
using UnityEngine.UI;

public class PhaseCount : MonoBehaviour{

    [HideInInspector] public int Phase; //player current Phase
    [HideInInspector] public int numOfPhases;

    public Image[] Phases;
    public Sprite Fish;
    public Sprite blackFish;

    void Update(){
        for (int i = 0; i < Phases.Length; i++){

            if(i < Phase){
                Phases[i].sprite = Fish;
            } else {
                Phases[i].sprite = blackFish;
            }

            if(i < numOfPhases){
                Phases[i].enabled = true;
            } else{
                Phases[i].enabled = false;
            }
        }
    }
}
