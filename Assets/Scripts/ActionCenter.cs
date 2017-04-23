using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionCenter : MonoBehaviour {

    public bool actionIsHappening = false;
    public Darken dark;
    public TextPanel.TextGenerator generator;

    public bool Herbalist = false;
    public int HerbAmmount = 0;
    public int BandageAmmount = 0;
    public int LogAmmount = 6;

    public IObject.HeroFriend heroFriend;
    public IObject.Bonfire bonfire;
    
    

    public IEnumerator CallAction(Action sentInAction)
    {
        actionIsHappening = true;
        dark.Make(1);
        while (dark.hasStarted)
        {
            yield return new WaitForSeconds(0.1f);
        }

        sentInAction();

        dark.Make(0);
        actionIsHappening = false;
    }

    public IEnumerator CallAction(Action firstAction, Action secondAction)
    {
        actionIsHappening = true;
        dark.Make(1);
        while (dark.hasStarted)
        {
            yield return new WaitForSeconds(0.1f);
        }

        firstAction();

        dark.Make(0);
        Debug.Log("firstAction");
        yield return new WaitForSeconds(1f);
        Debug.Log("secondAction");
        dark.Make(1);
        while (dark.hasStarted)
        {
            yield return new WaitForSeconds(0.1f);
        }

        secondAction();
        
        dark.Make(0);
        actionIsHappening = false;
    }

    public IEnumerator CallAction(Action firstAction, Action secondAction, Action thirdAction)
    {
        actionIsHappening = true;
        dark.Make(1);
        while (dark.hasStarted)
        {
            yield return new WaitForSeconds(0.1f);
        }

        firstAction();

        dark.Make(0);
        
        StartCoroutine(Searching(secondAction, thirdAction));

        
        actionIsHappening = false;

    }

    IEnumerator Searching(Action secondAction, Action thirdAction)
    {
        yield return new WaitForSeconds(1f);
        dark.Make(1);
        while (dark.hasStarted)
        {
            yield return new WaitForSeconds(0.1f);
        }

        secondAction();

        dark.Make(0);

        while (heroFriend.isOutside)
        {
            yield return new WaitForSeconds(0.1f);
        }

        dark.Make(1);
        while (dark.hasStarted)
        {
            yield return new WaitForSeconds(0.1f);
        }

        thirdAction();

        dark.Make(0);
    }

    public IEnumerator Converse(Action convoStart, Action convoEnd, string[] lines)
    {

        WaitForSeconds darkenTime = new WaitForSeconds(1f);
        actionIsHappening = true;

        dark.Make(1);

        yield return darkenTime;

        convoStart();

        dark.Make(0);

        StartCoroutine(Converse(convoEnd, lines));

    }

    IEnumerator Converse(Action convoEnd, string[] lines)
    {



        generator.NewMessage(lines);


        while (generator.messageQueue.Count > 0 || generator.currentlyAnimatingText) 
        {
            yield return new WaitForSeconds(0.1f);
        }
        
        

        dark.Make(1);
        while (dark.hasStarted)
        {
            yield return new WaitForSeconds(0.1f);
        }

        convoEnd();

        dark.Make(0);
        while (dark.hasStarted)
        {
            yield return new WaitForSeconds(0.1f);
        }

        actionIsHappening = false;
    }

}
