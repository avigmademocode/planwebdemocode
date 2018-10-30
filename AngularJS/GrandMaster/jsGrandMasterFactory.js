HomeApp.factory('GrandMasterFactory', ['$http', function ($http) {

    var dataFactory = {};

    dataFactory.GetGrandMasterData = function (data) {
        debugger;
        return $http.post('/GrandMaster/GetGrantBudgetMaster',data);
    };

    dataFactory.SaveGrandMasterData = function (data) {
        debugger;
        return $http.post('/GrandMaster/SaveGrantBudgetMaster', data);
    };
    return dataFactory;
}]);