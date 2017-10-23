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
                public Footnote[] footnotes { get; set; }

                public class Desc
                {
                    public string Name { get; set; }
                    public int Ndbno { get; set; }
                }

                public class Ing
                {
                    public string desc { get; set; }  
                }

                public class Footnote
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
        {
           "ndbno":"45078606",
          "name":"AARDVARK HABENERO HOT SAUCE, UPC: 853393000030",
          "ds":"LI",
          "manu":"Secret Aardvark Trading Company",
          "ru":"g"
          },
          "ing":
                {
                "desc":"TOMATOES (TOMATOES AND FIRE ROASTED TOMATOES, TOMATO JUICE, CITRIC ACID, CALCIUM CHLORIDE), WHITE WINE VINEGAR, CARROTS, WATER, YELLOW ONION, HABANERO CHILI PEPPER (HABANERO CHILI PEPPERS, WATER, SALT, CITRIC ACID), MUSTARD (DISTILLED VINEGAR, WATER, MUSTARD SEED, SALT, TURMERIC, SPICES), ORGANIC CANE SUGAR, SALT, MODIFIED FOOD STARCH, GARLIC, SUNFLOWER OIL, HERBS AND SPICES.",
                  "upd":"09/23/2016"
                  },
           "nutrients":  
           [       { "nutrient_id":"208","name":"Energy","derivation":"LCCS","group":"Proximates","unit":"kcal","value":"0","measures":[{"label":"tsp","eqv":5.0,"eunit":"g","qty":1.0,"value":"0"}]},{"nutrient_id":"203","name":"Protein","derivation":"LCCS","group":"Proximates","unit":"g","value":"0.00","measures":[{"label":"tsp","eqv":5.0,"eunit":"g","qty":1.0,"value":"0.00"}]},{"nutrient_id":"204","name":"Total lipid (fat)","derivation":"LCCS","group":"Proximates","unit":"g","value":"0.00","measures":[{"label":"tsp","eqv":5.0,"eunit":"g","qty":1.0,"value":"0.00"}]},{"nutrient_id":"205","name":"Carbohydrate, by difference","derivation":"LCCS","group":"Proximates","unit":"g","value":"0.00","measures":[{"label":"tsp","eqv":5.0,"eunit":"g","qty":1.0,"value":"0.00"}]},{"nutrient_id":"307","name":"Sodium, Na","derivation":"LCCS","group":"Minerals","unit":"mg","value":"700","measures":[{"label":"tsp","eqv":5.0,"eunit":"g","qty":1.0,"value":"35"}]}],"footnotes":[]}}],"count":1,"notfound":0,"api":2.0} */


/*   {"foods":
    [{"food":
     {"sr":"28",
     "type":"f",
    "desc":
    {"ndbno":"35182",
     "name":"Acorn stew (Apache)","sd":"ACORN STEW (APACHE)",
       "fg":"American Indian/Alaska Native Foods",
       "sn":"",
       "cn":"",
       "manu":"",
       "nf":6.25,
       "cf":0.0,
       "ff":0.0,
       "pf":0.0,
       "r":"0%",
       "rd":"",
       "ds":"Standard Reference",
       "ru":"g"},
       "nutrients":[
       {"nutrient_id":255,"name":"Water","group":"Proximates","unit":"g","value":79.78,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":208,"name":"Energy","group":"Proximates","unit":"kcal","value":95.0,"derivation":"NC","sourcecode":"","dp":"","se":"","measures":[]},{"nutrient_id":268,"name":"Energy","group":"Proximates","unit":"kJ","value":399.0,"derivation":"NC","sourcecode":"","dp":"","se":"","measures":[]},{"nutrient_id":203,"name":"Protein","group":"Proximates","unit":"g","value":6.81,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":204,"name":"Total lipid (fat)","group":"Proximates","unit":"g","value":3.47,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":207,"name":"Ash","group":"Proximates","unit":"g","value":0.72,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":205,"name":"Carbohydrate, by difference","group":"Proximates","unit":"g","value":9.22,"derivation":"NC","sourcecode":"","dp":"","se":"","measures":[]},{"nutrient_id":291,"name":"Fiber, total dietary","group":"Proximates","unit":"g","value":0.7,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":269,"name":"Sugars, total","group":"Proximates","unit":"g","value":0.34,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":210,"name":"Sucrose","group":"Proximates","unit":"g","value":0.16,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":211,"name":"Glucose (dextrose)","group":"Proximates","unit":"g","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":212,"name":"Fructose","group":"Proximates","unit":"g","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":213,"name":"Lactose","group":"Proximates","unit":"g","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":214,"name":"Maltose","group":"Proximates","unit":"g","value":0.19,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":287,"name":"Galactose","group":"Proximates","unit":"g","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":209,"name":"Starch","group":"Proximates","unit":"g","value":6.57,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":301,"name":"Calcium, Ca","group":"Minerals","unit":"mg","value":14.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":303,"name":"Iron, Fe","group":"Minerals","unit":"mg","value":1.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":304,"name":"Magnesium, Mg","group":"Minerals","unit":"mg","value":12.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":305,"name":"Phosphorus, P","group":"Minerals","unit":"mg","value":62.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":306,"name":"Potassium, K","group":"Minerals","unit":"mg","value":110.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":307,"name":"Sodium, Na","group":"Minerals","unit":"mg","value":130.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":309,"name":"Zinc, Zn","group":"Minerals","unit":"mg","value":1.6,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":312,"name":"Copper, Cu","group":"Minerals","unit":"mg","value":0.03,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":315,"name":"Manganese, Mn","group":"Minerals","unit":"mg","value":0.14,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":317,"name":"Selenium, Se","group":"Minerals","unit":"\u00b5g","value":8.3,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":401,"name":"Vitamin C, total ascorbic acid","group":"Vitamins","unit":"mg","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":404,"name":"Thiamin","group":"Vitamins","unit":"mg","value":0.175,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":405,"name":"Riboflavin","group":"Vitamins","unit":"mg","value":0.125,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":406,"name":"Niacin","group":"Vitamins","unit":"mg","value":2.14,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":410,"name":"Pantothenic acid","group":"Vitamins","unit":"mg","value":0.212,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":415,"name":"Vitamin B-6","group":"Vitamins","unit":"mg","value":0.055,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":417,"name":"Folate, total","group":"Vitamins","unit":"\u00b5g","value":33.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":431,"name":"Folic acid","group":"Vitamins","unit":"\u00b5g","value":15.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":432,"name":"Folate, food","group":"Vitamins","unit":"\u00b5g","value":18.0,"derivation":"AS","sourcecode":[1],"dp":"","se":"","measures":[]},{"nutrient_id":435,"name":"Folate, DFE","group":"Vitamins","unit":"\u00b5g","value":44.0,"derivation":"NC","sourcecode":"","dp":"","se":"","measures":[]},{"nutrient_id":418,"name":"Vitamin B-12","group":"Vitamins","unit":"\u00b5g","value":0.68,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":578,"name":"Vitamin B-12, added","group":"Vitamins","unit":"\u00b5g","value":0.0,"derivation":"NONE","sourcecode":"","dp":"","se":"","measures":[]},{"nutrient_id":320,"name":"Vitamin A, RAE","group":"Vitamins","unit":"\u00b5g","value":0.0,"derivation":"NC","sourcecode":"","dp":"","se":"","measures":[]},{"nutrient_id":319,"name":"Retinol","group":"Vitamins","unit":"\u00b5g","value":0.0,"derivation":"NONE","sourcecode":"","dp":"","se":"","measures":[]},{"nutrient_id":318,"name":"Vitamin A, IU","group":"Vitamins","unit":"IU","value":0.0,"derivation":"AS","sourcecode":"","dp":"","se":"","measures":[]},{"nutrient_id":323,"name":"Vitamin E (alpha-tocopherol)","group":"Vitamins","unit":"mg","value":0.3,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":573,"name":"Vitamin E, added","group":"Vitamins","unit":"mg","value":0.0,"derivation":"NONE","sourcecode":"","dp":"","se":"","measures":[]},{"nutrient_id":341,"name":"Tocopherol, beta","group":"Vitamins","unit":"mg","value":0.12,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":342,"name":"Tocopherol, gamma","group":"Vitamins","unit":"mg","value":0.23,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":343,"name":"Tocopherol, delta","group":"Vitamins","unit":"mg","value":0.05,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":430,"name":"Vitamin K (phylloquinone)","group":"Vitamins","unit":"\u00b5g","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":606,"name":"Fatty acids, total saturated","group":"Lipids","unit":"g","value":1.28,"derivation":"NC","sourcecode":"","dp":"","se":"","measures":[]},{"nutrient_id":609,"name":"8:0","group":"Lipids","unit":"g","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":610,"name":"10:0","group":"Lipids","unit":"g","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":611,"name":"12:0","group":"Lipids","unit":"g","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":612,"name":"14:0","group":"Lipids","unit":"g","value":0.09,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":652,"name":"15:0","group":"Lipids","unit":"g","value":0.01,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":613,"name":"16:0","group":"Lipids","unit":"g","value":0.75,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":653,"name":"17:0","group":"Lipids","unit":"g","value":0.04,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":614,"name":"18:0","group":"Lipids","unit":"g","value":0.39,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":615,"name":"20:0","group":"Lipids","unit":"g","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":624,"name":"22:0","group":"Lipids","unit":"g","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":654,"name":"24:0","group":"Lipids","unit":"g","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":645,"name":"Fatty acids, total monounsaturated","group":"Lipids","unit":"g","value":1.68,"derivation":"NC","sourcecode":"","dp":"","se":"","measures":[]},{"nutrient_id":625,"name":"14:1","group":"Lipids","unit":"g","value":0.02,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":697,"name":"15:1","group":"Lipids","unit":"g","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":626,"name":"16:1 undifferentiated","group":"Lipids","unit":"g","value":0.11,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":687,"name":"17:1","group":"Lipids","unit":"g","value":0.03,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":617,"name":"18:1 undifferentiated","group":"Lipids","unit":"g","value":1.51,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":628,"name":"20:1","group":"Lipids","unit":"g","value":0.01,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":630,"name":"22:1 undifferentiated","group":"Lipids","unit":"g","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":671,"name":"24:1 c","group":"Lipids","unit":"g","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":646,"name":"Fatty acids, total polyunsaturated","group":"Lipids","unit":"g","value":0.297,"derivation":"NC","sourcecode":"","dp":"","se":"","measures":[]},{"nutrient_id":618,"name":"18:2 undifferentiated","group":"Lipids","unit":"g","value":0.26,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":619,"name":"18:3 undifferentiated","group":"Lipids","unit":"g","value":0.01,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":627,"name":"18:4","group":"Lipids","unit":"g","value":0.02,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":672,"name":"20:2 n-6 c,c","group":"Lipids","unit":"g","value":0.007,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":689,"name":"20:3 undifferentiated","group":"Lipids","unit":"g","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":620,"name":"20:4 undifferentiated","group":"Lipids","unit":"g","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":629,"name":"20:5 n-3 (EPA)","group":"Lipids","unit":"g","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":857,"name":"21:5","group":"Lipids","unit":"g","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":858,"name":"22:4","group":"Lipids","unit":"g","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":631,"name":"22:5 n-3 (DPA)","group":"Lipids","unit":"g","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":621,"name":"22:6 n-3 (DHA)","group":"Lipids","unit":"g","value":0.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":601,"name":"Cholesterol","group":"Lipids","unit":"mg","value":20.0,"derivation":"NONE","sourcecode":[1],"dp":1,"se":"","measures":[]},{"nutrient_id":501,"name":"Tryptophan","group":"Amino Acids","unit":"g","value":0.04,"derivation":"NONE","sourcecode":[1],"dp":"","se":"","measures":[]},{"nutrient_id":502,"name":"Threonine","group":"Amino Acids","unit":"g","value":0.36,"derivation":"NONE","sourcecode":[1],"dp":"","se":"","measures":[]},{"nutrient_id":503,"name":"Isoleucine","group":"Amino Acids","unit":"g","value":0.35,"derivation":"NONE","sourcecode":[1],"dp":"","se":"","measures":[]},{"nutrient_id":504,"name":"Leucine","group":"Amino Acids","unit":"g","value":0.62,"derivation":"NONE","sourcecode":[1],"dp":"","se":"","measures":[]},{"nutrient_id":505,"name":"Lysine","group":"Amino Acids","unit":"g","value":0.58,"derivation":"NONE","sourcecode":[1],"dp":"","se":"","measures":[]},{"nutrient_id":506,"name":"Methionine","group":"Amino Acids","unit":"g","value":0.16,"derivation":"NONE","sourcecode":[1],"dp":"","se":"","measures":[]},{"nutrient_id":507,"name":"Cystine","group":"Amino Acids","unit":"g","value":0.09,"derivation":"NONE","sourcecode":[1],"dp":"","se":"","measures":[]},{"nutrient_id":508,"name":"Phenylalanine","group":"Amino Acids","unit":"g","value":0.33,"derivation":"NONE","sourcecode":[1],"dp":"","se":"","measures":[]},{"nutrient_id":509,"name":"Tyrosine","group":"Amino Acids","unit":"g","value":0.25,"derivation":"NONE","sourcecode":[1],"dp":"","se":"","measures":[]},{"nutrient_id":510,"name":"Valine","group":"Amino Acids","unit":"g","value":0.39,"derivation":"NONE","sourcecode":[1],"dp":"","se":"","measures":[]},{"nutrient_id":511,"name":"Arginine","group":"Amino Acids","unit":"g","value":0.47,"derivation":"NONE","sourcecode":[1],"dp":"","se":"","measures":[]},{"nutrient_id":512,"name":"Histidine","group":"Amino Acids","unit":"g","value":0.23,"derivation":"NONE","sourcecode":[1],"dp":"","se":"","measures":[]},{"nutrient_id":513,"name":"Alanine","group":"Amino Acids","unit":"g","value":0.45,"derivation":"NONE","sourcecode":[1],"dp":"","se":"","measures":[]},{"nutrient_id":514,"name":"Aspartic acid","group":"Amino Acids","unit":"g","value":0.83,"derivation":"NONE","sourcecode":[1],"dp":"","se":"","measures":[]},{"nutrient_id":515,"name":"Glutamic acid","group":"Amino Acids","unit":"g","value":1.51,"derivation":"NONE","sourcecode":[1],"dp":"","se":"","measures":[]},{"nutrient_id":516,"name":"Glycine","group":"Amino Acids","unit":"g","value":0.42,"derivation":"NONE","sourcecode":[1],"dp":"","se":"","measures":[]},{"nutrient_id":517,"name":"Proline","group":"Amino Acids","unit":"g","value":0.46,"derivation":"NONE","sourcecode":[1],"dp":"","se":"","measures":[]},{"nutrient_id":518,"name":"Serine","group":"Amino Acids","unit":"g","value":0.33,"derivation":"NONE","sourcecode":[1],"dp":"","se":"","measures":[]}
       ],
       "sources":
            [{"id":1,
            "title":"National Food and Nutrient Analysis Program Wave 8c",
            "authors":"Nutrient Data Laboratory, ARS, USDA",
            "vol":"Beltsville",
            "iss":"MD",
            "year":"2004"}],
       "footnotes":
             [{
             "id":"a",
             "desc":"Boiled stew made with water, beef or deer, prepared acorns, dumpling strips,    salt,    and pepper."
             }],
       "langual":[]}
       }],
       "count":1,
       "notfound":0,
       "api":2.0}  */
