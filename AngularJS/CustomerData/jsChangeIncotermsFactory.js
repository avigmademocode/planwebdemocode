HomeApp.factory('ChangeIncotermsFactory', ['$http', function ($http) {
    var dataFactory = {};

    dataFactory.GetCustIncoTermDtls = function (IncoTermId) {
        debugger
        return $http.post('/CustomerData/GetCustIncoTermData?IncoTermId=' + IncoTermId);
    }

    dataFactory.SaveIncoTerm = function (data) {
        debugger
        return $http.post('/CustomerData/SaveIncoTerm' , data);
    }
   
    return dataFactory;
}]);