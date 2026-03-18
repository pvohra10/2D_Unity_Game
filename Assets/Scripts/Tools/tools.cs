using UnityEngine;
using System.Collections;



namespace Utilities
{
    public static class tools
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public static bool isFacingLeft(Vector3 scaleData)
        {
            if (scaleData.x > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static void playerOptions(string option1, string option2){


            return;


        }


    }
}
