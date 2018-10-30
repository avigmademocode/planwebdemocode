HomeApp.factory('AddMultiProductFactory', ['$http', '$q', function ($http, $q) {


    var dataFactory = {};

    //dataFactory.viewRequestOrderDetails = function (OrderId) {
    //    return $http.post('/ViewRequest/ViewRequestOrderDetails?OrderID=' + OrderId);
    //};

    //dataFactory.ChangedStatus = function (Data) {
    //    debugger
    //    return $http.post('/ViewRequest/ChangedStatus', Data);
    //};

    dataFactory.UploadFile = function (data) {
        debugger
        return $http.post("/ViewProduct/SaveFiles/", data, {
            withCredentials: true,
            processData: false,
            headers: { 'Content-Type': undefined },
            transformRequest: angular.identity
        });

    }

    //dataFactory.GetRequestedDocs = function (OrderId) {
    //    return $http.post('/ViewRequest/GetRequestedDocs?OrderID=' + OrderId);
    //};

    return dataFactory;
}]);


