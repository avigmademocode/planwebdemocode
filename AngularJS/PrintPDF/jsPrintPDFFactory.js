HomeApp.factory('PrintPDFFactory', ['$http', function ($http) {
    var dataFactory = {};

    dataFactory.ViewPrintPDFData = function (OrderId) {
        debugger;
        return $http.post('/PrintPDF/GetPrintPDFData?OrderID=' + OrderId);
    };
 
    dataFactory.ChangedStatus = function (data) {
        //debugger;
        return $http.post('/ViewRequest/ChangedStatus', Data);
    };
    return dataFactory;



}]);