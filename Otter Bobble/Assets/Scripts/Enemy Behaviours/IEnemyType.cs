using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 8, 2016
//
// Interface to be inherited by any enemy behaviours. 
// **************************************************************************
 
public interface IEnemyType
{
    
    void ActivateEnemy(GameObject aEnemy, float aSpd);
    void DeactivateEnemy();
    void UpdateBehaviour();
    void FlipSprite();
    void Reset( Vector3 aScale, Vector3 aPos );  

}
