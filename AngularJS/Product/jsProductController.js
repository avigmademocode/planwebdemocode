HomeApp.controller('ProductController', ['$scope', '$window', 'ProductFactory', function ($scope, $window, ProductFactory) {
    //debugger;
    var Custid = '';
    var CustomerId = $window.localStorage.getItem('CustomerId');
    $scope.OrderID = $scope.OrderID ? $scope.OrderID.split('?')[1] : window.location.search.slice(1);
    $scope.Quantity = 0;
    $scope.TotalPrice = 0;
    $scope.productcart = [];
    $scope.btnHide = false;
    $scope.CartCount = 0;
    if (CustomerId != null && CustomerId != undefined && CustomerId != '') {
        Custid = $window.localStorage.getItem('CustomerId');
    }
    else {
        $window.location.href = '/NewRequest';
    }

    $scope.GetOrderList = function () {
        if ($window.localStorage.getItem('pageID') != null) {
            var pageID = JSON.parse($window.localStorage.getItem('pageID'));
            if (pageID == '5') {
                $scope.btnHide = true;
            }
        }

        //debugger
        if ($scope.OrderID != null && $scope.OrderID != undefined && $scope.OrderID != 'undefined' && $scope.OrderID != '') {
            $window.localStorage.removeItem('productlist');
            var data =
                {
                    strorderID: $scope.OrderID
                }
            ProductFactory.GetOrderDetailsList(data)
                .then(function (response) {
                    //    debugger
                    if (response.data[0].length != 0) {
                        $scope.Prodlst = response.data[0];
                        for (var i = 0; i < $scope.Prodlst.length; i++) {
                            //debugger
                            var data = {
                                ODID: $scope.Prodlst[i].ODID,
                                ProdID: $scope.Prodlst[i].ProductId,
                                ProdName: $scope.Prodlst[i].ProductName,
                                ProdPrice: $scope.Prodlst[i].Rate,
                                TotalPrice: $scope.Prodlst[i].Amount,// * Quantity,
                                Quantity: $scope.Prodlst[i].Qty,
                            }
                            //debugger
                            $scope.productcart.push(data)

                        }
                        $window.localStorage.setItem('productlist', JSON.stringify($scope.productcart));
                        $scope.BindCartCount();
                    }

                });
        }
    }

    $scope.GetOrderList();
    $scope.BindCartCount = function () {

        //alert('Cart Count.');
        $scope.CartCount = 0;
        if ($window.localStorage.getItem('productlist') != undefined && $window.localStorage.getItem('productlist') != '') {
            //  $scope.CartCount = JSON.parse($window.localStorage.getItem('productlist')).length;
            var items = $window.localStorage.getItem('productlist');
            if (items != null) {
                var listitems = JSON.parse(items);
                $scope.CartCount = listitems.length;
            }
        }
        else {
            $scope.CartCount = 0;
        }

        $scope.CartProducts = JSON.parse($window.localStorage.getItem('productlist'));
    }
    $scope.BindCartCount();
    /*
    shoppingCart.prototype.toNumber = function (value) {
        value = value * 1;
        return isNaN(value) ? 0 : value;
    }
    */


    $scope.BackToPrevious = function () {
        debugger;
        if ($window.localStorage.getItem('pageID') != null) {
            debugger;
            var pageID = JSON.parse($window.localStorage.getItem('pageID'));
            if (pageID == '1') {
                $window.localStorage.removeItem('pageID');
                $window.localStorage.setItem('pageID', JSON.stringify('2'));
                $window.location.href = '/NewRequest?' + encodeURIComponent(decodeURIComponent($scope.OrderID));
            }
            else if (pageID == '4') {
                $window.localStorage.removeItem('pageID');
                $window.localStorage.setItem('pageID', JSON.stringify('2'));
                $window.location.href = '/ViewRequest?OrderId=' + encodeURIComponent(decodeURIComponent($scope.OrderID));
            }
            else if (pageID == '9') {
                $window.localStorage.removeItem('pageID');
                $window.localStorage.setItem('pageID', JSON.stringify('10'));
                $window.location.href = '/NewRequest?Prod';
            }
        }

    }



    $scope.GetCategoryList = function () {
        ProductFactory.getCatList(Custid)
            .then(function (response) {

                if (response.data[0].length != 0) {
                    $scope.CatList = response.data[0];
                    if (response.data[1].length != 0) {
                        $scope.ProductList = response.data[1];
                    }
                    else {
                        alert('Products Not Found!');
                    }
                    //console.log('Single Cat',$scope.ProductList)
                }
            });

    }
    $scope.GetCategoryList();

    //$scope.selectedCategory = function (selected) {

    //    var ProdCatId = selected.originalObject.CatId ? selected.originalObject.CatId : '';
    //    ProductFactory.getProductList(Custid, ProdCatId)
    //        .then(function (response) {
    //            if (response.data[0].length != 0) {
    //                $scope.ProductList = '';
    //               $scope.ProductList = response.data[0];
    //            }
    //            else {

    //                alert('Products Not Found!');
    //            }
    //        });
    //}


    $scope.BidProducts = function () {
        var ProdCatId = $scope.formData.SelectedCategory ? $scope.formData.SelectedCategory : '';
        ProductFactory.getProductList(Custid, ProdCatId)
            .then(function (response) {
                if (response.data[0].length != 0) {
                    $scope.ProductList = '';
                    $scope.ProductList = response.data[0];
                }
                else {
                    $scope.ProductList = '';
                    alert('Products Not Found!');
                }
            });
    }





    $scope.AddToCart = function (product) {
        // debugger;
        // product.disabled = true;
        //  $scope.selectedRow = product;
        $scope.SingleProduct = $window.localStorage.getItem('productlist');
        $scope.btnHide = true;
        if ($scope.SingleProduct != '' && $scope.SingleProduct != null) {
            var Added = false;
            $scope.SingleProduct = JSON.parse($window.localStorage.getItem('productlist'));
            for (var i = 0; i < $scope.SingleProduct.length; i++) {
                if ($scope.SingleProduct[i].ProdID == product.ProdID) {
                    if ($window.localStorage.getItem('pageID') != null) {
                        var pageID = JSON.parse($window.localStorage.getItem('pageID'));
                        if (pageID == '5') {
                            $scope.btnHide = true;
                        }
                        else {
                            $scope.btnHide = false;
                        }
                    }

                    alert('Product is already added!');
                    var Added = true;
                }
            }
            if (Added == false) {
                var data = {
                    ODID: 0,
                    ProdID: product.ProdID,
                    ProdName: product.ProdName,
                    ProdPrice: product.ProdPrice,
                    TotalPrice: product.ProdPrice,// * Quantity,
                    Quantity: 1,
                }
                //debugger
                $scope.SingleProduct.push(data)
                $window.localStorage.setItem('productlist', JSON.stringify($scope.SingleProduct));
                // alert('Product added to cart.')
                $scope.BindCartCount();
                //  $window.location.reload();
            }
        }
        else {
            var data = {
                ODID: 0,
                ProdID: product.ProdID,
                ProdName: product.ProdName,
                ProdPrice: product.ProdPrice,
                TotalPrice: product.ProdPrice,// * Quantity,
                Quantity: 1,
            }

            $scope.productcart.push(data);
            $window.localStorage.setItem('productlist', JSON.stringify($scope.productcart));
            // alert('Product added to cart.')
            $scope.BindCartCount();

            // $window.location.reload();
        }
    }



    $scope.GoToCart = function () {
        if ($scope.OrderID != null || $scope.OrderID != undefined || $scope.OrderID != 'undefined' || $scope.OrderID != '') {
            $window.localStorage.removeItem('pageID');
            $window.localStorage.setItem('pageID', JSON.stringify('2'));
            $window.location.href = '/OrderCart?' + encodeURIComponent(decodeURIComponent($scope.OrderID));
        }
        else {
            if ($scope.CartCount != undefined && $scope.CartCount != '') {
                $window.location.href = '/OrderCart';
            }
            else {
                alert('Products not added in cart!');
            }
        }

    }



    $scope.ViewProdDesc = function (prod) {
        //alert(prod.ProdName)
        $scope.ProductDescription = '';
        $scope.ProductName = '';
        $scope.ProductID = '';

        $scope.ProductName = prod.ProdName;
        $scope.ProductDescription = prod.Desc;
        $scope.ProductID = prod.ProdID;
        //  alert($scope.ProductID);
    }



    /*Scroll to top when arrow up clicked BEGIN*/
    $(window).scroll(function () {
        var height = $(window).scrollTop();
        if (height > 100) {
            $('#back2Top').fadeIn();
        } else {
            $('#back2Top').fadeOut();
        }
    });
    $(document).ready(function () {
        $("#back2Top").click(function (event) {
            event.preventDefault();
            $("html, body").animate({ scrollTop: 0 }, "slow");
            return false;
        });

    });
    /*Scroll to top when arrow up clicked END*/




}]);