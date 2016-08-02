using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 18, 2016
//
// Defines what an enemy drops and the chance of it dropping. 
// **************************************************************************

public enum enItemDrop { Watermelon, Cherry, Tomato, Pineapple, Peach }

public class EnemyDrops : MonoBehaviour
{

    //Constants
    //
    private const float DROP_HEIGHT = 5.0F;

    [Header("Drop Items")]
    public int[] ArrayOfDropChance; 
    public GameObject[] ArrayOfItemDrops;

    [Header("Managers")]
    [SerializeField] private ItemManager theItemManager; 

    public void DropItem()
    {

        int itemToDrop = CalculateRandomDropRate();

        // As long as the returned index is less than the length of the drop items then 
        // an item is dropped
        if (itemToDrop < ArrayOfItemDrops.Length)
            CreateItem(ArrayOfItemDrops[itemToDrop]);

    }

    int CalculateRandomDropRate()
    {

        int randRange = 0;

        // First all of the weighted values are added together
        for (int i = 0; i < ArrayOfDropChance.Length; i++)
        {
            randRange += ArrayOfDropChance[i]; 
        }

        // A number is generated between 0 and the total weighted value
        int randNum = Random.Range(0, randRange);

        int top = 0;

        // Goes through all weighted values
        for (int i = 0; i < ArrayOfDropChance.Length; i++)
        {

            top += ArrayOfDropChance[i];

            // If the current weighted value is greater than the generated number then 
            // return the index
            if (top > randNum)
                return i; 

        }

        // Worse comes to worse the enemy drops nothing
        return ArrayOfDropChance.Length - 1;

    }

    void CreateItem( GameObject aItem )
    {

        // Makes the drop appear a certain distance above the enmy
        Vector3 dropPos = this.transform.position;
        dropPos.y += DROP_HEIGHT;

        // Creates the new drop and adds it to the list of active drops
        theItemManager.ListOfItemDrops.Add( Instantiate(aItem, dropPos, Quaternion.identity) as GameObject );

    }

}
