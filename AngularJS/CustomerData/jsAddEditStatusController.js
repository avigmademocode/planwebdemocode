HomeApp.controller('AddEditStatusController', ['$scope', '$window', 'NewRequestFactory', 'AddEditStatusFactory', function ($scope, $window, NewRequestFactory, AddEditStatusFactory) {

    $scope.custstatusdata = {};

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

    $scope.GetStatusDetails = function (StatusId) {
        debugger;
        if (StatusId == '' || StatusId == "" || StatusId == null) {
            StatusId = 0;
        }
        AddEditStatusFactory.GetStatusMasterData(StatusId)
            .then(function (response) {
                if (response.data.length > 0) {
                    debugger;
                    $scope.GetStatusDataList = response.data[0];
                    $scope.custstatusdata = response.data[1];
                    console.log($scope.custstatusdata)
                }
            });
    }
    $scope.GetStatusDetails(0);

    var bool = true;
    function validation() {
        bool = true;
        if ($scope.Name == '' || $scope.Name == 'undefined' || $scope.Name == undefined) {
            bool = false;
            alert('Please Enter Status Name');
            return bool;
        }
        if ($scope.AltName == '' || $scope.AltName == 'undefined' || $scope.AltName == undefined) {
            bool = false;
            alert('Please Enter Alt Name');
            return bool;
        }

        //if ($scope.UserAction == '' || $scope.UserAction == 'undefined' || $scope.UserAction == undefined) {
        //    bool = false;
        //    alert('Please Enter UserAction');
        //    return bool;
        //}
        bool = false;
        for (var i = 0; i < $scope.Finalcustlist.length; i++) {
            if ($scope.Finalcustlist[i].IsCat == true) {
                bool = true;

                return bool;
            }
            else {
                bool = false;
            }
           // return bool;
            
        }
        if (!bool) {
            alert('Please Select Atleast one Customer');
            return bool;
        }
    }
    $scope.Back = function()
    {
        $window.location.href = '/ManageCustomer';
    }
    $scope.UpdateStatus = function (StatusList) {
        debugger;
        var bool = false;
        for (var i = 0; i < StatusList.length; i++)
        {
            if (StatusList[i].IsDelete)
            {
                bool = true;
                break;
            }
        }
        if (!bool)
        {
            bool = validation();
        }
        
        if (bool) {
            var data =
                {
                    StatusId: 0,
                    StatusName: "",
                    AltName: "",
                    StatusData: JSON.stringify(StatusList),
                    Type: 1
                }
            AddEditStatusFactory.SaveStatus(data)
                .then(function (response) {
                    if (response.data.length > 0) {
                        debugger;
                        $scope.GetStatusDataList = response.data[0][0];
                        $scope.custstatusdata = response.data[0][1];
                        if (response.data[2] == -99) {
                            alert('Status Already Exists.');
                        }
                        else {
                            alert('Request has been saved successfully.');
                        }

                    }
                });
        }
        else
        {
            alert("Please Select Minimum One Customer");
        }
        

    }

    $scope.SaveStatus = function () {
        //console.log($scope.Customers)
        debugger;
        var bool = validation();
        if (bool) {


            var data =
                {
                    StatusName: $scope.Name,
                    AltName: $scope.AltName,
                    UserAction: $scope.UserAction,
                    StatusId: $scope.StatusID,
                    CustID: JSON.stringify($scope.Finalcustlist),
                    Type: 2
                }
            AddEditStatusFactory.SaveStatus(data)
                .then(function (response) {
                    if (response.data.length > 0) {
                        debugger;
                        if (response.data[0] == -99)
                        {
                            alert('Status Name Already Exists.');
                        }
                      else  if (response.data[0] == -98)
                        {
                            alert('Alt Name Already Exists.');
                        }

                        else
                        {
                            alert('Request has been saved successfully.');
                        }
                        $scope.GetStatusDataList = response.data[1][0];
                        $scope.custstatusdata = response.data[1][1];
                        

                    }
                });
        }

    }

    $scope.Checked = function () {
        for (var i = 0; i <= $scope.GetStatusDataList.length; i++) {
            $scope.GetStatusDataList[i].IsEdit = false;
            $scope.GetStatusDataList[i].Ischange = 0;

        }
    }

    $scope.EditStatus = function (StatusData) {
        debugger;
         
        $scope.tempcustlist = [];
        $scope.Finalcustlist = [];

        $scope.StatusID = StatusData.StatusId;
        $scope.Name = StatusData.StatusName;
        $scope.AltName = StatusData.AltName;
        $scope.UserAction = StatusData.UserAction;
        for (var i = 0; i < $scope.custstatusdata.length; i++) {
            if ($scope.custstatusdata[i].StatusId == StatusData.StatusId) {

                var obj = {};
                obj.CustId = $scope.custstatusdata[i].CustId;
                obj.IsCat = true;
                obj.ShowStatus = $scope.custstatusdata[i].ShowStatus;
                $scope.tempcustlist.push(obj);


            }

        }
        debugger

        for (var i = 0; i < $scope.custList.length; i++) {
            var obj = {};
            obj.CustId = $scope.custList[i].CustId;
            obj.CustName = $scope.custList[i].CustName;
            obj.ShowStatus = false;
            obj.IsCat = false;
            for (var j = 0; j < $scope.tempcustlist.length; j++)
            {
                
                if ($scope.custList[i].CustId == $scope.tempcustlist[j].CustId) {
                    obj.IsCat = true;
                    obj.ShowStatus = $scope.tempcustlist[j].ShowStatus;
                }

            }

            $scope.Finalcustlist.push(obj);
        }




    }

 
    $scope.ResetData = function () {
        debugger;
        $scope.Name = '';
        $scope.AltName = '';
        $scope.UserAction = 0;
        $scope.StatusId = 0;
        $scope.Finalcustlist = [];
        for (var i = 0; i < $scope.custList.length; i++) {
            var obj = {};
            obj.CustId = $scope.custList[i].CustId;
            obj.CustName = $scope.custList[i].CustName;
            obj.IsCat = false;
            obj.ShowStatus = false;
            $scope.Finalcustlist.push(obj);
        }
    }
    

}]);