HomeApp.controller('UpdateCategoriesController', ['$scope', '$window', '$location', 'UpdateCategoriesFactory', 'NewRequestFactory', function ($scope, $window, $location, UpdateCategoriesFactory, NewRequestFactory) {
    $scope.display = true;
    $scope.GetCustomersList = function () {
       debugger;
        NewRequestFactory.GetCustomers(2)
            .then(function (response) {
                if (response.data.length != 0) {
                    $scope.custList = response.data[0];
                }
            });
    }
    $scope.GetCustomersList();

    //New Upload File Code
    $scope.SelectedFileForUpload = "";
    $scope.FileDescription = "";

    $scope.files = [];

    //File Select Event
    $scope.SelectFileforUpload = function (file) {
        $scope.files = file[0];
    }

    $scope.Back = function () {
        $window.location.href = '/ViewProduct';
    }

    $scope.UpdateCategories = function () {
        $scope.showLoader = true;
        $scope.display = false;
        var CustID = $scope.SelectedCustomer;
        if (CustID != 'undifined' && CustID != '' && CustID != null) {
            $scope.FileDescription = CustID;
            var data = new FormData();
            data.append("files[0]", $scope.files)
            data.append("description[0]", $scope.FileDescription);
            UpdateCategoriesFactory.UploadFile(data)
                .then(function (response) {
                    alert('Total Number of Record ' + response.data[0].TotalReccount +
                        '  \n Total Number of Record Processed ' + response.data[0].TotaProcesscount);
                    //debugger;
                    document.getElementById("FileUpload").value = "";
                    $scope.SelectedCustomer = '';
                    $scope.showLoader = false;
                    $scope.display = true;
                   // $window.location.reload();
                });
        }
        else {
            alert('Invalid Customer!');
        }
    }

}]);