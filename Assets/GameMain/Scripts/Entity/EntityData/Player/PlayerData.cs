using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{
    [SelectionBase]
    public class PlayerData : EntityData
    {
        private float m_SpeedY = 0;
        private float m_SpeedX = 0;
        private bool m_Stop;
        private bool m_Left;
        private bool m_Die;

        public float SpeedY
        {
            get { return m_SpeedY; }
            set { m_SpeedY = value; }
        }

        public float SpeedX
        {
            get { return m_SpeedX; }
            set { m_SpeedX = value; }
        }

        public bool Stop
        {
            get { return m_Stop; }
            set { m_Stop = value; }
        }

        public bool Left
        {
            get { return m_Left; }
            set { m_Left = value; }
        }

        public bool Die
        {
            get { return m_Die; }
            set { m_Die = value; }
        }

        public PlayerData(int entityId, int typeId) : base(entityId, typeId)
        {
        }
    }

}

