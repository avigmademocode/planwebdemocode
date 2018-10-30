HomeApp.controller('AddFreightQuoteController', ['$scope', '$window', 'AddFreightQuoteFactory', function ($scope, $window, AddFreightQuoteFactory) {

    $scope.OrderID = $scope.OrderID ? $scope.OrderID.split('?')[1] : window.location.search.slice(1);
    $scope.RequestDetails;
    //$scope.SaveAndNotify = function () {
    //    AddFreightQuoteFactory.saveAndNotify($scope.OrderID, $scope.LeadTime, $scope.Freight, $scope.Tax)
    //        .then(function (response) {
    //            if (response.data > 0) {
    //                alert('Redirect to')
    //                $window.location.href = "/RequestFinalize?" + $scope.OrderID;
    //            }
    //        });

    //    //Call after getting response success
    //}



    $scope.ViewRequestOrderDetails = function () {
        debugger;
        AddFreightQuoteFactory.ViewFreightQuoteDetails($scope.OrderID)
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
                    $scope.Freight.find(v => v.fldType == "String").Data = $scope.RequestDetails[0].IncoTermDesc;
                }


            });
    }
    $scope.ViewRequestOrderDetails();

    $scope.BackToPrevious = function () {
        debugger;

        $window.location.href = '/ViewRequest?OrderId=' + encodeURIComponent(decodeURIComponent($scope.OrderID));
    }


    $scope.SaveAndNotify = function (Freight) {
       // debugger;
        var total = $scope.RequestDetails[0].TotalOrderAmount;
        var Freightlist =
            {
                
                Freightdata: JSON.stringify(Freight),
                TotalAmount: total,
                strOrderID: $scope.OrderID
                
            };


        // var  FreightlistDetails: 
        AddFreightQuoteFactory.AddFreightQuoteDetails(Freightlist)
            .then(function (response) {
                if (response.data.length != 0) {
                    $scope.RequestDetails[0].TotalOrderAmount = response.data[1];
                    alert('Request has been saved successfully.');
                    $window.location.href = '/ViewRequest?OrderId=' + encodeURIComponent(decodeURIComponent($scope.OrderID));
                }
              
            });
                }

}]);