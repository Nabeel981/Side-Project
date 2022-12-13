using System.Collections.Generic;
using UnityEngine;


    public class PathMaker : MonoBehaviour, IPathMaker
    {
        public List<PathInfo> AllPaths;
        private LineRenderer lineRenderer;
        public PathInfo pathtoRemove;

        public static PathMaker Instance { get; private set; }

        private void Awake() => this.lineRenderer = this.gameObject.GetComponent<LineRenderer>();

        private void OnEnable()
        {
            if ((Object)PathMaker.Instance == (Object)null)
                PathMaker.Instance = this;
            else
                Object.Destroy((Object)this.gameObject);
        }

        public void CreatePath(int fromTower, int toTower)
        {
            GameObject tower1 = LevelDetails.Instance.Towers[fromTower];
            GameObject tower2 = LevelDetails.Instance.Towers[toTower];
            NonRangedTowerClass component1 = tower1.GetComponent<NonRangedTowerClass>();
            ParentTowerClass component2 = tower2.GetComponent<ParentTowerClass>();
            Vector3 position = tower1.transform.position;
            string str1 = position.ToString();
            position = tower2.transform.position;
            string str2 = position.ToString();
            Debug.Log((object)(str1 + " to " + str2));
            if (!this.CanDrawPath(component1) || !PathMaker.PathHurdlesCheck(tower1, tower2) || !this.AlreadyDrawnPathCheck(component1, component2.gameObject))
                return;
            if ((bool)(Object)tower2.GetComponent<NonRangedTowerClass>())
            {
                tower2.GetComponent<NonRangedTowerClass>();
                if (!this.CanPathReverseAlly(tower1, tower2))
                    return;
                this.MakePathAndLineRendererAndAddInGlobal(tower1, tower2);
                PathableObjects component3 = tower2.GetComponent<PathableObjects>();
                this.AssignNewPath(component1, component3);
            }
            else
            {
                if (!(bool)(Object)tower2.GetComponent<RangedTowerClassParent>() && !tower2.GetComponent<Obstacles>().interactable)
                    return;
                PathableObjects component4 = tower2.GetComponent<PathableObjects>();
                this.MakePathAndLineRendererAndAddInGlobal(tower1, tower2);
                this.AssignNewPath(component1, component4);
            }
        }

        public void CannonPath(GameObject CannonTower, Vector3 cannonTarget)
        {
            new GameObject("cannonPath").AddComponent<LineRenderer>();
            CannonTowerClass component = CannonTower.GetComponent<CannonTowerClass>();
            component.targetArea = cannonTarget;
            component.startshooting = true;
        }

        public void CreateFakePath(Vector3 selfTower, Vector3 fakePathPoint)
        {
            RaycastHit hitInfo;
            if (Physics.Linecast(selfTower, fakePathPoint, out hitInfo))
            {
                if ((bool)(Object)hitInfo.transform.GetComponent<Obstacles>())
                {
                    hitInfo.transform.GetComponent<Obstacles>();
                    this.GetComponent<LineRenderer>().material = Resources.Load<Material>("Materials/BlackLine");
                    this.lineRenderer.SetPosition(0, selfTower);
                    this.lineRenderer.SetPosition(1, fakePathPoint);
                }
                this.GetComponent<LineRenderer>().material = Resources.Load<Material>("Materials/NormalLine");
                this.lineRenderer.SetPosition(0, selfTower);
                this.lineRenderer.SetPosition(1, fakePathPoint);
            }
            this.GetComponent<LineRenderer>().material = Resources.Load<Material>("Materials/NormalLine");
            this.lineRenderer.SetPosition(0, selfTower);
            this.lineRenderer.SetPosition(1, fakePathPoint);
        }

        public bool CanDrawPath(NonRangedTowerClass thisTower) => thisTower.level > thisTower.outgoingPaths;

        public void AssignNewPath(NonRangedTowerClass thisTower, PathableObjects thatPathableObject) => this.AddTowersAndPathNumbers(thisTower, thatPathableObject);

        public void PathRemovalCheckInList(int fromTower, int toTower)
        {
            for (int index = 0; index < LevelDetails.Instance.Towers[fromTower].GetComponent<NonRangedTowerClass>().paths.Length; ++index)
            {
                if ((Object)LevelDetails.Instance.Towers[fromTower].GetComponent<NonRangedTowerClass>().paths[index] != (Object)null && LevelDetails.Instance.Towers.IndexOf(LevelDetails.Instance.Towers[fromTower].GetComponent<NonRangedTowerClass>().paths[index].GetComponent<PathInfo>().endingTower) == toTower)
                {
                    this.RemoveOldPath(LevelDetails.Instance.Towers[fromTower].GetComponent<NonRangedTowerClass>().paths[index]);
                    LevelDetails.Instance.Towers[fromTower].GetComponent<NonRangedTowerClass>().paths[index] = (PathInfo)null;
                    break;
                }
            }
        }

        public void RemoveOldPath(PathInfo pathInfo) => this.RemoveTowersAndPathNumbers(pathInfo.startingTower.GetComponent<NonRangedTowerClass>(), pathInfo.endingTower.GetComponent<PathableObjects>(), pathInfo);

        public void RemoveTowersAndPathNumbers(
          NonRangedTowerClass thisTower,
          PathableObjects thatObject,
          PathInfo pathInfo)
        {
            --thisTower.outgoingPaths;
            --thatObject.incomingPaths;
            thisTower.targetTowers.Remove(thatObject.gameObject);
            thatObject.targetedBy.Remove(thisTower.selfTower);
            Object.Destroy((Object)pathInfo.gameObject);
        }

        public bool CanStartPath(NonRangedTowerClass thisTower) => thisTower.outgoingPaths < thisTower.level;

        public bool CanPathReverseAlly(GameObject thisTowerGameobject, GameObject thatTowerGameobject)
        {
            NonRangedTowerClass component1 = thisTowerGameobject.GetComponent<NonRangedTowerClass>();
            NonRangedTowerClass component2 = thatTowerGameobject.GetComponent<NonRangedTowerClass>();
            Debug.Log((object)(thisTowerGameobject?.ToString() + "asdasd" + thatTowerGameobject?.ToString()));
            if (component1.civilization == component2.civilization)
            {
                for (int index1 = 0; index1 < component2.targetTowers.Count; ++index1)
                {
                    if (component2.targetTowers.Contains(component1.selfTower))
                    {
                        component2.targetTowers.RemoveAt(index1);
                        for (int index2 = 0; index2 < this.AllPaths.Count; ++index2)
                        {
                            if ((Object)component2.selfTower == (Object)this.AllPaths[index2].startingTower && (Object)component1.selfTower == (Object)this.AllPaths[index2].endingTower)
                            {
                                this.PathRemovalCheckInList(LevelDetails.Instance.Towers.IndexOf(component2.gameObject), LevelDetails.Instance.Towers.IndexOf(component1.gameObject));
                                return true;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public bool AlreadyDrawnPathCheck(NonRangedTowerClass thisTower, GameObject thatGameObject) => !thisTower.targetTowers.Contains(thatGameObject);

        public void AddTowersAndPathNumbers(
          NonRangedTowerClass thisTower,
          PathableObjects thatPathableObject)
        {
            ++thisTower.outgoingPaths;
            ++thatPathableObject.incomingPaths;
            thisTower.targetTowers.Add(thatPathableObject.gameObject);
            thatPathableObject.targetedBy.Add(thisTower.selfTower);
        }

        public void IgnoreRayCastOrNot(GameObject path, NonRangedTowerClass thisTower)
        {
            if (thisTower.civilization == Civilization.Green)
                return;
            path.layer = LayerMask.NameToLayer("Ignore Raycast");
        }

        public void MakePathAndLineRendererAndAddInGlobal(
          GameObject thisTowerGameobject,
          GameObject thatTowerGameobject)
        {
            NonRangedTowerClass component = thisTowerGameobject.GetComponent<NonRangedTowerClass>();
            GameObject gameObject = new GameObject("path");
            ServernGameBridge.Instance.MovethistoScene(gameObject);
            PathInfo pathInfo = gameObject.AddComponent<PathInfo>();
            pathInfo.civilization = component.civilization;
            pathInfo.startingTower = thisTowerGameobject;
            pathInfo.endingTower = thatTowerGameobject;
            gameObject.name = "Path From " + pathInfo.startingTower.name.ToString() + " to " + pathInfo.endingTower.name.ToString();
            this.IgnoreRayCastOrNot(gameObject, component);
            this.AllPaths.Add(pathInfo);
            component.selfTower = thisTowerGameobject;
            Debug.Log((object)thatTowerGameobject.name.ToString());
            if (component.towerType != TowerType.archerTower && component.towerType != TowerType.archerTower)
            {
                LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
                lineRenderer.SetPosition(0, new Vector3(component.selfTower.transform.position.x, component.selfTower.transform.position.y + 0.05f, component.selfTower.transform.position.z));
                lineRenderer.SetPosition(1, pathInfo.endingTower.transform.position);
                Mesh mesh1 = new Mesh();
                MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
                gameObject.GetComponent<LineRenderer>().BakeMesh(mesh1);
                Mesh mesh2 = mesh1;
                meshCollider.sharedMesh = mesh2;
                this.PathColor(gameObject, component);
                for (int index = 0; index < component.paths.Length; ++index)
                {
                    if ((Object)component.paths[index] == (Object)null)
                    {
                        component.paths[index] = pathInfo;
                        return;
                    }
                }
            }
            component.enable = 1;
        }

        public void PathColor(GameObject path, NonRangedTowerClass thisTower) => path.GetComponent<LineRenderer>().material.color = thisTower.GetComponent<MeshRenderer>().material.color;

        public static bool PathHurdlesCheck(GameObject startingTower, GameObject endingTower)
        {
            RaycastHit hitInfo;
            return !Physics.Linecast(startingTower.transform.position, endingTower.transform.position, out hitInfo) || hitInfo.transform.GetComponent<ObjectDefiner>().objectType != ObjectType.Tower || !((Object)hitInfo.transform.gameObject != (Object)startingTower) || !((Object)hitInfo.transform.gameObject != (Object)endingTower);
        }

        private void OnDisable()
        {
            if (!((Object)PathMaker.Instance == (Object)this))
                return;
            PathMaker.Instance = (PathMaker)null;
        }

        public void RemoveAllOutgoingPathsOnConversion(GameObject Tower)
        {
            Debug.Log((object)"joined loop  for path removal");
            foreach (PathInfo allPath in this.AllPaths)
            {
                if (Tower.transform.position == allPath.startingTower.transform.position)
                {
                    this.pathtoRemove = allPath;
                    this.RemoveOldPath(allPath);
                }
            }
            this.AllPaths.Remove(this.pathtoRemove);
        }
    }

