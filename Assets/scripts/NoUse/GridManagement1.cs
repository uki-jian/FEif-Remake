//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GridManagement1 : MonoBehaviour
//{
//    public GridManagement grids;
//    public MeshRenderer meshFilter;
//    public Camera my_camera;
//    Vector3 m_scale;
//    float width, height;
//    public float cellsize=2;
//    void Start()
//    {
//        cellsize = 2;
//        meshFilter = GetComponent<MeshRenderer>();
//        my_camera = Camera.main;
//        m_scale = meshFilter.bounds.size;

//        Debug.Log("scale: " + m_scale);

//        width = m_scale.x;
//        height = m_scale.z;

//        grids = new GridManagement(width, height, cellsize, meshFilter.bounds.min);
//    }

//    // Update is called once per frame
//    void Update()
//    {

//        //Debug.Log("ChosenGrid: " + grids.chosenGrid.pos[0] + grids.chosenGrid.pos[1]);


//        if (Input.GetMouseButtonDown(0))
//        {
//            grids.GetPosAndSetValue(GetCollidedPos(), 1);
//            //Debug.Log("ChosenGrid1: " + grids.chosenGrid.pos[0] + grids.chosenGrid.pos[1]);
//        }
//    }

//    public Vector3 GetCollidedPos()
//    {
//        Vector3 posOnGround = Vector3.zero;
//        Vector3 mp = Input.mousePosition;
//        mp.z = 100;
//        Vector3 pos = Camera.main.ScreenToWorldPoint(mp);

//        //Vector3 origin = my_camera.transform.position;
//        //Vector3 origin = new Vector3(-5f, 5f, -5f);
//        //Ray ray = new Ray(origin, pos);

//        Ray ray = my_camera.ScreenPointToRay(Input.mousePosition);
        
//        RaycastHit hit;                                                              //射线撞击点
//        if (Physics.Raycast(ray, out hit))  //如果射线撞击到地面或者其他物体
//        {
//            posOnGround = hit.point;                                                 //新位置为射线撞击到地面的位置                        
//        }

//        //Debug.Log("pos:" + pos);
//        //Debug.Log("ray:" + ray);
//        //Debug.Log("Physics.Raycast(ray, out hit):" + Physics.Raycast(ray, out hit));
//        //Debug.Log("posOnGround:" + posOnGround);
//        return posOnGround;
//    }

//}
