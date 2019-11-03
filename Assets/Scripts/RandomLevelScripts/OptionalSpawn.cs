using UnityEngine;

public class OptionalSpawn : MonoBehaviour {

    public int spawnPercentage = 50;
    private int _alwaysSpawn = 100;
    private void Awake( ) {
        int rand = Random.Range(0, _alwaysSpawn );
        if(rand > spawnPercentage ) {
            this.gameObject.SetActive( false );
        } 
    }
}
