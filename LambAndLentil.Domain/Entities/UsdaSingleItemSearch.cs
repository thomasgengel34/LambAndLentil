using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambAndLentil.Domain.Entities
{
   public class UsdaSingleItemSearch
    {
        public ListOMatic list { get; set; }

       

        public class ListOMatic
        {
            public ItemOMatic[] item { get; set; }
            public int total { get; set; }

            public class ItemOMatic
            {
                public string name { get; set; }
                public int ndbno { get; set; }
            }
        }
    }
}

/*
 {
    "list": {
        "q": "aardvark",
        "sr": "28",
        "ds": "any",
        "start": 0,
        "end": 1,
        "total": 1,
        "group": "",
        "sort": "n",
        "item": [
            {
                "offset": 0,
                "group": "Branded Food Products Database",
                "name": "AARDVARK HABENERO HOT SAUCE, UPC: 853393000030",
                "ndbno": "45078606",
                "ds": "BL"
            }
        ]
    }
} 
 */

/*
 {
"list": {
    "q": "Orange",
    "sr": "28",
    "ds": "Standard Reference",
    "start": 0,
    "end": 1,
    "total": 61,
    "group": "",
    "sort": "r",
    "item": [
        {
            "offset": 0,
            "group": "Sweets",
            "name": "Sherbet, orange",
            "ndbno": "19097",
            "ds": "SR"
        }
    ]
}
}*/
