using System;

using UnityEngine;
using UnityEngine.InputSystem;

using MatchTile.Utils;

namespace MatchTile.Manager
{
    [DefaultExecutionOrder(-30)]
    public class InputManager : SingletonBase<InputManager>
    {
        public Action<Vector2> onLeftClick;
        public Action<Vector2> onRightClick;
        public Action<Vector2> onTouch;
        public Action onIncreaseTileTypePush;
        public Action onDecreaseTileTypePush;
        public Action onIncreaseLayerPush;
        public Action onDecreaseLayerPush;

        private TouchControls touchControls;
        private MouseControls mouseControls;
        private KeyboardControls keyboardControls;

        private void Awake()
        {
            touchControls = new TouchControls();
            mouseControls = new MouseControls();
            keyboardControls = new KeyboardControls();

            touchControls.Touch.PrimaryTouch.performed += ctx => onTouch?.Invoke(touchControls.Touch.PrimaryTouch.ReadValue<Vector2>());
            mouseControls.Mouse.LeftClick.started += ctx => onLeftClick?.Invoke(Mouse.current.position.ReadValue());

            if (GameManager.Instance.editorMode)
            {
                mouseControls.Mouse.RightClick.performed += ctx => onRightClick?.Invoke(Mouse.current.position.ReadValue());
                keyboardControls.Keyboard.IncreaseTileType.performed += ctx => onIncreaseTileTypePush?.Invoke();
                keyboardControls.Keyboard.DecreaseTileType.performed += ctx => onDecreaseTileTypePush?.Invoke();
                keyboardControls.Keyboard.IncreaseLayer.performed += ctx => onIncreaseLayerPush?.Invoke();
                keyboardControls.Keyboard.DecreaseLayer.performed += ctx => onDecreaseLayerPush?.Invoke();
            }
        }

        private void OnEnable()
        {
            touchControls.Enable();
            mouseControls.Enable();
            keyboardControls.Enable();
        }

        private void OnDisable()
        {
            touchControls.Disable();
            mouseControls.Disable();
            keyboardControls.Disable();
        }
    }
}
