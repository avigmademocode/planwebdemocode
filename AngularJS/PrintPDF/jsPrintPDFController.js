HomeApp.controller('PrintPDFController', ['$scope', '$window', 'PrintPDFFactory', function ($scope, $window, PrintPDFFactory) {

    $scope.OrderID = $scope.OrderID ? $scope.OrderID.split('?')[1] : window.location.search.slice(1);
    $scope.RequestDetails;
    $scope.ViewRequestPrintPDFDetails = function () {
        debugger;
        PrintPDFFactory.ViewPrintPDFData($scope.OrderID)
            .then(function (response) {

                if (response.data[0][0].length != 0) {
                    $scope.RequestDetails = response.data[0][0];
                }
                if (response.data[0][1][0].length != 0) {
                    $scope.ApprovarDetails = response.data[0][1][0];
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
                if (response.data[0][5][0].length != 0) {
                    $scope.Prodcount = response.data[0][5][0].ProdCount;
                }





            });
    }
    $scope.ViewRequestPrintPDFDetails();

    $scope.BackToPrevious = function () {
        debugger;

        $window.location.href = '/ViewRequest?OrderId=' + encodeURIComponent(decodeURIComponent($scope.OrderID));
    }


   
     
    $scope.PrintPDFDataResubmit = function () {

        PrintPDFFactory.AddPrintPDFResubmitDetails(data)
            .then(function (response) {

            });

    }
    $scope.PrintPDFDataBack = function () {

        PrintPDFFactory.AddPrintPDFBackDetails(data)
            .then(function (response) {

            });

    }



    $scope.Approve = function () {

        //  debugger;
        var Data = {
            OrderID: $scope.OrderID,
            Type: 6,
            ChangedStatus: 0,
            Reason: $scope.Reason,
            LeadTime: null,
            IncoID: null,
            FullStatus: null
        }
        PrintPDFFactory.ChangedStatus(Data)
            .then(function (response) {

            })
       // $scope.ViewRequestOrderDetails();
        // $window.location.reload();
    }



    $scope.Denny = function () {

        //  debugger;
        var Data = {
            OrderID: $scope.OrderID,
            Type: 5,
            ChangedStatus: 9,
            Reason: $scope.Reason,
            LeadTime: null,
            IncoID: null,
            FullStatus: null
        }
        PrintPDFFactory.ChangedStatus(Data)
            .then(function (response) {

            })
       // $scope.ViewRequestOrderDetails();
        // $window.location.reload();
    }


    $scope.CancelStatusChange = function () {

        //  debugger;
        var Data = {
            OrderID: $scope.OrderID,
            Type: 2,
            ChangedStatus: 0,  // For Cancel Pass StatusID = 0
            Reason: $scope.Reason, // Resason Value for that Pop up
            LeadTime: null,
            IncoID: null,
            FullStatus: null
        }
        PrintPDFFactory.ChangedStatus(Data)
            .then(function (response) {

            })
       // $scope.ViewRequestOrderDetails();
        // $window.location.reload();
    }

}]);