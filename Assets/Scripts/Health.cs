
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour{

    public int health; //player current HP
    public int numOfHearts;

    public Image[] hearts;
    public Sprite redheart;
    public Sprite blackheart;
    public AudioSource HurtSound;

    public void hurt(int amount)
    {
        health -= (health == 0 ? 0 : amount);
        HurtSound.time = 0.5f;
        HurtSound.Play();
    }

    void Update(){
        if(health > numOfHearts){
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++){

            if(i < health){
                hearts[i].sprite = redheart;
            } else {
                hearts[i].sprite = blackheart;
            }

            if(i < numOfHearts){
                hearts[i].enabled = true;
            } else{
                hearts[i].enabled = false;
            }
        }
    }
}
