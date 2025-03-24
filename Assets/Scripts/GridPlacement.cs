using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Tilemaps;

// IMPORTANT - Attach the Delete script on all Prefab Assets
public class GridBuild : MonoBehaviour
{
    public static GridBuild current;

    public GridLayout gridLayout;
    private Grid grid;

    [SerializeField] private Tilemap MainTilemap;

    public int scrollval = 0 - 1;
    private Vector3 cellPos;
    private Vector3Int cellPosInt;
    public List<Vector3> TakenPositions = new List<Vector3>();
    public Dictionary<Vector3, GameObject> InstantiatedObjDict = new Dictionary<Vector3, GameObject>();
    public List<GameObject> Asset = new List<GameObject>();

    public DeleteScript RemoveObj;

    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit rc_hit))
        {
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
            cellPos = GetMouseWorldPosition();
            Vector3Int d = grid.WorldToCell(cellPos);
            Vector3 Centre_be_Centre = grid.GetCellCenterWorld(d);
            Vector3 realcell = grid.CellToWorld(d);
            int AssetType = scrollval;
            print(scrollval);
            ObjectInstantiate(Centre_be_Centre, Asset[scrollval]);
            print(Asset[scrollval].name);
        }
        if (Input.GetMouseButtonDown(1))
        {
            cellPos = GetMouseWorldPosition();
            Vector3Int d = grid.WorldToCell(cellPos);
            Vector3 Centre_be_Centre = grid.GetCellCenterWorld(d);
            DeleteInstantiatedObj(Centre_be_Centre);
        }
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
            Quaternion Rot = Quaternion.Euler(0, -90, 0);
            GameObject f = Instantiate(objec, cepos, Rot);
            InstantiatedObjDict.Add(cepos, f);
            float size = f.transform.localScale.y;
            float offset = size/2; // if u want it standing on the tip remove the division. this calc is the furthest it can go without showing the curve of the obj. - Nathan Dzingai
            f.transform.position = new Vector3(cepos.x, offset, cepos.z);
            print(Quaternion.identity);
            //Co-Routine To set check and reset the position value.
            //f.transform.position = new Vector3(0, offset, 0);
            //Asset.Add(Instantiate(prefab1, cepos, Quaternion.identity));
            //TakenPositions.Add(cepos);
        }

    }
}