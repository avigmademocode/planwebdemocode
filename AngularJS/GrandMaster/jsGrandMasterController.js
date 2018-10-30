HomeApp.controller('GrandMasterController', ['$scope', '$window', 'NewRequestFactory', 'GrandMasterFactory', function ($scope, $window, NewRequestFactory, GrandMasterFactory) {

    var CustomerID = $window.localStorage.getItem('MCustID');
    var qvalue = window.location.search.slice(5);
     
    //alert(CustomerID);
     
    $scope.Title
    $scope.GetGrandMasterDetails = function (CustomerID) {
        debugger
        if (qvalue == 1) {

            $scope.Title = "Grant Title";
            $scope.btnTitle = "+ Add more Grant Title";
            $scope.btnsaveTitle = "Save Grant Title";
        }
        else {

            $scope.Title = "Budget Title";
            $scope.btnTitle = "+ Add more Budget Title";
            $scope.btnsaveTitle = "Save Budget Title";

        }


        var data =
            {
                value: qvalue,
                CustId: CustomerID
            }
        GrandMasterFactory.GetGrandMasterData(data)
            .then(function (response) {
                if (response.data.length > 0) {
                    $scope.CustomerId = parseInt(CustomerID);
                    $scope.GrandMasterList = response.data[0];
                    $window.localStorage.setItem('OrgGrandMasterList', JSON.stringify(response.data[0]));
                }
            });
    }

    $scope.GetGrandMasterDetails(CustomerID);
 
    $scope.GetCustomersList = function () {
        NewRequestFactory.GetCustomers(1)
            .then(function (response) {
                if (response.data.length != 0) {
                    debugger;
                    $scope.custList = response.data[0];
                    
                        for (var i = 0; i < $scope.custList.length; i++)
                        {
                            if (qvalue != 1)
                            {
                                if ($scope.custList[i].UseItemGroups) {
                                    $scope.custList.splice(i, 1);
                                }
                            }
                            else
                            {
                                $scope.custList.splice(i, 1);
                            }
                        }
                    
                    
                }
                else {
                    alert('Failled!!!');
                }
            });
    }
    $scope.GetCustomersList();

    $scope.Back = function () {
        $window.location.href = '/ManageCustomer';
    }

    $scope.addRow = function () {

        var obj = {};
        $scope.Edit = true;
        obj.IsActive = true;
        obj.GrantBudgetId = 0;
        obj.GrantBudgetTitle = '';
        obj.Serial = 0;
        obj.DependOn = 0;
        obj.isRequired = false;
        obj.fldlength = 0;
        obj.UserId = 0;
        obj.Type = 1;
        obj.IsEdit = $scope.Edit;
        obj.Ischange = 1;


        $scope.GrandMasterList.push(obj);
    };

    $scope.CheckVal = true;
    $scope.ChangeVal = function (index)
    {
        debugger;
        $scope.CheckVal = true;
        $scope.OrgGrandMasterList = JSON.parse($window.localStorage.getItem('OrgGrandMasterList'));
       

        if ($scope.OrgGrandMasterList.length != 0) {

            if ($scope.Addval) {
                var currserial = $scope.GrandMasterList[index].Serial;
                var orgserial = $scope.OrgGrandMasterList[index-1].Serial;
                var minval = Math.min.apply(Math, $scope.OrgGrandMasterList.map(function (item) { return item.Serial; }));
                //var maxval = Math.max.apply(Math, $scope.OrgGrandMasterList.map(function (item) { return item.Serial; }));
                var maxval = $scope.GrandMasterList.length;
                if (minval < currserial && currserial < maxval) {
                    for (var i = 0; i < $scope.GrandMasterList.length; i++) {

                        if ($scope.OrgGrandMasterList[i].Serial == currserial) {
                            var GrantBudgetId = $scope.OrgGrandMasterList[i].GrantBudgetId;
                            try
                            {
                                $scope.GrandMasterList.find(v => v.GrantBudgetId == GrantBudgetId).Serial = orgserial;
                            }
                            catch (e)
                            {
                                alert(e);
                            }

                            localStorage.removeItem('OrgGrandMasterList');
                            $window.localStorage.setItem('OrgGrandMasterList', JSON.stringify($scope.GrandMasterList));
                            break;

                        }
                    }
                }
               
            }
            else
            {
                var currserial = $scope.GrandMasterList[index].Serial;
                var orgserial = $scope.OrgGrandMasterList[index].Serial;
                var minval = Math.min.apply(Math, $scope.OrgGrandMasterList.map(function (item) { return item.Serial; }));
               // var maxval = Math.max.apply(Math, $scope.OrgGrandMasterList.map(function (item) { return item.Serial; }));
                var maxval = $scope.GrandMasterList.length;
                if (minval < currserial && currserial < maxval) {
                    for (var i = 0; i < $scope.GrandMasterList.length; i++) {

                        if ($scope.OrgGrandMasterList[i].Serial == currserial) {
                            var GrantBudgetId = $scope.OrgGrandMasterList[i].GrantBudgetId;
                            try {
                                $scope.GrandMasterList.find(v => v.GrantBudgetId == GrantBudgetId).Serial = orgserial;
                            }
                            catch (e) {
                                alert(e);
                            }

                            localStorage.removeItem('OrgGrandMasterList');
                            $window.localStorage.setItem('OrgGrandMasterList', JSON.stringify($scope.GrandMasterList));
                            break;




                        }
                    }
                }
                else
                {
                    $scope.CheckVal = false;
                    alert('Please Enter Value Less than ' + maxval + '  and Greater than  ' + minval);
                }
            }

            

        }
        else
        {
            $scope.Addval = true;
            localStorage.removeItem('OrgGrandMasterList');
            $window.localStorage.setItem('OrgGrandMasterList', JSON.stringify($scope.GrandMasterList));
        }
     
    
      


    };

    //$scope.removeRow = function (value) {
    //    alert(value)
    //    var arrObj = [];
    //    angular.forEach($scope.ManufactureList, function (value) {
    //        if (!value.Remove) {
    //            arrObj.push(value);
    //        }
    //    });
    //    $scope.ManufactureList = arrObj;
    //};

    //$scope.UpdateStatus = function (INDEX) {
    //    alert(INDEX)
    //    debugger
    //    for (var i = 0; i < $scope.ManufactureList.length; i++) {
    //        if (i = INDEX) {
    //            $scope.ManufactureList.IsActive = true;
    //        }
    //    }
    //}

    var bool = true;
    function validation(GrandMasterList)
    {
        var bool = true;
        for (var i = 0; i < GrandMasterList.length; i++)
        {
            if (GrandMasterList[i].GrantBudgetTitle == '' || GrandMasterList[i].GrantBudgetTitle == undefined || GrandMasterList[i].GrantBudgetTitle == 'undefined')
            {
                bool = false;
                return bool;
            }
        }
    }

    $scope.SaveManufacturer = function (GrandMasterList) {
        debugger;

        var bool = true;
        for (var i = 0; i < GrandMasterList.length; i++) {
            if (GrandMasterList[i].GrantBudgetTitle == '' || GrandMasterList[i].GrantBudgetTitle == undefined || GrandMasterList[i].GrantBudgetTitle == 'undefined')
            {
                bool = false;
                alert( 'Please Enter Value in  ' + $scope.Title );
                break;
            }
            if (GrandMasterList[i].fldlength == 0 || GrandMasterList[i].fldlength == undefined || GrandMasterList[i].fldlength == 'undefined') {
                bool = false;
                alert('Please Enter Value Greater than 0 in Field Length');
                break;
            }

        }
        if ($scope.CheckVal ) {
            if (bool) {
                var Manulist =
                    {
                        CustId: $scope.CustomerId,
                        value: qvalue,
                        GrantBudgeMsterlist: JSON.stringify(GrandMasterList)
                    }
                GrandMasterFactory.SaveGrandMasterData(Manulist)
                    .then(function (response) {
                        if (response.data.length > 0) {
                            debugger;
                            $scope.GrandMasterList = response.data[1][0];
                            alert('Request has been saved successfully.');
                        }
                    });
            }

        }
        else
        {
            alert('Please Check the Serial Enter');
        }
           

    }


}]);