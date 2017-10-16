using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambAndLentil.Domain.Entities
{
    public class UsdaFood
    {
        public Foods[] foods { get; set; }

        public class Foods
        {
            public Food food { get; set; }

            public class Food
            {
                public Desc desc { get; set; }
                public Ing ing { get; set; }

                public class Desc
                {
                    public string Name { get; set; }
                    public int Ndbno { get; set; }
                }

                public class Ing
                {
                    public string desc { get; set; }  
                }

            }
        }
    }


}
/* {"foods":
     [{"food":
       {"sr":"July, 2017",
        "type":"b",
        "desc":
        {"ndbno":"45078606",
          "name":"AARDVARK HABENERO HOT SAUCE, UPC: 853393000030",
          "ds":"LI",
          "manu":"Secret Aardvark Trading Company",
          "ru":"g"},
          "ing":
                {"desc":"TOMATOES (TOMATOES AND FIRE ROASTED TOMATOES, TOMATO JUICE, CITRIC ACID, CALCIUM CHLORIDE), WHITE WINE VINEGAR, CARROTS, WATER, YELLOW ONION, HABANERO CHILI PEPPER (HABANERO CHILI PEPPERS, WATER, SALT, CITRIC ACID), MUSTARD (DISTILLED VINEGAR, WATER, MUSTARD SEED, SALT, TURMERIC, SPICES), ORGANIC CANE SUGAR, SALT, MODIFIED FOOD STARCH, GARLIC, SUNFLOWER OIL, HERBS AND SPICES.",
           "upd":"09/23/2016"},"nutrients":[{"nutrient_id":"208","name":"Energy","derivation":"LCCS","group":"Proximates","unit":"kcal","value":"0","measures":[{"label":"tsp","eqv":5.0,"eunit":"g","qty":1.0,"value":"0"}]},{"nutrient_id":"203","name":"Protein","derivation":"LCCS","group":"Proximates","unit":"g","value":"0.00","measures":[{"label":"tsp","eqv":5.0,"eunit":"g","qty":1.0,"value":"0.00"}]},{"nutrient_id":"204","name":"Total lipid (fat)","derivation":"LCCS","group":"Proximates","unit":"g","value":"0.00","measures":[{"label":"tsp","eqv":5.0,"eunit":"g","qty":1.0,"value":"0.00"}]},{"nutrient_id":"205","name":"Carbohydrate, by difference","derivation":"LCCS","group":"Proximates","unit":"g","value":"0.00","measures":[{"label":"tsp","eqv":5.0,"eunit":"g","qty":1.0,"value":"0.00"}]},{"nutrient_id":"307","name":"Sodium, Na","derivation":"LCCS","group":"Minerals","unit":"mg","value":"700","measures":[{"label":"tsp","eqv":5.0,"eunit":"g","qty":1.0,"value":"35"}]}],"footnotes":[]}}],"count":1,"notfound":0,"api":2.0} */
