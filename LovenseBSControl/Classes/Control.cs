﻿
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LovenseBSControl.Configuration;
using LovenseBSControl.Classes.Modus;
using System.Linq;

namespace LovenseBSControl.Classes
{
    class Control
    {
        private List<Toy> Toys = new List<Toy>();

        private DefaultModus ActiveMode = new DefaultModus();

        public List<object> AvailableModi = new object[] { "Default" }.ToList();

        private Dictionary<string, DefaultModus> ModiList = new Dictionary<string, DefaultModus> {
            { "Default", new DefaultModus() }
        };

        public int HitCounter = 0;
        public int MissCounter = 0;

        private Classes.Request Request;
        public Control()
        {
            this.Request = new Classes.Request();
            this.LoadModes();
            this.SetMode();
        }

        public void SetMode()
        {
            if (ModiList.ContainsKey(PluginConfig.Instance.Modus))
            {
                this.ActiveMode = ModiList[PluginConfig.Instance.Modus];
            }
            else
            {
                this.ActiveMode = new DefaultModus();
            }
        }

        public DefaultModus GetMode()
        {
            return this.ActiveMode;
        }

        public async Task ConnectAsync()
        {
            await this.Request.Connect();
            Toys = GetToyList().ToList();
        }

        public IEnumerable<Toy> GetToyList()
        {
            return this.Request.RequestToysList().Select(d => new Toy(d, this.Request, true));
        }

        public void HandleCut(bool LHand, bool success, NoteCutInfo data = new NoteCutInfo())
        {
            if (success)
            {
                Plugin.Control.HitCounter++;
                this.ActiveMode.HandleHit(Toys, LHand, data);
            }
            else
            {
                Plugin.Control.MissCounter++;
                this.ActiveMode.HandleMiss(Toys, LHand);
            }
        }

        public void HandleBomb()
        {
            this.ActiveMode.HandleBomb(this.Toys);
        }

        public void HandleFireworks()
        {
            this.ActiveMode.HandleFireworks(this.Toys);
        }

        public void StopActive()
        {
            foreach (Toy toy in this.Toys)
            {
                if (toy.IsConnected())
                {
                    toy.stop(true);
                }
            }
        }

        public bool IsAToyActive()
        {
            foreach (Toy toy in this.Toys)
            {
                if (toy.IsConnected() && toy.IsActive())
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsToyAvailable()
        {
            return this.Toys.Count > 0;
        }

        public void PauseGame()
        {
            foreach (Toy toy in this.Toys)
            {
                if (toy.IsConnected())
                {
                    toy.stop();
                }
            }
        }

        public void PunishmentBreak()
        {
            foreach (Toy toy in this.Toys)
            {
                if (toy.IsConnected())
                {
                    Random rng = new Random();
                    int intense = rng.Next(15, 20);

                    toy.vibrate(0, intense, !this.ActiveMode.useLastLevel());
                }

            }
        }



        public void ResumeGame()
        {
            foreach (Toy toy in this.Toys)
            {
                if (toy.IsConnected())
                {
                    toy.resume();
                }
            }
        }

        public void EndGame()
        {
            this.StopActive();
        }

        public void ResetCounter()
        {
            this.HitCounter = 0;
            this.MissCounter = 0;
        }

        public void LoadModes()
        {
            foreach (string obj in Utilities.GetAllClasses("LovenseBSControl.Classes.Modus"))
            {
                if (obj.Equals("Modus") || obj.Equals("DefaultModus")) continue;
                Type modi = Type.GetType("LovenseBSControl.Classes.Modus." + obj);
                if (modi != null)
                {
                    DefaultModus activeObj = Activator.CreateInstance(modi) as DefaultModus;
                    AvailableModi.Add(activeObj.GetModusName());
                    ModiList.Add(activeObj.GetModusName(), activeObj);
                }
            }
        }
    }
}
