using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrangerCade.Framework
{
    /// <summary>
    /// Huge extension of Monogame Drawing.
    /// </summary>
    /// <remarks>
    /// Not really an extension, more like a wrapper that adds a lot of features</remarks>
    class GMKeyboard
    {
        /// <summary>
        /// List of keys pressed last frame
        /// </summary>
        public List<Keys> LastPressedKeys { get; private set; }
        /// <summary>
        /// List of keys pressed current frame
        /// </summary>
        public List<Keys> PressedKeys { get; private set; }
        /// <summary>
        /// Simulated keypresses
        /// </summary>
        public List<Keys> SimulatedKeys { get; private set; }
        /// <summary>
        /// Keys that got triggered
        /// </summary>
        public List<Keys> TriggeredKeys { get; private set; }
        /// <summary>
        /// The length each key has been pressed
        /// </summary>
        public Dictionary<Keys, double> PressTime { get; private set; }

        /// <summary>
        /// The typed text. Can be cleared or even changed.
        /// </summary>
        public string String;
        
        public KeyboardSettings Settings;

        public GMKeyboard()
        {
            LastPressedKeys = new List<Keys>();
            PressedKeys = new List<Keys>();
            SimulatedKeys = new List<Keys>();
            TriggeredKeys = new List<Keys>();
            PressTime = new Dictionary<Keys, double>();
            String = "";
            Settings = new KeyboardSettings();
        }

        /// <summary>
        /// Has to be called every Update to ensure correct calling
        /// </summary>
        public void Update(GameTime gameTime)
        {
            // Shift pressed to lastPressed
            LastPressedKeys = new List<Keys>(PressedKeys);
            PressedKeys.Clear();

            KeyboardState state = Keyboard.GetState();
            PressedKeys.AddRange(state.GetPressedKeys());

            // Add simulated keys
            foreach (Keys key in SimulatedKeys)
            {
                if (!PressedKeys.Contains(key))
                {
                    PressedKeys.Add(key);
                }
            }

            // Get pressed time for gmkeyboard.string
            // Remove released keys
            foreach (Keys key in PressTime.Keys.ToList())
            {
                if (!PressedKeys.Contains(key))
                {
                    PressTime.Remove(key);
                }
            }
            // Add newly pressed keys to pressTime
            foreach (Keys key in PressedKeys)
            {
                if (!PressTime.ContainsKey(key))
                {
                    PressTime.Add(key, -gameTime.ElapsedGameTime.TotalMilliseconds);
                }
            }

            Dictionary<Keys, double> lastPressTime = new Dictionary<Keys, double>(PressTime);

            TriggeredKeys.Clear();
            // Handle KeyboardString
            foreach (Keys key in PressTime.Keys.ToList())
            {
                // Don't add key to string if it isn't allowed to
                if (Settings.AddSimulatedKeysToKeyboardString == false)
                { if (SimulatedKeys.Contains(key)) { break; } }

                PressTime[key] += gameTime.ElapsedGameTime.TotalMilliseconds;
                bool shouldFire = false;

                // Fire if key is just pressed
                if (PressTime[key] == 0)
                {
                    shouldFire = true;
                }

                // Check if it should refire because key is hold
                if (PressTime[key] >= Settings.ReFireDelay)
                {
                    int maxTime = Settings.ReFireDelay + Settings.ReFireInterval * 20;
                    if (PressTime[key] > maxTime)
                    {
                        PressTime[key] -= Settings.ReFireInterval * 20;
                        lastPressTime[key] -= Settings.ReFireInterval * 20;
                    }
                    for (int t = Settings.ReFireDelay; t < maxTime; t += Settings.ReFireInterval)
                    {
                        if (PressTime[key] > t && lastPressTime[key] < t)
                        {
                            shouldFire = true;
                        }
                    }
                }

                if (shouldFire)
                {
                    TriggeredKeys.Add(key);
                    // s = shift pressed?
                    bool s = Check(Keys.LeftShift) | Check(Keys.RightShift);
                    string keyString = key.ToString();
                    if (Alphabet.Contains(key))
                    {
                        s = state.CapsLock ? !s : s; // Invert shift if CapsLock is on
                        String += s ? keyString : keyString.ToLower();
                    }
                    else
                    {
                        bool nl = state.NumLock;
                        switch (key)
                        {
                            case Keys.Space: String += " "; break;
                            case Keys.Back: if (String.Length > 0) String = String.Substring(0, String.Length - 1); break;
                            case Keys.D1: String += s ? "!" : "1"; break;
                            case Keys.D2: String += s ? "@" : "2"; break;
                            case Keys.D3: String += s ? "#" : "3"; break;
                            case Keys.D4: String += s ? "$" : "4"; break;
                            case Keys.D5: String += s ? "%" : "5"; break;
                            case Keys.D6: String += s ? "^" : "6"; break;
                            case Keys.D7: String += s ? "&" : "7"; break;
                            case Keys.D8: String += s ? "*" : "8"; break;
                            case Keys.D9: String += s ? "(" : "9"; break;
                            case Keys.D0: String += s ? ")" : "0"; break;
                            case Keys.OemComma: String += s ? "<" : ","; break;
                            case Keys.OemPeriod: String += s ? ">" : "."; break;
                            case Keys.OemQuestion: String += s ? "?" : "/"; break;
                            case Keys.OemSemicolon: String += s ? ":" : ";"; break;
                            case Keys.OemQuotes: String += s ? "\"" : "'"; break;
                            case Keys.OemPipe: String += s ? "|" : "\\"; break;
                            case Keys.OemMinus: String += s ? "_" : "-"; break;
                            case Keys.OemPlus: String += s ? "+" : "="; break;
                            case Keys.OemOpenBrackets: String += s ? "{" : "["; break;
                            case Keys.OemCloseBrackets: String += s ? "}" : "]"; break;
                            case Keys.OemTilde: String += s ? "~" : "`"; break;
                            case Keys.NumPad0: String += nl ? "0" : ""; break;
                            case Keys.NumPad1: String += nl ? "1" : ""; break;
                            case Keys.NumPad2: String += nl ? "2" : ""; break;
                            case Keys.NumPad3: String += nl ? "3" : ""; break;
                            case Keys.NumPad4: String += nl ? "4" : ""; break;
                            case Keys.NumPad5: String += nl ? "5" : ""; break;
                            case Keys.NumPad6: String += nl ? "6" : ""; break;
                            case Keys.NumPad7: String += nl ? "7" : ""; break;
                            case Keys.NumPad8: String += nl ? "8" : ""; break;
                            case Keys.NumPad9: String += nl ? "9" : ""; break;
                            case Keys.Multiply: String += "*"; break;
                            case Keys.Divide: String += "/"; break;
                            case Keys.Add: String += "+"; break;
                            case Keys.Subtract: String += "-"; break;
                            case Keys.Decimal: String += "."; break;
                            case Keys.Enter: String += Settings.ParseEnter ? "\n" : ""; break;
                            case Keys.Tab: String += Settings.ParseTab ? "\t" : ""; break;
                            default: break;
                        }
                    }

                    // Limit Keyboard.String length
                    if (String.Length > Settings.StoreLength)
                    {
                        String = String.Substring(String.Length - Settings.StoreLength);
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a key is pressed
        /// </summary>
        public bool Check(Keys key)
        {
            return PressedKeys.Contains(key);
        }

        /// <summary>
        /// Returns true on the frame that the key was pressed. Triggered once per keystroke
        /// </summary>
        public bool CheckPressed(Keys key)
        {
            return (PressedKeys.Contains(key) && !LastPressedKeys.Contains(key));
        }

        /// <summary>
        /// Check if a key got released. Triggered once per keystroke
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool CheckReleased(Keys key)
        {
            return (!PressedKeys.Contains(key) && LastPressedKeys.Contains(key));
        }

        /// <summary>
        /// Check if a key got pressed or released. Triggered on the falling and rising edge of a keystroke.
        /// </summary>
        public bool CheckEdge(Keys key)
        {
            return (PressedKeys.Contains(key) ^ LastPressedKeys.Contains(key));
        }

        /// <summary>
        /// Checks if a key is triggered.
        /// </summary>
        /// <remarks>You know when you hold a key and after a short delay you type multiple characters? Well, this returns true when an this happens</remarks>
        public bool CheckTriggered(Keys key)
        {
            return TriggeredKeys.Contains(key); 
        }

        /// <summary>
        /// Simulate key press
        /// </summary>
        public void KeyPress(Keys key)
        {
            if (!PressedKeys.Contains(key))
            {
                PressedKeys.Add(key);
            }
            if (!SimulatedKeys.Contains(key))
            {
                SimulatedKeys.Add(key);
            }
        }

        /// <summary>
        /// Simulate key release
        /// </summary>
        public void KeyRelease(Keys key)
        {
            if (PressedKeys.Contains(key))
            {
                PressedKeys.Remove(key);
            }
            if (SimulatedKeys.Contains(key))
            {
                SimulatedKeys.Remove(key);
            }
        }

        /// <summary>
        /// Clears state of the keyboard.
        /// </summary>
        public void Clear()
        {
            PressedKeys.Clear();
            LastPressedKeys.Clear();
            SimulatedKeys.Clear();
            PressTime.Clear();
            String = "";
        }

        private Keys[] Alphabet = new Keys[] { Keys.E, Keys.T, Keys.A, Keys.O, Keys.I, Keys.N, Keys.S, Keys.H, Keys.R,
                                               Keys.D, Keys.L, Keys.C, Keys.U, Keys.M, Keys.W, Keys.F, Keys.G, Keys.Y,
                                               Keys.P, Keys.B, Keys.V, Keys.K, Keys.J, Keys.X, Keys.Q, Keys.Z };
    }

    /// <summary>
    /// All changable settings for the <see cref="GMKeyboard">GMKeyboard</see>
    /// </summary>
    public class KeyboardSettings
    {
        /// <summary>
        /// The amount of characters stored in keyboard string.
        /// </summary>
        public int StoreLength = 1024;
        /// <summary>
        /// If enters should be added to keyboard string.
        /// </summary>
        public bool ParseEnter = false;
        /// <summary>
        /// If tabs should be added to keyboard string
        /// </summary>
        public bool ParseTab = false;
        /// <summary>
        /// If simulated key presses should be added to keyboard string
        /// </summary>
        public bool AddSimulatedKeysToKeyboardString = false;
        /// <summary>
        /// The delay in ms between the initial key press and the characters that should be added after a short delay
        /// </summary>
        public int ReFireDelay = 500;
        /// <summary>
        /// The delay in ms between the keys after the ReFireDelay
        /// </summary>
        public int ReFireInterval = 50;
    }
}
