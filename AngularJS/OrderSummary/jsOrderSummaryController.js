HomeApp.controller('OrderSummaryController', ['$scope', '$window', 'OrderSummaryFactory', function ($scope, $window, OrderSummaryFactory) {
 
    var orderID = $window.localStorage.getItem('orderID');
    var CustId = $window.localStorage.getItem('CustomerId');
    $scope.OrderID = $scope.OrderID ? $scope.OrderID.split('?')[1] : window.location.search.slice(1);
    $scope.productcart = [];
    $scope.Approver = [];
    //var orderID = 3;
    //var CustId = 2;
    var UserId = 2;
    $scope.btnHide;
    $scope.btnUnHide;
    $scope.btnSubmithide;
    $scope.btnITUnHide = true;
    $scope.GetOrderDetails = function () {
        debugger;

        if ($scope.OrderID == null || $scope.OrderID == undefined || $scope.OrderID == 'undefined' || $scope.OrderID == '')
        {
            $scope.btnHide = true;
            $scope.btnUnHide = false;
            $scope.btnSubmithide = true;
            if (orderID != null && orderID != undefined && orderID != '' && CustId != null && CustId != undefined && CustId != '' && UserId != null && UserId != undefined && UserId != '')
            {
                //$scope.GetOrderSummary();
                debugger;
                OrderSummaryFactory.getOrderSummary(orderID, CustId, UserId,1)
                    .then(function (response) {
                        if (response.data.length > 0) {
                            $scope.Summary = response.data[0][0];
                            $scope.Approver = response.data[1];
                            $scope.ProductSummary = response.data[2];
                            $scope.LineItems = $scope.ProductSummary.length;
                            $scope.SAdd1 = response.data[3][0];
                            $scope.SAdd2 = response.data[3][1];
                            $scope.SAdd3 = response.data[3][2];
                            $scope.SCity = response.data[3][3];
                            $scope.SState = response.data[3][4];
                            $scope.SZip = response.data[3][5];
                            $scope.SCountry = response.data[3][6];

                            $scope.BAdd1 = response.data[4][0];
                            $scope.BAdd2 = response.data[4][1];
                            $scope.BAdd3 = response.data[4][2];
                            $scope.BCity = response.data[4][3];
                            $scope.BState = response.data[4][4];
                            $scope.BZip = response.data[4][5];
                            $scope.BCountry = response.data[4][6];





                        }
                    });
            }
            else
            {
                //$window.location.href = '/NewRequest';
            }
        }

        else
        {
            debugger;
            $scope.btnHide = false;
            $scope.btnUnHide = true;

            $scope.btnSubmithide = false;
            $scope.btnITUnHide = false;

            OrderSummaryFactory.getOrderSummary($scope.OrderID, 0, 0, 2)
                        .then(function (response) {
                            if (response.data.length > 0) {
                                $scope.Summary = response.data[0][0];
                                $scope.Approver = response.data[1];
                                $scope.ProductSummary = response.data[2];
                                $scope.LineItems = $scope.ProductSummary.length;
                                $scope.SAdd1 = response.data[3][0];
                                $scope.SAdd2 = response.data[3][1];
                                $scope.SAdd3 = response.data[3][2];
                                $scope.SCity = response.data[3][3];
                                $scope.SState = response.data[3][4];
                                $scope.SZip = response.data[3][5];
                                $scope.SCountry = response.data[3][6];

                                $scope.BAdd1 = response.data[4][0];
                                $scope.BAdd2 = response.data[4][1];
                                $scope.BAdd3 = response.data[4][2];
                                $scope.BCity = response.data[4][3];
                                $scope.BState = response.data[4][4];
                                $scope.BZip = response.data[4][5];
                                $scope.BCountry = response.data[4][6];





                            }
                        });
       
     
        }
       

    }
    $scope.SaveOrderDetails = function (val) {
        debugger;
        var orddata = {}
        if (val == 2)
        {
              orddata =
                {
                    strorderID: orderID,
                    strCustId: CustId,
                    type: val

                };
        }
        else if (val == 3)
        {
            orddata =
                {
                strorderID: $scope.OrderID,
                    strCustId: CustId,
                    type: val

                };
        }
      


        // var  FreightlistDetails: 
        OrderSummaryFactory.SaveOrderSummary(orddata)
            .then(function (response) {
                if (response.data.length != 0) {
                    debugger;
                    alert('Request has been saved successfully.');
                    if (val != 2)
                    {
                        $window.location.href = '/ViewRequest?OrderId=' + encodeURIComponent(decodeURIComponent($scope.OrderID));
                    }
                    else
                    {
                        $scope.OrderID = response.data[0];
                        $scope.Prodlst = response.data[1][0];
                        $window.localStorage.removeItem('productlist');
                        for (var i = 0; i < $scope.Prodlst.length; i++) {
                            debugger
                            var data =
                                {
                                    ODID: $scope.Prodlst[i].ODID,
                                    ProdID: $scope.Prodlst[i].ProductId,
                                    ProdName: $scope.Prodlst[i].ProductName,
                                    ProdPrice: $scope.Prodlst[i].Rate,
                                    TotalPrice: $scope.Prodlst[i].Amount,
                                    Quantity: $scope.Prodlst[i].Qty,
                                }

                            $scope.productcart.push(data)

                        }
                        $window.localStorage.setItem('productlist', JSON.stringify($scope.productcart));
                    }
                   
                   
                    

                    $scope.btnHide = false;
                    $scope.btnUnHide = true;
                    $scope.btnSubmithide = false;

                    if (val == 2)
                    {
                        $scope.btnITUnHide = false;

                    }
                }
            });
    }

    $scope.GetOrderDetails();


    $scope.BackToPrevious = function ()
    {
        debugger;
        if ($scope.OrderID == null || $scope.OrderID == undefined || $scope.OrderID == 'undefined' || $scope.OrderID == '')
        {
            $window.location.href = '/OrderCart';
        }
        else
        {
            $window.location.href = '/OrderCart?' + encodeURIComponent(decodeURIComponent($scope.OrderID));
        }
       
    }

    $scope.AddITSetup = function (val) {

        $window.location.href = '/Itsetup?' + encodeURIComponent(decodeURIComponent($scope.OrderID));
    }


    $scope.Next = function () {
        debugger;

        $window.location.href = '/ViewRequest?OrderId=' + encodeURIComponent(decodeURIComponent($scope.OrderID));
    }

}]);
