using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class MenuPageModel : UGuiFormModel<MenuPage, MenuPageModel>
    {
        #region PlayerIdProperty

        private readonly Property<int> _privatePlayerIdProperty = new Property<int>();

        public Property<int> PlayerIdProperty
        {
            get { return _privatePlayerIdProperty; }
        }

        public int PlayerId
        {
            get { return _privatePlayerIdProperty.GetValue(); }
            set { _privatePlayerIdProperty.SetValue(value); }
        }

        #endregion

        #region PlayerPosProperty

        private readonly Property<Vector3> _privatePlayerPosProperty = new Property<Vector3>();

        public Property<Vector3> PlayerPosProperty
        {
            get { return _privatePlayerPosProperty; }
        }

        public Vector3 PlayerPos
        {
            get { return _privatePlayerPosProperty.GetValue(); }
            set { _privatePlayerPosProperty.SetValue(value); }
        }

        #endregion

        public void StartGame()
        {
            Page.StartGame();
        }

        public void NextPlayer()
        {
            PlayerId += 1;
        }

        public void LastPlayer()
        {
            PlayerId -= 1;
        }
    }

    public class MenuPage : UGuiFormPage<MenuPage, MenuPageModel>
    {
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            Model.PlayerId = 1;
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }

        public void StartGame()
        {
            GameEntry.Event.Fire(this, ReferencePool.Acquire<StartGameEventArgs>().Fill());
        }
    }
}