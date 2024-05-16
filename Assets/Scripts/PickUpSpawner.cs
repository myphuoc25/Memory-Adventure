using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    //[SerializeField] private GameObject goinCoinPrefab;
    [SerializeField] private GameObject golfCoin, healthGlobe, staminaGlobe;

    public void DropItems()
    {
        //Instantiate(goinCoinPrefab, transform.position, Quaternion.identity);
        int randomNum = Random.Range(1, 5);

        if(randomNum == 1)
        {
            Instantiate(healthGlobe, transform.position, Quaternion.identity);
        }

        if(randomNum == 2)
        {
            Instantiate(staminaGlobe, transform.position, Quaternion.identity);
        }

        if(randomNum == 3)
        {
            int randomAmountOfGold = Random.Range(1, 4);

            for (int i = 0; i<randomAmountOfGold; i++)
            {
                Instantiate(golfCoin, transform.position, Quaternion.identity);
            }
        }
    }
}
