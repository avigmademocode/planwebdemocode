HomeApp.controller('LanguageController', ['$scope', '$window', 'NewRequestFactory', 'LanguageFactory', function ($scope, $window, NewRequestFactory,LanguageFactory) {

    $scope.GetCustomersList = function (val) {
        //debugger;
        NewRequestFactory.GetCustomers(val)
            .then(function (response) {
                if (response.data.length != 0) {
                    $scope.custList = response.data[0];
                }
            });
    }
    $scope.GetCustomersList(2);


    $scope.GetLanguageDetails = function (CustID) {
        debugger;
        if (CustID == '' || CustID == "" || CustID == null) {
            CustID = 0;
        }
        LanguageFactory.GetLanguageData(CustID)
            .then(function (response) {
                if (response.data.length > 0) {
                    $scope.LanguageList = response.data[0];
                    $scope.CustLanguageList = response.data[1];
                }
            });
    }

    $scope.GetLanguageDetails(0);

    $scope.addRow = function () {

        var obj = {};
        $scope.Edit = true;
        obj.IsActive = true;
        obj.LanguageId = 0;
        obj.LanguageName = '';
        obj.UserId = 0;
        obj.Type = 1;
        obj.IsEdit = $scope.Edit;
        obj.Ischange = 1;
        $scope.LanguageList.push(obj);
    };

    $scope.Back = function () {
        $window.location.href = '/ManageCustomer';
    }

    $scope.Checked = function () {
        for (var i = 0; i <= $scope.LanguageList.length; i++) {
            $scope.LanguageList[i].IsEdit = false;
            $scope.LanguageList[i].Ischange = 0;

        }
    }
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

    $scope.EditStatus = function (LanguageData) {
        debugger;

        $scope.tempcustlist = [];
        $scope.Finalcustlist = [];

        $scope.LanguageID = LanguageData.LanguageId;
        $scope.LanguageName =LanguageData.LanguageName;
 
        for (var i = 0; i < $scope.CustLanguageList.length; i++) {
            if ($scope.CustLanguageList[i].LanguageId == LanguageData.LanguageId) {

                var obj = {};
                obj.CustId = $scope.CustLanguageList[i].CustId;
                obj.IsCat = true;
                $scope.tempcustlist.push(obj);


            }

        }
        debugger

        for (var i = 0; i < $scope.custList.length; i++) {
            var obj = {};
            obj.CustId = $scope.custList[i].CustId;
            obj.CustName = $scope.custList[i].CustName;
            obj.IsCat = false;
            for (var j = 0; j < $scope.tempcustlist.length; j++) {
                if ($scope.custList[i].CustId == $scope.tempcustlist[j].CustId) {
                    obj.IsCat = true;
                }

            }

            $scope.Finalcustlist.push(obj);
        }




    }
    var bool = true;
    function validation() {
        bool = true;
        if ($scope.LanguageName == '' || $scope.LanguageName == 'undefined' || $scope.LanguageName == undefined) {
            bool = false;
            alert('Please Enter Language Name');
            return bool;
        }
  
        bool = false;
        for (var i = 0; i < $scope.Finalcustlist.length; i++) {
            if ($scope.Finalcustlist[i].IsCat == true) {
                bool = true;

                return bool;
            }
            else {
                bool = false;
            }

        }
        if (!bool) {
            alert('Please Select Atleast one Customer');
            return bool;
        }
        return bool;
    }
    $scope.SaveStatus = function () {
        //console.log($scope.Customers)
        debugger;
        var bool = validation();
        if (bool) {


            var data =
                {
                    LanguageName: $scope.LanguageName,
                    LanguageId: $scope.LanguageID,
                    Customerdet: JSON.stringify($scope.Finalcustlist),
                    Type: 2
                }
            LanguageFactory.SaveLanguageData(data)
                .then(function (response) {
                    if (response.data.length > 0) {
                        debugger;
                        $scope.LanguageList = response.data[1][0];
                        $scope.CustLanguageList = response.data[1][1];
                        if (response.data[0] == -99) {
                            alert('Language Already Exists.');
                        }
                        else {
                            alert('Request has been saved successfully.');
                        }

                    }
                });
        }

    }


    $scope.SaveManufacturer = function (LanguageList) {
        debugger;
        var Manulist =
            {
                Type : 1,
                Languagesdet: JSON.stringify(LanguageList)

            }
        LanguageFactory.SaveLanguageData(Manulist)
            .then(function (response) {
                if (response.data.length > 0) {
                    debugger;
                    $scope.LanguageList = response.data[1][0];
                    alert('Request has been saved successfully.');
                }
            });

    }

    $scope.ResetData = function () {
        debugger;
        $scope.LanguageName = '';
        $scope.LanguageID = 0;
        $scope.Finalcustlist = [];
        for (var i = 0; i < $scope.custList.length; i++) {
            var obj = {};
            obj.CustId = $scope.custList[i].CustId;
            obj.CustName = $scope.custList[i].CustName;
            obj.IsCat = false;
            $scope.Finalcustlist.push(obj);
        }
    }
}]);