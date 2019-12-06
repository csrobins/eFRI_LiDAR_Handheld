using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using eLiDAR.Models;

namespace eLiDAR.Services
{
    public class PickerService
    {
        public static bool IsIndexed(List<PickerItems> list, int val) 
        {
            foreach (var PickerItem in list)
            {
                if (PickerItem.ID == val)
                {
                    return true;
                }
            };
            return false;
        }
        public static int GetIndex(List<PickerItems> list, int val)
        {
            foreach (var PickerItem in list)
            {
                if (PickerItem.ID == val)
                {
                    return val;
                }
            };
            return 0;
        }
        public static string GetValue(List<PickerItems> list, int val)
        {
            foreach (var PickerItem in list)
            {
                if (PickerItem.ID == val)
                {
                    return PickerItem.NAME;
                }
            };
            return "";
        }
        public static PickerItems GetItem(List<PickerItems> list, int val)
        {
            foreach (var PickerItem in list)
            {
                if (PickerItem.ID == val)
                {
                    return PickerItem;
                }
            };
            PickerItems x = new PickerItems();
            x.ID = 0;
            x.NAME = "";
            return x;
        }


        public static List<PickerItems> SpeciesItems() {
            var list = new List<PickerItems>()
            {
                new PickerItems() {ID = 1, NAME = "White Pine"},
                new PickerItems() {ID = 2, NAME = "Red Pine"},
                new PickerItems() {ID = 3, NAME = "Jack Pine"}
            };
            return list;
        }
        public static List<PickerItems> VigourItems()
        {
            var list = new List<PickerItems>()
            {
                new PickerItems() {ID = 1, NAME = "healthy"},
                new PickerItems() {ID = 2, NAME = "light decline"},
                new PickerItems() {ID = 3, NAME = "moderate decline"},
                new PickerItems() {ID = 4, NAME = "severe decline"},
                new PickerItems() {ID = 5, NAME = "dead"},
                new PickerItems() {ID = 6, NAME = "down dead"}
            };
            return list;
        }
        public static List<PickerItems> CrownDamageItems()
        {
            var list = new List<PickerItems>()
            {
                new PickerItems() {ID = 1, NAME = "branch/twig dieback"},
                new PickerItems() {ID = 2, NAME = "transparency"},
                new PickerItems() {ID = 3, NAME = "discolouration"},
                new PickerItems() {ID = 4, NAME = "witches broom"},
                new PickerItems() {ID = 5, NAME = "defoliator"},
                new PickerItems() {ID = 6, NAME = "not discernable"}
            };
            return list;
        }
        public static List<PickerItems> DefoliatingInsectItems()
        {
            var list = new List<PickerItems>()
            {
                new PickerItems() {ID = 0, NAME = "none"},
                new PickerItems() {ID = 1, NAME = "defoliator"},
                new PickerItems() {ID = 2, NAME = "leaf roller/miner"},
                new PickerItems() {ID = 3, NAME = "tent/nestmaker"},
                new PickerItems() {ID = 4, NAME = "budminer"},
                new PickerItems() {ID = 5, NAME = "gallmaker"},
                new PickerItems() {ID = 6, NAME = "sucking"},
                new PickerItems() {ID = 7, NAME = "other"}
            };
            return list;
        }
        public static List<PickerItems> FoliarDiseaseItems()
        {
            var list = new List<PickerItems>()
            {
                new PickerItems() {ID = 0, NAME = "none"},
                new PickerItems() {ID = 1, NAME = "needle rust"},
                new PickerItems() {ID = 2, NAME = "needle cast"},
                new PickerItems() {ID = 3, NAME = "needle blight"},
                new PickerItems() {ID = 4, NAME = "leaf spot"},
                new PickerItems() {ID = 5, NAME = "anthracnose"},
                new PickerItems() {ID = 6, NAME = "leaf blister"},
                new PickerItems() {ID = 7, NAME = "physical"},
                new PickerItems() {ID = 8, NAME = "other"}
            };
            return list;
        }
        public static List<PickerItems> BarkRetentionItems()
        {
            var list = new List<PickerItems>()
            {
                new PickerItems() {ID = 1, NAME = "all bark"},
                new PickerItems() {ID = 2, NAME = "<5% lost"},
                new PickerItems() {ID = 3, NAME = "5-25% lost"},
                new PickerItems() {ID = 4, NAME = "26-50% lost"},
                new PickerItems() {ID = 5, NAME = "51-75% lost"},
                new PickerItems() {ID = 6, NAME = "76-99% lost"},
                new PickerItems() {ID = 7, NAME = "no bark"}
            };
            return list;
        }
        public static List<PickerItems> WoodConditionItems()
        {
            var list = new List<PickerItems>()
            {
                new PickerItems() {ID = 1, NAME = "no decay"},
                new PickerItems() {ID = 2, NAME = "probable decay"},
                new PickerItems() {ID = 3, NAME = "limited decay"},
                new PickerItems() {ID = 4, NAME = "mostly hard"},
                new PickerItems() {ID = 5, NAME = "balance hard/soft"}
            };
            return list;
        }
        public static List<PickerItems> MortalityCauseItems()
        {
            var list = new List<PickerItems>()
            {
                new PickerItems() {ID = 1, NAME = "natural process"},
                new PickerItems() {ID = 2, NAME = "environmental"},
                new PickerItems() {ID = 3, NAME = "human"},
                new PickerItems() {ID = 4, NAME = "unknown"}
            };
            return list;
        }
        public static List<PickerItems> DecayClassItems()
        {
            var list = new List<PickerItems>()
            {
                new PickerItems() {ID = 1, NAME = "1"},
                new PickerItems() {ID = 2, NAME = "2"},
                new PickerItems() {ID = 3, NAME = "3"},
                new PickerItems() {ID = 4, NAME = "4"},
                new PickerItems() {ID = 5, NAME = "5"}
            };
            return list;
        }
        public static List<PickerItems> DrainageItems()
        {
            var list = new List<PickerItems>()
            {
                new PickerItems() {ID = 1, NAME = "very rapid" },
                new PickerItems() {ID = 2, NAME = "rapid"},
                new PickerItems() {ID = 3, NAME = "well"},
                new PickerItems() {ID = 4, NAME = "moderately well"},
                new PickerItems() {ID = 5, NAME = "imperfect"},
                new PickerItems() {ID = 6, NAME = "poor"},
                new PickerItems() {ID = 7, NAME = "very poor"}
            };
            return list;
        }
        public static List<PickerItems> PorePatternItems()
        {
            var list = new List<PickerItems>()
            {
                new PickerItems() {ID = 0, NAME = "very open" },
                new PickerItems() {ID = 1, NAME = "open"},
                new PickerItems() {ID = 2, NAME = "moderately open"},
                new PickerItems() {ID = 3, NAME = "moderately retentive"},
                new PickerItems() {ID = 4, NAME = "retentive"},
                new PickerItems() {ID = 5, NAME = "very retentive"},
                new PickerItems() {ID = 6, NAME = "moderatley restricted"}
            };
            return list;
        }
        public static List<PickerItems> MoistureRegimeItems()
        {
            var list = new List<PickerItems>()
            {
                new PickerItems() {ID = 0, NAME = "mod. dry" },
                new PickerItems() {ID = 1, NAME = "mod. fresh"},
                new PickerItems() {ID = 2, NAME = "fresh"},
                new PickerItems() {ID = 3, NAME = "v. fresh"},
                new PickerItems() {ID = 4, NAME = "mod. moist"},
                new PickerItems() {ID = 5, NAME = "moist"},
                new PickerItems() {ID = 6, NAME = "v. moist"},
                new PickerItems() {ID = 7, NAME = "mod. wet"},
                new PickerItems() {ID = 8, NAME = "wet"},
                new PickerItems() {ID = 9, NAME = "v. wet"}

            };
            return list;
        }
        public static List<PickerItems> HumusFormItems()
        {
            var list = new List<PickerItems>()
            {
                new PickerItems() {ID = 1, NAME = "anmoor"},
                new PickerItems() {ID = 2, NAME = "fibrimor"},
                new PickerItems() {ID = 3, NAME = "humimor"},
                new PickerItems() {ID = 4, NAME = "moder"},
                new PickerItems() {ID = 5, NAME = "mull"},
                new PickerItems() {ID = 6, NAME = "peaty mor"},
            };
            return list;
        }
    }


}