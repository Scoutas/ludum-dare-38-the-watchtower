using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IObject
{
    public class Bonfire : InteractableObject
    {
        public enum BonfireState { Empty, Unlit, Lit };
        public BonfireState state = BonfireState.Empty;
        public AudioSource theGreatSoundOfFire;
        

        public Renderer bonfireRenderer;
        public Material bonfireNoLogs;
        public Material bonfireLogs1;
        public Material bonfireLogs2;
        public Material bonfireLogs3;

        public GameObject smallFire;
        public GameObject mediumFire;
        public GameObject largeFire;
        

        public override void SetupButtons(GameObject prefab, Transform parent)
        {
            GenerateButton(prefab, parent, "Examine", Examine);
            GenerateButton(prefab, parent, "Add Logs", AddLog);
            GenerateButton(prefab, parent, "Light Bonfire", LightBonfire);

        }

        void Examine()
        {
            mouseController.ManualUnselect();
            switch (state)
            {
                case BonfireState.Empty:
                    generator.NewMessage("Bonfire is empty.");
                    break;
                case BonfireState.Unlit:
                    generator.NewMessage("Bonfire has some logs.");
                    break;
                case BonfireState.Lit:
                    generator.NewMessage("Bonfire is lit.");
                    break;
            }
        }

        void AddLog()
        {
            mouseController.ManualUnselect();
            if (actionCenter.heroFriend.isOutside)
            {
                generator.NewMessage(actionCenter.heroFriend.objectName + " is outside and can't add logs to the fire.");
                return;
            }

            if(actionCenter.LogAmmount < 2)
            {
                generator.NewMessage("Borni: We need more logs.");
                return;
            }
            actionCenter.LogAmmount -= 2;
            StartCoroutine(actionCenter.CallAction(MoveHeroCloseAction, AddLogAction));
        }

        void MoveHeroCloseAction()
        {
            actionCenter.heroFriend.defaultPosition.SetActive(false);
            actionCenter.heroFriend.kindlingPosition.SetActive(true);

        }

        void MoveHeroAway()
        {
            actionCenter.heroFriend.defaultPosition.SetActive(true);
            actionCenter.heroFriend.kindlingPosition.SetActive(false);
        }

        void AddLogAction()
        {
            if(bonfireRenderer.sharedMaterial == bonfireLogs3)
            {
                MoveHeroAway();
                generator.NewMessage("The bonfire is full.");
                return;
            }

            if (bonfireRenderer.sharedMaterial == bonfireNoLogs)
            {

                state = BonfireState.Unlit;
                bonfireRenderer.sharedMaterial = bonfireLogs1;
                MoveHeroAway();
                generator.NewMessage(actionCenter.heroFriend.objectName + " adds logs to the bonfire.");
                
            }

            else if (bonfireRenderer.sharedMaterial == bonfireLogs1)
            {
                bonfireRenderer.sharedMaterial = bonfireLogs2;
                MoveHeroAway();
                generator.NewMessage(actionCenter.heroFriend.objectName + " adds logs to the bonfire.");
                
            }

            else if(bonfireRenderer.sharedMaterial == bonfireLogs2)
            {
                bonfireRenderer.sharedMaterial = bonfireLogs3;
                MoveHeroAway();
                generator.NewMessage(actionCenter.heroFriend.objectName + " adds logs to the bonfire.");
                
            }
            if (state == BonfireState.Lit)
            {
                CheckLogLevel();
            }

            


        }

        void LightBonfire()
        {
            mouseController.ManualUnselect();

            if (state == BonfireState.Lit)
            {
                generator.NewMessage("The bonfire is already lit");
                return;
            }

            if (actionCenter.heroFriend.isOutside)
            {
                generator.NewMessage(actionCenter.heroFriend.objectName + " is outside and can't add light the bonfire.");
                return;
            }

            StartCoroutine(actionCenter.CallAction(MoveHeroCloseAction, LightBonfireAction));
        }

        void LightBonfireAction()
        {
            if (bonfireRenderer.sharedMaterial == bonfireNoLogs)
            {
                MoveHeroAway();
                generator.NewMessage("There are no logs in the bonfire to light.");
            }
            else if (state == BonfireState.Unlit)
            {
                MoveHeroAway();
                CheckLogLevel();
                generator.NewMessage(actionCenter.heroFriend.objectName + " lights the bonfire");
                theGreatSoundOfFire.Play();
            }
            
        }

        void CheckLogLevel()
        {
            
            if (bonfireRenderer.sharedMaterial == bonfireLogs1)
            {
                state = BonfireState.Lit;
                smallFire.SetActive(true);
                mediumFire.SetActive(false);
                largeFire.SetActive(false);

            }

            if (bonfireRenderer.sharedMaterial == bonfireLogs2)
            {
                state = BonfireState.Lit;
                smallFire.SetActive(false);
                mediumFire.SetActive(true);
                largeFire.SetActive(false);

            }

            if (bonfireRenderer.sharedMaterial == bonfireLogs3)
            {
                state = BonfireState.Lit;
                smallFire.SetActive(false);
                mediumFire.SetActive(false);
                largeFire.SetActive(true);

            }

        }










    }
}