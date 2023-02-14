using UnityEngine;

namespace TowerWar
{
    public class InputManager : MonoBehaviour
    {
        public PathMaker pathMaker;
        private Camera cam;
        private Ray ray1;
        private Ray ray2;
        private Ray pathRay;
        public RaycastHit hit1;
        private RaycastHit hit2;
        private LineRenderer lineRenderer;
        public Vector3 currentPos;
        public bool AllowCutLine;
        public Vector3 startingPos;
        public Vector2 intersection;
        public bool mouseButtonHeld;
        public bool dragging;
        public bool allowFakePath;

        private void Awake()
        {
            this.cam = Camera.main;
            this.lineRenderer = this.GetComponent<LineRenderer>();
        }

        private void Update() => this.GetMouseInput();

        public void GetMouseInput()
        {
            if (Input.GetMouseButtonUp(0) && this.dragging)
            {
                PathMaker.Instance.GetComponent<LineRenderer>().enabled = false;
                this.lineRenderer.enabled = false;
                this.allowFakePath = false;
                this.dragging = false;
                this.ray2 = this.cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(this.ray2, out this.hit2))
                {
                    if (this.hit1.transform.position == this.hit2.transform.position)
                        return;
                    if(hit1.transform.gameObject.GetComponent<ParentTowerBehaviour>() && hit1.transform.gameObject.GetComponent<ParentTowerBehaviour>())
                    {
                        if (this.hit1.transform.GetComponent<ParentTowerClass>().civilization == LevelDetails.Instance.playerCivilization)
                        {
                            Debug.Log("reached Check pathable object type");
                            this.CheckPathableObjectType(this.hit1, this.hit2);
                        }
                        
                    }

                }
            }
            if (Input.GetMouseButton(0))
            {
                if (this.dragging)
                {
                    this.pathRay = this.cam.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(this.pathRay, out hitInfo))
                    {
                      //  Debug.Log((object)"checking path");
                        this.currentPos = hitInfo.point;
                        if (this.currentPos != this.startingPos)
                        {
                            Vector3 currentPos = this.currentPos;
                          //  Debug.Log((object)"mousepos changed for path");
                           // Debug.Log((object)this.startingPos);
                          //  Debug.Log((object)this.currentPos);
                            if (!(bool)(Object)this.hit1.transform.GetComponent<ParentTowerBehaviour>() && !(bool)(Object)this.hit2.transform.GetComponent<ParentTowerBehaviour>())
                            {
                            //    Debug.Log((object)"checking intersection");
                                this.IntersectionCalculator(this.startingPos, this.currentPos);
                            }
                        }
                    }
                }
                RaycastHit hitInfo1;
                if (this.allowFakePath && Physics.Raycast(this.cam.ScreenPointToRay(Input.mousePosition), out hitInfo1) && this.hit1.transform.GetComponent<ObjectDefiner>().pathType == PathType.pathmaker && !(bool)(Object)hitInfo1.transform.GetComponent<ParentTowerBehaviour>() && this.hit1.transform.GetComponent<ParentTowerClass>().civilization == LevelDetails.Instance.playerCivilization)
                {
                   
                    this.lineRenderer.enabled = true;
                    pathMaker.GetComponent<LineRenderer>().enabled = true;
                    this.pathMaker.CreateFakePath(this.hit1.transform.position, hitInfo1.point);
                }
                else
                {
                    
                }
            }
            if (!Input.GetMouseButtonDown(0))
                return;
            this.ray1 = this.cam.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(this.ray1, out this.hit1))
                return;
            this.dragging = true;
            Debug.Log((object)"dragging should be true here");
            this.startingPos = this.hit1.point;
            if (!hit1.transform.GetComponent<ParentTowerBehaviour>())
            {

                return;
                if (this.hit1.transform.gameObject.GetComponent<ObjectDefiner>().pathType != PathType.pathmaker && this.hit1.transform.gameObject.GetComponent<ObjectDefiner>().pathType != PathType.projectilepath)
                    return;
            }

            Debug.Log((object)"1st tower reached");
            this.allowFakePath = true;
        }

        //public void GetTouchInput()
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        this.dragging = true;
        //        this.ray1 = this.cam.ScreenPointToRay(Input.mousePosition);
        //        if (Physics.Raycast(this.ray1, out this.hit1) && (this.hit1.transform.gameObject.GetComponent<ObjectDefiner>().pathType == PathType.pathmaker || this.hit1.transform.gameObject.GetComponent<ObjectDefiner>().pathType == PathType.projectilepath))
        //            Debug.Log((object)"1st tower reached");
        //    }
        //    if (Input.GetMouseButtonUp(0))
        //    {
        //        PathMaker.Instance.gameObject.GetComponent<LineRenderer>().enabled = false;
        //        this.lineRenderer.enabled = false;
        //        this.startingPos = this.currentPos;
        //        this.dragging = false;
        //        this.ray2 = this.cam.ScreenPointToRay(Input.mousePosition);
        //        if (Physics.Raycast(this.ray2, out this.hit2))
        //        {
        //            if (this.hit1.transform.position == this.hit2.transform.position)
        //                return;
        //            if (this.hit1.transform.GetComponent<ParentTowerClass>().civilization == LevelDetails.Instance.playerCivilization)
        //                this.CheckPathableObjectType(this.hit1, this.hit2);
        //        }
        //    }
        //    if (Input.GetMouseButton(0) && this.dragging)
        //    {
        //        this.pathRay = this.cam.ScreenPointToRay(Input.mousePosition);
        //        RaycastHit hitInfo;
        //        if (Physics.Raycast(this.pathRay, out hitInfo))
        //        {
        //            PathInfo component;
        //            if (hitInfo.transform.TryGetComponent<PathInfo>(out component))
        //                this.pathMaker.PathRemovalCheckInList(LevelDetails.Instance.Towers.IndexOf(component.startingTower), LevelDetails.Instance.Towers.IndexOf(component.endingTower));
        //            this.startingPos = hitInfo.point;
        //            Debug.Log((object)"checking path");
        //            if (this.currentPos != this.startingPos)
        //            {
        //                Vector3 currentPos = this.currentPos;
        //                Debug.Log((object)"mousepos changed for path");
        //                Debug.Log((object)this.startingPos);
        //                Debug.Log((object)this.currentPos);
        //                if (!(bool)(Object)this.hit1.transform.GetComponent<ParentTowerBehaviour>() || (bool)(Object)this.hit2.transform.GetComponent<ParentTowerBehaviour>())
        //                    this.IntersectionCalculator(this.startingPos, this.currentPos);
        //            }
        //            this.currentPos = hitInfo.point;
        //        }
        //    }
        //    if (Input.GetMouseButton(0))
        //    {
        //        RaycastHit hitInfo;
        //        if (!Physics.Raycast(this.cam.ScreenPointToRay(Input.mousePosition), out hitInfo) || this.hit1.transform.GetComponent<ObjectDefiner>().pathType != PathType.pathmaker || (bool)(Object)hitInfo.transform.GetComponent<ParentTowerBehaviour>() || this.hit1.transform.GetComponent<ParentTowerClass>().civilization != LevelDetails.Instance.playerCivilization)
        //            return;
                
        //        this.lineRenderer.enabled = true;
        //        this.pathMaker.CreateFakePath(this.hit1.transform.position, hitInfo.point);
        //    }
          
                
               
        //}

        public void CheckPathableObjectType(RaycastHit hit1, RaycastHit hit2)
        {
            ParentTowerClass component1;
            ParentTowerClass component2;
            Debug.Log(hit1.transform.gameObject, hit2.transform.gameObject);
            Debug.Log("abc "+ hit2.transform.gameObject);
            if (!hit1.transform.gameObject.TryGetComponent<ParentTowerClass>(out component1) || !hit2.transform.gameObject.TryGetComponent<ParentTowerClass>(out component2) || (Object)component1 == (Object)component2)
                return;
            if (component1.objDefiner.pathType == PathType.pathmaker)
            {
                Debug.Log("reached here");
                if (component1.civilization != LevelDetails.Instance.playerCivilization || !LevelDetails.Instance.Towers.Contains(component1.gameObject) || !LevelDetails.Instance.Towers.Contains(component2.gameObject))
                    return;
                int fromTower = LevelDetails.Instance.Towers.IndexOf(component1.gameObject);
                int toTower = LevelDetails.Instance.Towers.IndexOf(component2.gameObject);
                Debug.Log("hurdles check reached");
                if (!PathMaker.PathHurdlesCheck(component1.gameObject, component2.gameObject))
                    return;
                //    if (ServernGameBridge.Instance.onlineGame)
                //       ServernGameBridge.Instance.MakePathOnServer(fromTower, toTower);
                //   else
                Debug.LogWarning("create path reached");
                    this.pathMaker.CreatePath(fromTower, toTower);
            }
        }

        public void IntersectionCalculator(Vector3 startingPoint, Vector3 endingPoint)
        {
            Debug.Log((object)"came in first loop of intersection");
            foreach (PathInfo allPath in this.pathMaker.AllPaths)
            {
                if (this.CheckIntersection(startingPoint, endingPoint, allPath.startingTower.transform.position, allPath.endingTower.transform.position) && allPath.civilization == LevelDetails.Instance.playerCivilization)
                {
                    Debug.Log((object)"checking found path to remove");
                //    if (ServernGameBridge.Instance.onlineGame)
            //        {
                     //   ServernGameBridge.Instance.RemovePathOnServer(LevelDetails.Instance.Towers.IndexOf(allPath.startingTower), LevelDetails.Instance.Towers.IndexOf(allPath.endingTower));
                     //   break;
                  //  }
                    this.pathMaker.PathRemovalCheckInList(LevelDetails.Instance.Towers.IndexOf(allPath.startingTower), LevelDetails.Instance.Towers.IndexOf(allPath.endingTower));
                    break;
                }
            }
        }

        private bool CheckIntersection(
          Vector3 startPos,
          Vector3 endPos,
          Vector3 pathStartPos,
          Vector3 pathEndPos)
        {
            return this.CheckIntersection(new Vector2(startPos.x, startPos.z), new Vector2(endPos.x, endPos.z), new Vector2(pathStartPos.x, pathStartPos.z), new Vector2(pathEndPos.x, pathEndPos.z));
        }

        private bool CheckIntersection(
          Vector2 startPos,
          Vector2 endPos,
          Vector2 pathStartPos,
          Vector2 pathEndPos)
        {
            if ((double)this.IntersectionHelperFunc(startPos.x, startPos.y, endPos.x, endPos.y, pathStartPos.x, pathStartPos.y) * (double)this.IntersectionHelperFunc(startPos.x, startPos.y, endPos.x, endPos.y, pathEndPos.x, pathEndPos.y) <= 0.0 && (double)this.IntersectionHelperFunc(pathStartPos.x, pathStartPos.y, pathEndPos.x, pathEndPos.y, startPos.x, startPos.y) * (double)this.IntersectionHelperFunc(pathStartPos.x, pathStartPos.y, pathEndPos.x, pathEndPos.y, endPos.x, endPos.y) <= 0.0)
            {
                Debug.Log((object)"YES");
                return true;
            }
            Debug.Log((object)"NO");
            return false;
        }

        public float IntersectionHelperFunc(
          float x1,
          float y1,
          float x2,
          float y2,
          float x,
          float y)
        {
            return (float)(((double)x2 - (double)x1) * ((double)y - (double)y1) - ((double)y2 - (double)y1) * ((double)x - (double)x1));
        }
    }
}