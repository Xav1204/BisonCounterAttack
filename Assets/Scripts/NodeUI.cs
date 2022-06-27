using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    private Node target;

    public Text upgradeText;
    public Text sellText;

    public Button upgradeButton;

    public void SetTarget(Node _target, string turret){
        target = _target;

        transform.position = target.GetBuildPosition();

        if(!target.isUpgraded){
            upgradeButton.interactable = true;
            if(turret == "Turret(Clone) (UnityEngine.GameObject)"){
                upgradeText.text = "<b>Upgrade</b> \n -$" + target.turretBlueprint.upgradeCost;
                sellText.text = "<b>Sell</b> \n +$50";
            }
            else if(turret == "MissileLauncher(Clone) (UnityEngine.GameObject)"){
                upgradeText.text = "<b>Upgrade</b> \n -$" + target.turretBlueprint.upgradeCost;
                sellText.text = "<b>Sell</b> \n +$" + target.turretBlueprint.sellCost;
            }
            else if(turret == "LaserBeamer(Clone) (UnityEngine.GameObject)"){
                upgradeText.text = "<b>Upgrade</b> \n -$" + target.turretBlueprint.upgradeCost;
                sellText.text = "<b>Sell</b> \n +$" + target.turretBlueprint.sellCost;
            }
        }
        else{
            upgradeText.text = "Déjà améliorée";
            upgradeButton.interactable = false;

            if(turret == "Turret_Upgrade(Clone) (UnityEngine.GameObject)"){
                sellText.text = "<b>Sell</b> \n +$" + target.turretBlueprint.sellUpgradeCost;
            }
            else if(turret == "MissileLauncher_Upgrade(Clone) (UnityEngine.GameObject)"){
                sellText.text = "<b>Sell</b> \n +$" + target.turretBlueprint.sellUpgradeCost;
            }
            else if(turret == "LaserBeamer_Upgrade(Clone) (UnityEngine.GameObject)"){
                sellText.text = "<b>Sell</b> \n +$" + target.turretBlueprint.sellUpgradeCost;
            }
        }
        ui.SetActive(true);

    }
    public void Hide(){
        ui.SetActive(false);
    }
    public void Upgrade(){
        target.upgradeTurret();
        BuildManager.instance.DeselectNode();
    }
    public void Sell(){
        target.sellTurret();
        BuildManager.instance.DeselectNode();
    }
}
