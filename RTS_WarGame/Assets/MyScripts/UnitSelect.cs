using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelect : MonoBehaviour
{
    private GameObject CurrentSelect;
    private ArrayList CurrentlySelected = new ArrayList();//of gameobjects
    public ArrayList UnitsOnScreen = new ArrayList();//of gameobjects
    private ArrayList UnitsInDragList = new ArrayList();//of gameobjects
    private bool FinishDragging;
    private Vector3 mouseDown;
    private Vector3 mouseCur;
    private Vector2 mouseStart;
    private Vector2 BoxStart;
    private Vector2 BoxEnd;
    private RaycastHit hit;
    private int layerMask = 9;
    private bool IsDragging;
    private float TimeLimit;
    private float TimeLeft;
    private float clickDragZone;
    private float BoxLeft, BoxTop, BoxWidth, BoxHeight;
    public GUIStyle DragSkin;
    public Material UnSelectedMaterial;
    public Material SelectedMaterial;

    private void Start()
    {
        mouseDown = Vector3.zero;
        layerMask = ~layerMask;
        TimeLimit = 0.0f;//fix
        clickDragZone = 1.5f;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
        {
            mouseCur = hit.point;
            if (Input.GetMouseButtonDown(0))
            {
                mouseDown = hit.point;
                TimeLeft = TimeLimit;
                mouseStart = Input.mousePosition;
                if (!ShiftKeyDown())
                {
                    DeselectGameObjects();
                }
            }
            else if(Input.GetMouseButton(0))
            {
                if(!IsDragging)
                {
                    TimeLeft -= Time.deltaTime;
                    if(TimeLeft <= 0)
                    {
                        IsDragging = true;
                    }
                    if (UserDragging(mouseStart, Input.mousePosition))
                    {
                        IsDragging = true;
                    }
                }
            }
            else if(Input.GetMouseButtonUp(0))
            {
                FinishDragging = true;
                IsDragging = false;
            }
            if (!IsDragging)
            {
                if (hit.collider.name == "Terrain")
                {
                    if (Input.GetMouseButtonUp(0) && UserMouseClick(mouseDown))
                    {
                        if (!ShiftKeyDown())
                        {
                            DeselectGameObjects();
                        }
                    }
                }
                else if (Input.GetMouseButtonUp(0) && UserMouseClick(mouseDown))
                {
                    if (hit.collider.gameObject.tag == "Friendly")
                    {
                        if (!AlreadySelected(hit.collider.gameObject))
                        {
                            if (!ShiftKeyDown())
                            {
                                DeselectGameObjects();
                            }
                            CurrentSelect = hit.collider.gameObject;
                            CurrentSelect.GetComponent<Renderer>().material = SelectedMaterial;
                            CurrentlySelected.Add(hit.collider.gameObject);
                        }
                        else
                        {
                            if (ShiftKeyDown())
                            {
                                removeUnitFromArray(hit.collider.gameObject);
                            }
                            else
                            {
                                DeselectGameObjects();
                                CurrentSelect = hit.collider.gameObject;
                                CurrentSelect.GetComponent<Renderer>().material = SelectedMaterial;
                                CurrentlySelected.Add(hit.collider.gameObject);
                            }
                        }
                    }
                }
                //else
                //{
                //    if (!ShiftKeyDown())
                //    {
                //        DeselectGameObjects();
                //    }
                //}
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.cyan);

        if(IsDragging == true)
        {
            //GUI Variables
            BoxWidth = Camera.main.WorldToScreenPoint(mouseDown).x - Camera.main.WorldToScreenPoint(mouseCur).x;
            BoxHeight = Camera.main.WorldToScreenPoint(mouseDown).y - Camera.main.WorldToScreenPoint(mouseCur).y;
            BoxLeft = Input.mousePosition.x;
            BoxTop = (Screen.height - Input.mousePosition.y) - BoxHeight;

            if(BoxWidth > 0)
            {
                if(BoxHeight > 0)
                {
                    BoxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y + BoxHeight);
                }
                else
                {
                    BoxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                }
            }
            else
            {
                if(BoxWidth < 0)
                {
                    if (BoxHeight > 0)
                    {
                        BoxStart = new Vector2(Input.mousePosition.x + BoxWidth, Input.mousePosition.y + BoxHeight);
                    }
                    else
                    {
                        BoxStart = new Vector2(Input.mousePosition.x + BoxWidth, Input.mousePosition.y);
                    }
                }
            }
        }

        BoxEnd = new Vector2(BoxStart.x + Mathf.Abs(BoxWidth), BoxStart.y - Mathf.Abs(BoxWidth));
    }

    private void LateUpdate()
    {
        UnitsInDragList.Clear();
        if ((IsDragging || FinishDragging) && UnitsOnScreen.Count > 0)
        {
            for (int i = 0; i < UnitsOnScreen.Count; i++)
            {
                GameObject gameObj = UnitsOnScreen[i] as GameObject;
                UnitMovement unitMove = gameObj.GetComponent<UnitMovement>();
                if (!AlreadyInDragList(gameObj))
                {
                    if (UnitInDragBox(unitMove.ScreenPos))
                    {
                        gameObj.GetComponent<Renderer>().material = SelectedMaterial;
                        UnitsInDragList.Add(gameObj);
                    }
                    else
                    {
                        if (!AlreadySelected(gameObj))
                        {
                            gameObj.GetComponent<Renderer>().material = UnSelectedMaterial;
                        }
                    }
                }
            }
        }
        if (FinishDragging)
        {
            FinishDragging = false;
            AddDragUnits();
        }

        if (Input.GetMouseButtonDown(2))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.collider.name == "Terrain")
                {
                    for (int i = 0; i < CurrentlySelected.Count; i++)
                    {
                        GameObject gameObj = CurrentlySelected[i] as GameObject;
                        gameObj.GetComponent<UnitMovement>().SetunitDestination(hit);
                        DeselectGameObjects();
                    }
                }
            }
        }
    }

    private void OnGUI()
    {
        if(IsDragging)
        {
            GUI.Box(new Rect(BoxLeft, BoxTop, BoxWidth, BoxHeight), "", DragSkin);
        }
    }

    public bool UserDragging(Vector2 DragStart, Vector2 DragEnd)
    {
        if(DragEnd.x > DragStart.x + clickDragZone || DragEnd.x < DragStart.x - clickDragZone || 
            DragEnd.y > DragStart.y + clickDragZone || DragEnd.y < DragStart.y - clickDragZone)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool UserMouseClick(Vector3 hitPoint)//if user clicked or draged?
    {
        if(mouseDown.x < (hitPoint.x + clickDragZone) && mouseDown.x > (hitPoint.x - clickDragZone) &&
            mouseDown.y < (hitPoint.y + clickDragZone) && mouseDown.y < (hitPoint.y - clickDragZone) &&
            mouseDown.z < (hitPoint.z + clickDragZone) && mouseDown.z < (hitPoint.z - clickDragZone))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void DeselectGameObjects()
    {
        Debug.Log("Deselecting");
        if (CurrentlySelected.Count > 0)
        {
            for(int i = 0; i < CurrentlySelected.Count; i++)
            {
                GameObject ArrayListUnits = CurrentlySelected[i] as GameObject;
                ArrayListUnits.GetComponent<Renderer>().material = UnSelectedMaterial;
                ArrayListUnits.GetComponent<UnitMovement>().Selected = false;//add tanks/planes
            }
            CurrentlySelected.Clear();
        }
    }

    public bool AlreadySelected(GameObject unit)
    {
        if (CurrentlySelected.Count > 0)
        {
            for (int i = 0; i < CurrentlySelected.Count; i++)
            {
                GameObject ArrayListUnits = CurrentlySelected[i] as GameObject;
                if(ArrayListUnits == unit)
                {
                    return true;
                }
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public void removeUnitFromArray(GameObject unit)
    {
        if (CurrentlySelected.Count > 0)
        {
            for (int i = 0; i < CurrentlySelected.Count; i++)
            {
                GameObject ArrayListUnits = CurrentlySelected[i] as GameObject;
                if (ArrayListUnits == unit)
                {
                    ArrayListUnits.GetComponent<Renderer>().material = UnSelectedMaterial;
                    CurrentlySelected.RemoveAt(i);
                }
            }
            return;
        }
        else
        {
            return;
        }
    }

    public bool ShiftKeyDown()
    {
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool InScreenSpace(Vector2 UnitScreenPos)//check if unit is in screen space
    {
        if(UnitScreenPos.x < Screen.width && UnitScreenPos.y < Screen.height && UnitScreenPos.x > 0 && UnitScreenPos.y > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveScreenUnit(GameObject unit)
    {
        for(int i = 0; i < UnitsOnScreen.Count; i++)
        {
            GameObject unitObj = UnitsOnScreen[i] as GameObject;
            if(unit == unitObj)
            {
                UnitsOnScreen.RemoveAt(i);
                unitObj.GetComponent<UnitMovement>().OnScreen = false;
                break;
            }
        }
    }

    public bool UnitInDragBox(Vector2 UnitScreenPos)
    {
        if(UnitScreenPos.x > BoxStart.x && UnitScreenPos.y < BoxStart.y && UnitScreenPos.x < BoxEnd.x && UnitScreenPos.y > BoxEnd.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool AlreadyInDragList(GameObject unit)
    {
        if (UnitsInDragList.Count > 0)
        {
            for (int i = 0; i < CurrentlySelected.Count; i++)
            {
                GameObject ArrayListUnits = CurrentlySelected[i] as GameObject;
                if (ArrayListUnits == unit)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void AddDragUnits()
    {
        if(UnitsInDragList.Count > 0)
        {
            for(int i = 0; i < UnitsInDragList.Count; i++)
            {
                GameObject gameObj = UnitsInDragList[i] as GameObject;
                CurrentlySelected.Add(gameObj);
                gameObj.GetComponent<UnitMovement>().Selected = true;
            }
        }
        UnitsInDragList.Clear();
    }
}