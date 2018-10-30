HomeApp.controller('AddManufactureController', ['$scope', '$window', 'AddManufactureFactory', function ($scope, $window, AddManufactureFactory) {

    $scope.GetManufactureDetails = function () {
        AddManufactureFactory.GetManufactureData()
             .then(function (response) {
                 if (response.data.length > 0) {
                     $scope.ManufactureList = response.data[0];
                 }
             });
    }

    $scope.GetManufactureDetails();

    $scope.addRow = function () {

        var obj = {};
        $scope.Edit = true;
        obj.IsActive = true;
        obj.ManufacturerId = 0;
        obj.ManufacturerDesc = '';
        obj.UserId = 0;
        obj.Type = 1;
        obj.IsEdit = $scope.Edit;
        obj.Ischange = 1;
        $scope.ManufactureList.push(obj);
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

    $scope.SaveManufacturer = function (ManufactureList) {
        debugger;
        var Manulist =
            {
                Manufacturerdet: JSON.stringify(ManufactureList)
            }
        AddManufactureFactory.SaveManufactureData(Manulist)
            .then(function (response) {
                if (response.data.length > 0) {
                    debugger;
                   
                    if (response.data[1] == "-99") {
                        alert('Manufacture Already Exist.');
                    }
                    else if (response.data[1] == "-98")
                    {
                        alert('Please Enter Value.');
                    }
                    else
                    {
                        $scope.ManufactureList = response.data[0];
                        alert('Request has been saved successfuully.');
                    }
                    
                }
            });
       
    }

    $scope.Back = function () {
        $window.location.href = '/ViewProduct';
    }
}]);