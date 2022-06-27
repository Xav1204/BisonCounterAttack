using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public bool buildAble;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Color startColor;
    private Renderer rend;

    private BuildManager buildManager;

    private void Start(){
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition(){
        return transform.position + positionOffset;
    }

    public void sellTurret(){
        if(isUpgraded){
            PlayerStats.money += turretBlueprint.sellUpgradeCost;
        }
        else{
            PlayerStats.money += turretBlueprint.sellCost;
        }

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 1f);

        Destroy(turret);
        turretBlueprint = null;
        isUpgraded = false;
    }
    
    public void upgradeTurret()
    {
        if(isUpgraded){
            return;
        }

        if(PlayerStats.money < turretBlueprint.upgradeCost){
            return;
        }

        PlayerStats.money -= turretBlueprint.upgradeCost;

        Destroy(turret);

        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 1f);

        isUpgraded = true;
    }

    private void buildTurret(TurretBlueprint blueprint){
        if(PlayerStats.money < blueprint.cost){
            return;
        }

        PlayerStats.money -= blueprint.cost;

        turretBlueprint = blueprint;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 1f);
    }

    private void OnMouseDown(){
        if(EventSystem.current.IsPointerOverGameObject()){
            return;
        }

        if(turret != null){
            buildManager.SelectNode(this);
            return;
        }

        if(!buildManager.canBuild){
            return;
        }

        if(buildAble){
            buildTurret(buildManager.getTurretToBuild());
        }
        
    }
    private void OnMouseEnter(){
        if(EventSystem.current.IsPointerOverGameObject()){
            return;
        }

        if(!buildManager.canBuild){
            return;
        }

        if(buildManager.hasMoney){
            rend.material.color = hoverColor;
        }
        else{
            rend.material.color = notEnoughMoneyColor;
        }
        
    }

    private void OnMouseExit(){
        rend.material.color = startColor;
    }
}
