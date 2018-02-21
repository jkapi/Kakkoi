using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrangerCade.Framework.GameObjects
{
    class GMKeyboard
    {
        private List<Keys> lastPressed;
        private List<Keys> pressed;
        private List<Keys> simulated;
        private List<Keys> triggered;
        private Dictionary<Keys, double> pressTime;

        public string String;

        public KeyboardSettings Settings;

        public GMKeyboard()
        {
            lastPressed = new List<Keys>();
            pressed = new List<Keys>();
            simulated = new List<Keys>();
            triggered = new List<Keys>();
            pressTime = new Dictionary<Keys, double>();
            String = "";
            Settings = new KeyboardSettings();
        }

        public void Update(GameTime gameTime)
        {
            // Shift pressed to lastPressed
            lastPressed = new List<Keys>(pressed);
            pressed.Clear();

            KeyboardState state = Keyboard.GetState();
            pressed.AddRange(state.GetPressedKeys());

            // Add simulated keys
            foreach (Keys key in simulated)
            {
                if (!pressed.Contains(key))
                {
                    pressed.Add(key);
                }
            }

            // Get pressed time for gmkeyboard.string
            // Remove released keys
            foreach (Keys key in pressTime.Keys.ToList())
            {
                if (!pressed.Contains(key))
                {
                    pressTime.Remove(key);
                }
            }
            // Add newly pressed keys to pressTime
            foreach (Keys key in pressed)
            {
                if (!pressTime.ContainsKey(key))
                {
                    pressTime.Add(key, -gameTime.ElapsedGameTime.TotalMilliseconds);
                }
            }

            Dictionary<Keys, double> lastPressTime = new Dictionary<Keys, double>(pressTime);

            triggered.Clear();
            // Handle KeyboardString
            foreach (Keys key in pressTime.Keys.ToList())
            {
                // Don't add key to string if it isn't allowed to
                if (Settings.AddSimulatedKeysToKeyboardString == false)
                { if (simulated.Contains(key)) { break; } }

                pressTime[key] += gameTime.ElapsedGameTime.TotalMilliseconds;
                bool shouldFire = false;

                // Fire if key is just pressed
                if (pressTime[key] == 0)
                {
                    shouldFire = true;
                }

                // Check if it should refire because key is hold
                if (pressTime[key] >= Settings.ReFireDelay)
                {
                    int maxTime = Settings.ReFireDelay + Settings.ReFireInterval * 20;
                    if (pressTime[key] > maxTime)
                    {
                        pressTime[key] -= Settings.ReFireInterval * 20;
                        lastPressTime[key] -= Settings.ReFireInterval * 20;
                    }
                    for (int t = Settings.ReFireDelay; t < maxTime; t += Settings.ReFireInterval)
                    {
                        if (pressTime[key] > t && lastPressTime[key] < t)
                        {
                            shouldFire = true;
                        }
                    }
                }

                if (shouldFire)
                {
                    triggered.Add(key);
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

        public bool Check(Keys key)
        {
            return pressed.Contains(key);
        }

        public bool CheckPressed(Keys key)
        {
            return (pressed.Contains(key) && !lastPressed.Contains(key));
        }

        public bool CheckReleased(Keys key)
        {
            return (!pressed.Contains(key) && lastPressed.Contains(key));
        }

        public bool CheckEdge(Keys key)
        {
            return (pressed.Contains(key) ^ lastPressed.Contains(key));
        }

        public bool CheckTriggered(Keys key)
        {
            return triggered.Contains(key); 
        }

        public void KeyPress(Keys key)
        {
            if (!pressed.Contains(key))
            {
                pressed.Add(key);
            }
            if (!simulated.Contains(key))
            {
                simulated.Add(key);
            }
        }

        public void KeyRelease(Keys key)
        {
            if (pressed.Contains(key))
            {
                pressed.Remove(key);
            }
            if (simulated.Contains(key))
            {
                simulated.Remove(key);
            }
        }

        public void Clear()
        {
            pressed.Clear();
            lastPressed.Clear();
            simulated.Clear();
            pressTime.Clear();
            String = "";
        }

        private Keys[] Alphabet = new Keys[] { Keys.E, Keys.T, Keys.A, Keys.O, Keys.I, Keys.N, Keys.S, Keys.H, Keys.R,
                                               Keys.D, Keys.L, Keys.C, Keys.U, Keys.M, Keys.W, Keys.F, Keys.G, Keys.Y,
                                               Keys.P, Keys.B, Keys.V, Keys.K, Keys.J, Keys.X, Keys.Q, Keys.Z };
    }

    public class KeyboardSettings
    {
        public int StoreLength = 1024;
        public bool ParseEnter = false;
        public bool ParseTab = false;
        public bool AddSimulatedKeysToKeyboardString = false;
        public int ReFireDelay = 500;
        public int ReFireInterval = 50;
    }
}
