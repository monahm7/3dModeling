using UnityEngine;
using System.Collections;

public class FoodPickup : MonoBehaviour
{
    public int coinReward = 1;
    public float weightGain = 1f;
    bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;
            GameUI.instance.AddCoins(coinReward);
            other.GetComponent<PlayerStats>().AddWeight(weightGain);
            StartCoroutine(CollectEffect());
        }
    }

    IEnumerator CollectEffect()
    {
        transform.localScale *= 2.2f;
        yield return new WaitForSeconds(0.18f);
        Destroy(gameObject);
    }
}