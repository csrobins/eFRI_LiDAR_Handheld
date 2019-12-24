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
        public static PROJECT GetProjectItem(List<PROJECT> list, string val)
        {
            foreach (var PROJECT in list)
            {
                if (PROJECT.PROJECTID  == val)
                {
                    return PROJECT;
                }
            };
            PROJECT x = new PROJECT();
            x.PROJECTID  = "";
            x.NAME = "";
            return x;
        }

        public static List<PickerItems> SpeciesItems() {
            var list = new List<PickerItems>()
            {
                new PickerItems() {ID = 1, NAME = "White pine"},
                new PickerItems() {ID = 44, NAME = "American beech"},
                new PickerItems() {ID = 2, NAME = "Red pine"},
                new PickerItems() {ID = 50, NAME = "American elm"},
                new PickerItems() {ID = 3, NAME = "Jack pine"},
                new PickerItems() {ID = 20, NAME = "Balsam fir"},
                new PickerItems() {ID = 11, NAME = "Black spruce"},
                new PickerItems() {ID = 73, NAME = "Balsam poplar"},
                new PickerItems() {ID = 12, NAME = "White spruce"},
                new PickerItems() {ID = 51, NAME = "Basswood"},
                new PickerItems() {ID = 15, NAME = "Norway spruce"},
                new PickerItems() {ID = 45, NAME = "Black ash"},
                new PickerItems() {ID = 19, NAME = "Eastern hemlock"},
                new PickerItems() {ID = 25, NAME = "Larch"},
                new PickerItems() {ID = 37, NAME = "Yellow birch"},
                new PickerItems() {ID = 22, NAME = "Northern white cedar"},
                new PickerItems() {ID = 139, NAME = "Pin cherry"},
                new PickerItems() {ID = 32, NAME = "Red (soft) maple"},
                new PickerItems() {ID = 41, NAME = "Red oak"},
                new PickerItems() {ID = 46, NAME = "White ash"},
                new PickerItems() {ID = 30, NAME = "Sugar maple"},
                new PickerItems() {ID = 56, NAME = "Ironwood"},
                new PickerItems() {ID = 46, NAME = "White ash"},
                new PickerItems() {ID = 58, NAME = "Black cherry"},
                new PickerItems() {ID = 38, NAME = "White birch"},
                new PickerItems() {ID = 70, NAME = "Largetooth aspen"},
                new PickerItems() {ID = 40, NAME = "White oak"},
                new PickerItems() {ID = 74, NAME = "Trembling aspen"},
                new PickerItems() {ID = 99, NAME = "Unknown species"},
                new PickerItems() {ID = 1086, NAME = "Willow"}
            };
            return list;
        }
        public static List<PickerItems> ForestItems()
        {
            var list = new List<PickerItems>()
            {
                new PickerItems() {ID=110 ,  NAME = "Abitibi River Forest"},
                new PickerItems() {ID=615 ,  NAME = "Algoma Forest"},
                new PickerItems() {ID=451 ,  NAME = "Algonquin Park Forest"},
                new PickerItems() {ID=220 ,  NAME = "Bancroft-Minden Forest"},
                new PickerItems() {ID=35 ,  NAME = "Black Spruce Forest"},
                new PickerItems() {ID=175 ,  NAME = "Caribou Forest"},
                new PickerItems() {ID=405 ,  NAME = "Crossroute Forest"},
                new PickerItems() {ID=177 ,  NAME = "Dog River-Matawin Forest"},
                new PickerItems() {ID=535 ,  NAME = "Dryden Forest"},
                new PickerItems() {ID=230 ,  NAME = "English River Forest"},
                new PickerItems() {ID=360 ,  NAME = "French-Severn Forest"},
                new PickerItems() {ID=438 ,  NAME = "Gordon Cosens Forest"},
                new PickerItems() {ID=601 ,  NAME = "Hearst Forest"},
                new PickerItems() {ID=350 ,  NAME = "Kenogami Forest"},
                new PickerItems() {ID=644 ,  NAME = "Kenora Forest"},
                new PickerItems() {ID=702 ,  NAME = "Lac Seul Forest"},
                new PickerItems() {ID=815 ,  NAME = "Lake Nipigon"},
                new PickerItems() {ID=796 ,  NAME = "Lakehead Forest"},
                new PickerItems() {ID=565 ,  NAME = "Magpie Forest"},
                new PickerItems() {ID=509 ,  NAME = "Martel Forest"},
                new PickerItems() {ID=140 ,  NAME = "Mazinaw-Lanark Forest"},
                new PickerItems() {ID=390 ,  NAME = "Nagagami Forest"},
                new PickerItems() {ID=754 ,  NAME = "Nipissing Forest"},
                new PickerItems() {ID=680 ,  NAME = "Northshore Forest"},
                new PickerItems() {ID=415 ,  NAME = "Ogoki Forest"},
                new PickerItems() {ID=780 ,  NAME = "Ottawa Valley Forest"},
                new PickerItems() {ID=966 ,  NAME = "Pic Forest"},
                new PickerItems() {ID=421 ,  NAME = "Pineland Forest"},
                new PickerItems() {ID=840 ,  NAME = "Red Lake Forest"},
                new PickerItems() {ID=930 ,  NAME = "Romeo Malette Forest"},
                new PickerItems() {ID=853 ,  NAME = "Sapawe Forest"},
                new PickerItems() {ID=210 ,  NAME = "Spanish Forest"},
                new PickerItems() {ID=889 ,  NAME = "Sudbury Forest"},
                new PickerItems() {ID=898 ,  NAME = "Temagami"},
                new PickerItems() {ID=280 ,  NAME = "Timiskaming Forest"},
                new PickerItems() {ID=120 ,  NAME = "Trout Lake Forest"},
                new PickerItems() {ID=130 ,  NAME = "Wabigoon Forest"},
                new PickerItems() {ID=490 ,  NAME = "Whiskey Jack Forest"},
                new PickerItems() {ID=60 ,  NAME = "White River Forest"},
                new PickerItems() {ID=994 ,  NAME = "Whitefeather Forest"},
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