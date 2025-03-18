using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem current;

    public GridLayout gridLayout;
    private Grid grid;

    [SerializeField] private Tilemap MainTilemap;


    public GameObject prefab1;
    public GameObject prefab2;
    private Vector3 cellPos;
    private Vector3Int cellPosInt;
    public List<Vector3> TakenPositions = new List<Vector3>();
    public Dictionary<Vector3, GameObject> InstiatedObjDict = new Dictionary<Vector3, GameObject>();
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
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cellPos = GetMouseWorldPosition();
            Vector3Int d = grid.WorldToCell(cellPos);
            Vector3 Centre_be_Centre = grid.GetCellCenterWorld(d);
            Vector3 realcell = grid.CellToWorld(d);
            ObjectInstantiate(Centre_be_Centre);
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
        if (InstiatedObjDict.ContainsKey(cepos))
        {
            InstiatedObjDict[cepos].gameObject.SetActive(false);
            InstiatedObjDict.Remove(cepos);
        }
    }
    private void ObjectInstantiate(Vector3 cepos)
    {
        if (InstiatedObjDict.ContainsKey(cepos))
        {
            print("Already Instantiated");
            return;
        }
        else
        {
            GameObject f = Instantiate(prefab1, cepos, Quaternion.identity);
            InstiatedObjDict.Add(cepos, f);
            //Asset.Add(Instantiate(prefab1, cepos, Quaternion.identity));
            //TakenPositions.Add(cepos);
        }

    }
}
