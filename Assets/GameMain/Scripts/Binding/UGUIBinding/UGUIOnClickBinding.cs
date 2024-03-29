using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace StarForce
{
    [RequireComponent(typeof(Button))]
    public class UGUIOnClickBinding : ActionBinding,IPointerUpHandler
    {
        private float m_LastClickTime;

        [SerializeField] private bool m_Block;

        [SerializeField] private float m_ClickInterval = 0.5f;

        [SerializeField, InspectorReadOnly] private Button m_Button;

        public bool Block
        {
            get { return m_Block; }
            set { m_Block = value; }
        }

        public float ClickInterval
        {
            get { return m_ClickInterval; }
            set { m_ClickInterval = value; }
        }

        public Button Button
        {
            get { return m_Button; }
            set { m_Button = value; }
        }

        protected override bool Bind()
        {
            if (base.Bind())
            {
//                m_Button.onClick.AddListener(OnClick);
                return true;
            }

            return false;
        }

        protected override void Unbind()
        {
//            m_Button.onClick.RemoveListener(OnClick);
            base.Unbind();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (m_Action != null && !Block&& m_Button.interactable)
            {
                var interval = Time.realtimeSinceStartup - m_LastClickTime;
                if (interval < m_ClickInterval) return;
                m_LastClickTime = Time.realtimeSinceStartup;
                m_Action.Invoke();
                GameEntry.Sound.PlayUISound(6);
            }
        }
        
        

        protected override void OnEditorValue()
        {
            base.OnEditorValue();
            if (m_Button == null)
            {
                m_Button = GetComponent<Button>();
            }
        }

    }
}