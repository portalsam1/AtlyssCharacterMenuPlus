using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace AtlyssCharacterMenuPlus
{
    [HarmonyPatch(typeof(VanityMirrorManager), "Awake")] [SuppressMessage("ReSharper", "UnusedMember.Local")] [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class VanityMirrorManagerPatch
    {
        
        private static GameObject? referenceText, referenceLeftButton, referenceRightButton;
        private static Slider? headWidthSlider, modifySlider, voicePitchSlider, heightSlider, widthSlider, chestSlider, armsSlider, bellySlider, bottomSlider;

        private static void Postfix()
        {
            
            referenceText = GameObject.Find("_GameUI_InGame/Canvas_DialogSystem/_dolly_vanityMirrorBox/_customizer_head/_dolly_headCustomizer/_characterSlider_headWidth/_tag");
            referenceLeftButton = GameObject.Find("_GameUI_InGame/Canvas_DialogSystem/_dolly_vanityMirrorBox/_customizer_head/_dolly_headCustomizer/_characterButtons_hairStyle/_button_leftSelect");
            referenceRightButton = GameObject.Find("_GameUI_InGame/Canvas_DialogSystem/_dolly_vanityMirrorBox/_customizer_head/_dolly_headCustomizer/_characterButtons_hairStyle/_button_rightSelect");

            headWidthSlider = GameObject.Find("_GameUI_InGame/Canvas_DialogSystem/_dolly_vanityMirrorBox/_customizer_head/_dolly_headCustomizer/_characterSlider_headWidth/Slider").GetComponent<Slider>();
            modifySlider = GameObject.Find("_GameUI_InGame/Canvas_DialogSystem/_dolly_vanityMirrorBox/_customizer_head/_dolly_headCustomizer/_characterSlider_headMod/Slider").GetComponent<Slider>();
            voicePitchSlider = GameObject.Find("_GameUI_InGame/Canvas_DialogSystem/_dolly_vanityMirrorBox/_customizer_head/_dolly_headCustomizer/_characterSlider_voicePitch/Slider").GetComponent<Slider>();
            heightSlider = GameObject.Find("_GameUI_InGame/Canvas_DialogSystem/_dolly_vanityMirrorBox/_customizer_body/_dolly_bodyCustomizer/_characterSlider_height/Slider_height").GetComponent<Slider>();
            widthSlider = GameObject.Find("_GameUI_InGame/Canvas_DialogSystem/_dolly_vanityMirrorBox/_customizer_body/_dolly_bodyCustomizer/_characterSlider_width/Slider_width").GetComponent<Slider>();
            chestSlider = GameObject.Find("_GameUI_InGame/Canvas_DialogSystem/_dolly_vanityMirrorBox/_customizer_body/_dolly_bodyCustomizer/_characterSlider_chest/Slider_chest").GetComponent<Slider>();
            armsSlider = GameObject.Find("_GameUI_InGame/Canvas_DialogSystem/_dolly_vanityMirrorBox/_customizer_body/_dolly_bodyCustomizer/_characterSlider_arms/Slider_arms").GetComponent<Slider>();
            bellySlider = GameObject.Find("_GameUI_InGame/Canvas_DialogSystem/_dolly_vanityMirrorBox/_customizer_body/_dolly_bodyCustomizer/_characterSlider_belly/Slider_belly").GetComponent<Slider>();
            bottomSlider = GameObject.Find("_GameUI_InGame/Canvas_DialogSystem/_dolly_vanityMirrorBox/_customizer_body/_dolly_bodyCustomizer/_characterSlider_bottom/Slider_bottom").GetComponent<Slider>();
            
            SetupSliderTextAndButtons(headWidthSlider);
            SetupSliderTextAndButtons(modifySlider);
            SetupSliderTextAndButtons(voicePitchSlider);
            SetupSliderTextAndButtons(heightSlider);
            SetupSliderTextAndButtons(widthSlider);
            SetupSliderTextAndButtons(chestSlider);
            SetupSliderTextAndButtons(armsSlider);
            SetupSliderTextAndButtons(bellySlider);
            SetupSliderTextAndButtons(bottomSlider);
            
            CharacterMenuPlus.Logger.LogInfo("Vanity Mirror Menu has been patched.");

        }
        
        private static void SetupSliderTextAndButtons(Slider slider)
        { 
            
            GameObject valueText = Object.Instantiate(referenceText!, slider.transform.parent, true);

            valueText.transform.localScale = Vector3.one;
            valueText.transform.localPosition = new Vector3(20f, -10f, 0f);

            Text sliderText = valueText.GetComponent<Text>();
            sliderText.fontSize = 14;
            sliderText.text = Configuration.UsePercentages.Value ? Math.Round((slider.value - slider.minValue) / (slider.maxValue - slider.minValue) * 100) + "%" : (Math.Truncate(slider.value * 100) / 100).ToString(CultureInfo.InvariantCulture);

            slider.onValueChanged.AddListener(sliderAction);

             GameObject leftSliderButton = Object.Instantiate(referenceLeftButton!, slider.transform.parent, true);

             leftSliderButton.transform.localScale = new Vector3(0.375f, 0.375f, 0.375f);
             leftSliderButton.transform.localPosition = new Vector3(85f, -20f, 0f);

             Button leftSliderButtonComponent = leftSliderButton.GetComponent<Button>();
             leftSliderButtonComponent.onClick = new Button.ButtonClickedEvent();
             leftSliderButtonComponent.onClick.AddListener(delegate { slider.value += slider.wholeNumbers ? 1f : (slider.maxValue - slider.minValue) * 0.0025f; });

             GameObject rightSliderButton = Object.Instantiate(referenceRightButton!, slider.transform.parent, true);

             rightSliderButton.transform.localScale = new Vector3(0.375f, 0.375f, 0.375f);
             rightSliderButton.transform.localPosition = new Vector3(70f, -20f, 0f);

             Button rightSliderButtonComponent = rightSliderButton.GetComponent<Button>();
             rightSliderButtonComponent.onClick = new Button.ButtonClickedEvent();
             rightSliderButtonComponent.onClick.AddListener(delegate { slider.value -= slider.wholeNumbers ? 1f : (slider.maxValue - slider.minValue) * 0.0025f; });
             
             return;

             // Function inside a function, fuck you.
             void sliderAction(float value) { sliderText.text = Configuration.UsePercentages.Value ? Math.Round((slider.value - slider.minValue) / (slider.maxValue - slider.minValue) * 100) + "%" : (Math.Truncate(slider.value * 100) / 100).ToString(CultureInfo.InvariantCulture); }
             
        }
        
    }
}
