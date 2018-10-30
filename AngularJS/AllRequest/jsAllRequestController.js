HomeApp.controller('AllRequestController', ['$scope', '$window', 'NewRequestFactory', 'AllRequestFactory', function ($scope, $window, NewRequestFactory, AllRequestFactory) {

    $scope.PageID = $scope.PageID ? $scope.OrderID.split('?')[1] : window.location.search.slice(1);
    //$scope.Country = [];
    //$window.localStorage.clear();
    $scope.divHide = false; 
    $scope.GetCustomersList = function () {
        resetdata();
        NewRequestFactory.GetCustomers(1)
        .then(function (response) {
            if (response.data.length != 0) {
                $scope.custList = response.data[0];
                $scope.CountryList = response.data[2];


                if (response.data[0].length == 1) {
                    $scope.CustomerId = response.data[0][0].CustId;
                    $window.localStorage.setItem('CustomerId', $scope.CustomerId);
                    //$scope.selectedCustomer = { CustName: response.data[0][0].CustName, CustId: response.data[0][0].CustId };
                    $scope.divEnabled = true;
                }
                else
                {
                    $scope.divEnabled = false;
                }

            }
            else
            {
                alert('Failled!!!');
            }
        });
    }
    $scope.GetCustomersList();

    $scope.GetAllOrderStatus = function () {
        AllRequestFactory.GetAllOrderStatus()
            .then(function (response) {
                if (response.data[0].length != 0) {
                   
                    $scope.OrderStatus = response.data[0];
                }
            });
    }
    $scope.GetAllOrderStatus();
    
    function resetdata()
    {
        $scope.SearchBox = '';
       // $scope.OrderStatus = '';
        $scope.selectedCountryTo = '';
    }
    var dataItem;
    $scope.SearchData = function (OrderStatus) {

        var SearchData = {};
        //debugger
        if ($scope.selectedCountryTo != '' && $scope.selectedCountryTo != 'undefined' && $scope.selectedCountryTo != null) {
            $scope.CountryId = $scope.selectedCountryTo.originalObject.CountryId ? $scope.selectedCountryTo.originalObject.CountryId : '';
        }
        else
        {
            $scope.CountryId = null;
        }
            
        if ($scope.PageID == 'a') {
             SearchData =
                {
                    CustomerID: $scope.CustomerId,
                    CountryId: $scope.CountryId,
                    CancelOrder: $scope.CancelOrder,
                    Status: JSON.stringify(OrderStatus)
                    ,StatusID: 0
                }

            $scope.divHide = false; 

        }
        else if ($scope.PageID == 'p') {
            SearchData =
                {
                    CustomerID: $scope.CustomerId,
                    CountryId: $scope.CountryId,
                    CancelOrder: $scope.CancelOrder,
                    Status: JSON.stringify(OrderStatus)
                  , StatusID: 6

                }
            $scope.divHide = true; 

        }

        else if ($scope.PageID == 'f') {
            SearchData =
                {
                    CustomerID: $scope.CustomerId,
                    CountryId: $scope.CountryId,
                    CancelOrder: $scope.CancelOrder,
                    Status: JSON.stringify(OrderStatus)
                   , StatusID: 2

                }
            $scope.divHide = true; 
        }
        AllRequestFactory.SearchAllData(SearchData)
            .then(function (response) {
                
            if (response.data.length != 0) 
            {
                $scope.AllOrders = response.data[0];
                dataItem = response.data[0];
             
                //Bind with initial data
                var people = dataItem
                if (people.length > 20) {
                    $('#grid').kendoGrid({
                        scrollable: true,
                        sortable: true,
                        height: 400,

                        pageable: {
                            pageSizes: true,
                            input: true,
                            pageSize: 20,
                            pageSizes: [20, 50, 100, 500]
                        }
                        // selectable: "row",//""multiple row"",
                        // filterable: true
                        ///ViewRequest?{{ord.OrderId}}
                        , dataSource:
                            {
                                data: people,

                                schema: {
                                    model: {
                                        fields: {
                                            ReferenceNo: { type: "string" },
                                            Department: { type: "string" },
                                            CreatedOn: { type: "string" },
                                            StatusName: { type: "string" },
                                            CountryName: { type: "string" },
                                            CityName: { type: "string" },
                                            TotalOrderAmount: { type: "string" }
                                        }
                                    }
                                }
                            }
                        , columns:
                            [

                                { field: "ReferenceNo", template: "<a href='../ViewRequest?OrderId=${encodeURIComponent(OrderId)}'>${ReferenceNo}</a>" }
                                , { field: "Department", title: "Department" }
                                , { field: "CreatedOn", title: "Created On" }
                                , { field: "StatusName", title: "Status Name" }
                                , { field: "CountryName", title: "Country Name" }
                                , { field: "CityName", title: "City Name" }
                                , { field: "TotalOrderAmount", title: "TotalOrder Amount" }

                            ]


                    });
                }
                else
                {
                    $('#grid').kendoGrid({
                        scrollable: true,
                        sortable: true,
                       

                        pageable: {
                            pageSizes: true,
                            input: true,
                            pageSize: 5,
                            pageSizes: [20, 50, 100, 500]
                        }
                        // selectable: "row",//""multiple row"",
                        // filterable: true
                        ///ViewRequest?{{ord.OrderId}}
                        , dataSource:
                            {
                                data: people,

                                schema: {
                                    model: {
                                        fields: {
                                            ReferenceNo: { type: "string" },
                                            Department: { type: "string" },
                                            CreatedOn: { type: "string" },
                                            StatusName: { type: "string" },
                                            CountryName: { type: "string" },
                                            CityName: { type: "string" },
                                            TotalOrderAmount: { type: "string" }
                                        }
                                    }
                                }
                            }
                        , columns:
                            [

                                  { field: "ReferenceNo", template: "<a href='../ViewRequest?OrderId=${encodeURIComponent(OrderId)}'>${ReferenceNo}</a>" }
                                , { field: "Department", title: "Department" }
                                , { field: "CreatedOn", title: "Created On" }
                                , { field: "StatusName", title: "Status Name" }
                                , { field: "CountryName", title: "Country Name" }
                                , { field: "CityName", title: "City Name" }
                                , { field: "TotalOrderAmount", title: "TotalOrder Amount" }

                            ]


                    });
                }
               
            
                resetdata();
            }
            else {
                alert('Record Not Found!');
            }
        });
    }
   $scope.SearchData('');


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