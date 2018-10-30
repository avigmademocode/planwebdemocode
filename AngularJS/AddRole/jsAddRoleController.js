HomeApp.controller('AddRoleController', ['$scope', '$window', 'AddRoleFactory', function ($scope, $window, AddRoleFactory) {


    $scope.GetRoleDetails = function () {
        debugger;
        AddRoleFactory.GetRoleData()
            .then(function (response) {
                debugger;
                if (response.data.length > 0) {
                    $scope.RoleList = response.data[0];
                }
            });
    }

    $scope.GetRoleDetails();

    $scope.addRow = function () {

        var obj = {};
        $scope.Edit = true;
        obj.IsActive = true;
        //change
        obj.RoleId = 0;
        obj.RoleName = '';
        obj.Description = '';
        obj.UserId = 0;
        obj.Type = 1;
        obj.IsEdit = $scope.Edit;
        obj.Ischange = 1;
        $scope.RoleList.push(obj);
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

    $scope.SaveRoleData = function (RoleList) {
        debugger;
        var Manulist =
        {
            RoleDesc: JSON.stringify(RoleList)
        }
        AddRoleFactory.SaveRoleData(Manulist)
            .then(function (response) {
                if (response.data.length > 0)
                {
                    debugger;

                    if (response.data[0] == "-99") {
                        alert('User Role Already Exist.');
                    }
                    else if (response.data[0] == "-98") {
                        alert('Please Enter Value.');
                    }
                    else
                    {
                        $scope.RoleList = response.data[1][0];
                        alert('Request has been Updated successfuully.');
                    }

                }
            });

    }

    $scope.Back = function () {
        $window.location.href = '/User_Master/UserView';
    }
}]);