#region File Description
//-----------------------------------------------------------------------------
// InputState.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// an enum of all available mouse buttons.
    /// </summary>
    public enum MouseButtons
    {
        LeftButton,
        MiddleButton,
        RightButton
    }

    /// <summary>
    /// Helper for reading input from keyboard, gamepad, and touch input. This class 
    /// tracks both the current and previous state of the input devices, and implements 
    /// query methods for high level input actions such as "move up through the menu"
    /// or "pause the game".
    /// </summary>
    public class InputState
    {
        #region Fields

        public KeyboardState CurrentKeyboardState;
        public GamePadState CurrentGamePadState;
        public MouseState CurrentMouseState;

        public KeyboardState LastKeyboardState;
        public GamePadState LastGamePadState;
        public MouseState LastMouseState;

        public bool GamePadWasConnected;

        public TouchCollection TouchState;

        public readonly List<GestureSample> Gestures = new List<GestureSample>();

        #endregion

        #region Initialization


        /// <summary>
        /// Constructs a new input state.
        /// </summary>
        public InputState()
        {
            CurrentKeyboardState = new KeyboardState();
            CurrentGamePadState = new GamePadState();

            LastKeyboardState = new KeyboardState();
            LastGamePadState = new GamePadState();

            GamePadWasConnected = false;
        }


        #endregion

        #region Public Methods


        /// <summary>
        /// Reads the latest state of the keyboard and gamepad.
        /// </summary>
        public void Update()
        {
            if (!Windows.Foundation.Metadata.ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1))
            {
                LastKeyboardState = CurrentKeyboardState;
                LastGamePadState = CurrentGamePadState;
                LastMouseState = CurrentMouseState;

                CurrentKeyboardState = Keyboard.GetState();
                CurrentGamePadState = GamePad.GetState(PlayerIndex.One);
                CurrentMouseState = Mouse.GetState();

                // Keep track of whether a gamepad has ever been
                // connected, so we can detect if it is unplugged.
                if (CurrentGamePadState.IsConnected)
                {
                    GamePadWasConnected = true;
                }
                else
                {
                    GamePadWasConnected = false;
                }
            }

            TouchState = TouchPanel.GetState();

            Gestures.Clear();
            while (TouchPanel.IsGestureAvailable)
            {
                Gestures.Add(TouchPanel.ReadGesture());
            }
        }


        /// <summary>
        /// Helper for checking if a key was newly pressed during this update. The
        /// controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When a keypress
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsNewKeyPress(Keys key, out PlayerIndex playerIndex)
        {
            // Read input from the specified player.
            playerIndex = PlayerIndex.One;

            return (CurrentKeyboardState.IsKeyDown(key) &&
                    LastKeyboardState.IsKeyUp(key));
        }


        /// <summary>
        /// Helper for checking if a button was newly pressed during this update.
        /// The controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When a button press
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsNewGamePadButtonPress(Buttons button, out PlayerIndex playerIndex)
        {
           
                // Read input from the specified player.
                playerIndex = PlayerIndex.One;

                return (CurrentGamePadState.IsButtonDown(button) &&
                        LastGamePadState.IsButtonUp(button));
            
        }

        /// <summary>
        /// Helper for checking if a button was newly pressed during this update.
        /// The controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When a button press
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsNewMouseButtonPress(MouseButtons button, out PlayerIndex playerIndex)
        {

            // Read input from the specified player.
            playerIndex = PlayerIndex.One;

            switch(button)
            {
                case MouseButtons.LeftButton:
                    return (
                        LastMouseState.LeftButton == ButtonState.Released &&
                        CurrentMouseState.LeftButton == ButtonState.Pressed);
                case MouseButtons.MiddleButton:
                    return (
                        LastMouseState.MiddleButton == ButtonState.Released &&
                        CurrentMouseState.MiddleButton == ButtonState.Pressed);
                case MouseButtons.RightButton:
                    return (
                        LastMouseState.RightButton == ButtonState.Released &&
                        CurrentMouseState.RightButton == ButtonState.Pressed);
                default:
                    return false;
            }

        }


        /// <summary>
        /// Checks for a "menu select" input action.
        /// The controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When the action
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsMenuSelect(out PlayerIndex playerIndex)
        {
            return IsNewKeyPress(Keys.Space, out playerIndex) ||
                   IsNewKeyPress(Keys.Enter, out playerIndex) ||
                   IsNewGamePadButtonPress(Buttons.A, out playerIndex) ||
                   IsNewGamePadButtonPress(Buttons.Start, out playerIndex);
        }


        /// <summary>
        /// Checks for a "menu cancel" input action.
        /// The controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When the action
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsMenuCancel(out PlayerIndex playerIndex)
        {
            return IsNewKeyPress(Keys.Escape, out playerIndex) ||
                   IsNewGamePadButtonPress(Buttons.B, out playerIndex) ||
                   IsNewGamePadButtonPress(Buttons.Back, out playerIndex);
        }


        /// <summary>
        /// Checks for a "menu up" input action.
        /// The controllingPlayer parameter specifies which player to read
        /// input for. If this is null, it will accept input from any player.
        /// </summary>
        public bool IsMenuUp()
        {
            PlayerIndex playerIndex;

            return IsNewKeyPress(Keys.Up, out playerIndex) ||
                   IsNewGamePadButtonPress(Buttons.DPadUp, out playerIndex) ||
                   IsNewGamePadButtonPress(Buttons.LeftThumbstickUp, out playerIndex);
        }


        /// <summary>
        /// Checks for a "menu down" input action.
        /// The controllingPlayer parameter specifies which player to read
        /// input for. If this is null, it will accept input from any player.
        /// </summary>
        public bool IsMenuDown()
        {
            PlayerIndex playerIndex;

            return IsNewKeyPress(Keys.Down, out playerIndex) ||
                   IsNewGamePadButtonPress(Buttons.DPadDown, out playerIndex) ||
                   IsNewGamePadButtonPress(Buttons.LeftThumbstickDown, out playerIndex);
        }


        /// <summary>
        /// Checks for a "pause the game" input action.
        /// The controllingPlayer parameter specifies which player to read
        /// input for. If this is null, it will accept input from any player.
        /// </summary>
        public bool IsPauseGame()
        {
            PlayerIndex playerIndex;

            return IsNewKeyPress(Keys.Escape, out playerIndex) ||
                   IsNewGamePadButtonPress(Buttons.Back, out playerIndex) ||
                   IsNewGamePadButtonPress(Buttons.Start, out playerIndex);
        }


        #endregion
    }
}
