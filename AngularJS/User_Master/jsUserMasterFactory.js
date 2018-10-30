HomeApp.factory('UserMasterFactory', ['$http', function ($http) {

    var dataFactory = {};

    //debugger;
    dataFactory.GetUserData = function (data) {
        //debugger;
        return $http.post('/User_Master/GetUserData', data);

    };
   // debugger;
    dataFactory.AddUserDetailsData = function (data) {
        return $http.post('/User_Master/AddUserData',data);

    };

   
    dataFactory.UpdateUserData = function (data) {
        debugger;
        return $http.post('/User_Master/UpdateUserData', data);

    };

    dataFactory.SaveRolesData = function (data) {
        //debugger;
        return $http.post('/User_Master/SaveUserRoleData', data);

    };


    

    return dataFactory;
}]);