using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IObject {

    public class HeroFriend : InteractableObject {

        public GameObject defaultPosition;
        public GameObject exitingPosition;
        public GameObject conversingPosition;
        public GameObject kindlingPosition;
        public GameObject herbalistPosition;

        int conversationCount = 0;

        public bool isOutside = false;

        //Bag

        int herbs = 0;
        int bandages = 0;
        int logs = 0;
        bool foundHerbalist = false;

        // Status

        float wet = 0;

        void Update()
        {
            if(isOutside == false & actionCenter.bonfire.state == Bonfire.BonfireState.Lit)
            {
                if (wet > 0)
                {
                    

                    

                    if (actionCenter.bonfire.bonfireRenderer.sharedMaterial == actionCenter.bonfire.bonfireLogs1)
                    {
                        wet -= 2f * Time.deltaTime;
                    }

                    else if (actionCenter.bonfire.bonfireRenderer.sharedMaterial == actionCenter.bonfire.bonfireLogs2)
                    {
                        wet -= 4f * Time.deltaTime;
                    }
                    else if (actionCenter.bonfire.bonfireRenderer.sharedMaterial == actionCenter.bonfire.bonfireLogs3)
                    {
                        wet -= 8f * Time.deltaTime;
                    }
                    
                }
                
                else if (wet < 0)
                {
                    wet = 0;
                    generator.NewMessage("Borni: I'm completely dry. I'm ready to go search for resources.");
                }
             }
        }
        
        public override void SetupButtons(GameObject prefab, Transform parent)
        {
            GenerateButton(prefab, parent, "Examine", Examine);
            GenerateButton(prefab, parent, "Converse", Talk);
            GenerateButton(prefab, parent, "Explore", Search);
            GenerateButton(prefab, parent, "Resources", CurrentResources);
        }

        // Examine
        // Talk
        // Search for resources
        void CurrentResources()
        {
            mouseController.ManualUnselect();
            generator.NewMessage("Borni: We have got " + actionCenter.HerbAmmount + " herbs, " + actionCenter.BandageAmmount + " bandages and " +  actionCenter.LogAmmount + " logs.");
        }

        void Examine()
        {
            mouseController.ManualUnselect();
            objectName = "Borni";
            generator.NewMessage("That's Borni, my compation in the adventure!");
        }

        void Search()
        {
            
            mouseController.ManualUnselect();
            if (wet > 0)
            {
                generator.NewMessage("Borni: I'm drenched. I won't go out until I have dried off.");
                return;
            }
            objectName = "Borni";
            StartCoroutine(actionCenter.CallAction(PrepareToExitAction, BeginSearching, ComeBack ));
        }

        void PrepareToExitAction()
        {
            generator.NewMessage(objectName + " is going out in search of resources.");
            defaultPosition.GetComponent<Collider>().enabled = false;
            defaultPosition.GetComponentInChildren<Renderer>().enabled = false;
            exitingPosition.SetActive(true);
        }

        void ComeBack()
        {
            if (actionCenter.Herbalist)
            {
                herbalistPosition.SetActive(true);
            }
            defaultPosition.GetComponent<Collider>().enabled = true;
            defaultPosition.GetComponentInChildren<Renderer>().enabled = true;
            exitingPosition.SetActive(false);
        }

        void BeginSearching()
        {
            exitingPosition.SetActive(false);
            isOutside = true;
            StartCoroutine(SearchOutside());
        }

        IEnumerator SearchOutside()
        {
            for (int i = 0; i < 15; i++)
            {
                wet += 5;
                int diceRoll = Random.Range(0, 100);
                if(diceRoll < 10)
                {
                    herbs++;
                }

                else if(diceRoll < 20)
                {
                    bandages++;
                }
                else if(diceRoll < 25)
                {
                    logs++;
                }

                else if(actionCenter.Herbalist == false && diceRoll < 30)
                {
                    foundHerbalist = true;
                }

                yield return new WaitForSeconds(0.5f);
            }
            generator.NewMessage(objectName + " came back. She got " + herbs + " herbs, " + bandages + " bandages and " + logs + " logs.");
            if(foundHerbalist == true)
            {
                generator.NewMessage("She also has found a herbalist.");
            }
            EmptyBag();
            isOutside = false;
            
        }

        void EmptyBag()
        {
            if (actionCenter.Herbalist == false)
            {
                actionCenter.Herbalist = foundHerbalist;
            }
            foundHerbalist = false;
            

            actionCenter.HerbAmmount += herbs;
            herbs = 0;

            actionCenter.BandageAmmount += bandages;
            bandages = 0;

            actionCenter.LogAmmount += logs;
            logs = 0;
        }

        void Talk()
        {
            
            objectName = "Borni";
            mouseController.ManualUnselect();

            if (conversationCount > 1)
            {
                generator.NewMessage("Your friend is resting and doesn't want to converse.");
                return;
            }
            generator.NewMessage(GetConversation());


            //StartCoroutine(actionCenter.Converse(StartConversation, EndConversation, ));
        }

        void StartConversation()
        {
            defaultPosition.SetActive(false);
            conversingPosition.SetActive(true);



        }

        void EndConversation()
        {
            defaultPosition.SetActive(true);
            conversingPosition.SetActive(false);
        }

        string[] GetConversation()
        {
            conversationCount++;
            if (conversationCount == 1)
            {
                return new string[] { "Borni: Hey there, how are you feeling?",
                    "You: I'm okay, but the leg hurts like hell.",
                    "Borni: I can bet. I'll try to find a way to patch you up." };
            }

            if (conversationCount == 2)
            {
                return new string[] { "Borni: This watchtower was a godsend. It will take a while for them to find us.",
                    "You: It could be a little warmer.",
                    "Borni: It will get warm soon enough." };
            }

            return new string[0];
        }


    }
}