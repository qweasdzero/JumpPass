using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{
    [SelectionBase]
    public class MapGridData : EntityData
    {
        public virtual int m_Score
        {
            get { return 20; }
        }
        
        private Player m_Player;

        public Player Player
        {
            get { return m_Player; }
            set { m_Player = value; }
        }


        
        public MapGridData(int entityId, int typeId) : base(entityId, typeId)
        {
            
        }
    }
}