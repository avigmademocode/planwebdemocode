HomeApp.controller('OrderCartController', ['$scope', '$window', 'OrderCartFactory', function ($scope, $window, OrderCartFactory) {
    //debugger;
    //$scope.TotalPrice = 0;
    //$scope.CartList = [];
    $scope.CartList = JSON.parse($window.localStorage.getItem('productlist'));
    $scope.SubTotal = 0;
    var data = [];
    $scope.OrderID = $scope.OrderID ? $scope.OrderID.split('?')[1] : window.location.search.slice(1);
    //if ($scope.CartList1.length > 0) {
    //    debugger
    //    //alert($scope.CartList1.length)
    //    for (var i = 0; i < $scope.CartList1.length; i++) {
    //        var data = [{
    //            ProdID: $scope.CartList1[i].ProdID,
    //            ProdName: $scope.CartList1[i].ProdName,
    //            ProdPrice: $scope.CartList1[i].ProdPrice,
    //            TotalPrice: $scope.CartList1[i].ProdPrice,
    //            Quantity: 1,
    //        }]
    //        $scope.CartList.push = data;
    //    }

    //}

    //console.log('CartList -', JSON.parse($scope.CartList));

    //$scope.ProductList = [];
    //$scope.Quantity = 9;
    
    $scope.getTotalPriceLoad = function () {
      //  debugger;
        var total = 0;

         
        for (var i = 0; i < $scope.CartList.length; i++) {
            var item = $scope.CartList[i];
            total += (item.Quantity * item.ProdPrice);

        }
        $scope.SubTotal = total;
    }
    $scope.getTotalPriceLoad();
    $scope.CalculatePrice = function (ProdID, Quantity) {
      //  debugger
        for (var i = 0; i < $scope.CartList.length; i++) {
            //var Quantity1 = $scope.CartList[i].Quantity
            if ($scope.CartList[i].ProdID == ProdID) {
                data = {
                    ProdID: $scope.CartList[i].ProdID,
                    ProdName: $scope.CartList[i].ProdName,
                    ProdPrice: $scope.CartList[i].ProdPrice,
                    TotalPrice: $scope.CartList[i].ProdPrice * Quantity,
                    Quantity: Quantity,
                }
                $scope.CartList1.push(data);
            }
            else {
                $scope.CartList1.push($scope.CartList[i]);
            }
        }
        $scope.CartList = [];
        $scope.CartList = $scope.CartList1;
    }
    //$scope.CalculatePrice();



    function cartItem(sku, name, price, quantity) {
        ProdID = sku;
        ProdName = name;
        ProdPrice = price * 1;
        Quantity = quantity * 1;
        TotalPrice = price * quantity;
    }


    $scope.CalculatePrice = function (ProdID, quantity, price, name) {
        //debugger;

        if (quantity != 0) {

            // update quantity for existing item
            var found = false;
            for (var i = 0; i < $scope.CartList.length && !found; i++) {
                var item = $scope.CartList[i];
                if ($scope.CartList[i].ProdID == ProdID) {
                    found = true;
                    item.Quantity = quantity;
                    item.TotalPrice = quantity * price;
                    if (item.quantity <= 0)
                    {
                        this.$scope.CartList.splice(i, 1);
                    }
                }
            }

            // new item, add now
            if (!found)
            {
                var item = new cartItem(ProdID, name, price, quantity);
                $scope.CartList.push(item);
            }

            // save changes
            $scope.getTotalPrice(ProdID);
            this.saveItems();
        }
    }


    // save items to local storage
    $scope.saveItems = function () {
       // debugger;
        if (localStorage != null && JSON != null) {
            localStorage["productlist"] = JSON.stringify($scope.CartList);
        }
    }

    $scope.RemoveProduct = function (index, ProdID) {
        if (confirm('Are you sure want to remove item from cart?')) {
           // debugger
            var index = -1;
            var dataArr = eval($scope.CartList);
            for (var i = 0; i < dataArr.length; i++) {
                if (dataArr[i].ProdID == ProdID) {
                    index = i;
                    break;
                }
            }
            $scope.CartList.splice(index, 1);
            $scope.getTotalPriceLoad();
            $window.localStorage.setItem('productlist', JSON.stringify($scope.CartList));
        }
    }

// get the total price for all items currently in the cart
    $scope.getTotalPrice = function (ProdID) {
        //debugger;
        var total = 0;
        for (var i = 0; i < $scope.CartList.length; i++) {
            var item = $scope.CartList[i];
             total += (item.Quantity * item.ProdPrice);
            
        }
        $scope.SubTotal = total;
    }

    $scope.SaveOrderDetails = function (CartList) {
       //debugger;
      //  var OrderID = 5;
        var OrderID = $window.localStorage.getItem('orderID');
        var CustomerID = $window.localStorage.getItem('CustomerId');
        var ProductList = JSON.stringify($window.localStorage.getItem('productlist'));
        var SubTotalAmount = $scope.SubTotal;
        var orderlength;
       
         /*   for (var i = 0; i < CartList.length; i++)
            {
              
                OrderCartFactory.saveOrderDetails(OrderID, CustomerID, SubTotalAmount, CartList[i])
                    .then(function (response) {
                        orderlength = i;
                        //alert('Review your order...');
                    });
            }*/
            var val = 0
        if ($scope.OrderID == null || $scope.OrderID == undefined || $scope.OrderID == 'undefined' || $scope.OrderID == '')
            {
                if (OrderID != '' && OrderID != null && OrderID != undefined && CustomerID != '' && CustomerID != null && CustomerID != undefined && SubTotalAmount != '' && SubTotalAmount != null && SubTotalAmount != undefined) {
                    var inData = { 'CustOrderDetails': JSON.stringify(CartList), 'Type': '1' };
                    OrderCartFactory.saveOrderDetails(OrderID, CustomerID, SubTotalAmount, inData)
                        .then(function (response) {
                            orderlength = i;
                            //alert('Review your order...');
                        });
                    $window.location.href = '/OrderSummary';
                }
                else
                {
                    alert('Unable to place order!');
                }
            }
            else
            {
                var inData = { 'CustOrderDetails': JSON.stringify(CartList), 'Type': '2' };
                OrderCartFactory.saveOrderDetails($scope.OrderID, CustomerID, SubTotalAmount, inData)
                    .then(function (response) {
                        orderlength = i;
                        //alert('Review your order...');
                    });
            $window.location.href = '/OrderSummary?' + encodeURIComponent(decodeURIComponent($scope.OrderID));

            }
           
        }
       
    $scope.BackToPrevious = function () {
        debugger;
        $window.localStorage.removeItem('pageID');
        $window.localStorage.setItem('pageID', JSON.stringify('5'));
        if ($scope.OrderID == null || $scope.OrderID == undefined || $scope.OrderID == 'undefined' || $scope.OrderID == '') {
            $window.location.href = '/Product';
        }
        else
        {
            $window.location.href = '/Product?' + encodeURIComponent(decodeURIComponent($scope.OrderID));
        }

    }


}]);