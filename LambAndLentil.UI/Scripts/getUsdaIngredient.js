
(function () {
   
    var ndbno = 0; 

    var getFoods = function () {
        //alert("entering getFoods()")
        var http = "https://api.nal.usda.gov/ndb/search/?format=json&q=";
        var searchFor = $("#text").val();
        var manu = $("#tbManufacturer").val();
        var apiKey = "&sort=n&max=15&offset=0&api_key=";
        var key = "sFtfcrVdSOKA4ip3Z1MlylQmdj5Uw3JoIIWlbeQm";
        var url = http.concat(searchFor, "&manu=", manu, apiKey, key);
        //  alert(url);
        $.get(url).always(getOneOrList);
        return false;
    };

    var getOneOrList = function (object) {
        //  alert("entering getOneOrList");
        try {
            ndbno = object.list.item[0].ndbno;
            var returnedCount = object.list.total;
            if (returnedCount == 1) {
                //    alert("Count is 1");
                getFoodIngredients(ndbno);

            }
            if (returnedCount > 1) {
                //  alert("Count > 1");
                getDropDownList(object);
            }
        }
        catch (e) {
            $("#output").text("Ingredient not found.  Please try again.");
        }
    };

    var getFoodIngredients = function (ndbno) {
        //   alert("entering getFoodIngredients");
        try {
            var http = "https://api.nal.usda.gov/ndb/V2/reports?ndbno=";
            var apiKey = "&type=f&format=json&api_key=";
            var key = "sFtfcrVdSOKA4ip3Z1MlylQmdj5Uw3JoIIWlbeQm";
            //  var foodsUrl = "https://api.nal.usda.gov/ndb/V2/reports?ndbno=45078606&type=f&format=json&api_key=" + key;
            var foodsUrl = http.concat(ndbno, apiKey, key);
            //  alert(foodsUrl); 4
            $.get(foodsUrl).always(showFoodReportForOneFood);
            return false;
        } catch (e) {
            alert("error in getFoodIngredients: " + e.message);
        }
    };

    var showFoodReportForOneFood = function (object) {
        try {
            //alert("entering showFoodReportForOneFood "); 
            //       alert(object.foods[0].food.desc.name);
            var ingredient = "not valid choice";
            if (object.foods[0].food.ing == null) {
                //   alert("successfully detected an undefined element");

                ingredient = object.foods[0].food.footnotes[0].desc;
            }
            else {
                //   alert("element was defined");
                ingredient = object.foods[0].food.ing.desc;
            }

            $("#output").text(object.foods[0].food.desc.name + " " + ingredient);



            //alert("success in entering showFoodReportForOneFood");

        } catch (e) {
            $("#output").text("error in showFoodReportForOneFood: " + e.message);
        }
    };

    var getDropDownList = function (object) {
        //    alert("entering getDropDownList");
        $('#starter').remove();
        var j = object.list.total;

        if (object.list.total > 15) {
            j = 15;
        }


        $('#outputList option').remove();

        $('#outputList').append('<option>Select:</option>');
        $("#output").text("Only up to the first 15 results are shown. There were:  " + object.list.total + " results");

        // alert(object.list);  returns [object Object]
        try {
            for (i = 0; i < j; i++) {

                var ndbno = object.list.item[i].ndbno;

                $('#outputList').append('<option ndbno=' + ndbno + ' >  ' + object.list.item[i].name + '  </option>');

            }
            //        alert("success in getDropDownList");

        }
        catch (e) {
            $("#output").text("drop down selection list with note error:" + e.message);
        }
    };


    $("#btnSearch").click(getFoods);


    $('#outputList').change(function () {
        //  alert("entering   $('#outputList').change(function ()");
        var obj = $('#outputList [selected="selected"]');
        var ndbno = $('option:selected', this).attr('ndbno');

        // alert("ndbno= "+ndbno);

        getFoodIngredients(ndbno);
        //   alert("leaving $('#outputList').change(function ()");
    });

})(); 