﻿using BeatSaberMarkupLanguage.Attributes;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovenseBSControl.Configuration
{

    public class ToysConfig
    {
        [UIValue(nameof(Id))]
        public String Id { get; set; } = "";

        [UIValue(nameof(LHand))]
        public bool LHand { get; set; } = false;

        [UIValue(nameof(RHand))]
        public bool RHand { get; set; } = false;

        [UIValue(nameof(Inactive))]
        public bool Inactive { get; set; } = false;

        [UIValue(nameof(Random))]
        public bool Random { get; set; } = false;

        [UIValue(nameof(HType))]
        public string HType = HTypes.bHands;

        
        public static ToysConfig createToyConfig(string Id) {
            ToysConfig toysConfig = new ToysConfig();

            toysConfig.Id = Id;
            toysConfig.LHand = true;
            toysConfig.RHand = true;
            toysConfig.Inactive = false;
            toysConfig.Random = false;
            toysConfig.HType = HTypes.bHands;

            return toysConfig;
        }

        public void setHType(String value) {
            this.HType = value;
            this.Inactive = false;
            this.Random = false;
            if (value.Equals(HTypes.bHands))
            {
                this.LHand = true;
                this.RHand = true;
            }
            else if (value.Equals(HTypes.lHand))
            {
                this.LHand = true;
                this.RHand = false;
            }
            else if (value.Equals(HTypes.rHand))
            {
                this.LHand = false;
                this.RHand = true;
            }
            else if (value.Equals(HTypes.random))
            {
                this.Random = true;
            }
            else if (value.Equals(HTypes.inactive))
            {
                this.Inactive = true;
            }

        }
    }

    public static class HTypes
    {
        public const string bHands = "Both Hands";
        public const string lHand = "Left Hand";
        public const string rHand = "Right Hand";
        public const string random = "Random";
        public const string inactive = "Inactive";
    }

    
}
