using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public abstract class MapGrid : Entity
    {
        private MapGridData m_MapGridData;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_MapGridData = (MapGridData) userData;
            if (m_MapGridData == null)
            {
                Log.Info("Data is invail");
                return;
            }
        }

        public void BeJump(Player player)
        {
            m_MapGridData.Player = player;
            GameEntry.Event.Fire(this, ReferencePool.Acquire<AddScoreEventArgs>().Fill(m_MapGridData.m_Score));
            if (Id % 6 >= 3)
            {
                GameEntry.Event.Fire(this, ReferencePool.Acquire<ShowNewMapEventArgs>().Fill(Id));
            }
        }

        public void Leave()
        {
            m_MapGridData.Player = null;
        }
    }
}