HomeApp.factory('SupportingEmailsFactory', ['$http', function ($http) {

    var dataFactory = {};

    dataFactory.SupportingEmailsData = function (CustID) {
        return $http.post('/SupportingEmails/GetSupportingEmailsData?CustID=' + CustID);
    };

    dataFactory.SaveSupportingEmailsData = function (EmailsList) {
        return $http.post('/SupportingEmails/SaveSupportingEmailsData', EmailsList);
    };

    return dataFactory;
}]);