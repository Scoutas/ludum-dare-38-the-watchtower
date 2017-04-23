using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IObject
{
    public class Herbalist : InteractableObject
    {

        public GameObject herbalistDefault;
        public GameObject herbalistHeal;
        public GameObject exit;
        bool isHealed = false;

        public override void SetupButtons(GameObject prefab, Transform parent)
        {
            GenerateButton(prefab, parent, "Heal", Heal);
        }

        void Heal()
        {
            mouseController.ManualUnselect();
            if (isHealed)
            {
                generator.NewMessage("You have already been treated.");
            }
            if (actionCenter.HerbAmmount > 5 && actionCenter.BandageAmmount > 3)
            {
                actionCenter.HerbAmmount -= 6;
                actionCenter.BandageAmmount -= 4;
                isHealed = true;
                StartCoroutine(actionCenter.CallAction(HealAction));
            }
            else
            {
                generator.NewMessage("You don't have enough resources to heal your wounds.");
                generator.NewMessage("You need 6 herbs and 4 bandages to do so.");
            }
        }

        void HealAction()
        {

            generator.NewMessage("Herbalist prepares to treat you.");
			StartCoroutine(BackToSpot());
			
        }

        IEnumerator BackToSpot()
        {
            yield return new WaitForSeconds(1f);
            actionCenter.dark.Make(1);
            yield return new WaitForSeconds(3f);

            generator.NewMessage("You've been patched up. Now you can leave!");
            exit.SetActive(true);
            actionCenter.dark.Make(0);
        }

    }
}