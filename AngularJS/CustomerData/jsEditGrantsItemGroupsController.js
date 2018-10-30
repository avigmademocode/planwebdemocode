HomeApp.controller('EditGrantsItemGroupsController', ['$scope', '$window', 'NewRequestFactory', 'EditGrantsItemGroupsFactory', function ($scope, $window, NewRequestFactory, EditGrantsItemGroupsFactory) {

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

    $scope.GetProductCategoryDetails = function (CustID) {

        EditGrantsItemGroupsFactory.GetProdCategryData(CustID)
            .then(function (response) {
                //console.log(response)
                if (response.data.length > 0) {
                    $scope.ProductCategories = response.data[0];
                    $scope.ProductCategoriesdata = response.data[1];
                }
            });
    }

    $scope.GetProductCategoryDetails(0);

    $scope.GetCategoryByCustomer = function () {
        $scope.GetProductCategoryDetails($scope.CustomerId);
    }

    $scope.addRow = function () {

        var obj = {};
        $scope.Edit = true;
        obj.IsActive = true;
        obj.ProdCatId = 0;
        obj.ProdCatDesc = '';
        obj.UserId = 0;
        obj.Type = 1;
        obj.IsEdit = $scope.Edit;
        obj.Ischange = 1;

        $scope.ProductCategories.push(obj);
    };
    $scope.UpdateIncoterms = function (ProductCatgryList) {
        var ProdCatgrylist =
            {
                Productcategorydet: JSON.stringify(ProductCatgryList),
                CustID: $scope.CustomerId,
                Type: 1
            }
        EditGrantsItemGroupsFactory.SaveProductCategryData(ProdCatgrylist)
            .then(function (response) {
                if (response.data.length > 0) {
                    //debugger;
                    $scope.ProductCategories = response.data[0];

                    alert('Request has been saved successfully.');
                }
            });

    }

    $scope.SaveCategory = function () {
        //console.log($scope.Customers)
        debugger;
        var ProdCatgrylist =
            {
                Productcategorydet: $scope.categoryname,
                CustID: JSON.stringify($scope.Finalcustlist),
                CatID: $scope.CatID,
                Type: 2
            }
        EditGrantsItemGroupsFactory.SaveProductCategryData(ProdCatgrylist)
            .then(function (response) {
                if (response.data.length > 0) {
                    //debugger;
                    $scope.ProductCategories = response.data[0];
                    $scope.ProductCategoriesdata = response.data[1];
                    if (response.data[2] == -99) {
                        alert('Product Category Already Exists.');
                    }
                    else {
                        alert('Request has been saved successfully.');
                    }

                }
            });

    }

    $scope.Checked = function () {
        for (var i = 0; i <= $scope.ProductCategories.length; i++) {
            $scope.ProductCategories[i].IsEdit = false;
            $scope.ProductCategories[i].Ischange = 0;
            $scope.categoryname = '';
        }
    }

    $scope.EditCategory = function (CatData) {
        debugger;

        $scope.tempcustlist = [];
        $scope.Finalcustlist = [];
        $scope.categoryname = CatData.ProdCatDesc;
        $scope.CatID = CatData.ProdCatId
        $scope.CustID = $scope.CustomerId; //=> This is dropdown value => Replace with CatData.CustId (Getting null value)
        for (var i = 0; i < $scope.ProductCategoriesdata.length; i++) {
            if ($scope.ProductCategoriesdata[i].ProdCatId == CatData.ProdCatId) {
                if ($scope.ProductCategoriesdata[i].IsActive) {
                    var obj = {};
                    obj.CustId = $scope.ProductCategoriesdata[i].CustID;
                    obj.IsCat = true;
                    $scope.tempcustlist.push(obj);
                }

            }

        }

        for (var i = 0; i < $scope.custList.length; i++) {
            var obj = {};
            obj.CustId = $scope.custList[i].CustId;
            obj.CustName = $scope.custList[i].CustName;
            obj.IsCat = false;
            for (var j = 0; j < $scope.tempcustlist.length; j++) {
                if ($scope.custList[i].CustId == $scope.tempcustlist[j].CustId) {
                    obj.IsCat = true;
                }

            }

            $scope.Finalcustlist.push(obj);
        }

    }

    $scope.ResetData = function () {
        debugger;
        $scope.categoryname = '';
        $scope.CatID = 0;
        $scope.Finalcustlist = [];
        for (var i = 0; i < $scope.custList.length; i++) {
            var obj = {};
            obj.CustId = $scope.custList[i].CustId;
            obj.CustName = $scope.custList[i].CustName;
            obj.IsCat = false;
            $scope.Finalcustlist.push(obj);
        }
    }


}]);