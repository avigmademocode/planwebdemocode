HomeApp.controller('RequestFinalizeController', ['$scope', '$window', 'RequestFinalizeFactory', function ($scope, $window, RequestFinalizeFactory) {

    $scope.OrderID = $scope.OrderID ? $scope.OrderID.split('?')[1] : window.location.search.slice(1);
    $scope.Atmhide = false;
    $scope.Perhide = true;

    $scope.AmountAndPer = [
        { id: 0, name: "Amount" },
        { id: 1, name: "Percent" }
    ];

    //dynamic Submit Approval Email To:

    $scope.ApprovalEmailMul = [{ AprEmail: '', AprName: '', AprTitle: '', OAID: '', Comments: '', OrderId: 0 }];
    //Remove Row
    $scope.removeRow = function (value) {
        //alert(value);
        $scope.ApprovalEmailMul.splice(value, 1)
    };
    $scope.addRow = function () {
        var obj = {};

        obj.AprEmail = '';
        obj.AprName = '';
        obj.AprTitle = '';
        obj.OAID = 0;
        obj.Comments = '';
        OrderId: 0;
        $scope.ApprovalEmailMul.push(obj);
    };

    $scope.BudgetDtls = [
        //{ Name: "John Hammond", Country: "United States" },
        //{ Name: "Mudassar Khan", Country: "India" },
        //{ Name: "Suzanne Mathews", Country: "France" },
        //{ Name: "Robert Schidner", Country: "Russia" }
    ];
    $scope.BudgetListRecords = [];
    $scope.FinalBudgetListRecords = [];
    $scope.BudgetListcal = [];
    $scope.Valid = true;

    $scope.Remove = function (index) {
        //Find the record using Index from Array.
        //var name = $scope.Customers[index].Name;
        if ($window.confirm("Do you want to delete: " + name)) {
            //Remove the item from Array using Index.
            $scope.BudgetDtls.splice(index, 1);
        }
    }

    $scope.BackToPrevious = function () {
        debugger;

        $window.location.href = '/ViewRequest?OrderId=' + encodeURIComponent(decodeURIComponent($scope.OrderID));
    }

    $scope.GetTableData = function () {
        debugger;
        console.log($scope.BudgetListRecords);
    }

    //$scope.BudgetList = {};

    $scope.GetSetType = function (val) {
        debugger;
        var cval = 0;
        var fval = 0;
        var perfval = 0;
        var ogval = 0;
        var Perogval = 0;
        var calval = 0;
        var ogTotalval = 0;
        var PerogTotalval = 0;
        var FinTotal = 0;
        var AllRowTotal = 0;
        var perval = 0;
        var cperval = 0;
        var ogTaxval = 0;
        var ogFiefghtval = 0;
        var perogTaxval = 0;
        var perogFiefghtval = 0;
        $scope.Valid = true;
        $scope.OldValue = JSON.parse($window.localStorage.getItem('BudgetListcal'));
        $scope.OldPerValue = JSON.parse($window.localStorage.getItem('BudgetListper'));
        $scope.OrgFinalTotalList = JSON.parse($window.localStorage.getItem('FinalTotalList'));
        
        if (val == 0) {
            $scope.Atmhide = false;
            $scope.FinalBudgetListRecords = [];
            $scope.FinalBudgetListRecords = JSON.parse($window.localStorage.getItem('FinalBudgetList'));
            $scope.Perhide = true;
            $scope.BudgetListRecords = [];
            $scope.BudgetListRecords = $scope.FinalBudgetListRecords;
            $scope.BudgetList[1] = $scope.OldValue;
            for (var i = 0; i < $scope.BudgetListRecords.length; i++) {
                angular.forEach($scope.BudgetListRecords[i], function (value, key) {
                    angular.forEach($scope.BudgetList[1], function (value2, key2) {
                        if (key == key2) {
                            if (key.match("odid")) {

                                var tempval = (parseFloat(Math.round($scope.BudgetListRecords[i][key] * 100) / 100));
                                var tempfinaltotal = (parseFloat(Math.round($scope.BudgetList[1][key] * 100) / 100));
                                tempfinaltotal -= tempval;
                                $scope.BudgetList[1][key] = (parseFloat(Math.round(tempfinaltotal * 100) / 100));




                            }
                            if (key == 'Total_1') {

                                var tempval = (parseFloat(Math.round($scope.BudgetListRecords[i][key] * 100) / 100));
                                var tempfinaltotal = (parseFloat(Math.round($scope.BudgetList[1][key] * 100) / 100));
                                tempfinaltotal -= tempval;
                                $scope.BudgetList[1][key] = (parseFloat(Math.round(tempfinaltotal * 100) / 100));
                            }
                        }





                    });
                });

            }
        }
        else {
            $scope.Atmhide = true;

            $scope.Perhide = false;

            for (var i = 0; i < $scope.BudgetListRecords.length; i++) {
                var rowtotal = 0;
                cval = 0;
                fval = 0;
                perfval = 0;
                ogval = 0;
                Perogval = 0;
                calval = 0;
                ogTotalval = 0;
                ogTaxval = 0;
              ogFiefghtval = 0;
                PerogTotalval = 0;
                FinTotal = 0;
                AllRowTotal = 0;
                perval = 0;
                cperval = 0;
                angular.forEach($scope.BudgetListRecords[i], function (value, key) {
                    ogval = 0;
                    perval = 0;
                    var keyval = key;
                    angular.forEach($scope.OrgFinalTotalList, function (value1, key1) {

                        if (key1.match("odid")) {
                            if (key == key1) {
                                ogval = parseInt(value1);
                            }

                            else
                                if (key.match("odid")) {
                                    if (key.split('_')[2] == key1.split('_')[1]) {
                                        ogval = parseInt(value1);
                                    }
                                }
                         
                        }
                        if (key1 == "Total_1") {
                            ogTotalval = parseInt(value1)
                        }
                        else if (key1.match("Total")) {
                            ogTotalval = parseInt(value1)
                        }

                        if (key1 == "TotalFiefght_1") {
                            ogFiefghtval = parseInt(value1)
                        }
                        if (key1 == "TotalTax_1") {
                            ogTaxval = parseInt(value1)
                        }
                       
                    })
                    angular.forEach($scope.OldPerValue, function (value1, key1) {

                        if (key1.match("odid")) {
                            if (key == key1) {
                                Perogval = parseFloat(value1);
                            }
                            else
                                if (key.match("odid")) {
                                    if (key.split('_')[2] == key1.split('_')[1]) {
                                        Perogval = parseFloat(value1);
                                    }
                                }
                           
                        }
                        if (key1 == "Total_1") {
                            PerogTotalval = parseFloat(value1);
                        }
                        else if (key1.match("Total")) {
                            PerogTotalval = parseFloat(value1)
                        }

                        if (key1 == "TotalFiefght_1") {
                            Perogval = parseInt(value1)
                        }
                        if (key1 == "TotalTax_1") {
                            Perogval = parseInt(value1)
                        }
                    })

                    if (key.match("odid")) {

                        cval = 0;
                        if (value != '' && value != null) {
                            cval += parseInt(value);
                            perval = ((cval / ogval) * 100);
                        }
                        cperval = parseFloat(Math.round(perval * 100) / 100)
                        if (cperval != 0) {
                            $scope.BudgetListRecords[i][key] = cperval + '%';
                        }


                        if (key == "$$hashKey") {
                            // alert(key);
                        }
                        else {
                            if (key != 'Total_1') {
                                if (value != '' && value != null) {
                                    rowtotal += (cperval);
                                }
                            }
                            else if (key.match("odid")) {
                                if (value != '' && value != null) {
                                    rowtotal += (cperval);
                                }
                            }

                        }
                    }
                    else
                        if (key.match("TotalFiefght")) {
                            cval = 0;
                            if (value != '' && value != null) {
                                cval += parseInt(value);
                                perval = ((cval / ogFiefghtval) * 100);
                                cperval = parseFloat(Math.round(perval * 100) / 100)
                                if (cperval != 0) {
                                    $scope.BudgetListRecords[i][key] = cperval + '%';
                                    rowtotal += (cperval);
                                }
                            }
                        }
                        else if (key.match("TotalTax")) {
                            cval = 0;
                            if (value != '' && value != null) {
                                cval += parseInt(value);
                                perval = ((cval / ogTaxval) * 100);
                                cperval = parseFloat(Math.round(perval * 100) / 100)
                                if (cperval != 0) {
                                    $scope.BudgetListRecords[i][key] = cperval + '%';
                                    rowtotal += (cperval);
                                }
                            }
                        }
                    perfval = Perogval - cperval;

                    angular.forEach($scope.BudgetListper, function (value2, key2) {
                        if (key2.match("odid")) {
                            if (key == key2) {
                                if (perfval >= 0) {
                                    $scope.OldPerValue[key] = (parseFloat(Math.round(perfval * 100) / 100));
                                    $scope.BudgetListper[key] = (parseFloat(Math.round(perfval * 100) / 100)) + "%";

                                }


                            }
                            else
                                if (key.match("odid")) {
                                    if (key.split('_')[2] == key2.split('_')[1]) {
                                        {

                                            if (perfval >= 0) {
                                                $scope.OldPerValue[key2] = (parseFloat(Math.round(perfval * 100) / 100));
                                                $scope.BudgetListper[key2] = (parseFloat(Math.round(perfval * 100) / 100)) + "%";

                                            }

                                        }
                                    }
                            }
                        }


                    });
                });




                angular.forEach($scope.BudgetListRecords[i], function (value, key) {


                    if (key == 'Total_1') {

                        $scope.BudgetListRecords[i][key] = rowtotal + '%';
                    }
                    else if (!key.match("TotalFiefght") && !key.match("TotalTax"))
                    {
                        if (key.match("Total"))
                        {
                            $scope.BudgetListRecords[i][key] = (parseFloat(Math.round(rowtotal * 100) / 100)) + '%';
                        }
                    }
                    
                })

                AllRowTotal += rowtotal;
            }
        }


    }
    //$scope.BudgetList = {};

    $scope.CalculateValue = function (ogkey, val) {
        $scope.Newogkey = ogkey;
        //debugger;
        var cval = 0;
        var fval = 0;
        var ogval = 0;
        var calval = 0;
        var ogTotalval = 0;
        var FinTotal = 0;
        var AllRowTotal = 0;
        var Perogval = 0;
        var PerogTotalval = 0;
        $scope.Valid = true;
        $scope.OldValue = JSON.parse($window.localStorage.getItem('BudgetListcal'));
        $scope.OldPerValue = JSON.parse($window.localStorage.getItem('BudgetListper'));
        $scope.FinalBudgetListRecords = [];
        $scope.FinalBudgetListRecords = JSON.parse($window.localStorage.getItem('FinalBudgetList'));
        var x = $scope.ddltype;
        // x = 1;
        if (x == 0) {
            $window.localStorage.removeItem('FinalBudgetList');

            angular.forEach($scope.OldValue, function (value, key) {
                if (key.match("odid")) {
                    
                    if ($scope.Newogkey == key) {
                        ogval = parseInt(value);
                    }
                    else if ($scope.Newogkey.split('_')[2] == key.split('_')[1])
                    {
                        ogval = parseInt(value);
                    }
                    
                }
                if (key == "Total_1") {
                    ogTotalval = parseInt(value)
                }
                else if (key.match("Total"))
                {
                    ogTotalval = parseInt(value1)
                }
            })

            for (var i = 0; i < $scope.BudgetListRecords.length; i++) {
                var rowtotal = 0;
                if (x == 0) {
                    $scope.OldValue = JSON.parse($window.localStorage.getItem('BudgetListcal'));
                    $scope.OldPerValue = JSON.parse($window.localStorage.getItem('BudgetListper'));
                    angular.forEach($scope.BudgetListRecords[i], function (value, key) {
                        var tempcval = 0;
                        if (key.match("odid")) {

                            if ($scope.Newogkey == key) {
                                if (value != '' && value != null) {
                                    cval += parseInt(value);
                                    tempcval += parseInt(value);

                                }


                            }
                            if (key == "$$hashKey") {
                                // alert(key);
                            }
                            else {
                                if (key != 'Total_1') {
                                    if (value != '' && value != null) {
                                        rowtotal += parseInt(value);
                                    }
                                }

                            }
                        }
                    });

                    angular.forEach($scope.BudgetListRecords[i], function (value, key) {


                        if (key == 'Total_1') {

                            $scope.BudgetListRecords[i][key] = rowtotal;

                        }
                        else if (key.match("Total")) {
                            $scope.BudgetListRecords[i][key] = rowtotal;
                        }
                    })

                }

                AllRowTotal += rowtotal;
            }

            //if (x == 1) {
            //    calval = (ogval * cval) / 100
            //    fval = ogval - calval;
            //}
            //else
            {
                fval = ogval - cval;
            }
            FinTotal = ogTotalval - AllRowTotal;
            angular.forEach($scope.BudgetListCal , function (value, key) {
                if ($scope.Newogkey == key) {
                    if (key.match("odid")) {
                        if (fval >= 0) {

                            $scope.BudgetListCal[key] = fval;
                        }
                        else {
                            $scope.Valid = false;
                            alert('Please Enter Amount Value than Total Value');
                        }

                    }
                }

                else if ($scope.Newogkey.split('_')[2] == key.split('_')[1])
                {
                    if (key.match("odid")) {
                        if (fval >= 0) {

                            $scope.BudgetListCal[key] = fval;
                        }
                        else {
                            $scope.Valid = false;
                            alert('Please Enter Amount Value than Total Value');
                        }

                    }
                }

                if (key == "Total_1") {
                    if (FinTotal >= 0)
                    {
                        $scope.BudgetListCal[key] = FinTotal;
                    }
                    else
                    {
                        $scope.Valid = false;
                        alert('Please Enter Amount Value than Total Value');
                    }

                }
                else if (key.match("Total"))
                {
                    if (FinTotal >= 0) {
                        $scope.BudgetListCal[key] = FinTotal;
                    }
                    else {
                        $scope.Valid = false;
                        alert('Please Enter Amount Value than Total Value');
                    }
                }


            });

            $window.localStorage.setItem('FinalBudgetList', JSON.stringify($scope.BudgetListRecords));
        }
        else if (x == 1) {
            angular.forEach($scope.OldPerValue, function (value, key) {
                if ($scope.Newogkey == key) {
                    Perogval = parseInt(value);
                }
                if (key == "Total_1") {
                    PerogTotalval = parseInt(value)
                }
            })

            angular.forEach($scope.OldValue, function (value, key) {
                if ($scope.Newogkey == key) {
                    ogval = parseInt(value);
                }
                if (key == "Total_1") {
                    ogTotalval = parseInt(value)
                }
            })


            for (var i = 0; i < $scope.BudgetListRecords.length; i++) {
                var rowtotal = 0;
                var tempcval = 0;
                if (x == 1) {

                    angular.forEach($scope.BudgetListRecords[i], function (value, key) {
                      
                        if (key.match("odid")) {

                            if ($scope.Newogkey == key) {
                                if (value != '' && value != null) {
                                    cval += parseInt(value);
                                    tempcval += parseInt(value);
                                }


                            }
                            if (key == "$$hashKey") {
                                // alert(key);
                            }
                            else {
                                if (key != 'Total_1') {
                                    if (value != '' && value != null) {
                                        rowtotal += parseInt(value);
                                    }
                                }

                            }
                        }
                    });

                    angular.forEach($scope.BudgetListRecords[i], function (value, key) {


                        if (key == 'Total_1') {

                            $scope.BudgetListRecords[i][key] = rowtotal;
                        }
                    })

                }
                AllRowTotal += rowtotal;

                var amval = (ogval * tempcval) / 100
                var amtotal = 0;
                var finamtotal = 0;
                $scope.FinalBudgetListRecords[i][$scope.Newogkey] = (parseFloat(Math.round(amval * 100) / 100));
                angular.forEach($scope.FinalBudgetListRecords[i], function (value, key) {


                    if (key.match("odid")) {

                        amtotal = (parseFloat(Math.round($scope.FinalBudgetListRecords[i][key] * 100) / 100));
                        finamtotal += (parseFloat(Math.round(amtotal * 100) / 100));
                    }
                })
                $scope.FinalBudgetListRecords[i]['Total_1'] = finamtotal;
                if (localStorage.getItem("FinalBudgetList") != null) {
                    $window.localStorage.removeItem('FinalBudgetList');
                }
                $window.localStorage.setItem('FinalBudgetList', JSON.stringify($scope.FinalBudgetListRecords));
            }




            //if (x == 1) {
            //    calval = (ogval * cval) / 100
            //    fval = ogval - calval;
            //}
            //else
            {
                fval = Perogval - cval;
            }
            FinTotal = PerogTotalval - AllRowTotal;
            angular.forEach($scope.BudgetList[2], function (value, key) {
                if ($scope.Newogkey == key) {
                    if (key.match("odid")) {
                        if (fval >= 0) {

                            $scope.BudgetList[2][key] = fval + "%";
                        }
                        else {
                            $scope.Valid = false;
                            alert('Please Enter Amount Value than Total Value');
                        }

                    }
                }
                if (key == "Tot_2") {
                    if (FinTotal >= 0) {
                        $scope.BudgetList[2][key] = FinTotal + "%";
                    }
                    else {
                        $scope.Valid = false;
                        alert('Please Enter Amount Value than Total Value');
                    }

                }


            });
        }




    }



    function isNumeric(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }


    $scope.AddNewRow = function () {

        //debugger;

        var item = JSON.parse(JSON.stringify($scope.BudgetList[0]));
        for (var p in item) item[p] = "";
        $scope.BudgetListRecords.push(item);
        $scope.FinalBudgetListRecords.push(item);
        if (localStorage.getItem("FinalBudgetList") != null) {
            $window.localStorage.removeItem('FinalBudgetList');
        }
        $window.localStorage.setItem('FinalBudgetList', JSON.stringify($scope.FinalBudgetListRecords));
        // $window.localStorage.removeItem('FinalBudgetList');
        //  $window.localStorage.setItem('FinalBudgetList', JSON.stringify($scope.FinalBudgetListRecords));
    }


    $scope.AddRow = function () {

        //debugger;
        for (var i = 0; i < $scope.BudgetList.length-3; i++) {
            if (i % 2 == 1)
            {
                var item = JSON.parse(JSON.stringify($scope.BudgetList[i]));
               // for (var p in item)
                   // item[p] = "";
                $scope.BudgetListRecords.push(item);
                $scope.FinalBudgetListRecords.push(item);
                if (localStorage.getItem("FinalBudgetList") != null) {
                    $window.localStorage.removeItem('FinalBudgetList');
                }
                $window.localStorage.setItem('FinalBudgetList', JSON.stringify($scope.FinalBudgetListRecords));
            }
        }
      
       
        // $window.localStorage.removeItem('FinalBudgetList');
        //  $window.localStorage.setItem('FinalBudgetList', JSON.stringify($scope.FinalBudgetListRecords));
    }

    //});
    $scope.Remove = function (index) {

        if ($window.confirm("Do you want to delete: " + name)) {

            $scope.BudgetListRecords.splice(index, 1);
            $scope.FinalBudgetListRecords.splice(index, 1);
            $window.localStorage.removeItem('FinalBudgetList');
            $window.localStorage.setItem('FinalBudgetList', JSON.stringify($scope.FinalBudgetListRecords));
        }
    }
    $scope.SaveAndNotify = function (BudgetListRecords) {
    
        debugger;
        var ddlval = 0;
        var inttype;
        var value = $window.localStorage.getItem('EditBudget');
        if (value == 0)
        {
            inttype = 1;
        }
        else if (value == 1)
        {
            inttype = 2;
        }
        $scope.FinalBudgetListRecords = JSON.parse($window.localStorage.getItem('FinalBudgetList'));
        if ($scope.Valid) {
            if ($scope.ddltype != undefined)
            {
                ddlval = $scope.ddltype;
            }

            var list =
                {
                    OrderID: $scope.OrderID,
                    Plansoncomment: $scope.PlansonComment,
                    Approvercomment: $scope.ApproverComment,
                    Approveremail: JSON.stringify($scope.ApprovalEmailMul), //$scope.ApproverEmail,
                    BudgetSplit: ddlval,
                    BudgetFreight: 1,// Need to ask customer for logic
                    BudgetList: JSON.stringify($scope.FinalBudgetListRecords),
                    Type: inttype 
                };
           // alert(JSON.stringify(list));
            RequestFinalizeFactory.AddBudgetDetails(list)
                .then(function (response) {
                    if (response.data.length != 0) {

                        alert('Request has been saved successfully.');
                        $window.location.href = '/ViewRequest?OrderId=' + encodeURIComponent(decodeURIComponent($scope.OrderID));
                    }

                });
        }
        else {
            alert('Please Enter Correct Value.');
        }
    }

    /*
    $('#btnAdd').click(function () {
           debugger;
           var count = 1,
               first_row = $('#Row1');
           while (count-- > 0) first_row.clone().appendTo('#blacklistgrid');
       });
    
    */



    $scope.ViewRequestOrderDetails = function () {
        //debugger;
        RequestFinalizeFactory.ViewOrderFinalizeDetails($scope.OrderID)
            .then(function (response) {

                if (response.data[0][0].length != 0) {
                    $scope.RequestDetails = response.data[0][0];
                }
                if (response.data[0][1].length != 0) {
                    $scope.ApprovarDetails = response.data[0][1];
                    $scope.ApprovalEmailMul = $scope.ApprovarDetails;
                }
                if (response.data[0][2].length != 0) {
                    $scope.CartItems = response.data[0][2];
                }
                if (response.data[0][3].length != 0) {
                    $scope.SAdd1 = response.data[0][3][0];
                    $scope.SAdd2 = response.data[0][3][1];
                    $scope.SAdd3 = response.data[0][3][2];
                    $scope.SCity = response.data[0][3][3];
                    $scope.SState = response.data[0][3][4];
                    $scope.SZip = response.data[0][3][5];
                    $scope.SCountry = response.data[0][3][6];
                }
                if (response.data[0][4].length != 0) {
                    $scope.BAdd1 = response.data[0][4][0];
                    $scope.BAdd2 = response.data[0][4][1];
                    $scope.BAdd3 = response.data[0][4][2];
                    $scope.BCity = response.data[0][4][3];
                    $scope.BState = response.data[0][4][4];
                    $scope.BZip = response.data[0][4][5];
                    $scope.BCountry = response.data[0][4][6];
                }
                if (response.data[0][5].length != 0) {
                    $scope.Prodcount = response.data[0][5][0].ProdCount;
                }


                if (response.data[1][0].length != 0) {
                    $scope.Freight = response.data[1][0];
                }


            });
    }
    $scope.ViewRequestOrderDetails();


    $scope.GetCustBudgetMastr = function () {
        //debugger;
        RequestFinalizeFactory.GetCustBudgetMastr($scope.OrderID)
            .then(function (response) {
                //debugger

                $scope.BudgetList = response.data;
                $scope.BudgetListCal = response.data[1];
                $scope.BudgetListper = response.data[2];
                //$scope.BudgetListcal = response.data[1];
                $window.localStorage.setItem('BudgetListcal', JSON.stringify(response.data[1]));
                $window.localStorage.setItem('BudgetListper', JSON.stringify(response.data[2]));
                $window.localStorage.setItem('FinalTotalList', JSON.stringify(response.data[1]));
                if (!$scope.BudgetListRecords.length)
                    $scope.AddNewRow();

            });
    }
  //  $scope.GetCustBudgetMastr();



    $scope.GetBudgetOrderDetails = function () {
        //debugger;
        RequestFinalizeFactory.GetBudgetOrderDetails($scope.OrderID)
            .then(function (response) {
                //debugger

                $scope.BudgetList = response.data;
                $scope.BudgetListCal = $scope.BudgetList[($scope.BudgetList.length - 3)];
                $scope.BudgetListper = $scope.BudgetList[($scope.BudgetList.length - 2)];
                $scope.FinalTotalList = $scope.BudgetList[($scope.BudgetList.length - 1)];
                $window.localStorage.setItem('BudgetListcal', JSON.stringify($scope.BudgetListCal));
                $window.localStorage.setItem('FinalTotalList', JSON.stringify($scope.FinalTotalList));
                $window.localStorage.setItem('BudgetListper', JSON.stringify($scope.BudgetListper));

                if (!$scope.BudgetListRecords.length)
                    $scope.AddRow();
            });
    }

    

    $scope.AddEditBudgetOrderDetails = function ()
    {
        debugger;
        var value = $window.localStorage.getItem('EditBudget');
        if (value == 0) {
          
            $scope.GetCustBudgetMastr();
          //  $window.localStorage.removeItem('EditBudget');
            $scope.BudEnableDisable = false;
            $scope.ButtonEnableDisable = false;
        }
        else if (value == 1){
            $scope.GetBudgetOrderDetails();
           // $window.localStorage.removeItem('EditBudget');
            $scope.BudEnableDisable = false;
            $scope.ButtonEnableDisable = true;
        }
        else if (value == 2) {
            $scope.GetBudgetOrderDetails();
           // $window.localStorage.removeItem('EditBudget');
            $scope.BudEnableDisable = true;
            $scope.ButtonEnableDisable = true;
        }
      
    }

    $scope.AddEditBudgetOrderDetails();
}]);
