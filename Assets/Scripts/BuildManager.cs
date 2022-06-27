using UnityEngine;

public class BuildManager : MonoBehaviour
{
    #region Singleton
    public static BuildManager instance;
    private void Awake() {
        if(instance != null){
            Debug.LogError("il y a déjà un BuildManager");
            return;
        }
        instance = this;
    }
    #endregion

    public GameObject buildEffect;
    public GameObject sellEffect;
    private TurretBlueprint turretToBuild;
    private Node currentTurret;
    private Node selectedNode;
    public NodeUI nodeUI;

    public bool canBuild { get { return turretToBuild != null; } }
    public bool hasMoney { get { return PlayerStats.money >= turretToBuild.cost; } }

    public void SelectTurretToBuild(TurretBlueprint turret){
        turretToBuild = turret;

        DeselectNode();
    }
    public TurretBlueprint getTurretToBuild(){
        return turretToBuild;
    }    
    public void SelectNode(Node node){
        if(node == selectedNode){
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node, node.turret.ToString());
    }

    public void DeselectNode(){
        selectedNode = null;
        nodeUI.Hide();
    }
}

