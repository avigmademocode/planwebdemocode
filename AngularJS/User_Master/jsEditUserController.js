HomeApp.controller('EditUser', ['$scope', '$window', 'NewRequestFactory', 'UserMasterFactory', function ($scope, $window, NewRequestFactory,UserMasterFactory) {


    $scope.UserIDdata = localStorage.getItem('User');
    $scope.UserIDdata = JSON.parse($scope.UserIDdata);
    $scope.flag;
    $scope.UserID = $scope.UserID ? $scope.UserID.split('?')[1] : window.location.search.slice(1);
    $scope.UnflagHide = true;
    $scope.newUserFlag = true;
    $scope.AllBranchList;
    $scope.BranchList = [];
    $scope.Pwd;
    if ($scope.UserID == "")
    {
        $scope.newUserFlag = false;
    }
    if ($scope.UserID == "&nbp")
    {

        var UserID = $scope.UserIDdata.UID;
        $scope.flagHide = true;
        $scope.UnflagHide = false;
        $scope.flag = 2;
    } else
    {
        
        var UserID = $scope.UserID;
        $scope.flag = 1;
    }


    $scope.GetCustomersList = function () {
        //debugger;
        NewRequestFactory.GetCustomers(4)
            .then(function (response) {
                if (response.data.length != 0) {
                    //debugger
                    $scope.custList = response.data[0];
                    $scope.CityList = response.data[1];
                    $scope.CountryList = response.data[2];

                    $scope.GetUserData(UserID);
                }
                else {
                    alert('Failled!!!');
                }
            });
    };
    $scope.GetCustomersList();
    

    $scope.GetBranchList = function (CustID) {
            $scope.BranchList = [];
        for (var i = 0; i < $scope.AllBranchList.length; i++) {
            if ($scope.AllBranchList[i].CustId == CustID) {

                var obj = {};
                obj.BranchId = $scope.AllBranchList[i].BranchId;
                obj.DisplayName = $scope.AllBranchList[i].DisplayName;

                $scope.BranchList.push(obj);


            }
        }
    };


//get user data

  
    $scope.GetUserData = function (userid) {
        //debugger;
        var data;
        if ($scope.flag == 1)
        {
            data = { strUserId: userid };
        }
        else
        {
            data = { UserId: userid };
        }
            UserMasterFactory.GetUserData(data)
                .then(function (response) {
                    //debugger;
                    $scope.AllBranchList = response.data[1];
                    $scope.UserRoleList = response.data[2];
                    $scope.UserRoleRelList = response.data[3];
                    if (response.data.length != 0) {



                        if (userid != null && userid != undefined && userid != 'undefined' && userid != '' && userid != "")
                        {
                          
                            $scope.pUserId = response.data[0][0].UserId;
                            $scope.form.LoginId = response.data[0][0].LoginId;

                            $scope.form.UserName = response.data[0][0].UserName;
                            $scope.form.CustomerId = response.data[0][0].CustId;

                          //  $scope.GetBranchList(response.data[0][0].CustId);

                            for (var i = 0; i < $scope.AllBranchList.length; i++) {
                                if ($scope.AllBranchList[i].CustId == response.data[0][0].CustId) {

                                    var obj = {};
                                    obj.BranchId = $scope.AllBranchList[i].BranchId;
                                    obj.DisplayName = $scope.AllBranchList[i].DisplayName;

                                    $scope.BranchList.push(obj);


                                }
                            }
                          //      $scope.form.BranchID = response.data[0][0].BranchId;
                                //$scope.form.CustomerName = response.data[0][0].CustomerName;
                                //$scope.form.CityId = response.data[0][0].CityId;
                                //$scope.form.CityName = response.data[0][0].CityName;
                                $scope.form.FirstName = response.data[0][0].FirstName;
                                $scope.form.LastName = response.data[0][0].LastName;
                            $scope.form.Pwd = response.data[0][0].Pwd;
                            $scope.Pwd = response.data[0][0].Pwd;;
                                $scope.form.ConfirmPassword = response.data[0][0].Pwd;

                                $scope.form.IsPlansonUser = response.data[0][0].IsPlansonUser;
                                $scope.form.Locked = response.data[0][0].Locked;
                                $scope.form.IsActive = response.data[0][0].IsActive;
                                var SCountryID = response.data[0][0].CountryId;
                                var SCityID = response.data[0][0].CityId;
                                 $scope.form.ReadCountry = response.data[0][0].CityName;
                                 $scope.form.ReadCity = response.data[0][0].CountryName;
                              
                            $scope.$broadcast('angucomplete-alt:changeInput', 'ex1', { CityName: response.data[0][0].CityName, CityID: SCityID });
                            $scope.$broadcast('angucomplete-alt:changeInput', 'ex2', { CountryName: response.data[0][0].CountryName, CountryId: SCountryID });


                            var index = $scope.custList.findIndex(obj => obj.CustId == response.data[0][0].CustId);
                            if (index >=0) {
                                $scope.form.CustomerId = $scope.custList[index].CustId;
                            }
                                

                            var indexbranch = $scope.BranchList.findIndex(obj => obj.BranchId == response.data[0][0].BranchId);
                            if (indexbranch >= 0 ) {
                                $scope.form.SelBranchID = $scope.BranchList[indexbranch].BranchId;
                            }

                        }
                    }
                });
        
       
    };
    $scope.CheckVal = function (val)
    {
        //debugger;
        var value = val.IsCat;
        for (var i = 0; i < $scope.Finalcustlist.length; i++)
        {
            $scope.Finalcustlist[i].IsCat = false;
        }
       // $scope.Finalcustlist.find(v => v.RoleId != val.RoleId).IsCat = false;

        $scope.Finalcustlist.find(v => v.RoleId == val.RoleId).IsCat = value;
    }
    $scope.Finalcustlist = [];
    $scope.EditStatus = function () {
        debugger;

        $scope.tempcustlist = [];
        $scope.Finalcustlist = [];

       

        for (var i = 0; i < $scope.UserRoleRelList.length; i++) {
            if ($scope.UserRoleRelList[i].UserId == $scope.pUserId) {

                var obj = {};
                obj.RoleId = $scope.UserRoleRelList[i].RoleId;
                obj.IsCat = true;
                $scope.tempcustlist.push(obj);


            }

        }
        //debugger

        for (var i = 0; i < $scope.UserRoleList.length; i++) {
            var obj = {};
            obj.RoleId = $scope.UserRoleList[i].RoleId;
            obj.RoleName = $scope.UserRoleList[i].RoleName;
            obj.IsCat = false;
            for (var j = 0; j < $scope.tempcustlist.length; j++) {
                if ($scope.UserRoleList[i].RoleId == $scope.tempcustlist[j].RoleId) {
                    obj.IsCat = true;
                }

            }

            $scope.Finalcustlist.push(obj);
        }


        console.log($scope.Finalcustlist);

    }

   

    var bool = true;
    function validation() {
        debugger
        bool = false;
        for (var i = 0; i < $scope.Finalcustlist.length; i++) {
            if ($scope.Finalcustlist[i].IsCat == true) {
                bool = true;

                return bool;
            }
            else {
                bool = false;
            }

        }
        if (!bool) {
            alert('Please Select Assign Atleast one Role to Customer');
            return bool;
        }
        return bool;
    }

    //user role get and set 
    $scope.Role = function (Finalcustlist) {
        //debugger;
          var bool = validation();
        //var bool = true;
        if (bool) {
            var data =
                {
                   
                    UserId: $scope.pUserId,
                    UserRoledet: JSON.stringify($scope.Finalcustlist),
                    Type: 2
                }
           // alert(JSON.stringify(data));
            UserMasterFactory.SaveRolesData(data)
                .then(function (response) {
                    if (response.data.length > 0) {
                        $scope.GetUserData(0);
                        debugger
                        if (response.data[0] == -99)
                        {
                            alert('Role(s) Already Exists.');
                        }
                        else
                        {
                            $scope.GetUserData($scope.UserID);//$scope.pUserId
                            alert('Role has been saved successfully.');
                        }

                    }
                });
        }

    }

     

    //SaveChangePassword
    $scope.SaveChangePassword = function () {
        debugger;
        if ($scope.Pwd != $scope.form.OldPassword) {

            alert("Old Password is Wrong...!");

            return;
        } else {
            $scope.form.Newpwd// this is a optional data
         //   alert($scope.form.Newpwd);
            if (validatePassword($scope.form.Newpwd)) {
                $scope.submitForm(2);
            } else {
                alert("Please check the Password Policy..!!")
            }
            
        }
    }

    var valbool = true;

    function validation() {
        if ($scope.form.SelBranchID == '' || $scope.form.SelBranchID == undefined || $scope.form.SelBranchID == 'undefined')
        {
            alert('Please Enter Department');
            bool = false
            return valbool;
        }
    }

    // update users data
  
    $scope.submitForm = function (arg) {
        //debugger;
        var type;
        var Password;
        var ShipcityID, ShipCityName;

        if ($scope.selectedCityTo != '') {
            ShipcityID = $scope.selectedCityTo.originalObject.CityID ? $scope.selectedCityTo.originalObject.CityID : '';
            ShipCityName = $scope.selectedCityTo.originalObject.CityName ? $scope.selectedCityTo.originalObject.CityName : '';
        }
        if ($scope.selectedCountryTo != '') {
            Shipcountryid = $scope.selectedCountryTo.originalObject.CountryId ? $scope.selectedCountryTo.originalObject.CountryId : '';
            Shipcountryname = $scope.selectedCountryTo.originalObject.CountryName ? $scope.selectedCountryTo.originalObject.CountryName : '';
        }
        debugger;
        if ($scope.UserID == null || $scope.UserID == "" || $scope.UserID == undefined || $scope.UserID == 'undefined') {
            type = 1;
        }
        else {
            type = 2;
        }
        if (arg == 1) {
            Password = $scope.form.Pwd
        } else {
            Password = $scope.form.Newpwd;
            $scope.UserID = UserID;
            type = 2;
        }

        if (validation) { 
        var Data = {

            strUserId: $scope.UserID,
            LoginId: $scope.form.LoginId,
            BranchId: $scope.form.SelBranchID,
            UserName: $scope.form.UserName,
            CustId: $scope.form.CustomerId,
            FirstName: $scope.form.FirstName,
            LastName: $scope.form.LastName,
            Pwd: Password,
            confirmPwd: $scope.form.confirmPwd,
            CountryId: Shipcountryid,
            CityId: ShipcityID,
            IsPlansonUser: $scope.form.IsPlansonUser,
            Locked: $scope.form.Locked,
            UserID: 0, //using local stoarge 
            Type: type,
            IsActive: $scope.form.IsActive
        }
        // alert(JSON.stringify(Data));
        UserMasterFactory.AddUserDetailsData(Data)
            .then(function (response) {

                if (response.data.length != 0) {
                    debugger;
                    $scope.form.OldPassword = "";
                    $scope.form.Newpwd = "";
                    $scope.form.NewCofirmpwd = "";
                    if (response.data[0] == 1) {
                        debugger;
                        $scope.UserID = response.data[2];
                        $scope.GetUserData($scope.UserID);
                        alert('Request has been saved successfully.');

                    }
                    else if (response.data[0] == -98) {

                        alert(' UserId Already exist User.');
                    }
                    else if (response.data[0] == -99) {
                        alert('LoginId Already exist User.');
                    }
                }
            });
    }
    };


    $scope.clicked = function () {
        
        $window.location.href = '/User_Master/UserView';
    }


    

}]);
