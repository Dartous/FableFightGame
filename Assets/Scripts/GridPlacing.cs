using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridPlacing : MonoBehaviour
{
    public static GridPlacing current;

    public GridLayout gridLayout;
    private Grid grid;

    [SerializeField] private Tilemap MainTilemap;

    [Header("Unit costs in order: hat, skeleton, mushroom, cube")]
    public int[] costs;
    public GameScript gameScript;
    public int scrollval = 0 - 1;
    private Vector3 cellPos;
    private Vector3Int cellPosInt;
    public List<Vector3> TakenPositions = new List<Vector3>();
    public Dictionary<Vector3, GameObject> InstantiatedObjDict = new Dictionary<Vector3, GameObject>();
    public List<GameObject> Asset = new List<GameObject>();
    static LayerMask LUI;


    public DeleteScript RemoveObj;

    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }



    //public static Vector3 GetMouseWorldPosition()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    if (Physics.Raycast(ray, out RaycastHit rc_hit))
    //    {
    //        return rc_hit.point;
    //    }
    //    else
    //    {
    //        return new Vector3(0, 0, 0);
    //    }
    //}
    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit rc_hit) && rc_hit.collider.tag == "GridObject")
        {
            print(rc_hit.collider.gameObject);
            return rc_hit.point;
        }
        else
        {
            return new Vector3(0, 0, 0);
        }
    }

    public int ScrollRight()
    {
        int max = Asset.Count;
        if (max != scrollval)
        {
            scrollval = scrollval + 1;
            return scrollval;
        }
        else
        {
            print("This is the maximum asset it can go");
            return 0;
        }
    }

    public int ScrollLeft()
    {
        int max = Asset.Count;
        if (-1 != scrollval)
        {
            scrollval = scrollval - 1;
            return scrollval;
        }
        else
        {
            print("This is the minimum asset it can go");
            return 0;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown((KeyCode.Q)))
        {
            ScrollLeft();

        }
        if (Input.GetKeyDown((KeyCode.E)))
        {
            ScrollRight();
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (GetMouseWorldPosition() == Vector3.zero) { return; }
            else
            {
                cellPos = GetMouseWorldPosition();
                Vector3Int d = grid.WorldToCell(cellPos);
                Vector3 Centre_be_Centre = grid.GetCellCenterWorld(d);
                Vector3 realcell = grid.CellToWorld(d);
                int AssetType = scrollval;
                print(scrollval);
                print(Centre_be_Centre);
                ObjectInstantiate(Centre_be_Centre, Asset[scrollval]);
                print(Asset[scrollval].name);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            cellPos = GetMouseWorldPosition();
            Vector3Int d = grid.WorldToCell(cellPos);
            Vector3 Centre_be_Centre = grid.GetCellCenterWorld(d);
            DeleteInstantiatedObj(Centre_be_Centre);
        }
    }

    public void UnitDead(Vector3 Pos)
    {
        Vector3Int d = grid.WorldToCell(Pos);
        Vector3 Centre_be_Centre = grid.GetCellCenterWorld(d);
        DeleteInstantiatedObj(Centre_be_Centre);
    }

    public void DeleteInstantiatedObj(Vector3 cepos)
    {
        if (InstantiatedObjDict.ContainsKey(cepos))
        {
            InstantiatedObjDict[cepos].gameObject.SetActive(false);
            InstantiatedObjDict.Remove(cepos);
        }
    }
    private void ObjectInstantiate(Vector3 cepos, GameObject objec)
    {
        if (InstantiatedObjDict.ContainsKey(cepos))
        {
            print("Already Instantiated");
            return;
        }
        else
        {
            if (scrollval == 0)
            {
                //0.065
                //only place if enough money
                if (gameScript.knowledge >= costs[0])
                {
                    WizardHatInstatiate(cepos, objec);
                    gameScript.knowledge -= costs[0];
                }
                else
                {
                    print("Not enough Knowledge");
                }

            }
            if (scrollval == 1)
            {
                if (gameScript.knowledge >= costs[1])
                {
                    SkullThrowerInstantiate(cepos, objec);
                    gameScript.knowledge -= costs[1];
                }
                else
                {
                    print("Not enough Knowledge");
                }
            }
            if (scrollval == 2)
            {
                if (gameScript.knowledge >= costs[3])
                {
                    MushroomInstantiate(cepos, objec);
                    gameScript.knowledge -= costs[3];
                }
                else
                {
                    print("Not enough Knowledge");
                }
            }
            if (scrollval == 3)
            {
                if (gameScript.knowledge >= costs[2])
                {
                    SlimeInstantiate(cepos, objec);
                    gameScript.knowledge -= costs[2];
                }
                else
                {
                    print("Not enough Knowledge");
                }
            }
            //f.transform.position = new Vector3(0, offset, 0);
            //Asset.Add(Instantiate(prefab1, cepos, Quaternion.identity));
            //TakenPositions.Add(cepos);
        }

    }
    private void MushroomInstantiate(Vector3 ipos, GameObject objec)
    {
        //0.898
        Quaternion Rot = Quaternion.Euler(0, 90, 0);
        GameObject f = Instantiate(objec, new Vector3(ipos.x, 0f, ipos.z), Rot);
        InstantiatedObjDict.Add(ipos, f);
    }
    private void SkullThrowerInstantiate(Vector3 ipos, GameObject objec)
    {
        Quaternion Rot = Quaternion.Euler(0, 0, 0);
        GameObject f = Instantiate(objec, new Vector3(ipos.x, 1f, ipos.z), Rot);
        InstantiatedObjDict.Add(ipos, f);
    }
    private void SlimeInstantiate(Vector3 ipos, GameObject objec)
    {
        Quaternion Rot = Quaternion.Euler(0, 0, 0);
        GameObject f = Instantiate(objec, new Vector3(ipos.x, 1f, ipos.z), Rot);
        InstantiatedObjDict.Add(ipos, f);
    }

    private void WizardHatInstatiate(Vector3 ipos, GameObject objec)
    {
        Quaternion Rot = Quaternion.Euler(0, -90, 0);
        GameObject f = Instantiate(objec, new Vector3(ipos.x, 0.3f, ipos.z), Rot);
        InstantiatedObjDict.Add(ipos, f);
    }
}
