HomeApp.controller('AddMultiProductController', ['$scope', '$window', '$location', 'AddMultiProductFactory', 'ViewProductFactory', function ($scope, $window, $location, AddMultiProductFactory, ViewProductFactory) {

    //$scope.OrderID = $scope.OrderID ? $scope.OrderID.split('?')[1] : window.location.search.slice(9);

    $scope.customerList

    //New Upload File Code
    $scope.SelectedFileForUpload = "";
    $scope.FileDescription = "";

    $scope.files = [];
    $scope.display = true;
    //File Select Event
    $scope.SelectFileforUpload = function (file) {
        //for single file
        $scope.SelectedFileForUpload = file[0];

        //for multiple files
        // STORE THE FILE OBJECT IN AN ARRAY.
        for (var i = 0; i < file.length; i++) {
            $scope.files.push(file[i])
        }


    }
    $scope.Back = function () {
        $window.location.href = '/ViewProduct';
    }



    document.getElementById("radiobtn").checked;

    //Save File
    $scope.SaveFile = function () {
        debugger
        $scope.showLoader = true;
        $scope.display = false;
        var tierval;
        var CustomerID = $scope.CustomerID;
        if ($scope.Tier != undefined)
        {
            tierval = $scope.Tier;
        }
        else
        {
            tierval = 0;
        }

        $scope.FileDescription = $scope.CustomerID + tierval;
        var data = new FormData();
        for (var i = 0; i < $scope.files.length; i++) {
            data.append("files[" + i + "]", $scope.files[i])
            data.append("description[" + i + "]", tierval + "," + CustomerID);

        }
        console.log(data)
        AddMultiProductFactory.UploadFile(data)
            .then(function (response) {
                debugger;
                $scope.UploadedFiles = response.data[0];
                alert('Total Number of Record ' + response.data[0].TotalReccount +
                    '  \n Total Number of Record Processed ' + response.data[0].TotaProcesscount +
                    '  \n Total Number of Duplicate Record  ' + response.data[0].TotalDupcount);
                ResetData();
              
       
                $scope.showLoader = false;
                $scope.display = true;
                $window.location.reload();
            });
    }


    //Dynamic File Upload
    $scope.uploadfiles = [];

    $scope.GetCustomerData = function () {
        ViewProductFactory.GetProductsData()
            .then(function (response) {
                if (response.data.length != 0) {
                    $scope.customerList = response.data[0][0];
                    $scope.custList = $scope.customerList;
                }
            });
    }
    $scope.GetCustomerData();


    function ResetData()
    {
        //debugger;
        $scope.custList = $scope.customerList;
        $scope.SelectedCustomer = '';
        document.getElementById("FileUpload").value = "";
        $scope.Tier = 0;
    }

}]);