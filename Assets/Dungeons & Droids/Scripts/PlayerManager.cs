using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public static int playerHP = 100;
    private static int enemyCount = 5;
    public static int winCount = 0;
    public static int loseCount = 0;
    public TextMeshProUGUI playerHPText;
    public TextMeshProUGUI winCountText;
    public TextMeshProUGUI loseCountText;
    public GameObject bloodOverlay;
    public GameObject rifleIcon;
    public GameObject pistolIcon;
    public GameObject heavylIcon;
    public static bool isGameOver;
    void Start()
    {
        isGameOver = false;
        playerHP = 100;
    }

    // Update is called once per frame
    void Update()
    {
        playerHPText.text = "+" + playerHP;
        winCountText.text = $"Won: {winCount}";
        loseCountText.text = $"Lost: {loseCount}";

        if (isGameOver)
        {
            SceneManager.LoadScene("LoseScene");
            enemyCount = 5;
            loseCount += 1;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("IntroMenu");
            enemyCount = 5;
        }
    }

    public IEnumerator TakeDamage(int damageAmount)
    {
        bloodOverlay.SetActive(true);
        playerHP -= damageAmount;
        if (playerHP <= 0)
            isGameOver = true;

        yield return new WaitForSeconds(1);
        bloodOverlay.SetActive(false);

    }

    public IEnumerator EnemyCount(int enemyDown)
    {
        enemyCount -= enemyDown;
        if (enemyCount == 0)
        {
            SceneManager.LoadScene("WinScene");
            enemyCount = 5;
            winCount += 1;
            yield return new WaitForSeconds(1);
        }
    }

    public IEnumerator SelectedWeapon(int weaponIndex)
    {
        if (weaponIndex == 1)
        {
            rifleIcon.SetActive(false);
            pistolIcon.SetActive(true);
            heavylIcon.SetActive(true);
            yield return new WaitForSeconds(.1f);
        }
        else if (weaponIndex == 2)
        {
            rifleIcon.SetActive(true);
            pistolIcon.SetActive(false);
            heavylIcon.SetActive(true);
            yield return new WaitForSeconds(.1f);
        }
        else if (weaponIndex == 3)
        {
            rifleIcon.SetActive(true);
            pistolIcon.SetActive(true);
            heavylIcon.SetActive(false);
            yield return new WaitForSeconds(.1f);
        }
    }
}
