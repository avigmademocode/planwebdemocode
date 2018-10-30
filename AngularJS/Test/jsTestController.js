HomeApp.controller('TestController', ['$scope', '$window', 'TestFactory', function ($scope, $window, TestFactory) {
    debugger;
    $scope.gridOptions = {
        sortable: true,
        selectable: true,
        dataSource: [
            { text: "Foo", id: 1 },
            { text: "Bar", id: 2 },
            { text: "Baz", id: 3 }
        ],
        columns: [
            { field: "text", title: "Text" },
            { field: "id", title: "Id" }
        ]
    };

}]);