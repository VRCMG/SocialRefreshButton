using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnhollowerBaseLib;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace SocialRefreshButton
{
    public class Main : MelonMod
    {
        public static bool SocialRefreshButtonHasBeenMade = false;

        public override void OnUpdate()
        {
            if (!SocialRefreshButtonHasBeenMade)
            {
                SetupRefreshButton();
            }
        }

        public static void Popup(string Title, string Content)
        {
            Resources.FindObjectsOfTypeAll<VRCUiPopupManager>()[0].Method_Public_Void_String_String_Single_0(Title, Content, 999f);
        }

        public static float LastRefreshTime = 0f;

        private static void SetupRefreshButton()
        {
            GameObject gameObject = GameObject.Find("Screens/Social/Current Status/StatusButton");
            if (gameObject != null)
            {
                GameObject gameObject2 = Object.Instantiate(gameObject.transform.GetComponentInChildren<Button>().gameObject);
                gameObject2.gameObject.name = "SocialRefreshButton";
                gameObject2.transform.localPosition += new Vector3(250f, 0f, 0f);
                gameObject2.GetComponentInChildren<Text>().text = "Refresh";
                gameObject2.GetComponentInChildren<Button>().onClick = new Button.ButtonClickedEvent();
                gameObject2.GetComponentInChildren<Button>().onClick.AddListener((System.Action)delegate
                {
                    if (Time.time > LastRefreshTime)
                    {
                        LastRefreshTime = Time.time + 30f;
                        Il2CppArrayBase<UiUserList> il2CppArrayBase = Resources.FindObjectsOfTypeAll<UiUserList>();
                        if (il2CppArrayBase != null)
                        {
                            for (int i = 0; i < il2CppArrayBase.Count; i++)
                            {
                                UiUserList uiUserList = il2CppArrayBase[i];
                                if (!(uiUserList == null))
                                {
                                    uiUserList.Method_Public_Void_1();
                                    uiUserList.Method_Public_Void_0();
                                    uiUserList.Method_Public_Void_3();
                                }
                            }
                        }
                    }
                    else
                    {
                        float num = LastRefreshTime - Time.time;
                        Popup("API Call Is On Cooldown!", "Please Wait " + Math.Floor(num) + " Seconds Before Trying Again!");
                    }
                });

                GameObject gameObject3 = GameObject.Find("Screens/Social/Current Status");
                gameObject2.transform.SetParent(gameObject3.transform, worldPositionStays: false);
                GameObject.Find("Screens/Social/Current Status/StatusText").GetComponent<Text>().transform.localPosition += new Vector3(250f, 0f, 0f);
                GameObject.Find("Screens/Social/Current Status/StatusIcon").transform.localPosition += new Vector3(250f, 0f, 0f);

                SocialRefreshButtonHasBeenMade = true;
            }
        }
    }
}
