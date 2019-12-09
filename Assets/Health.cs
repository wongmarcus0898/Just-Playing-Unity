using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject player;

    public int health = 3;
    public int num_heart;
    public Image[] hearts;

    public Sprite full_heart;
    public Sprite empty_heart;

    private Vector2 screenBounds;
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    private void Update()
    {
        // If player falls off the platform
        if (health == 0 || transform.position.y < -screenBounds.y * 1.5)
        {
            player.SetActive(false);
            Gameover();
        }
        else player.SetActive(true);

        // Heart sprite HUD
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health) hearts[i].sprite = full_heart;
            else hearts[i].sprite = empty_heart;

            if (i < num_heart) hearts[i].enabled = true;
            else hearts[i].enabled = false;
        } 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Missile")
        {
            health--;
        }
    }

    void Gameover()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
