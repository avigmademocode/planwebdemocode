HomeApp.factory('LanguageFactory', ['$http', function ($http) {

    var dataFactory = {};

    dataFactory.GetLanguageData = function (langid) {
        debugger;
        return $http.post('/Language/GetLanguagesData/?langid=' + langid);
    };

    dataFactory.SaveLanguageData = function (Manulist) {
        debugger;
        return $http.post('/Language/SaveLanguagesData', Manulist);
    };
    return dataFactory;
}]);