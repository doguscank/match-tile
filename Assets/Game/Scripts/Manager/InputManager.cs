using System;

using UnityEngine;
using UnityEngine.InputSystem;

using MatchTile.Utils;

namespace MatchTile.Manager
{
    public class InputManager : SingletonBase<InputManager>
    {
        public Action<Vector2> onTap;

        private TouchControls touchControls;
        private MouseControls mouseControls;

        private void Awake()
        {
            touchControls = new TouchControls();
            mouseControls = new MouseControls();

            touchControls.Touch.PrimaryContact.started += ctx => onTap?.Invoke(ctx.ReadValue<Vector2>());
            mouseControls.Mouse.LeftClick.started += ctx => onTap?.Invoke(Mouse.current.position.ReadValue());
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
