HomeApp.controller('UserView', ['$scope', '$window', 'UserMasterFactory', function ($scope, $window, UserMasterFactory) {

    $scope.UserID = $scope.UserID ? $scope.UserID.split('?')[1] : window.location.search.slice(1);
    $scope.UpdatedUserData = [];

    //kendo grid data

    function BindGrid(dataItem) {
        try {
            //debugger
            var people = dataItem
            $('#grid').kendoGrid({
                scrollable: true,
                sortable: true,


                pageable: {
                    pageSizes: true,
                    input: true,
                    pageSize: 5,
                    pageSizes: [5, 10, 20, 500]
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
                                LoginId: { type: "string" },
                                UserName: { type: "string" },
                                IsPlansonUser: { type: "boolean" },
                                FirstName: { type: "string" },
                                LastName: { type: "string" },
                                Locked: { type: "boolean" },
                                IsActive: { type: "boolean" },

                            }
                        }
                    }
                }
                , columns:
                    [
                        { field: "LoginId", title: "Login Id", template: "<a href='Index?${encodeURIComponent(strUserId)}'>${LoginId}</a>" }
                        , { field: "UserName", title: "User Name" }
                        , { field: "FirstName", title: "First Name" }
                        , { field: "LastName", title: "Last Name" }
                        , { field: "IsPlansonUser", title: "IsPlansonUser", template: '<input type="checkbox" #= IsPlansonUser ? \'checked="checked"\' : "" # class="chkbx" />'} 
                        , { field: "IsActive", title: "Locked", template: '<input type="checkbox" #= Locked ? \'checked="checked"\' : "" # class="chkbx1" />', width: 70}
                        , { field: "IsActive", title: "IsActive", template: '<input type="checkbox" #= IsActive ? \'checked="checked"\' : "" # class="chkbx2"/>', width: 70}
                    ]


            });
        }
        catch (e) {

        }



        $("#grid .k-grid-content").on("change", "input.chkbx", function (e) {
            //debugger;

            var grid = $("#grid").data("kendoGrid"),
                dataItem = grid.dataItem($(e.target).closest("tr"));
            dataItem.set("IsPlansonUser", this.checked);

            if ($scope.UpdatedUserData.length != 0 ) {
                var index = $scope.UpdatedUserData.findIndex(obj => obj.UserId == dataItem.UserId);
                if (index >= 0)
                {
                    $scope.UpdatedUserData.splice(index, 1);
                    $scope.UpdatedUserData.push(dataItem);
                }
                else
                {
                    $scope.UpdatedUserData.push(dataItem);
                }
            }
           
            else
            {
                $scope.UpdatedUserData.push(dataItem);
            }
            

            //  OnGridDataBound();
        });

        $("#grid .k-grid-content").on("change", "input.chkbx1", function (e) {
            //debugger;

            var grid = $("#grid").data("kendoGrid"),
                dataItem = grid.dataItem($(e.target).closest("tr"));
            dataItem.set("Locked", this.checked);

            if ($scope.UpdatedUserData.length != 0) {
                var index = $scope.UpdatedUserData.findIndex(obj => obj.UserId == dataItem.UserId);
                if (index >= 0) {
                    $scope.UpdatedUserData.splice(index, 1);
                    $scope.UpdatedUserData.push(dataItem);
                }
                else {
                    $scope.UpdatedUserData.push(dataItem);
                }
            }

            else {
                $scope.UpdatedUserData.push(dataItem);
            }


            //  OnGridDataBound();
        });

        $("#grid .k-grid-content").on("change", "input.chkbx2", function (e) {
            //debugger;

            var grid = $("#grid").data("kendoGrid"),
                dataItem = grid.dataItem($(e.target).closest("tr"));
            dataItem.set("IsActive", this.checked);

            if ($scope.UpdatedUserData.length != 0) {
                var index = $scope.UpdatedUserData.findIndex(obj => obj.UserId == dataItem.UserId);
                if (index >= 0) {
                    $scope.UpdatedUserData.splice(index, 1);
                    $scope.UpdatedUserData.push(dataItem);
                }
                else {
                    $scope.UpdatedUserData.push(dataItem);
                }
            }

            else {
                $scope.UpdatedUserData.push(dataItem);
            }


            //  OnGridDataBound();
        });

    }
    data = { UserId: 0 };

    $scope.GetUserData = function (data) {
        //debugger;
        UserMasterFactory.GetUserData(data)
            .then(function (response) {
               // debugger;
                if (response.data.length != 0) {
                   //debugger;
                    dataItem = response.data[0];
                    try {

                        BindGrid(dataItem);

                    }
                    catch (e) {
                        alert(e);
                    }


                } else {
                    alert('Record Not Found!');
                }

            });

    }



    function isNumeric(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }

    function getBoolean(n) {
        if (n == 0)
        {
            return false;
        }
        else if (n == 1)
        {
            return true;
        }
        else if (n == false)
        {
            return false;
        }
        else if (n == true)
        {
            return true;
        }
        else
        {
            return null;
        }
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
    $scope.GetUserData();

    $scope.Save = function ()
    {
        debugger;
       
        var Data =
            {
                strUserData: JSON.stringify($scope.UpdatedUserData)
            };
        UserMasterFactory.UpdateUserData(Data)
            .then(function (response) {

                if (response.data.length != 0) {
                    debugger;
                    if (response.data[0] == 1)
                    {
                        $scope.UserID = response.data[2];
                        alert('Request has been saved successfully.');

                    }
                    else if (response.data[0] == -98)
                    {

                        alert(' UserId Already exist User.');
                    }
                    else if (response.data[0] == -99)
                    {
                        alert('LoginId Already exist User.');
                    }
                }
            });
    }


     
    //$('.check_row').live('click', function (e) {
    //    //Getting checkbox
    //    var $cb = $(this);
    //    //Getting checkbox value
    //    var checked = $cb.is(':checked');
    //    //Setting checkbox value to data Item
    //    setValueToGridData(checked);
    //});



    $scope.AddNewUser = function () {
        //debugger
        $window.location.href = '/User_Master/Index';
    }


    $scope.AddNewRole = function () {
        //debugger
        $window.location.href = '/AddRole/Index';
    }

}]);