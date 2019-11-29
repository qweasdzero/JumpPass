using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{
    [SelectionBase]
    public class HorizontalGridData : MapGridData
    {
        public override int m_Score
        {
            get { return 30; }
        }

        private float m_Pos;

        private bool m_Left;

        public bool Left
        {
            get { return m_Left; }
            set { m_Left = value; }
        }

        public float Pos
        {
            get { return m_Pos; }
            set { m_Pos = value; }
        }


        public HorizontalGridData(int entityId, int typeId) : base(entityId, typeId)
        {
        }
    }
}