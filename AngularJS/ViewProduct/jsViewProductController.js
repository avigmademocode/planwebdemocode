HomeApp.controller('ViewProductController', ['$scope', '$window', 'ViewProductFactory', function ($scope, $window, ViewProductFactory) {

    $scope.isDisabled;

    $scope.GetProductsData = function () {
        //debugger
        ViewProductFactory.GetProductsData()
        .then(function (response) {
            //console.log(response)
            //debugger
            if (response.data.length != 0) {
                $scope.custList = response.data[0][0];
                if (response.data[3] == "True")
                {
                    $scope.Categories = response.data[1][0];
                    $scope.isDisabled = false;
                }
                else
                {
                    $scope.Categories = response.data[1][0];
                    $scope.isDisabled = true;
                }
            }
            else {
                alert('Failled!!!');
            }
        });
    }
    $scope.GetProductsData();

    $scope.GetCatByCustomerID = function () {
        ViewProductFactory.GetCatByCustomerID($scope.CustomerId)
            .then(function (response) {
                console.log(response)
                if (response.data[0].length != 0) {
                    $scope.Categories = '';
                    $scope.Categories = response.data[0];
                }
            });
    }






    //$scope.GetAllOrderStatus = function () {
    //    AllRequestFactory.GetAllOrderStatus()
    //        .then(function (response) {
    //            if (response.data[0].length != 0) {

    //                $scope.OrderStatus = response.data[0];
    //            }
    //        });
    //}
    //$scope.GetAllOrderStatus();

    //function resetdata() {
    //    $scope.SearchBox = '';
    //    $scope.selectedCountryTo = '';
    //}

    //var dataItem;
    $scope.SearchData = function () {

        debugger;
        
        var SearchData =
        {
                CustID: $scope.CustomerId,
                CatID: $scope.CategoryID,
                IsActive: $scope.IsYes
      

        }


        ViewProductFactory.SearchAllData(SearchData)
            .then(function (response) {

                if (response.data.length != 0) {
                    $scope.AllOrders = response.data[0];
                    dataItem = response.data[0];

                    //Bind with initial data
                    var people = dataItem

                    $('#grid').kendoGrid({
                        scrollable: true,
                        sortable: true ,
                        height: 400,

                        pageable: {
                            pageSizes: true,
                            input: true,
                            pageSize: 20,
                            pageSizes: [20, 50, 100, 500]
                        }
                        , dataSource:
                            {
                                data: people,

                                schema: {
                                    model: {
                                        fields: {
                                        
                                            ProductName: { type: "string" }
                                            //ProdPrice: { type: "string" },
                                           
                                        }
                                    }
                                }
                            }
                        , columns:
                            [

                                { field: "ProductName", template: "<a href='Editproduct/AddProduct?ProdID=${strProdID}'>${ProdName}</a>" }
                             // , { field: "ProdPrice", title: "ProdPrice" }
                         

                            ]


                    });

                   // resetdata();
                }
                else {
                    alert('Record Not Found!');
                }
            });
    }
    $scope.SearchData();

    function isNumeric(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }

    $('#filter').on('input', function (e) {

        var grid = $('#grid').data('kendoGrid');
        var columns = grid.columns;

        var filter = { logic: 'or', filters: [] };
        columns.forEach(function (x) {
            if (x.field) {
                var type = grid.dataSource.options.schema.model.fields[x.field].type;
                if (type == 'string') {
                    filter.filters.push({
                        field: x.field,
                        operator: 'contains',
                        value: e.target.value
                    })
                }
                else if (type == 'number') {
                    if (isNumeric(e.target.value)) {
                        filter.filters.push({
                            field: x.field,
                            operator: 'eq',
                            value: e.target.value
                        });
                    }

                } else if (type == 'date') {
                    var data = grid.dataSource.data();
                    for (var i = 0; i < data.length; i++) {
                        var dateStr = kendo.format(x.format, data[i][x.field]);

                        if (dateStr.startsWith(e.target.value)) {
                            filter.filters.push({
                                field: x.field,
                                operator: 'eq',
                                value: data[i][x.field]
                            })
                        }
                    }
                } else if (type == 'boolean' && getBoolean(e.target.value) !== null) {
                    var bool = getBoolean(e.target.value);
                    filter.filters.push({
                        field: x.field,
                        operator: 'eq',
                        value: bool
                    });
                }
            }
        });
        grid.dataSource.filter(filter);
    });

}]);