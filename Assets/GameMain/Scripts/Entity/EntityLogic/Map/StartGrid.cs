using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class StartGrid : MapGrid
    {
        private StartGridData m_StaticGridData;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_StaticGridData = (StartGridData) userData;
            if (m_StaticGridData == null)
            {
                Log.Info("Data is invail");
               
                return;
            }
        }
    }
}