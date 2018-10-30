HomeApp.controller('AddNewGrantCodeController', ['$scope', '$window', 'AddNewGrantCodeFactory', 'RequestFinalizeFactory', function ($scope, $window, AddNewGrantCodeFactory, RequestFinalizeFactory) {

    $scope.OrderID = $scope.OrderID ? $scope.OrderID.split('?')[1] : window.location.search.slice(1);
    $scope.Seriallst;
    $scope.grantcodeOrderdatalst;
    $scope.Productdatalst;
    $scope.Serialordlst;
    //$scope.GroupList = [];

    $scope.AddGroup = function (Group) {
        var obj = {
            OrderID: $scope.OrderID,
            GrantCodeMaster: JSON.stringify(Group)
        }
        AddNewGrantCodeFactory.AddNewGrantCode(obj)
           .then(function (response) {
               if (response.data[0].length > 0) {
                   $scope.Seriallst = response.data[2];
                   $scope.grantcodeOrderdatalst = response.data[0];
                   $scope.Productdatalst = response.data[1];
                   $scope.Serialordlst = response.data[3]
                   $window.location.href = '/AddNewGrantCode/AddMultipleGrantCode?' + $scope.OrderID;
               }
           });
    }

    $scope.BackToPrevious = function () {
        debugger;

        $window.location.href = '/ViewRequest?OrderId=' + encodeURIComponent(decodeURIComponent($scope.OrderID));
    }

    $scope.ClearData = function (val) {
        debugger;
        for (var i = 0; i < val.length; i++) {
            val[i].Data = '';
        }
        $scope.GrantCodeData = null;
        $scope.GrantCodeData = val;
    }

    //$scope.GetProdPrice = function (index, key, value) {
    //    debugger;
    //    var id = $scope.Serialordlst[index].SelODID
    //    var Rate;
    //    for (var j = 0; j < $scope.Productdatalst.length; j++) {
    //        if ($scope.Productdatalst[i].ODID == id) {
    //            Rate = $scope.Productdatalst[i].Rate;
    //        }
    //    }
    //}

    $scope.GetNewGrantCode = function () {
        AddNewGrantCodeFactory.GetNewGrantCode($scope.OrderID)
           .then(function (response) {
               if (response.data[0].length > 0) {
                   //console.log(response.data[0])
                   $scope.GrantCodeData = response.data[0];
               }
           });
    }

    //$scope.GrantCodeOrderDetails = function () {
    //    AddNewGrantCodeFactory.GrantCodeOrderDetails($scope.OrderID)
    //        .then(function (response) {
    //            if (response.data[0].length > 0) {
    //                //console.log(response.data[0])
    //                $scope.Seriallst = response.data[2];
    //                $scope.grantcodeOrderdatalst = response.data[0];
    //                $scope.Productdatalst = response.data[1];
    //                $scope.Serialordlst = response.data[3]
    //            }
    //        });
    //}
    //$scope.GrantCodeOrderDetails();

    //$scope.BudgetListRecords = [];
    //$scope.AddNewRow = function (index) {
    //    debugger
    //    var obj = {};
    //    obj.index = index + 1;
    //    //obj.ProdCatDesc = '';
    //    $scope.BudgetListRecords.push(obj);

    //}

    //$scope.Remove = function (index) {
    //    debugger
    //    if ($window.confirm("Do you want to delete: " + name)) {

    //        $scope.BudgetListRecords.splice(index, 1);
    //    }
    //}


    $scope.ViewRequestOrderDetails = function () {
        RequestFinalizeFactory.ViewOrderFinalizeDetails($scope.OrderID)
            .then(function (response) {

                if (response.data[0][0].length != 0) {
                    $scope.RequestDetails = response.data[0][0];
                }
                if (response.data[0][1].length != 0) {
                    $scope.ApprovarDetails = response.data[0][1];
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

}]);