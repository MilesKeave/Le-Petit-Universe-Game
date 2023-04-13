using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InstantiateTerrain : MonoBehaviour

{
    public GameObject Terrain1;
    public GameObject Terrain2;
    public GameObject Terrain3;
    public GameObject Terrain4;
    public GameObject Terrain5;
    public GameObject Terrain6;
    public GameObject Terrain7;
    public GameObject Terrain8;
    public GameObject Terrain9;

    public GameObject Terrain1WithGem1;
    public GameObject Terrain1WithGem2;
    public GameObject Terrain2WithGem1;
    public GameObject Terrain2WithGem2;
    public GameObject Terrain3WithGem1;
    public GameObject Terrain3WithGem2;
    public GameObject Terrain4WithGem1;
    public GameObject Terrain4WithGem2;
    public GameObject Terrain5WithGem;
    public GameObject Terrain6WithGem;

    public GameObject EndTerrain;
    public GameObject StartTerrain;

    public GameObject TerrainFiller;


    private List<(GameObject, GameObject)> TerrainsWithGems;
    private List<(GameObject, GameObject)> ShuffledTerrainsWithGems;
    private List<(GameObject, List<GameObject>)> TerrainsWithNoGems;
    private List<(GameObject, List<GameObject>)> ShuffledTerrainsWithNoGems;

    private int GemTerrainIndex;
    private int NoGemTerrainIndex;

    private float Location;
    private float FillerLocation;

    // Start is called before the first frame update
    void Start()
    {
        Location = 27.52f;
        FillerLocation = 13.73f;
        TerrainsWithNoGems = new List<(GameObject, List<GameObject>)>{(Terrain1, 
                                                                                new List<GameObject> {Terrain1WithGem1, Terrain1WithGem2}), 
                                                                       (Terrain2, 
                                                                                new List<GameObject> {Terrain2WithGem1, Terrain2WithGem2}),
                                                                       (Terrain3, 
                                                                                new List<GameObject> {Terrain3WithGem1, Terrain3WithGem2}),
                                                                       (Terrain4, 
                                                                                new List<GameObject> {Terrain4WithGem1, Terrain4WithGem2}),
                                                                       (Terrain5, 
                                                                                new List<GameObject> {Terrain5WithGem}), 
                                                                       (Terrain6, 
                                                                                new List<GameObject> {Terrain6WithGem}), 
                                                                       (Terrain7, 
                                                                                new List<GameObject> {}), 
                                                                       (Terrain8, 
                                                                                new List<GameObject> {}), 
                                                                       (Terrain9, 
                                                                                new List<GameObject> {})};   

        ShuffledTerrainsWithNoGems = new List<(GameObject, List<GameObject>)>(TerrainsWithNoGems.OrderBy( x => Random.value ).ToList( ));

        TerrainsWithGems = new List<(GameObject, GameObject)>{(Terrain1WithGem1, Terrain1), (Terrain1WithGem2, Terrain1), (Terrain2WithGem1, Terrain2),
                                                                 (Terrain2WithGem2, Terrain2), (Terrain3WithGem1, Terrain3), (Terrain3WithGem2, Terrain3),
                                                                 (Terrain4WithGem1, Terrain4), (Terrain4WithGem2, Terrain4), (Terrain5WithGem, Terrain5),
                                                                 (Terrain6WithGem, Terrain6)};
        ShuffledTerrainsWithGems = new List<(GameObject, GameObject)>(TerrainsWithGems.OrderBy( x => Random.value ).ToList( ));

        GemTerrainIndex = 0;
        NoGemTerrainIndex = 0;

        for(int i = 1; i < 19; i++)
        {
            if (i%2 == 1)
            {
                if (NoGemTerrainIndex != 0)
                {
                    while (ShuffledTerrainsWithNoGems[(NoGemTerrainIndex-1)%9].Item2.Contains(ShuffledTerrainsWithGems[GemTerrainIndex%10].Item1)) 
                    {
                        GemTerrainIndex++;
                    }
                }
                GameObject temp = Instantiate(ShuffledTerrainsWithGems[GemTerrainIndex%10].Item1, new Vector3 (Location, 0, 0), Quaternion.identity);
                if (i == 1)
                {
                    temp.AddComponent<Shake>();
                }
                GameObject temp2 = Instantiate(ShuffledTerrainsWithGems[GemTerrainIndex%10].Item2, new Vector3 (Location, 0, 0), Quaternion.identity);
                if (i == 1)
                {
                    temp2.AddComponent<Shake>();
                }
                GemTerrainIndex++;
            }

            else 
            {
                if (GemTerrainIndex != 0)
                {
                    while (ShuffledTerrainsWithGems[(GemTerrainIndex-1)%10].Item2 == ShuffledTerrainsWithNoGems[NoGemTerrainIndex%9].Item1) 
                    {
                        NoGemTerrainIndex++;
                    }
                }
                GameObject temp = Instantiate(ShuffledTerrainsWithNoGems[NoGemTerrainIndex%9].Item1, new Vector3 (Location, 0, 0), Quaternion.identity);
                if (i == 1)
                {
                    temp.AddComponent<Shake>();
                }
                NoGemTerrainIndex++;
            }

            Location += 27.52f;
        }

        for(int i = 0; i < 20; i++)
        {
            Instantiate(TerrainFiller, new Vector3 (FillerLocation, -7.4f, 0), Quaternion.identity);

            FillerLocation += 27.52f;
        }

        Instantiate(EndTerrain, new Vector3 (Location, 0, 0), Quaternion.identity);
        Instantiate(StartTerrain, new Vector3 (Location + 27.52f, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    
   

}
