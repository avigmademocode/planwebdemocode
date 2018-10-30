HomeApp.controller('AddMultiGrantCodeEditController', ['$scope', '$window', 'AddMultiGrantCodeFactory', 'ViewRequestFactory', function ($scope, $window, AddMultiGrantCodeFactory, ViewRequestFactory) {


    $scope.hideFlag = true;
    $scope.showFlag = false;

    $scope.btnShowHide = true;


    $scope.OrderID = $scope.OrderID ? $scope.OrderID.split('?')[1] : window.location.search.slice(1);
    $scope.Seriallst = {};
    $scope.grantcodeOrderdatalst;
    $scope.Productdatalst;
    $scope.Serialordlst;
    $scope.AddGroup = function (Group) {
        var obj = {
            OrderID: $scope.OrderID,
            GrantCodeMaster: JSON.stringify(Group)
        }
        AddMultiGrantCodeFactory.AddNewGrantCode(obj)
            .then(function (response) {
                if (response.data[0].length > 0) {
                    $scope.Seriallst = response.data[1];
                   
                    $scope.grantcodeOrderdatalst = response.data[0];
                    $scope.Productdatalst = response.data[1];


                }
            });
    }

    $scope.GetNewGrantCode = function () {
        AddMultiGrantCodeFactory.GetNewGrantCode($scope.OrderID)
            .then(function (response) {
                if (response.data[0].length > 0) {
                    $scope.GrantCodeData = response.data[0];

                }
            });
    }
    $scope.ClearData = function (val) {
        debugger;
        for (var i = 0; i < val.length; i++) {
            val[i].Data = '';
        }
        $scope.GrantCodeData = null;
        $scope.GrantCodeData = val;
    }
    $scope.GrantCodeOrderDetails = function () {
        //debugger;
        if ($window.localStorage.getItem("IsEdit") == 'True') {
            //alert('Ready To Edit.');
            AddMultiGrantCodeFactory.ViewGrantCodeOrderDetails($scope.OrderID)
                .then(function (response) {
                    if (response.data[0].length > 0) {
                        //debugger;

                        $scope.Seriallst = response.data[1];
                        $scope.grantcodeOrderdatalst = response.data[0];
                        $scope.OriginalTotalOrderAmount = response.data[2][0].TotalOrderAmount
                    }
                });
        }
        else {
            AddMultiGrantCodeFactory.GrantCodeOrderDetails($scope.OrderID)
                .then(function (response) {
                    if (response.data[0].length > 0) {
                        //debugger;
                        $scope.Seriallst = response.data[1];
                        $scope.grantcodeOrderdatalst = response.data[0];
                        $scope.OriginalTotalOrderAmount = response.data[2][0].TotalOrderAmount
                    }
                });
        }
    }
    $scope.GrantCodeOrderDetails();

    $scope.BackToPrevious = function () {
        debugger;

        $window.location.href = '/ViewRequest?OrderId=' + encodeURIComponent(decodeURIComponent($scope.OrderID));
    }

    $scope.SaveData = function (datalst) {
        console.log(datalst)
        if ($scope.val) {


            var data =
                {
                    OrderID: $scope.OrderID,
                    GrantCodeMaster: JSON.stringify(datalst)
                }
            AddMultiGrantCodeFactory.SaveGrantData(data)
                .then(function (response) {
                    if (response.data[0].length > 0) {
                        alert('Request Saved Sucessfully');
                    }
                });
        }
        else {
            alert('Please check the Total Amount & Quantity');
        }
    }

    $scope.AddNewRow = function (index, row) {
        var obj = {};
        obj.Items = $scope.Seriallst[row - 1].Data[0].Items;
        obj.SelAmount = '';
        obj.SelectedItem = '';
        obj.SelODID = '';
        obj.SelQty = 1;
        obj.SelRate = '';
        obj.SelSubTotal = '';
        $scope.Seriallst[row - 1].Data.push(obj);
    }

    $scope.Remove = function (index, row) {
        if ($window.confirm("Do you want to delete: " + name)) {
            $scope.Seriallst[row - 1].Data.splice(index, 1);
        }
    }
    $scope.TotalAmount = 0;
    $scope.val = true;
    $scope.CalcTotalAmt = function (AmtData) {
        //debugger;
        var TotalAmt = 0;
        var currQty = 0;
        var currProdname;
        var currODID = 0;
        $scope.val = true;
        $scope.TotalAmount = 0;
        for (var i = 0; i < AmtData.length; i++) {
            TotalAmt = 0
            for (var j = 0; j < AmtData[i].Data.length; j++) {
                if (AmtData[i].Data[j].SelectedItem != undefined && AmtData[i].Data[j].SelectedItem != null && AmtData[i].Data[j].SelectedItem != "") {

                    currProdname = AmtData[i].Data[j].SelectedItem.split('\,')[3];

                    currODID = AmtData[i].Data[j].SelectedItem.split('\,')[0];
                    AmtData[i].Data[j].SelODID = currODID;
                    for (var k = 0; k < AmtData[0].Data[0].Items.length; k++) {
                        if (AmtData[0].Data[0].Items[k].ODID == currODID) {
                            currQty = AmtData[0].Data[0].Items[k].Qty;
                        }
                    }
                    if (currQty < AmtData[i].Data[j].SelQty) {

                        if (currQty == AmtData[i].Data[j].SelQty) {
                            $scope.val = true;
                        }
                        else {
                            $scope.val = false;
                            alert('Please set the Quantity less than ' + currQty + '  for this product   ' + currProdname);
                            break;
                        }

                    }
                    TotalAmt += AmtData[i].Data[j].SelectedItem.split('\,')[1] * AmtData[i].Data[j].SelQty;
                    AmtData[i].Data[j].SelSubTotal = TotalAmt;
                    AmtData[i].Data[j].SelRate = AmtData[i].Data[j].SelectedItem.split('\,')[1];
                    AmtData[i].Data[j].SelAmount = AmtData[i].Data[j].SelectedItem.split('\,')[1] * AmtData[i].Data[j].SelQty;


                }
                else {
                    TotalAmt += 0;
                }
            }

            $scope.Seriallst[i].TotalAmt = TotalAmt;
        }

        for (var k = 0; k < $scope.Seriallst.length; k++) {
            $scope.TotalAmount += $scope.Seriallst[k].TotalAmt
        }
        if ($scope.OriginalTotalOrderAmount < $scope.TotalAmount) {
            $scope.val = false;
            alert('Amount is greater than Total Amount');
        }
    }


    //show 
    $scope.showDropdown = function () {
        //alert("hey");
        $scope.showFlag = true;
        $scope.hideFlag = false;
    }


    $scope.GrantCodeOrderDetailsTemp = function () {
        ViewRequestFactory.GrantCodeOrderDetails($scope.OrderID)
            .then(function (response) {
                if (response.data[0].length > 0) {
                    //$scope.CalcTotalAmt(response.data[1]);
                    //debugger
                    $scope.SeriallstTemp = response.data[1];
                    $scope.grantcodeOrderdatalst = response.data[0];
                    $scope.OriginalTotalOrderAmount = response.data[2][0].TotalOrderAmount
                }
            });
    }
    $scope.GrantCodeOrderDetailsTemp();


}]);