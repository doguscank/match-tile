using System;

using UnityEngine;
using UnityEngine.InputSystem;

using MatchTile.Utils;

namespace MatchTile.Manager
{
    public class InputManager : SingletonBase<InputManager>
    {
        public Action<Vector2> onLeftClick;
        public Action<Vector2> onRightClick;
        public Action<KeyCode> onKeyboardKeyPressed;

        private TouchControls touchControls;
        private MouseControls mouseControls;
        private KeyboardControls keyboardControls;

        private void Awake()
        {
            touchControls = new TouchControls();
            mouseControls = new MouseControls();
            keyboardControls = new KeyboardControls();

            touchControls.Touch.PrimaryContact.started += ctx => onLeftClick?.Invoke(ctx.ReadValue<Vector2>());
            mouseControls.Mouse.LeftClick.started += ctx => onLeftClick?.Invoke(Mouse.current.position.ReadValue());
            mouseControls.Mouse.RightClick.started += ctx => onRightClick?.Invoke(Mouse.current.position.ReadValue());

#if UNITY_EDITOR
            keyboardControls.Keyboard.KeyPressed.started += ctx => onKeyboardKeyPressed?.Invoke(ctx.ReadValue<KeyCode>());
#endif
        }

        private void OnEnable()
        {
            touchControls.Enable();
            mouseControls.Enable();
        }

        private void OnDisable()
        {
            touchControls.Disable();
            mouseControls.Disable();
        }
    }
}
