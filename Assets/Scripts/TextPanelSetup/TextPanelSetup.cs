using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameInfo;

namespace TextPanelSetup
{
    public class TextPanelSetup : MonoBehaviour
    {

        RectTransform panelTransform;
        public RectTransform textPanelTransform;
        public GameObject textPrefab;

        float panelWidth;
        float panelHeight;

        int horizontalPadding;
        int verticalPadding;
        float verticalSpacing;

        float textWidth;
        float textHeight;



        void Awake()
        {
            CalculateInformation();
            PanelSetup();
            PaddingSetup();
            TextSetup();
        }

        void CalculateInformation()
        {
            panelWidth = Screen.width * Constants.TextPanelPortionOfScreen;
            panelHeight = Screen.height;

            horizontalPadding = Mathf.FloorToInt(Screen.width * Constants.leftPaddingPercent);
            verticalPadding = Mathf.FloorToInt(Screen.height * Constants.topPaddingPercent);
            verticalSpacing = panelHeight * Constants.spacingPercent;

            textWidth = panelWidth - (horizontalPadding * 2);
            textHeight = ((panelHeight - (verticalPadding * 2)) - (verticalSpacing * (Constants.maxMessages - 2))) / (Constants.maxMessages - 1);
        }

        void PanelSetup()
        {
            panelTransform = GetComponent<RectTransform>();

            panelTransform.anchorMax = Vector2.zero;
            panelTransform.anchorMin = Vector2.zero;

            panelTransform.sizeDelta = new Vector2(panelWidth, panelHeight);

            textPanelTransform.sizeDelta = new Vector2(panelWidth, 1);
        }

        void PaddingSetup()
        {
            VerticalLayoutGroup group = textPanelTransform.GetComponent<VerticalLayoutGroup>();

            group.padding = new RectOffset(horizontalPadding, 0, verticalPadding, 0);
            group.spacing = verticalSpacing;
        }

        void TextSetup()
        {
            RectTransform textTransform = textPrefab.GetComponent<RectTransform>();
            textTransform.sizeDelta = new Vector2(textWidth, textHeight);
        }

    }
}